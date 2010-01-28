﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Reanimator
{
    public partial class TableForm : Form, IDisposable
    {
        Index index;
        FileStream indexFile;
        public FileStream IndexFile
        {
            get
            {
                return indexFile;
            }
        }

        FileStream dataFile;
        Strings strings;

        public TableForm(FileStream file, Index idx)
        {
            index = idx;
            indexFile = file;
            String dataFileName = file.Name.Substring(0, file.Name.LastIndexOf('.')) + ".dat";
            try
            {
                dataFile = new FileStream(dataFileName, FileMode.Open);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open accompanying data file:\n" + dataFileName + "\nYou will be unable to extract any files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            InitializeComponent();
            this.dataGridView.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(dataGridView_CellContextMenuStripNeeded);
            this.dataGridView.RowHeadersVisible = false;

            //Initialize the DataGridViewColumn control
            IndexFileCheckBoxColumn.DefaultCellStyle.DataSourceNullValue = false;
            IndexFileCheckBoxColumn.Frozen = false;
            IndexFileCheckBoxColumn.Width = 24;
            IndexFileCheckBoxColumn.TrueValue = true;
            IndexFileCheckBoxColumn.FalseValue = false;
            IndexFileCheckBoxColumn.Name = "IndexFileCheckBoxColumn";
        }

        public TableForm(Strings strs)
        {
            strings = strs;

            InitializeComponent();
            this.dataGridView.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(dataGridView_CellContextMenuStripNeeded);
            this.dataGridView.RowHeadersVisible = false;

            //Initialize the DataGridViewColumn control
            IndexFileCheckBoxColumn.DefaultCellStyle.DataSourceNullValue = false;
            IndexFileCheckBoxColumn.Frozen = false;
            IndexFileCheckBoxColumn.TrueValue = true;
            IndexFileCheckBoxColumn.FalseValue = false;
            IndexFileCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            IndexFileCheckBoxColumn.Width = 24;
            IndexFileCheckBoxColumn.Name = "IndexFileCheckBoxColumn";
        }

        /// <summary>
        /// Returns a list of items that were checked in the DataGridView
        /// </summary>
        /// <returns>A list of all checked items</returns>
        public Index.FileIndex[] GetCheckedFiles()
        {
            int counter = 0;
            //A list of checked files
            List<Index.FileIndex> fileIndex = new List<Index.FileIndex>();

            //Get the data bound to the DataGridView
            Index.FileIndex[] index = (Index.FileIndex[])this.dataGridView.DataSource;

            //Iterate through the Rows and check if the checkBoxes were checked 
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                //Get the checkBox of the current row
                DataGridViewCell cell = row.Cells["IndexFileCheckBoxColumn"];

                //As the checkBox.Value doesn't seem to get initialized even though the default value is set to "false",
                //check if the value is not null (right now they get initialized after being checked or unchecked once)
                if (cell.Value != null)
                {
                    //if the value is not null, it is a bool value. If it is checked, add the file to the new file list
                    if ((bool)cell.Value)
                    {
                        //adds the current row (FileIndex) to the new list (assues, that the original sequence was not
                        //modified/changed by sorting/rearranging)
                        fileIndex.Add(index[counter]);
                    }
                }
                counter++;
            }

            return fileIndex.ToArray();
        }

        public Index.FileIndex[] GetSelectedFiles()
        {
            int counter = 0;
            List<Index.FileIndex> fileIndex = new List<Index.FileIndex>();
            Index.FileIndex[] index = (Index.FileIndex[])this.dataGridView.DataSource;

            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                if (row.Selected)
                {
                    fileIndex.Add(index[counter]);
                }

                counter++;
            }

            return fileIndex.ToArray();
        }

        private void dataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellEventArgs e)
        {
            Point pt = dataGridView.PointToClient(MousePosition);
            DataGridView.HitTestInfo hti = dataGridView.HitTest(pt.X, pt.Y);

            if (hti.Type == DataGridViewHitTestType.Cell)
            {
                /*
                if (dataGridView.SelectedRows.Count == 1)
                {
                    Index.FileIndex[] index = (Index.FileIndex[])this.dataGridView.DataSource;
                    Index.FileIndex file = index[hti.RowIndex];
                    if (file.FilenameString.EndsWith("cooked"))
                    {
                        System.Windows.Forms.ToolStripMenuItem viewCookedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                        viewCookedToolStripMenuItem.Name = "viewCookedToolStripMenuItem";
                        viewCookedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
                        viewCookedToolStripMenuItem.Text = "View as Text...";
                        viewCookedToolStripMenuItem.Click += new System.EventHandler(viewCookedToolStripMenuItem_Click);
                        contextMenuStrip1.Items.Add(viewCookedToolStripMenuItem);
                    }
                }
                */

                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void viewCookedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Index.FileIndex file = GetSelectedFiles()[0];
            byte[] data = ReadDataFile(file);

            FileStream fileOut = new FileStream("blah.txt", FileMode.CreateNew);
             * //  ColorSets colorSets = new ColorSets(data);
             * */
        }

        private void extractSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractFiles(GetSelectedFiles());
        }

        private void extractCheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractFiles(GetCheckedFiles());
        }

        private void extractAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractFiles(index.GetFileTable());
        }

        private void ExtractFiles(Index.FileIndex[] files)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = indexFile.Name.Substring(0, indexFile.Name.LastIndexOfAny("\\".ToCharArray()));
            if (folderBrowserDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            bool keepPath = false;
            DialogResult dr = MessageBox.Show("Keep directory structure?", "Path", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else if (dr == DialogResult.Yes)
            {
                keepPath = true;
            }

            foreach (Index.FileIndex file in files)
            {
                byte[] buffer = ReadDataFile(file);

                string keepPathString = "\\";
                if (keepPath)
                {
                    keepPathString += file.DirectoryString;
                    Directory.CreateDirectory(folderBrowserDialog.SelectedPath + keepPathString);
                }

                FileStream fileOut = new FileStream(folderBrowserDialog.SelectedPath + keepPathString + file.FilenameString, FileMode.Create);
                fileOut.Write(buffer, 0, buffer.Length);
                fileOut.Close();
            }

            MessageBox.Show(files.Length + " file(s) saved!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private byte[] ReadDataFile(Index.FileIndex file)
        {
            // Yes, this needs to be recreated for each file - it was having a fit when I didn't
            ManagedZLib.Decompress decompress = new ManagedZLib.Decompress(dataFile);
            byte[] buffer = new byte[file.UncompressedSize];
            dataFile.Seek(file.Offset, SeekOrigin.Begin);
            if (file.CompressedSize == 0)
            {
                decompress.Read(buffer, 0, 0);
            }
            else
            {
                decompress.Read(buffer, 0, file.UncompressedSize);
            }

            return buffer;
        }

        private void dataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            infoText_Label.Text = "Contains " + ((Array)dataGridView.DataSource).Length + " files.";
        }

        #region debug help
        //When a new DataSource is loaded display some information


        //Just for debugging purposes... uncomment this section and the event for final use
        //private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{

        //  Index.FileIndex[] index = GetSelectedFiles();

        //  string files = string.Empty;
        //  for (int counter = 0; counter < index.Length; counter++)
        //  {
        //    files += index[counter].FilenameString + "\n";
        //  }

        //  MessageBox.Show("The following items were checked:\n\n" + files, "Checked files");
        //}
        #endregion

        #region IDisposable Members

        new public void Dispose()
        {
            if (dataFile != null)
            {
                dataFile.Dispose();
            }

            Dispose(true);
        }

        #endregion

        //is doing the job, but iterating over every single field is slow... needs improvement
        //DataView features REAL search functionality, but I couldn't figure out how to use it
        /// <summary>
        /// Marks all rows that contain the entered keyword
        /// </summary>
        /// <param name="sender">The control that fired the event</param>
        /// <param name="e">Parameters</param>
        private void Search_Click(object sender, EventArgs e)
        {
            int counter = 0;

            this.dataGridView.SuspendLayout();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                row.Selected = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(tb_searchString.Text))
                    {
                        cell.Selected = true;
                        counter++;
                    }
                }
                this.dataGridView.ResumeLayout();

                searchResults_Label.Text = counter + " matching entries found";
            }
        }

        private void SelectAllEntries(bool selected)
        {
            dataGridView.SuspendLayout();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                row.Selected = selected;
            }
            dataGridView.ResumeLayout();
        }

        private void CheckAllEntries(bool check)
        {
            dataGridView.SuspendLayout();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                DataGridViewCell cell = row.Cells["IndexFileCheckBoxColumn"];
                cell.Value = check;
            }
            dataGridView.ResumeLayout();
        }

        private void CheckAll_Click(object sender, EventArgs e)
        {
            CheckAllEntries(true);
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            SelectAllEntries(true);
        }

        private void UnCheckAll_Click(object sender, EventArgs e)
        {
            CheckAllEntries(false);
        }

        private void UnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAllEntries(false);
        }
    }
}
