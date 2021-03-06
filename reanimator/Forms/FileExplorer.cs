using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Hellgate;
using Config = Revival.Common.Config;
using ExceptionLogger = Revival.Common.ExceptionLogger;
using Revival.Common;
using Reanimator.Forms.HeroEditorFunctions;

namespace Reanimator.Forms
{
    public partial class FileExplorer : Form
    {
        private readonly FileManager _fileManager;
        private readonly FileManager _fileManagerTCv4;
        private TreeView _clonedTreeView;
        private bool _isFiltering;

        /// <summary>
        /// Main constructor. Initialises the file tree system from a valid FileManager.
        /// </summary>
        /// <param name="fileManager">The FileManager to base the explorer tree on.</param>
        /// <param name="fileManagerTCv4">The FileManager to use for TCv4 specific extractions.</param>
        public FileExplorer(FileManager fileManager, FileManager fileManagerTCv4)
        {
            // init stuffs
            InitializeComponent();
            _files_fileTreeView.DoubleBuffered(true);
            _fileManager = fileManager;
            _fileManagerTCv4 = fileManagerTCv4;
            backupKey_label.ForeColor = BackupColor;
            noEditorKey_label.ForeColor = NoEditColor;

            // load icons
            ImageList imageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };
            foreach (Icon icon in Icons)
            {
                imageList.Images.Add(icon);
            }
            _files_fileTreeView.ImageList = imageList;

            // generate tree data
            _files_fileTreeView.BeginUpdate();
            _GenerateFileTree();
            _files_fileTreeView.TreeViewNodeSorter = new NodeSorter();
            _files_fileTreeView.EndUpdate();

            // general setups
            _quickExcelTCv4_checkBox.Checked = (fileManagerTCv4 != null);
            _quickExcelTCv4_checkBox.Enabled = Config.LoadTCv4DataFiles;
            _fileActionsExtract_checkBox.Enabled = true;
            _fileActionsPath_textBox.Text = Config.HglDir;

            // populate index files used
            // todo
            _indexFiles_listBox.Items.AddRange(fileManager.IndexFiles.ToArray());
            //for (int i = 0; i < fileManager.IndexFiles.Count; i++) _indexFiles_listBox.SetSelected(i, true);
            //if (fileManagerTCv4 != null) _indexFiles_listBox.Items.AddRange(fileManagerTCv4.IndexFiles.ToArray());
        }

        /// <summary>
        /// Attempt to decrease user wait time by hiding until completely filled.
        /// </summary>
        /// <param name="sender">The TreeView clicked.</param>
        /// <param name="e">The Shown event args.</param>
        private void FileExplorer_Shown(object sender, EventArgs e)
        {
            _files_fileTreeView.EndUpdate();
        }

        /// <summary>
        /// Event Function for Tree View - After Select.
        /// Will update the file details based upon selection.
        /// Will update the file image preview if .dds file
        /// </summary>
        /// <param name="sender">The TreeView clicked.</param>
        /// <param name="e">The After Select event args.</param>
        private void _FilesTreeView_AfterSelect(Object sender, TreeViewEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeNode selectedNode = treeView.SelectedNode;
            NodeObject nodeObject = (NodeObject)selectedNode.Tag;
            PackFileEntry fileEntry = nodeObject.FileEntry;
            AtlasImageLoader loader = new AtlasImageLoader();

            pictureBox1.Image = null;
            pictureBox1.Tag = null;
            loader.ClearImageList();

            textBoxPath.Text = selectedNode.FullPath;

            pictureBox1.Hide();

            if (checkBoxPreview.Checked)
            {
                if (selectedNode.Name.EndsWith(".dds"))
                {
                    _fileManager.BeginAllDatReadAccess();
                    Bitmap bmp = AtlasImageLoader.TextureFromGameFile(selectedNode.FullPath, _fileManager);
                    bmp.Tag = selectedNode.Text.Replace(Path.GetExtension(selectedNode.Text), string.Empty);
                    _fileManager.EndAllDatAccess();

                    pictureBox1.Image = bmp;

                    pictureBox1.Show();
                }
                else if (selectedNode.Name.EndsWith("_atlas.xml"))
                {
                    try
                    {
                        _fileManager.BeginAllDatReadAccess();
                        loader.LoadAtlas(selectedNode.FullPath, _fileManager);
                        _fileManager.EndAllDatAccess();

                        if (loader.ImageCount > 0)
                        {
                            pictureBox1.Image = loader.GetImage(loader.GetImageNames()[0]);
                            pictureBox1.Tag = loader;
                            pictureBox1.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            _files_listView.Items.Clear();
            if (nodeObject.IsFolder)  return;

            // no file index means it's either an uncooked file, or a new file
            if (fileEntry == null)
            {
                // todo: this entires if-block is a little dodgy and assumes alot

                // assuming only uncooked at the moment
                Debug.Assert(nodeObject.IsUncookedVersion);

                // if it's the uncooked version, we need to use the parents node path
                TreeNode parentNode = selectedNode.Parent;
                String fileDataPath = parentNode.Name.Replace(".cooked", "");
                String filePath = Path.Combine(Config.HglDir, fileDataPath);

                FileInfo fileInfo;
                try
                {
                    fileInfo = new FileInfo(filePath);
                }
                catch (Exception)
                {
                    // todo remove file from tree as it has been moved(?)
                    return;
                }

                String[] fileDetails = new String[5];
                fileDetails[0] = fileInfo.Name;
                fileDetails[1] = fileInfo.Length.ToString();
                fileDetails[2] = String.Empty;
                fileDetails[3] = fileInfo.LastWriteTime.ToString();
                fileDetails[4] = fileDataPath;

                ListViewItem listViewItem = new ListViewItem(fileDetails);

                _files_listView.Items.Add(listViewItem);
                return;
            }

            _ListView_AddFileItem(fileEntry);

            if (fileEntry.Siblings == null) return;

            foreach (PackFileEntry siblingEntry in fileEntry.Siblings)
            {
                _ListView_AddFileItem(siblingEntry);
            }
        }

        private void _ListView_AddFileItem(PackFileEntry fileEntry)
        {
            String[] fileDetails = new String[5];
            fileDetails[0] = fileEntry.Name;
            fileDetails[1] = fileEntry.SizeUncompressed.ToString();
            fileDetails[2] = fileEntry.SizeCompressed.ToString();
            fileDetails[3] = DateTime.FromFileTime(fileEntry.FileTime).ToString();
            fileDetails[4] = fileEntry.IsPatchedOut ? fileEntry.Path : fileEntry.Pack.ToString();

            ListViewItem listViewItem = new ListViewItem(fileDetails) { Tag = fileEntry };
            if (fileEntry.IsPatchedOut) listViewItem.ForeColor = BackupColor;

            _files_listView.Items.Add(listViewItem);
        }

        // todo: finish me
        private void _FilesTreeView_DoubleClick(Object sender, EventArgs e)
        {
            TreeNode selectedNode = _files_fileTreeView.SelectedNode;
            NodeObject nodeObject = (NodeObject)selectedNode.Tag;
            Debug.Assert(nodeObject != null);

            // can't do anything with a folder
            if (nodeObject.IsFolder) return;

            if (selectedNode.Name.EndsWith(LevelRulesFile.Extension))
            {
                LevelRulesEditor levelRulesEditor;
                try
                {
                    levelRulesEditor = new LevelRulesEditor(_fileManager, nodeObject.FileEntry);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open Level Rules Editor:\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                levelRulesEditor.Show(MdiParent);
            }

            return;



            //if (nodeObject.IsFolder || !nodeObject.CanEdit) return;

            //String editorPath = null;

            //// todo: this section needs a good cleaning
            //// todo: implementation of choosing default program in the options menu
            //// todo: current implementation overwites already extracted/uncooked file without asking - open it instead or ask?
            //String nodeFullPath = String.Empty;
            //String filePath = String.Empty;
            //if (nodeFullPath.EndsWith(ExcelFile.Extension))
            //{
            //    MessageBox.Show("todo");
            //}
            //else if (nodeFullPath.EndsWith(StringsFile.Extention))
            //{
            //    MessageBox.Show("todo");
            //}
            //else if (nodeFullPath.EndsWith(XmlCookedFile.Extension))
            //{
            //    PackFileEntry fileIndex = nodeObject.FileEntry;
            //    String xmlDataPath = Path.Combine(Config.HglDir, nodeFullPath.Replace(".cooked", ""));

            //    byte[] fileData = _fileManager.GetFileBytes(fileIndex);
            //    if (fileData == null)
            //    {
            //        MessageBox.Show("Failed to read xml.cooked from source!", "Error", MessageBoxButtons.OK,
            //                        MessageBoxIcon.Error);
            //        return;
            //    }

            //    XmlCookedFile xmlCookedFile = new XmlCookedFile();
            //    try
            //    {
            //        xmlCookedFile.ParseFileBytes(fileData);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Failed to uncook xml file!\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    try
            //    {
            //        Directory.CreateDirectory(Path.GetDirectoryName(xmlDataPath));
            //        xmlCookedFile.SaveXml(xmlDataPath);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Failed to save uncooked xml file!\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    DialogResult drOpen = MessageBox.Show("Uncooked XML file saved at " + xmlDataPath + "\n\nOpen with editor?", "Success",
            //                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (drOpen == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            Process notePad = new Process { StartInfo = { FileName = Config.XmlEditor, Arguments = xmlDataPath } };
            //            notePad.Start();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Failed to start editor!\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //else if (nodeFullPath.EndsWith(".xml"))
            //{
            //    editorPath = Config.XmlEditor;
            //}
            //else if (nodeFullPath.EndsWith(".txt"))
            //{
            //    editorPath = Config.TxtEditor;
            //}
            //else if (nodeFullPath.EndsWith(".csv"))
            //{
            //    editorPath = Config.CsvEditor;
            //}
            //else
            //{
            //    MessageBox.Show("Unexpected editable file!\n(this shouldn't happen - please report this)", "Warning",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            //if (String.IsNullOrEmpty(editorPath) || String.IsNullOrEmpty(filePath)) return;

            //try
            //{
            //    Process process = new Process { StartInfo = { FileName = editorPath, Arguments = filePath } };
            //    process.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Failed to start editor!\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Event Function for "Start" Button - Click.
        /// Extracts and/or patches files out of selected index files to the specified location.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _StartProcess_Button_Click(object sender, EventArgs e)
        {
            // make sure we have at least 1 checked file
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            {
                MessageBox.Show("No files checked for extraction!", "Need Files", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            // are we extracting?
            bool extractFiles = _fileActionsExtract_checkBox.Checked;
            String savePath = String.Empty;
            bool keepStructure = false;
            if (extractFiles)
            {
                // where do we want to save it
                savePath = _fileActionsPath_textBox.Text;
                if (String.IsNullOrEmpty(savePath))
                {
                    // where do we want to save it
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = Config.HglDir };
                    if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) return;

                    savePath = folderBrowserDialog.SelectedPath;
                }

                // do we want to keep the directory structure?
                DialogResult drKeepStructure = MessageBox.Show("Keep directory structure?", "Path", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (drKeepStructure == DialogResult.Cancel) return;

                keepStructure = (drKeepStructure == DialogResult.Yes);
            }

            // are we patching?
            bool patchFiles = _fileActionsPatch_checkBox.Checked;

            // are we doing anything...
            if (!extractFiles && !patchFiles)
            {
                MessageBox.Show("Please select at least one action out of extracting or patching to perform!", "No Actions Checked", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // start process
            ExtractPackPatchArgs extractPatchArgs = new ExtractPackPatchArgs
            {
                ExtractFiles = extractFiles,
                KeepStructure = keepStructure,
                PatchFiles = patchFiles,
                RootDir = savePath,
                CheckedNodes = checkedNodes
            };

            ProgressForm progressForm = new ProgressForm(_DoExtractPatch, extractPatchArgs);
            progressForm.SetLoadingText(String.Format("Extracting file(s)... ({0})", checkedNodes.Count));
            progressForm.Show(this);
        }

        /// <summary>
        /// Event Function for "Browse" Button - Click for File Actions section.
        /// Promts user for folder location.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _FileActionsBrowse_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = Config.HglDir };
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) return;

            _fileActionsPath_textBox.Text = folderBrowserDialog.SelectedPath;
            _fileActionsExtract_checkBox.Checked = true;
        }


        /// <summary>
        /// Event Function for "Extract to..." Button - Click.
        /// Checks and extracts files to prompted location.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _ExtractButton_Click(object sender, EventArgs e)
        {
            // make sure we have at least 1 checked file
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            {
                MessageBox.Show("No files checked for extraction!", "Need Files", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            // where do we want to save it
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = Config.HglDir };
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) return;

            // do we want to keep the directory structure?
            DialogResult drKeepStructure = MessageBox.Show("Keep directory structure?", "Path", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (drKeepStructure == DialogResult.Cancel) return;

            ExtractPackPatchArgs extractPatchArgs = new ExtractPackPatchArgs
            {
                ExtractFiles = true,
                KeepStructure = (drKeepStructure == DialogResult.Yes),
                PatchFiles = false,
                RootDir = folderBrowserDialog.SelectedPath,
                CheckedNodes = checkedNodes
            };

            ProgressForm progressForm = new ProgressForm(_DoExtractPatch, extractPatchArgs);
            progressForm.SetLoadingText(String.Format("Extracting file(s)... ({0})", checkedNodes.Count));
            progressForm.Show(this);
        }

        /// <summary>
        /// Event Function for "Extract and Patch Index" Button -  Click.
        /// Checks and extracts files to HGL data locations, then patches out files and saves updated index files.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _ExtractPatchButton_Click(object sender, EventArgs e)
        {
            // make sure we have at least 1 checked file
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            {
                MessageBox.Show("No files checked for extraction!", "Need Files", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                "Extract & Patch out checked file's?",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No) return;

            ExtractPackPatchArgs extractPatchArgs = new ExtractPackPatchArgs
            {
                ExtractFiles = true,
                KeepStructure = true,
                PatchFiles = true,
                RootDir = Config.HglDir,
                CheckedNodes = checkedNodes
            };

            _files_fileTreeView.BeginUpdate();
            ProgressForm progressForm = new ProgressForm(_DoExtractPatch, extractPatchArgs);
            progressForm.SetLoadingText(String.Format("Extracting and Patching file(s)... ({0})", checkedNodes.Count));
            progressForm.Disposed += delegate { _files_fileTreeView.EndUpdate(); };
            progressForm.Show(this);
        }

        // todo: rewrite me
        private void _PackPatchButton_Click(object sender, EventArgs e)
        {
            // make sure we have at least 1 checked file
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            {
                MessageBox.Show("No files checked for packing!", "Need Files", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            // get our custom index - or create if doesn't exist
            IndexFile packIndex = null; // todo: rewrite IndexFiles.FirstOrDefault(index => index.FileNameWithoutExtension == ReanimatorIndex);
            if (packIndex == null)
            {
                String indexPath = String.Format(@"data\{0}.idx", ReanimatorIndex);
                indexPath = Path.Combine(Config.HglDir, indexPath);
                try
                {
                    File.Create(indexPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create custom index file!\n\n" + ex, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // todo: rewrite packIndex = new Index(indexPath);
                // todo: rewrite IndexFiles.Add(packIndex);
            }

            ExtractPackPatchArgs extractPackPatchArgs = new ExtractPackPatchArgs
            {
                PackIndex = packIndex,
                RootDir = Config.HglDir,
                CheckedNodes = checkedNodes,
                PatchFiles = false
            };

            _files_fileTreeView.BeginUpdate();
            ProgressForm progressForm = new ProgressForm(_DoPackPatch, extractPackPatchArgs);
            progressForm.Disposed += delegate { _files_fileTreeView.EndUpdate(); };
            progressForm.SetLoadingText("Packing and Patching files...");
            progressForm.Show();
        }

        private void _DoPackPatch(ProgressForm progressForm, Object param)
        {
            ExtractPackPatchArgs args = (ExtractPackPatchArgs)param;

            // find which checked nodes actually have files we can pack
            StringWriter packResults = new StringWriter();

            String state = String.Format("Checking {0} file(s) for packing...", args.CheckedNodes.Count);
            const int packCheckStep = 200;
            progressForm.SetLoadingText(state);
            progressForm.ConfigBar(0, args.CheckedNodes.Count, packCheckStep);
            packResults.WriteLine(state);

            int i = 0;
            List<TreeNode> packNodes = new List<TreeNode>();
            foreach (TreeNode checkedNode in args.CheckedNodes)
            {
                String filePath = Path.Combine(args.RootDir, checkedNode.FullPath);

                if (i % packCheckStep == 0)
                {
                    progressForm.SetCurrentItemText(filePath);
                }
                i++;

                // ensure exists
                if (!File.Exists(filePath))
                {
                    packResults.WriteLine("{0} - File Not Found", filePath);
                    continue;
                }

                // ensure it was once packed (need FilePathHash etc)
                // todo: implement Crypt.StringHash for FilePathHash and FolderPathHash
                NodeObject nodeObject = (NodeObject)checkedNode.Tag;
                if (nodeObject.FileEntry == null ||
                    nodeObject.FileEntry.NameHash == 0)
                {
                    packResults.WriteLine("{0} - File Has No Base Version", filePath);
                    continue;
                }

                packResults.WriteLine(filePath);
                packNodes.Add(checkedNode);
            }


            // write our error log if we have it
            const String packResultsFile = "packResults.log";
            if (packNodes.Count != args.CheckedNodes.Count)
            {
                try
                {
                    File.WriteAllText(packResultsFile, packResults.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to write to log file!\n\n" + e, "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }


                // can we pack any?
                if (packNodes.Count == 0)
                {
                    String errorMsg =
                        String.Format("None of the {0} files were able to be packed!\nSee {1} for more details.",
                                      args.CheckedNodes.Count, packResultsFile);
                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    String errorMsg =
                        String.Format("Of the {0} files checked, only {1} will be able to be packed.\nSee {2} for more details.\n\nContinue with packing and patching process?",
                                      args.CheckedNodes.Count, packNodes.Count, packResultsFile);
                    DialogResult continuePacking = MessageBox.Show(errorMsg, "Notice", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information);
                    if (continuePacking == DialogResult.No) return;
                }
            }


            // pack our files
            // todo: rewrite
            //if (!_BeginDatAccess(IndexFiles, true))
            //{
            //    MessageBox.Show(
            //        "Failed to open dat files for writing!\nEnsure no other programs are using them and try again.",
            //        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            state = String.Format("Packing {0} files...", packNodes.Count);
            progressForm.ConfigBar(0, packNodes.Count, packCheckStep);
            progressForm.SetLoadingText(state);
            packResults.WriteLine(state);

            i = 0;
            bool allNodesPacked = true;
            bool datNeedsCleaning = false;
            foreach (TreeNode packNode in packNodes)
            {
                NodeObject oldNodeObject = (NodeObject)packNode.Tag;

                if (i % packCheckStep == 0)
                {
                    progressForm.SetCurrentItemText(packNode.FullPath);
                }
                i++;

                // add to our custom index if not already present
                PackFileEntry fileEntry = null; // todo: rewrite args.PackIndex.GetFileFromIndex(packNode.FullPath);
                if (fileEntry == null)
                {
                    //fileEntry = args.PackIndex.AddFileToIndex(oldNodeObject.FileEntry);
                }
                else
                {
                    // file exists - we'll need to clean the dat afterwards and remove orphaned data bytes
                    datNeedsCleaning = true;
                }

                // update fileTime to now - ensures it will override older versions
                fileEntry.FileTime = DateTime.Now.ToFileTime();

                // read in file data
                String filePath = Path.Combine(Config.HglDir, packNode.FullPath);
                byte[] fileData;
                try
                {
                    fileData = File.ReadAllBytes(filePath);
                }
                catch (Exception)
                {
                    packResults.WriteLine("{0} - Failed to read file data", filePath);
                    allNodesPacked = false;
                    continue;
                }

                // append to dat file
                try
                {
                    // todo: rewite args.PackIndex.AddFileToDat(fileData, fileEntry);
                }
                catch (Exception)
                {
                    packResults.WriteLine("{0} - Failed to add to data file", filePath);
                    allNodesPacked = false;
                    continue;
                }

                packResults.WriteLine(filePath);

                // update our node object while we're here
                //oldNodeObject.AddSibling(oldNodeObject);
                //NodeObject newNodeObject = new NodeObject
                //{
                //    Siblings = oldNodeObject.Siblings,
                //    CanEdit = oldNodeObject.CanEdit,
                //    FileEntry = fileEntry,
                //    Index = args.PackIndex,
                //    IsFolder = false
                //};

                //packNode.Tag = newNodeObject;
                //packNode.ForeColor = BaseColor;
            }


            // were all files packed?
            if (!allNodesPacked)
            {
                try
                {
                    File.WriteAllText(packResultsFile, packResults.ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to write to log file!\n\n" + e, "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }

                String warningMsg = String.Format("Not all files were packed!\nCheck {0} for more details.",
                                                packResultsFile);
                MessageBox.Show(warningMsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            // do we need to clean our dat?
            progressForm.SetLoadingText("Generating and saving files...");
            progressForm.SetStyle(ProgressBarStyle.Marquee);
            if (datNeedsCleaning)
            {
                progressForm.SetCurrentItemText("Removing orphan data...");
                // todo: rewrite args.PackIndex.RebuildDatFile();
            }
            _fileManager.EndAllDatAccess();


            // write updated index
            progressForm.SetCurrentItemText("Writing update dat index...");
            try
            {
                byte[] idxBytes = args.PackIndex.ToByteArray();
                Crypt.Encrypt(idxBytes);
                File.WriteAllBytes(args.PackIndex.FilePath, idxBytes);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to write updated index file!\n\n" + e, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("File packing and idx/dat writing completed!", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        /// <summary>
        /// todo: finish me
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _RevertRestoreButton_Click(object sender, EventArgs e)
        {
            bool pass = true;
            foreach (IndexFile file in _fileManager.IndexFiles)
            {
                file.Repair();
                try
                {
                    byte[] indexBytes = file.ToByteArray();
                    Crypt.Encrypt(indexBytes);
                    File.WriteAllBytes(file.Path, indexBytes);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogException(ex);
                    pass = false;
                }
            }
            if (pass)
            {
                string title = "Success";
                string message = "Your installation was repaired without error. Please restart Reanimator.";
                MessageBox.Show(message, title, MessageBoxButtons.OK);
            }
            else
            {
                string title = "Failure";
                string message = "The repair is finished but one or more errors occured. Your installation may or may not have been repaired.";
                MessageBox.Show(message, title, MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Changes the folder icon in the TreeView to reflect tree expansion status.
        /// </summary>
        /// <param name="sender">The TreeView clicked.</param>
        /// <param name="e">The After Expand event args.</param>
        private void _FilesTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            NodeObject nodeObject = (NodeObject)e.Node.Tag;
            if (!nodeObject.IsFolder) return;

            e.Node.ImageIndex = (int)IconIndex.FolderOpen;
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        /// <summary>
        /// Changes the folder icon in the TreeView to reflect tree expansion status.
        /// </summary>
        /// <param name="sender">The TreeView clicked.</param>
        /// <param name="e">The After Expand event args.</param>
        private void _FilesTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            NodeObject nodeObject = (NodeObject)e.Node.Tag;
            if (!nodeObject.IsFolder) return;

            e.Node.ImageIndex = (int)IconIndex.Folder;
            e.Node.SelectedImageIndex = e.Node.ImageIndex;
        }

        /// <summary>
        /// Function to recursivly check the checked node children.
        /// </summary>
        /// <param name="sender">The TreeView clicked.</param>
        /// <param name="e">The After Check event args.</param>
        private void _FilesTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            _files_fileTreeView.AfterCheck -= _FilesTreeView_AfterCheck;
            //files_treeView.BeginUpdate();

            /* note: even after disabling the check event, the performance is still the same.
             * This is only left here for the meantime for demonstrative/debugging purposes.
             * >> Alex tested this and found it to be siginifcantly better with event removed - debug vs release compiles maybe?
             * 
             * After much testing - it isn't the looping/events/function calls causing the lag (at least, not the most siginificant)
             * it's the .Checked setter that seriously kills it (.Checked getter has no issues)
             * 
             * Also, Begin/End Update - while it might help on excessivly large amounts of check boxes,
             * on only small nodes it causes a huge noticable lag... (wtf?)
             * 
             * todo: possibly investigate reflection and private state member modification
             */
            _CheckChildNodes(e.Node);

            //files_treeView.EndUpdate();
            _files_fileTreeView.AfterCheck += _FilesTreeView_AfterCheck;
        }

        /// <summary>
        /// Event function to apply the filter with an Enter key.
        /// </summary>
        /// <param name="sender">The TextBox selected.</param>
        /// <param name="e">The TextBox on KeyDown event args.</param>
        private void _Filter_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) _FilterApplyButton_Click(filterApply_button, null);
        }

        /// <summary>
        /// Event function to apply a filter to the TreeView for button "Apply"-filter.
        /// </summary>
        /// <param name="sender">The Button clicked.</param>
        /// <param name="e">The ButtonClick event args.</param>
        private void _FilterApplyButton_Click(object sender, EventArgs e)
        {
            String filterText = filter_textBox.Text;
            if (String.IsNullOrEmpty(filterText) || _isFiltering) return;

            _isFiltering = true;
            filterApply_button.Enabled = false;
            filterReset_button.Enabled = false;


            // if "reset"
            if (filterText == "*.*")
            {
                _ResetFilter();
                _isFiltering = false;
                filterApply_button.Enabled = true;
                filterReset_button.Enabled = true;
                return;
            }


            _files_fileTreeView.BeginUpdate();
            // clone tree for filtering
            if (_clonedTreeView == null)
            {
                _clonedTreeView = new TreeView();
                foreach (TreeNode treeNode in _files_fileTreeView.Nodes)
                {
                    _clonedTreeView.Nodes.Add((TreeNode)treeNode.Clone());
                }
            }
            else // if not null, then we need to "reset" the current view to original
            {
                _files_fileTreeView.Nodes.Clear();
                foreach (TreeNode treeNode in _clonedTreeView.Nodes)
                {
                    _files_fileTreeView.Nodes.Add((TreeNode)treeNode.Clone());
                }
                _AssignColors(_files_fileTreeView.Nodes);
            }


            // apply filter
            int nodeCount = _files_fileTreeView.Nodes.Count;
            for (int i = 0; i < nodeCount; i++)
            {
                if (!_ApplyFilter(_files_fileTreeView.Nodes[i], filterText)) continue;

                i--;
                nodeCount--;
            }

            // some aesthetics
            foreach (TreeNode treeNode in _files_fileTreeView.Nodes)
            {
                if (treeNode.Index == 0)
                {
                    _files_fileTreeView.SelectedNode = treeNode;
                }

                treeNode.Expand();
            }
            _files_fileTreeView.EndUpdate();
            _isFiltering = false;
            filterApply_button.Enabled = true;
            filterReset_button.Enabled = true;
        }

        /// <summary>
        /// Event Function for "Reset"-filter Button - Click.
        /// Calls ResetFilter() function to reset the TreeView filter.
        /// </summary>
        /// <param name="sender">The Button clicked.</param>
        /// <param name="e">The ButtonClick event args.</param>
        private void _FilterResetButton_Click(object sender, EventArgs e)
        {
            if (_isFiltering) return;

            _isFiltering = true;
            filterApply_button.Enabled = false;
            filterReset_button.Enabled = false;

            filter_textBox.Text = "*.*";
            _ResetFilter();

            _isFiltering = false;
            filterApply_button.Enabled = true;
            filterReset_button.Enabled = true;
        }

        /// <summary>
        /// Determines if the file HGL tries to load exists.<br />
        /// That is, checks .dat, if patched out, checks HGL data dir's.
        /// </summary>
        /// <param name="filePath">The path to the file - relative to HGL e.g. "data\colorsets.xml.cooked"</param>
        /// <returns>true for file exists, false otherwise.</returns>
        //public bool GetFileExists(String filePath)
        //{
        //    if (String.IsNullOrEmpty(filePath)) return false;

        //    if (filePath[0] == '\\')
        //    {
        //        filePath = filePath.Replace(@"\data", "data");
        //    }

        //    TreeNode treeNode = (TreeNode)_fileTable[filePath];
        //    if (treeNode == null) return false;

        //    // is not backup (in idx/dat)
        //    NodeObject nodeObject = (NodeObject)treeNode.Tag;
        //    if (!nodeObject.IsBackup) return true;

        //    // get full file path
        //    filePath = Path.Combine(Config.HglDir, treeNode.FullPath);
        //    return File.Exists(filePath);
        //}

        /// <summary>
        /// Event Function for "Uncook" Button -  Click.
        /// Uncooks checked files to their respective HGL file system location.
        /// </summary>
        /// <param name="sender">The button clicked.</param>
        /// <param name="e">The Click event args.</param>
        private void _UncookButton_Click(object sender, EventArgs e)
        {
            // make sure we have at least 1 checked file
            List<TreeNode> checkedNodes = new List<TreeNode>();
            if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            {
                MessageBox.Show("No files checked for extraction!", "Need Files", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            // we're uncooking, so we want only uncook-able files
            List<TreeNode> uncookingNodes = (from treeNode in checkedNodes
                                             let nodeObject = (NodeObject)treeNode.Tag
                                             where nodeObject.CanCookWith && !nodeObject.IsUncookedVersion
                                             select treeNode).ToList();
            if (uncookingNodes.Count == 0)
            {
                MessageBox.Show("Unable to find any checked files that can be uncooked!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ProgressForm progressForm = new ProgressForm(_DoUnooking, uncookingNodes);
            progressForm.SetLoadingText(String.Format("Uncooking file(s)... ({0})", uncookingNodes.Count));
            progressForm.Show(this);
        }

        private void _CookButton_Click(object sender, EventArgs e)
        {
            // todo fix me
            return;

            // make sure we have at least 1 checked file
            //List<TreeNode> checkedNodes = new List<TreeNode>();
            //if (_GetCheckedNodes(_files_fileTreeView.Nodes, checkedNodes) == 0)
            //{
            //    MessageBox.Show("No files checked for extraction!", "Need Files", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Information);
            //    return;
            //}

            //// we're uncooking, so we want only cook-able files
            //List<TreeNode> cookNodes = (from treeNode in checkedNodes
            //                            let nodeObject = (NodeObject)treeNode.Tag
            //                            where nodeObject.CanCookWith && nodeObject.IsUncookedVersion
            //                            select treeNode).ToList();
            //if (cookNodes.Count == 0)
            //{
            //    MessageBox.Show("Unable to find any checked files that can be cooked!", "Error", MessageBoxButtons.OK,
            //                    MessageBoxIcon.Exclamation);
            //    return;
            //}

            //ProgressForm progressForm = new ProgressForm(_DoCooking, cookNodes);
            //progressForm.SetLoadingText(String.Format("Uncooking file(s)... ({0})", cookNodes.Count));
            //progressForm.Show(this);
        }

        private void _DoCooking(ProgressForm progressForm, Object param)
        {
            List<TreeNode> cookNodes = (List<TreeNode>)param;
            const int progressUpdateFreq = 20;
            if (progressForm != null)
            {
                progressForm.ConfigBar(1, cookNodes.Count, progressUpdateFreq);
            }

            int i = 0;
            //foreach (String nodeFullPath in cookNodes.Select(treeNode => treeNode.FullPath))
            foreach (TreeNode treeNode in cookNodes)
            {
                TreeNode cookedNode = treeNode.Parent;
                String nodeFullPath = cookedNode.FullPath.Replace(".cooked", "");
                String filePath = Path.Combine(Config.HglDir, nodeFullPath);
                Debug.Assert(filePath.EndsWith(".xml"));

                if (i % progressUpdateFreq == 0 && progressForm != null)
                {
                    progressForm.SetCurrentItemText(filePath);
                }
                i++;

                //if (nodeFullPath.Contains("actor_ghost.xml"))
                //{
                //    int bp = 0;
                //}

                if (!File.Exists(filePath)) continue;
                XmlDocument xmlDocument = new XmlDocument();
                XmlCookedFile cookedXmlFile = new XmlCookedFile(_fileManager);

                DialogResult dr = DialogResult.Retry;
                byte[] cookedBytes = null;
                while (dr == DialogResult.Retry && cookedBytes == null)
                {
                    try
                    {
                        xmlDocument.Load(filePath);
                        cookedBytes =  cookedXmlFile.CookXmlDocument(xmlDocument);
                    }
                    catch (Exception e)
                    {
                        ExceptionLogger.LogException(e, true);

                        String errorMsg = String.Format("Failed to cook file!\n{0}\n\n{1}", nodeFullPath, e);
                        dr = MessageBox.Show(errorMsg, "Error",
                                             MessageBoxButtons.AbortRetryIgnore,
                                             MessageBoxIcon.Exclamation);
                        if (dr == DialogResult.Abort) return;
                        if (dr == DialogResult.Ignore) break;
                    }
                }

                if (cookedBytes == null) continue;

                // todo: update newly cooked file to file tree
                String savePath = Path.Combine(Config.HglDir, filePath + ".cooked");
                File.WriteAllBytes(savePath, cookedBytes);

                // debug section
                //String savePath2 = Path.Combine(Config.HglDir, filePath + ".cooked2");
                //File.WriteAllBytes(savePath2, cookedBytes);
                //byte[] origBytes = File.ReadAllBytes(savePath);

                //if (cookedBytes.Length != origBytes.Length)
                //{
                //    int bp = 0;
                //}

                //if (!origBytes.SequenceEqual(cookedBytes))
                //{
                //    int bp = 0;
                //}
            }
        }

        private void _QuickXmlButtonClick(object sender, EventArgs e)
        {
            // where do we want to save it
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = Config.HglDir, Description = "Select folder to save uncooked XML files..."};
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) return;

            ExtractPackPatchArgs extractPatchArgs = new ExtractPackPatchArgs
            {
                RootDir = folderBrowserDialog.SelectedPath
            };

            ProgressForm progressForm = new ProgressForm(_QuickXmlWorker, extractPatchArgs);
            progressForm.SetLoadingText("Extracting and uncooking *.xml.cooked files...");
            progressForm.Show(this);
        }

        private void _QuickXmlWorker(ProgressForm progressForm, Object param)
        {
            const int progressStepRate = 50;
            const String outputResultsName = "uncook_results.txt";
            ExtractPackPatchArgs extractPatchArgs = (ExtractPackPatchArgs)param;

            TextWriter consoleOut = Console.Out;
            TextWriter textWriter = new StreamWriter(outputResultsName);
            Console.SetOut(textWriter);
            Console.WriteLine("Results of most recent uncooking of .xml.cooked files. Please scroll to end for tallied results.");


            // get all .xml.cooked
            List<PackFileEntry> xmlCookedFiles = _fileManager.FileEntries.Values.Where(fileEntry => fileEntry.Name.EndsWith(XmlCookedFile.Extension)).ToList();
            _fileManager.BeginAllDatReadAccess();

            // loop through file entries
            int count = xmlCookedFiles.Count();
            progressForm.ConfigBar(1, count, progressStepRate);
            progressForm.SetLoadingText("Extracting and uncooking .xml.cooked files... (" + count + ")");
            int i = 0;
            int uncooked = 0;
            int readFailed = 0;
            int uncookFailed = 0;
            int testCentreWarnings = 0;
            int resurrectionWarnings = 0;
            int excelWarnings = 0;
            foreach (PackFileEntry fileEntry in xmlCookedFiles)
            {
                // update progress
                if (i % progressStepRate == 0)
                {
                    progressForm.SetCurrentItemText(fileEntry.Path);
                }
                i++;

                // get file and uncook
                Console.WriteLine(fileEntry.Path);
                byte[] fileBytes;
                XmlCookedFile xmlCookedFile = new XmlCookedFile(_fileManager);

                try
                {
                    fileBytes = _fileManager.GetFileBytes(fileEntry);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: FileManager failed to read file!\n");
                    readFailed++;
                    continue;
                }

                try
                {
                    xmlCookedFile.ParseFileBytes(fileBytes, true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine("Warning: Failed to uncook file: " + fileEntry.Name + "\n");
                    uncookFailed++;
                    continue;
                }

                // did we have any uncooking issues?
                bool hadWarning = false;
                if (xmlCookedFile.HasTestCentreElements)
                {
                    Console.WriteLine("Warning: File has TestCentre-specific elements.");
                    hadWarning = true;
                    testCentreWarnings++;
                }
                if (xmlCookedFile.HasResurrectionElements)
                {
                    Console.WriteLine("Warning: File has Resurrection-specific elements.");
                    hadWarning = true;
                    resurrectionWarnings++;
                }
                if (xmlCookedFile.HasExcelStringsMissing)
                {
                    Console.WriteLine("Warning: File has " + xmlCookedFile.ExcelStringsMissing.Count + " unknown excel strings: ");
                    foreach (String str in xmlCookedFile.ExcelStringsMissing) Console.WriteLine("\t- \"" + str + "\"");
                    hadWarning = true;
                    excelWarnings++;
                }

                // save file
                String savePath = Path.Combine(extractPatchArgs.RootDir, fileEntry.Path);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                try
                {
                    xmlCookedFile.SaveXmlDocument(savePath.Replace(".cooked", ""));
                }
                catch (Exception)
                {
                    Console.WriteLine("Warning: Failed to save XML file: " + savePath + "\n");
                    continue;
                }

                if (hadWarning) Console.WriteLine();
                uncooked++;
            }

            _fileManager.EndAllDatAccess();

            // output final results
            Console.WriteLine("\nXML Files Uncooked: " + uncooked);
            if (readFailed > 0) Console.WriteLine(readFailed + " file(s) could not be read from the data files.");
            if (uncookFailed > 0) Console.WriteLine(uncookFailed + " file(s) failed to uncook at all.");
            if (testCentreWarnings > 0) Console.WriteLine(testCentreWarnings + " file(s) had TestCentre-specific XML elements which wont be included when recooked.");
            if (resurrectionWarnings > 0) Console.WriteLine(resurrectionWarnings + " file(s) had Resurrection-specific XML elements which wont be included when recooked.");
            if (excelWarnings > 0) Console.WriteLine(excelWarnings + " file(s) had excel warnings and cannot be safely recooked.");
            textWriter.Close();
            Console.SetOut(consoleOut);

            try
            {
                Process process = new Process { StartInfo = { FileName = Config.TxtEditor, Arguments = outputResultsName } };
                process.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Failed to open results!\nThe " + outputResultsName + " can be found in your Reanimator folder.\n" +
                    e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _QuickLevelRulesButtonClick(object sender, EventArgs e)
        {
            // where do we want to save it
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = Config.HglDir, Description = "Select folder to save converted Level Rule files..."};
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK) return;

            ExtractPackPatchArgs extractPatchArgs = new ExtractPackPatchArgs
            {
                RootDir = folderBrowserDialog.SelectedPath
            };

            ProgressForm progressForm = new ProgressForm(_QuickLevelRulesWorker, extractPatchArgs);
            progressForm.SetLoadingText("Converting *.drl files...");
            progressForm.Show(this);
        }

        private void _QuickLevelRulesWorker(ProgressForm progressForm, Object param)
        {
            const int progressStepRate = 50;
            const String outputResultsName = "conversion_results_drl.txt";
            ExtractPackPatchArgs extractPatchArgs = (ExtractPackPatchArgs)param;

            TextWriter consoleOut = Console.Out;
            TextWriter textWriter = new StreamWriter(outputResultsName);
            Console.SetOut(textWriter);
            Console.WriteLine("Results of most recent conversion of *.drl files. Please scroll to end for tallied results.");

            // get files
            List<PackFileEntry> drlFiles = _fileManager.FileEntries.Values.Where(fileEntry => fileEntry.Name.EndsWith(LevelRulesFile.Extension)).ToList();
            _fileManager.BeginAllDatReadAccess();

            // loop through file entries
            int count = drlFiles.Count();
            progressForm.ConfigBar(1, count, progressStepRate);
            progressForm.SetLoadingText("Converting *.drl files... (" + count + ")");
            int i = 0;
            int successful = 0;
            int readFailed = 0;
            int exportFailed = 0;
            int saveFailed = 0;
            int failed = 0;
            foreach (PackFileEntry fileEntry in drlFiles)
            {
                // update progress
                if (i % progressStepRate == 0)
                {
                    progressForm.SetCurrentItemText(fileEntry.Path);
                }
                i++;

                // get file and convert
                Console.WriteLine("Processing file " + fileEntry.Path + "...");
                byte[] fileBytes;
                LevelRulesFile levelRulesFile = new LevelRulesFile();

                try
                {
                    fileBytes = _fileManager.GetFileBytes(fileEntry);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine("Error: FileManager failed to read file!\n");
                    readFailed++;
                    continue;
                }

                try
                {
                    levelRulesFile.ParseFileBytes(fileBytes);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine("Warning: Failed to convert file: " + fileEntry.Name + "\n");
                    failed++;
                    continue;
                }

                // generate file
                String savePath = Path.Combine(extractPatchArgs.RootDir, fileEntry.Path);

                byte[] documentBytes;
                try
                {
                    documentBytes = levelRulesFile.ExportAsDocument();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine("Warning: ExportAsDocument() failed for file: " + savePath + "\n");
                    exportFailed++;
                    continue;
                }

                // save file
                String filePath = savePath.Replace(LevelRulesFile.Extension, LevelRulesFile.ExtensionDeserialised);
                try
                {
                    String directoryName = Path.GetDirectoryName(savePath);
                    Debug.Assert(directoryName != null);

                    Directory.CreateDirectory(directoryName);
                    
                    // FormTools.WriteFileWithRetry(filePath, documentBytes); should use this - but want a way to "cancel all" first, or will get endless questions for big failures
                    File.WriteAllBytes(filePath, documentBytes);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Console.WriteLine("Warning: WriteAllBytes() failed for file: " + filePath + "\n");
                    saveFailed++;
                    continue;
                }

                successful++;
            }

            _fileManager.EndAllDatAccess();

            // output final results
            Console.WriteLine("\nFiles Found: " + count);
            Console.WriteLine("\nFiles Created: " + successful);
            if (readFailed > 0) Console.WriteLine(readFailed + " file(s) could not be read from the data files.");
            if (exportFailed > 0) Console.WriteLine(readFailed + " file(s) could not be exported.");
            if (saveFailed > 0) Console.WriteLine(readFailed + " file(s) could not be saved.");
            if (failed > 0) Console.WriteLine(failed + " file(s) failed.");
            textWriter.Close();
            Console.SetOut(consoleOut);

            try
            {
                Process process = new Process { StartInfo = { FileName = Config.TxtEditor, Arguments = outputResultsName } };
                process.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Failed to open results!\nThe " + outputResultsName + " can be found in your Reanimator folder.\n" +
                    e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (PackFileEntry fileEntry in _fileManager.FileEntries.Values)
            {
                PackFile indexFile = fileEntry.Pack;


                // don't patch out string files or sound/movie files
                if (IndexFile.NoPatchExt.Any(ext => fileEntry.Name.EndsWith(ext)))
                {
                    if ((indexFile.NameWithoutExtension == "sp_hellgate_1.10.180.3416_1.0.86.4580" ||
                        indexFile.NameWithoutExtension == "sp_hellgate_localized_1.10.180.3416_1.0.86.4580") &&
                        (fileEntry.Name.EndsWith(StringsFile.Extention) ||
                        fileEntry.Name.EndsWith(ExcelFile.Extension)))
                    {
                        fileEntry.IsPatchedOut = true;
                    }

                    continue;
                }

                fileEntry.IsPatchedOut = true;
                if (fileEntry.Siblings == null) continue;

                foreach (PackFileEntry siblingEntry in fileEntry.Siblings)
                {
                    siblingEntry.IsPatchedOut = true;
                }
            }

            foreach (IndexFile indexFile in _fileManager.IndexFiles)
            {
                byte[] indexBytes = indexFile.ToByteArray();
                Crypt.Encrypt(indexBytes);
                File.WriteAllBytes(indexFile.FilePath, indexBytes);
            }
        }

        private void _QuickExcelBrowse_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dr = folderBrowserDialog.ShowDialog(this);

            if (dr != DialogResult.OK) return;
            _quickExcelDir_textBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void _QuickExcel_Button_Click(object sender, EventArgs e)
        {
            ProgressForm progressForm = new ProgressForm(_DoQuickExcel, _fileManager);
            progressForm.ConfigBar(0, 20, 1);
            progressForm.SetLoadingText("Uncooking excel files..."); ;
            progressForm.Show(this);


            if (!_quickExcelTCv4_checkBox.Checked || !_quickExcelTCv4_checkBox.Enabled) return;
            ProgressForm progressFormTCv4 = new ProgressForm(_DoQuickExcel, _fileManagerTCv4);
            progressFormTCv4.ConfigBar(0, 20, 1);
            progressFormTCv4.SetLoadingText("Uncooking TCv4 excel files..."); ;
            progressFormTCv4.Show(this);
            progressFormTCv4.Top = progressForm.Top + progressForm.Height + 20;
        }

        private void _DoQuickExcel(ProgressForm progressForm, Object param)
        {
            FileManager fileManager = (FileManager)param;
            String root = _quickExcelDir_textBox.Text;
            if (root == "") return;
            if (fileManager.IsVersionTestCenter) root = Path.Combine(root, "tcv4");

            foreach (IndexFile file in fileManager.IndexFiles) // Open Dats for reading
                file.BeginDatReading();

            int i = 0;
            foreach (PackFileEntry fileEntry in fileManager.FileEntries.Values)
            {
                if (!fileEntry.Name.EndsWith(ExcelFile.Extension) &&
                    (!fileEntry.Name.EndsWith(StringsFile.Extention) || !fileEntry.Directory.Contains(fileManager.Language))) continue;

                if (i++ % 10 == 0 && progressForm != null) progressForm.SetCurrentItemText(fileEntry.Path);

                byte[] fileBytes;
                try
                {
                    fileBytes = fileManager.GetFileBytes(fileEntry, true);
                }
                catch (Exception e)
                {
                    String error = String.Format("Failed to get '{0}' file bytes, continue?\n\n{1}", fileEntry.Path, e);
                    DialogResult dr = MessageBox.Show(error, "Excel Extration Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dr == DialogResult.No) return;
                    continue;
                }

                ExcelFile excelFile = null;
                if (fileEntry.Name.EndsWith(ExcelFile.Extension))
                {
                    excelFile = new ExcelFile(fileBytes, fileEntry.Path, fileManager.ClientVersion);
                    if (excelFile.Attributes.IsEmpty) continue;
                }

                byte[] writeBytes = fileBytes;
                if (excelFile != null)
                {
                    String[] columns = null;
                    if (fileManager.IsVersionTestCenter)
                    {
                        DataFile spVersion;
                        if (_fileManager.DataFiles.TryGetValue(excelFile.StringId, out spVersion))
                        {
                            ObjectDelegator objectDelegator = _fileManager.DataFileDelegators[spVersion.StringId];

                            FieldInfo[] fieldInfos = spVersion.Attributes.RowType.GetFields();
                            columns = new String[objectDelegator.PublicFieldCount];

                            int col = 0;
                            foreach (ObjectDelegator.FieldDelegate fieldDelegate in objectDelegator.FieldDelegatesPublicList)
                            {
                                columns[col++] = fieldDelegate.Name;
                            }
                        }
                    }

                    try
                    {
                        writeBytes = excelFile.ExportCSV(fileManager, columns);
                    }
                    catch (Exception e)
                    {
                        String error = String.Format("Failed to export CSV for '{0}', continue?\n\n{1}", fileEntry.Path, e);
                        DialogResult dr = MessageBox.Show(error, "Excel Extration Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dr == DialogResult.No) return;
                        continue;
                    }
                }

                String filePath = Path.Combine(root, fileEntry.Path).Replace(ExcelFile.Extension, ExcelFile.ExtensionDeserialised);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllBytes(filePath, writeBytes);
            }

            foreach (IndexFile file in fileManager.IndexFiles) // Close Dats
                file.EndDatAccess();
        }

        /// <summary>
        /// Will open a new Form for the image on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Tag == null && pictureBox1.Image != null)
            {
                Bitmap bmp = (Bitmap)pictureBox1.Image;

                string text = (string)pictureBox1.Image.Tag;
                TextureSheetPreview preview = new TextureSheetPreview();
                preview.SetBitmap(bmp, text);
                preview.Show(this);
            }
            else if(pictureBox1.Tag != null)
            {
                AtlasImageLoader loader = (AtlasImageLoader)pictureBox1.Tag;

                if (loader.ImageCount > 0)
                {
                    TextureSheetPreview preview = new TextureSheetPreview();
                    preview.SetAtlasImageLoader(loader);
                    preview.Show(this);

                    //loader.ClearImageList();
                }
            }
        }

        private void checkBoxPreview_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = checkBoxPreview.Enabled;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxPath.Text);
        }
    }
}