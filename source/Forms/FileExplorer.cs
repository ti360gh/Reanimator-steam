﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Reanimator.Properties;

namespace Reanimator.Forms
{
    public partial class FileExplorer : Form
    {
        private static readonly Icon[] Icons = {Resources.Generic_Document, Resources.folder, Resources.folder_open};
        private enum IconIndex : int
        {
            GenericDocument,
            Folder,
            FolderOpen
        }

        private static readonly Color BackupColor = Color.IndianRed;
        private static readonly Color NoEditColor = Color.DimGray;

        private static readonly int[] IndexOpen = { Index.Base000, Index.LatestPatch, Index.LatestPatchLocalized };
        private readonly List<Index> _indexFiles;
        private readonly Hashtable _fileTable;

        private class NodeObject
        {
            public Index Index;
            public Index.FileIndex FileIndex;
            public bool IsFolder;
            public bool IsBackup;
            public bool CanEdit;
        }

        private class NodeSorter : IComparer
        {
            public int Compare(Object objX, Object objY)
            {
                TreeNode treeNodeX = (TreeNode)objX;
                TreeNode treeNodeY = (TreeNode)objY;

                NodeObject nodeObjectX = (NodeObject)treeNodeX.Tag;
                NodeObject nodeObjectY = (NodeObject)treeNodeY.Tag;

                if (nodeObjectX != null && nodeObjectY != null)
                {
                    if (nodeObjectX.IsFolder && !nodeObjectY.IsFolder) return -1;
                    if (!nodeObjectX.IsFolder && nodeObjectY.IsFolder) return 1;
                }

                return treeNodeX.Text.CompareTo(treeNodeY.Text);
            }
        }

        public FileExplorer()
        {
            InitializeComponent();

            _indexFiles = new List<Index>();
            _fileTable = new Hashtable();
            backupKey_label.ForeColor = BackupColor;
            noEditorKey_label.ForeColor = NoEditColor;
        }

        public void LoadIndexFiles(ProgressForm progressForm, Object param)
        {
            if (progressForm != null)
            {
                progressForm.SetLoadingText("Loading game file system...");
                progressForm.ConfigBar(1, IndexOpen.Length, 1);
            }

            files_treeView.BeginUpdate();


            // load icons
            ImageList imageList = new ImageList {ColorDepth = ColorDepth.Depth32Bit};
            foreach (Icon icon in Icons)
            {
                imageList.Images.Add(icon);
            }
            files_treeView.ImageList = imageList;
            files_treeView.SelectedImageIndex = -1;

            
            // load index files
            foreach (int idxIndex in IndexOpen)
            {
                String indexPath = Path.Combine(Path.Combine(Config.HglDir, "data"), Index.FileNames[idxIndex] + ".idx");
                if (!File.Exists(indexPath)) continue;


                // update progress
                if (progressForm != null)
                {
                    progressForm.SetCurrentItemText("Loading " + Index.FileNames[idxIndex] + "...");
                }


                // read in/parse index file
                byte[] indexData = File.ReadAllBytes(indexPath);
                Index index = new Index();
                if (!index.ParseData(indexData, indexPath))
                {
                    MessageBox.Show("Failed to read index file:\n" + indexPath, "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    continue;
                }
                _indexFiles.Add(index);


                // parse file index array
                foreach (Index.FileIndex file in index.FileTable)
                {
                    NodeObject nodeObject = new NodeObject { Index = index };
                    String[] nodeKeys = file.DirectoryString.Split('\\');
                    TreeNode treeNode = null;


                    // can we edit the file via. Reanimator?
                    if ((nodeKeys.Contains(XmlCookedSkill.RootFolder) && file.FileNameString.EndsWith(XmlCookedFile.FileExtention)) ||
                        file.FileNameString.EndsWith(ExcelFile.FileExtention) ||
                        file.FileNameString.EndsWith("txt"))
                    {
                        nodeObject.CanEdit = true;
                    }

                    if (nodeKeys.Contains("_environments"))
                    {
                        int bp = 0;
                    }

                    foreach (string nodeKey in nodeKeys.Where(nodeKey => !String.IsNullOrEmpty(nodeKey)))
                    {
                        if (nodeKey == Index.BackupPrefix)
                        {
                            nodeObject.IsBackup = true;
                            continue;
                        }

                        if (treeNode == null)
                        {
                            treeNode = files_treeView.Nodes[nodeKey] ?? files_treeView.Nodes.Add(nodeKey, nodeKey);
                        }
                        else
                        {
                            treeNode = treeNode.Nodes[nodeKey] ?? treeNode.Nodes.Add(nodeKey, nodeKey);
                        }
                    }
                    Debug.Assert(treeNode != null);


                    // have we already added the file? if so, remove it for updated version
                    String key = file.DirectoryString + file.FileNameString;
                    if (_fileTable.Contains(key))
                    {
                        TreeNode fileNode = (TreeNode)_fileTable[key];
                        NodeObject nodeObj = (NodeObject)fileNode.Tag;

                        // is it the same index?
                        if (nodeObj.Index.FilePath == index.FilePath) continue;

                        // no, it's from a newer index
                        treeNode.Nodes.Remove(fileNode);
                        _fileTable.Remove(key);
                    }


                    // add file/node
                    TreeNode node = treeNode.Nodes.Add(key, file.FileNameString);
                    node.Tag = nodeObject;
                    _fileTable.Add(key, node);


                    // final nodeObject setups
                    nodeObject.FileIndex = file;
                    if (nodeObject.IsBackup)
                    {
                        node.ForeColor = BackupColor;
                    }
                    else if (!nodeObject.CanEdit)
                    {
                        node.ForeColor = NoEditColor;
                    }
                }
            }


            // aesthetics etc
            foreach (TreeNode treeNode in files_treeView.Nodes)
            {
                if (treeNode.Index == 0)
                {
                    files_treeView.SelectedNode = treeNode;
                }

                treeNode.Expand();
                _FlagFolderNodes(treeNode);
            }
            files_treeView.TreeViewNodeSorter = new NodeSorter();
            files_treeView.Sort();

            files_treeView.EndUpdate();
        }

        private static void _FlagFolderNodes(TreeNode treeNode)
        {
            if (treeNode.Nodes.Count <= 0) return;

            if (treeNode.Tag == null)
            {
                treeNode.Tag = new NodeObject { IsFolder = true };
                treeNode.ImageIndex = (int)IconIndex.Folder;
                treeNode.SelectedImageIndex = treeNode.ImageIndex;
            }

            foreach (TreeNode childNode in treeNode.Nodes)
            {
                _FlagFolderNodes(childNode);
            }
        }

        private void _FilesTreeViewAfterSelect(Object sender, TreeViewEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;
            NodeObject nodeObject = (NodeObject)selectedNode.Tag;

            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();

            if (nodeObject.IsFolder) // if is a folder
            {
                textBox1.Text = selectedNode.Text;
                textBox2.Text = "NA";
                textBox3.Text = "NA";
                textBox4.Text = "NA";

                revertFile_button.Enabled = false;
                label5.Text = "Folder elements can't be backed-up/restored.";
                label6.Text = "Extract this folder and all files & folders within it.";
                label7.Text = "Extract this folder and all files & folders within it, and then patch the index to force the game to load the extracted files over the .dat.\n\nWarning: Not all files can be safely patched; use with caution.";

                return;
            }

            Index.FileIndex fileIndex = nodeObject.FileIndex;
            Debug.Assert(fileIndex != null);

            textBox1.DataBindings.Add("Text", fileIndex, "FileNameString");
            textBox2.DataBindings.Add("Text", fileIndex, "UncompressedSize");
            textBox3.DataBindings.Add("Text", fileIndex, "CompressedSize");

            if (nodeObject.IsBackup)
            {
                revertFile_button.Enabled = true;
                label5.Text = "Restore this file to its original state so the game loads from the .dat as originally.";
            }
            else
            {
                revertFile_button.Enabled = false;
                label5.Text = "This file is neither modified nor patched out.";
            }

            if (fileIndex.Modified)
            {
                String fileDataPath = Path.Combine(fileIndex.DirectoryString.Replace(Index.BackupPrefix, ""), fileIndex.FileNameString);
                String filePath = Config.HglDir + fileDataPath;
                if (File.Exists(filePath))
                {
                    textBox4.DataBindings.Clear();
                    textBox4.Text = filePath;
                }
                else
                {
                    textBox4.DataBindings.Clear();
                    textBox4.DataBindings.Add("Text", fileIndex, "InIndex");
                }
            }
            else
            {
                textBox4.DataBindings.Clear();
                textBox4.DataBindings.Add("Text", fileIndex, "InIndex");
            }
        }

        private void _FilesTreeViewDoubleClick(Object sender, EventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;
            NodeObject nodeObject = (NodeObject)selectedNode.Tag;
            Debug.Assert(nodeObject != null);

            if (nodeObject.IsFolder || !nodeObject.CanEdit) return;

            Index.FileIndex fileIndex = nodeObject.FileIndex;

            if (fileIndex.FileNameString.EndsWith(ExcelFile.FileExtention) || fileIndex.FileNameString.EndsWith(StringsFile.FileExtention))
            {

            }
            else if (fileIndex.FileNameString.EndsWith(XmlCookedFile.FileExtention))
            {

            }
            else if (fileIndex.FileNameString.EndsWith(".txt"))
            {

            }
            else
            {
                MessageBox.Show("Unexpected editable file!\n(this shouldn't happen - please report this)", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _RevertFileButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Todo");
        }

        private void _ExtractButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Todo");
        }

        private void _ExtractPatchButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Todo");
        }

        private void _FilesTreeViewAfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = (int)IconIndex.FolderOpen;
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void _FilesTreeViewAfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = (int)IconIndex.Folder;
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }
    }
}