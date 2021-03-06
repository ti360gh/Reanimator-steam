using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hellgate;
using Reanimator.Forms;

namespace Reanimator.Controls
{
    public partial class TablesLoaded : UserControl
    {
        private readonly FileManager _fileManager;
        private TableEditorForm _excelTableForm;

        /// <summary>
        /// Constructs a list of tables that can be clicked to view.
        /// </summary>
        /// <param name="fileManager">FileManager dependency.</param>
        /// <param name="excelTableForm">Parents control of tables..</param>
        public TablesLoaded(FileManager fileManager, TableEditorForm excelTableForm)
        {
            _InitializeComponent();

            _fileManager = fileManager;
            _excelTableForm = excelTableForm;

            if (_fileManager == null || _fileManager.DataFiles.Count == 0) return;
            _loadedTables_ListBox.DataSource = new BindingSource { DataSource = _fileManager.DataFiles }; // new BindingSource(_fileManager.DataFiles, null); // fix for .net know issue
            _loadedTables_ListBox.DoubleClick += _loadedTables_ListBox_MouseDoubleClick;
            _loadedTables_ListBox.Format += _loadedTables_ListBox_Format;
        }

        /// <summary>
        /// Creates a mew tab control for the clicked table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _loadedTables_ListBox_MouseDoubleClick(object sender, EventArgs e)
        {
            KeyValuePair<String, DataFile> item = (KeyValuePair<String, DataFile>)_loadedTables_ListBox.SelectedItem;

            if (_excelTableForm == null || _excelTableForm.IsDisposed)
            {
                _excelTableForm = new TableEditorForm(_fileManager);
            }

            bool isOpen = _excelTableForm.IsTabOpen(item.Key);
            if (isOpen)
            {
                _excelTableForm.FocusTabPage(item.Key);
            }
            else
            {
                _excelTableForm.CreateTab(item.Key);
                _excelTableForm.FocusTabPage(item.Key);
            }

            _excelTableForm.Show();
        }

        /// <summary>
        /// Defines the list format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _loadedTables_ListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            KeyValuePair<String, DataFile> item = (KeyValuePair<String, DataFile>)e.ListItem;
            e.Value = item.Key;
        }
    }
}
