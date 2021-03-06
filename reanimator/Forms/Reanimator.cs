using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Hellgate;
using Revival.Common;
using Reanimator.Forms.DropOverviewForm;
using Reanimator.Forms.ItemQualityCalculatorForm;
using Reanimator.Forms.HeroEditorFunctions;

namespace Reanimator.Forms
{
    public partial class Reanimator : Form
    {
        private FileExplorer _fileExplorer;
        private FileManager _fileManager;
        private FileManager _fileManagerTCv4;
        private TableEditorForm _excelTableForm;
        private TableEditorForm _excelTableFormTCv4;
        private Options _optionsForm;
        private readonly List<TableForm> _openTableForms = new List<TableForm>();
        private readonly List<TableEditorForm> _openExcelTableForms = new List<TableEditorForm>();
        private List<HeroEditor> _openHeroEditorForms = new List<HeroEditor>();

        public Reanimator()
        {
            InitializeComponent();
            _SetIcon();

            #region snotz_stuff
            //TestScripts.ExtractFunctionList();
            //const String path = @"C:\test\functionslist_asm.txt";
            //String[] functionCode = File.ReadAllLines(path);
            //ExcelScript.ExtractSteamFunctionList(functionCode);
            #endregion

            #region alexs_stuff
            //Config.HglDir = @"D:\Games\Hellgate"; // Resurrection dir
            //Config.HglDir = @"D:\Projects\Hellgate London\Flagshipped\ServerTest\bin\Debug";
            //Config.HglDir = @"D:\Games\Hellgate London"; // SP dir
            //Config.HglDir = @"D:\Games\Hellgate London.fresh"; // virgin SP dir
            //TestScripts.TestAllXml(true);
            //TestScripts.ExtractFunctionList();
            //TestScripts.TestAllExcelScripts();
            //TestScripts.TestExcelCooking(true);
            //TestScripts.TestDataTableExportAndImport();
            //TestScripts.TestAllLevelRules();
            //ExcelScript.GlobalDebug(true);
            //XlsFile.GenerateExcelTables();
            if (true) return;
            //Config.HglDir = @"D:\Projects\Hellgate London\Flagshipped\ServerTest\bin\Debug";
            ////Config.HglDir = @"D:\Games\Hellgate";
            //int bitCount = 6;
            //int shift = (1 << (bitCount - 1));
            //bitCount -= shift;
            //if (true) return;
            //ExcelScript.GlobalDebug(true);
            //FileManager fileManager = new FileManager(Config.HglDir, FileManager.ClientVersions.Mod);
            //fileManager.BeginAllDatReadAccess();
            //fileManager.LoadTableFiles();
            //fileManager.EndAllDatAccess();
            //fileManager.ProcessTables();
            //UnitObject.FileManager = fileManager;
            //byte[] charBytes = FileTools.HexFileToByteArray(File.ReadAllText("C:\\stats.txt"));
            //UnitObject character = new UnitObject(true);
            //character.ParseUnitObject(charBytes, 0, charBytes.Length);

            //if (true) return;

            //byte[] charBytes = {
            //                       0xCD,
            //                       0x00, 0x04, 0x02, 0x27, 0xE8, 0x81, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x60, 0x34, 0x52,
            //                       0x2B, 0x5D,
            //                       0x00, 0x00, 0x00, 0x04, 0x03, 0x33, 0x27, 0x01, 0x52, 0x56, 0x45, 0xF1, 0x99, 0x0D,
            //                       0x02, 0x00,
            //                       0x00, 0x20, 0x0F, 0xB4, 0x1A, 0xA8, 0x8E, 0xCA, 0x35, 0x18, 0x0E, 0x13, 0xE9, 0x97,
            //                       0x9D, 0x8F,
            //                       0xE1, 0x27, 0x3C, 0xC0, 0xEA, 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            //                       0x00, 0x00,
            //                       0x00, 0x00, 0x00, 0x00, 0xF0, 0x07, 0x00, 0x00, 0x00, 0x20, 0x20, 0x00, 0x00, 0x00,
            //                       0x80, 0x40,
            //                       0x01, 0x20, 0x40, 0x01, 0x00, 0x00, 0x24, 0x8C, 0x82, 0x01, 0x00, 0x00, 0xB2, 0x21,
            //                       0x06, 0xE4,
            //                       0xC4, 0xC8, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA3, 0x91, 0x5A, 0x01
            //};

            //FileManager fileManager = new FileManager(@"D:\Projects\Hellgate London\Flagshipped\ServerTest\bin\Debug");
            //fileManager.LoadTableFiles();
            //UnitObject.FileManager = fileManager;

            //byte[] charBytes = { 0xCD, 0x00, 0x04, 0x02, 0x67, 0xE8, 0x01, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x60, 0x34, 0x52, 0x2B, 0x0E, 0x00, 0x00, 0x00, 0x65, 0x16, 0x26, 0x05, 0x00, 0x00, 0x40, 0xE5, 0x61, 0x26, 0x68, 0xE3, 0xB6, 0x21, 0x58, 0xA3, 0x31, 0xF1, 0xD7, 0xCD, 0xED, 0x8E, 0x56, 0xFF, 0xFF, 0xEF, 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x07, 0x00, 0x00, 0x00, 0xC0, 0x1F, 0x00, 0x00, 0x00, 0x80, 0x81, 0x02, 0x40, 0x80, 0x00, 0x00, 0x00, 0x48, 0x00, 0x00, 0x46, 0x23, 0xB5, 0x02 };
            //byte[] goldDropBytes = { 0xCD, 0x00, 0x04, 0x02, 0x27, 0xE8, 0x81, 0x03, 0x0C, 0x00, 0x00, 0x00, 0x60, 0x34, 0x52, 0x2B, 0x5D, 0x00, 0x00, 0x00, 0x04, 0x03, 0x33, 0x27, 0x01, 0x52, 0x56, 0x45, 0xF1, 0x99, 0x0D, 0x02, 0x00, 0x00, 0x20, 0x0F, 0xB4, 0x1A, 0xA8, 0x8E, 0xCA, 0x35, 0x18, 0x0E, 0x13, 0xE9, 0x97, 0x9D, 0x8F, 0xE1, 0x27, 0x3C, 0xC0, 0xEA, 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x07, 0x00, 0x00, 0x00, 0x20, 0x20, 0x00, 0x00, 0x00, 0x80, 0x40, 0x01, 0x20, 0x40, 0x01, 0x00, 0x00, 0x24, 0x8C, 0x82, 0x01, 0x00, 0x00, 0xB2, 0x21, 0x06, 0xE4, 0xC4, 0xC8, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA3, 0x91, 0x5A, 0x01 };

            ////const String test = @"accounts\alex2069\SwordMan.hg1";
            ////byte[] charBytes = File.ReadAllBytes(test);
            //CharacterFile saveFile = new CharacterFile("asdf");
            //UnitObject goldDrop = new UnitObject(true);
            //try
            //{
            //    goldDrop.ParseUnitObject(goldDropBytes);
            //    // saveFile.ParseFileBytes(charBytes, true);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //    //int bp = 0;
            //}




            //TestScripts.ExtractFunctionList();


            //const String test = @"data\background\city\bldg_c_station_warp_next_layout.xml.cooked";
            //String dir = Path.GetDirectoryName(test) + "\\";
            //String name = Path.GetFileName(test);

            //uint dirHash = Crypt.GetStringSHA1UInt32(dir);
            //uint nameHash = Crypt.GetStringSHA1UInt32(name);

            //const String hellpackTablePath = @"D:\Games\Hellgate\Data\hgl.hpt";
            //byte[] hellpackFileBytes = File.ReadAllBytes(hellpackTablePath);
            //HellgatePackFile test = new HellgatePackFile(hellpackTablePath, hellpackFileBytes);



            //FileManager fileManager = new FileManager(Config.HglDir);
            //fileManager.ExtractAllExcel();

            //FileManager fileManagerTCv4 = new FileManager(Config.HglDir, true);
            //fileManagerTCv4.LoadTableFiles();
            //ExcelScript.GenerateExcelScriptFunctions(fileManagerTCv4);
            //ExcelScript.SetStaticFileManager(fileManagerTCv4);
            //fileManagerTCv4.ExtractAllExcel(@"C:\excel_tcv4\", true);
            //TestScripts.ExtractAllCSV();

            //return;
            //TestScripts.RepackMPDats();
            //TestScripts.CheckIdenticalFieldsToTCv4();
            //TestScripts.TestExcelCooking(true);
            //TestScripts.TestAllCodeValues();

            //return;

            //TestScripts.ExtractFunctionList();
            //TestScripts.ExcelValuesDeepScan();
            //TestScripts.DoCookTest();
            //TestScripts.ConvertTCv4ExcelToSP();
            //TestScripts.LoadAllMLIFiles();
            //TestScripts.LoadAllRooms();
            //TestScripts.LoadAllLevelRules();
            //TestScripts.UncookAllXml();
            //return;

            //LevelRulesEditor levelRulesEditor = new LevelRulesEditor(@"D:\Games\Hellgate London\data\background\city\rule_pmt02.drl")
            //{
            //    MdiParent = this
            //};
            //levelRulesEditor.Show(););
            //Process.GetCurrentProcess().Kill();
            //Environment.FailFast("asdf");
            //return;

            //
            //return;


            //const String hashStr1 = @"data\background\catacombs\";
            //const String hashStr2 = "ct_connb_path.xml.cooked";
            //byte[] data1 = FileTools.StringToASCIIByteArray(hashStr1);
            //byte[] data2 = FileTools.StringToASCIIByteArray(hashStr2);

            //SHA1 sha = new SHA1CryptoServiceProvider();
            //byte[] result1 = sha.ComputeHash(data1);
            //byte[] result2 = sha.ComputeHash(data2);

            //byte[] cryptoBytes = Crypt.GetStringsSHA1Bytes(hashStr1, hashStr2);
            //UInt64 cryptoValue = Crypt.GetStringsSHA1UInt64(hashStr1, hashStr2);


            //const String filePath = @"D:\Games\Hellgate London\MP_x64\hellgate_mp_dx9_x64.txt";
            //String[] strings = File.ReadAllLines(filePath);
            //foreach (String str in strings)
            //{
            //    if (str.Length <= 37) continue;

            //    String subStr = str.Substring(37);
            //    if (subStr == "pszSpineSidesTop")
            //    {
            //        int bp = 0;
            //    }
            //}

            #endregion
        }

        private void _OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = (!(String.IsNullOrEmpty(Config.LastDirectory))) ? Config.LastDirectory : Config.HglDataDir,
                Filter = "Hellgate London Files (*.*)|*.idx;*.hpt;*.txt.cooked;*.xls.uni.cooked;*.xml.cooked;*.hg1|" +
                         "Index Files|*.idx|" +
                         "Hellgate Pack Files|*.hpt|" +
                         "Excel Files|*.txt.cooked|" +
                         "String Files|*.xls.uni.cooked|" +
                         "XML Files|*.xml.cooked|" +
                         "Save Files|*.hg1"
            };

            if ((openFileDialog.ShowDialog(this) != DialogResult.OK)) return;

            string fileName = openFileDialog.FileName;
            Config.LastDirectory = Path.GetDirectoryName(fileName);

            if (fileName.EndsWith(IndexFile.Extension) || fileName.EndsWith(HellgatePackFile.Extension))
            {
                _OpenIndexFile(fileName);
                return;
            }

            if ((fileName.EndsWith(".txt.cooked")))
            {
                OpenExcelFile(fileName);
                return;
            }

            if ((fileName.EndsWith(".xls.uni.cooked")))
            {
                // Open String File
                return;
            }

            if ((fileName.EndsWith(".xml.cooked")))
            {
                // Open Xml File
                return;
            }

            if ((fileName.EndsWith(".hg1")))
            {
                // Open Save File
                return;
            }
        }

        /// <summary>
        /// Opens a TableForm based on the path to a Index or StringsFile.
        /// </summary>
        /// <param name="filePath">Path to the Index or StringsFile.</param>
        private void _OpenIndexFile(String filePath)
        {
            TableForm tableForm;
            PackFile packFile;
            if (filePath.EndsWith(IndexFile.Extension))
            {
                packFile = new IndexFile(filePath);
            }
            else
            {
                packFile = new HellgatePackFile(filePath);
            }



            // Check if the form is already open.
            // If true, then activate the form.
            bool isOpen = _openTableForms.Where(tf => tf.FilePath == filePath).Any();
            if (isOpen)
            {
                tableForm = _openTableForms.Where(tf => tf.FilePath == filePath).First();
                if (tableForm.Created)
                {
                    tableForm.Select();
                    return;
                }
            }

            // Try read the file.
            // If an exception is caught, log the error and inform the user.
            byte[] buffer;
            try
            {
                buffer = File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, false);
                return;
            }

            // parse file
            try
            {
                packFile.ParseFileBytes(buffer);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, false);
                return;
            }

            tableForm = new TableForm(packFile)
            {
                MdiParent = this,
                Text = Text + ": " + packFile.Path
            };
            if (!_openTableForms.Contains(tableForm)) _openTableForms.Add(tableForm);
            tableForm.Show();
        }

        /// <summary>
        /// Opens a ExcelTableForm based on the given path of a Excel/Strings file.
        /// </summary>
        /// <param name="filePath">Path to the file to open.</param>
        private void OpenExcelFile(String filePath)
        {
            //byte[] buffer;
            //Hellgate.ExcelFile excelFile;
            //ExcelTableForm excelTableForm;

            //// Check if the form is already open.
            //// If true, then activate the form.
            //bool isOpen = _openExcelTableForms.Where(etf => etf.FilePath == filePath).Any();
            //if (isOpen)
            //{
            //    excelTableForm = _openExcelTableForms.Where(etf => etf.FilePath == filePath).First();
            //    if ((excelTableForm.Created))
            //    {
            //        excelTableForm.Select();
            //        return;
            //    }
            //}

            //// Try read the file.
            //// If an exception is caught, log the error and inform the user.
            //try
            //{
            //    buffer = File.ReadAllBytes(filePath);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.LogException(ex, false);
            //    return;
            //}

            //// Initialize the ExcelFile.
            //excelFile = new Hellgate.ExcelFile(buffer)
            //{
            //    FilePath = filePath
            //};

            //// If the Excel file is initialized without error, load the form.
            //// Otherwise, show a message box.
            //if ((excelFile.IntegrityCheck == true))
            //{
            //    excelTableForm = new ExcelTableForm(excelFile)
            //    {
            //        MdiParent = this
            //    };
            //    if (!(_openExcelTableForms.Contains(excelTableForm)))
            //        _openExcelTableForms.Add(excelTableForm);
            //    excelTableForm.Show();
            //}
            //else
            //{
            //    string message = String.Format("The excel file {0} appears invalid or malformed.", filePath);
            //    string caption = "Bad File Format";
            //    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        private void _OpenXmlFile(String filePath)
        {
            byte[] xmlCookedBytes;
            try
            {
                xmlCookedBytes = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to read in file!\n\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlCookedFile xmlCookedFile = new XmlCookedFile(_fileManager);
            try
            {
                xmlCookedFile.ParseFileBytes(xmlCookedBytes, true);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to uncook xml file!\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String xmlPath = filePath.Replace(".cooked", "");
            try
            {
                xmlCookedFile.SaveXmlDocument(xmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save uncooked xml file!\n\n" + ex, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process process = new Process { StartInfo = { FileName = Config.XmlEditor, Arguments = xmlPath } };
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start default XML Editor.\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void _OpenFileHg1(String fileName)
        //{
        //    try
        //    {
        //        Unit heroUnit = UnitHelpFunctions.OpenCharacterFile(_tableDataSet, fileName);

        //        //Unit wrapper test
        //        //UnitWrapper wrapper = new UnitWrapper(heroUnit);
        //        //wrapper.Mode.IsElite = true;
        //        ////wrapper.Mode.IsElite = true;
        //        ////UnitWrapper w = new UnitWrapper(wrapper.Items.Items[2]);

        //        ////UnitWrapper drone = new UnitWrapper(wrapper.Drone.Drone);
        //        ////CharacterValues values = drone.Values;

        //        //UnitHelpFunctions.SaveCharacterFile(heroUnit, @"F:\test.hg1");

        //        //Comment me when testing the unit wrapper!!!

        //        HeroEditor2 heroEditor = new HeroEditor2(fileName, _tableDataSet)
        //        {
        //            Text = "Hero Editor: " + fileName,
        //            MdiParent = this
        //        };
        //        heroEditor.Show();
        //        //if (heroUnit.IsGood)
        //        //{
        //        //    HeroEditor heroEditor = new HeroEditor(heroUnit, _tableDataSet, fileName)
        //        //    {
        //        //        Text = "Hero Editor: " + fileName,
        //        //        MdiParent = this
        //        //    };
        //        //    heroEditor.Show();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogger.LogException(ex, "OpenFileHg1", false);
        //    }
        //}


        //private void _OpenFileStrings(String fileName)
        //{
        //    // todo: make me neater etc - i.e. merge with cooked file above
        //    // copy-paste for most part
        //    try
        //    {
        //        String name = Path.GetFileNameWithoutExtension(fileName);


        //        // todo: this doesn't work 100% as string IDs are stored with each first letter capitalized
        //        DataFile excelTable = _tableFiles.GetTableFromFileName(name);
        //        // todo: Add check for file differing from what's in dataset, and open as new file if different etc
        //        if (excelTable == null)
        //        {
        //            MessageBox.Show("TODO");
        //            return;
        //        }

        //        ExcelTableForm excelTableForm = new ExcelTableForm(excelTable, _tableDataSet)
        //        {
        //            Text = "Excel Table: " + fileName,
        //            MdiParent = this
        //        };
        //        excelTableForm.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogger.LogException(ex, "OpenFileCooked", false);
        //    }
        //}

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Doesn't appear to do anything...
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                };

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, "SaveAsToolStripMenuItem_Click");
            }
             */
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name != "ExcelTablesLoaded")
                {
                    childForm.Close();
                }
            }
        }

        private void _ClientPatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "EXE Files (*.exe)|*.exe|All Files (*.*)|*.*",
                InitialDirectory = Config.HglDir + "\\SP_x64"
            };
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) return;

            FileStream clientFile;
            try
            {
                clientFile = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                return;
            }

            Patches clientPatcher = new Patches(FileTools.StreamToByteArray(clientFile));
            if (clientPatcher.ApplyHardcorePatch())
            {
                FileStream fileOut = new FileStream(openFileDialog.FileName + ".patched.exe", FileMode.Create);
                fileOut.Write(clientPatcher.Buffer, 0, clientPatcher.Buffer.Length);
                fileOut.Dispose();
                MessageBox.Show("Hardcore patch applied!");
            }
            else
            {
                MessageBox.Show("Failed to apply Hardcore patch!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            clientFile.Dispose();
        }

        private void _ReanimatorResizeEnd(object sender, EventArgs e)
        {
            Config.ClientHeight = Height;
            Config.ClientWidth = Width;
        }

        private void _ReanimatorLoad(object sender, EventArgs e)
        {
            try
            {
                Height = Config.ClientHeight;
                Width = Config.ClientWidth;
                Show();
                Refresh();

                if (_CheckInstallation())
                {
                    ProgressForm progressForm = new ProgressForm(_DoLoadingThread, null);
                    progressForm.SetStyle(ProgressBarStyle.Marquee);
                    progressForm.SetLoadingText("Initializing Reanimator subsystems...");
                    progressForm.Disposed += delegate
                    {
                        _OpenExcelTableEditor();
                        _OpenExcelTableEditorTCv4();

                        if (Config.ShowFileExplorer) _OpenFileExplorer();
                    };
                    progressForm.Show(this);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show(ex.Message, "ReanimatorLoad");
            }
        }

        private void _OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            Options options = new Options(_fileManager);
            options.ShowDialog(this);
        }

        private static void _AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("Reanimator by the Revival Team (c) 2009-2010" + Environment.NewLine
                + "Credits: Maeyan, Alex2069, Kite & Malachor" + Environment.NewLine
                + "For more info visit us at: http://www.hellgateaus.net", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void _FileExplorerToolStripMenuItemClick(object sender, EventArgs e)
        {
            _OpenFileExplorer();
        }

        private void _ExcelTableEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            _OpenExcelTableEditor();
            _OpenExcelTableEditorTCv4();
        }

        private void _SaveToolStripButton_Click(object sender, EventArgs e)
        {
            IMdiChildBase mdiChildBase = ActiveMdiChild as IMdiChildBase;
            if (mdiChildBase == null) return;

            mdiChildBase.SaveAs();
        }

        private void _HardcoreModex64DX9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Exectuable Files (*.exe)|*.exe|All Files (*.*)|*.*",
                InitialDirectory = Config.HglDir
            };
            if (openFileDialog.ShowDialog(this) != DialogResult.OK || !openFileDialog.FileName.EndsWith("exe")) return;


            Patches hglexe = new Patches(File.ReadAllBytes(openFileDialog.FileName));
            try
            {
                hglexe.ApplyHardcorePatch();
                File.WriteAllBytes(openFileDialog.FileName.Insert(openFileDialog.FileName.Length - 4, "-patched"),
                                   hglexe.Buffer);
                MessageBox.Show("Patch successfully applied!");
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex, false);
                MessageBox.Show("Problem Applying Patch. :(");
            }
        }

        private void _ApplyModificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ModificationForm modificationForm = new ModificationForm(_tableDataSet);
            //    modificationForm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.LogException(ex, "_ApplyModificationsToolStripMenuItem_Click", false);
            //}
        }

        private void _TradeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //MessageBox.Show("Tables are being loaded. This may take a few seconds!");

            //    //ItemTransferForm transfer = new ItemTransferForm(_tableDataSet, _tableFiles);
            //    ItemTransferForm transfer = new ItemTransferForm(_tableDataSet, _fileExplorer);
            //    //Displays a warning message before opening the item trading window.
            //    transfer.DisplayWarningMessage(null, null);
            //    transfer.ShowDialog(this);
            //    transfer.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.LogException(ex, "_TradeItemsToolStripMenuItem_Click", false);
            //}
        }

        private void _SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void _ItemShopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CharacterShop shop = new CharacterShop(_tableDataSet, _tableFiles);
            //    shop.ShowDialog(this);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.LogException(ex, "_ItemShopToolStripMenuItem_Click", false);
            //}
        }

        private void _SearchTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    TableSearch search = new TableSearch(_tableDataSet, _tableFiles);
            //    search.ShowDialog(this);
            //}
            //catch (Exception ex)
            //{
            //    ExceptionLogger.LogException(ex, "_SearchTablesToolStripMenuItem_Click", false);
            //}
        }

        private void _PatchToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PatchForm patchForm = new PatchForm { MdiParent = this };
                patchForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
            }
        }

        private void _SaveSinglePlayerFiles(ProgressForm progress, object obj)
        {
            //    string savePath = (string)obj;

            //    foreach (DataTable spDataTable in _tableDataSet.XlsDataSet.Tables)
            //    {
            //        if (spDataTable.TableName.Contains("_TCv4_")) continue;
            //        if (spDataTable.TableName.Contains("Strings_")) continue;

            //        progress.SetCurrentItemText("Current table... " + spDataTable.TableName);

            //        ExcelFile spExcelFile = _tableDataSet.TableFiles.GetExcelTableFromId(spDataTable.TableName);

            //        byte[] buffer = spExcelFile.GenerateFile(spDataTable);
            //        string path = Path.Combine(savePath, spExcelFile.FilePath);
            //        string filename = spExcelFile.FileName + "." + spExcelFile.FileExtension;

            //        if (!Directory.Exists(path))
            //        {
            //            Directory.CreateDirectory(path);
            //        }

            //        File.WriteAllBytes(path + filename, buffer);
            //    }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMdiChildBase mdiChildBase = ActiveMdiChild as IMdiChildBase;
            if (mdiChildBase == null) return;
            mdiChildBase.Import();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IMdiChildBase mdiChildBase = ActiveMdiChild as IMdiChildBase;
            if (mdiChildBase == null) return;
            mdiChildBase.Export();
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void dropTableOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DropOverview drops = new DropOverview(_fileManager);
            drops.Show(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemQualityCalculator calculator = new ItemQualityCalculator(_fileManager);
            calculator.Show(this);
        }

        private void savegameOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //pre-load the tables to prevent additional waiting times when accessing certain information later on
            _fileManager.PreLoadTable("ITEMS");
            _fileManager.PreLoadTable("PLAYERS");
            _fileManager.PreLoadTable("SKILLS");
            _fileManager.PreLoadTable("STATS");
            _fileManager.PreLoadTable("LEVEL");

            string[] savegames = Directory.GetFiles(Config.SaveDir, "*.hg1");

            SavegameOverviewForm savegameForm = new SavegameOverviewForm();

            foreach (string savegame in savegames)
            {
                UnitWrapper2 wrapper = new UnitWrapper2(savegame, _fileManager);
                savegameForm.AddCharacter(wrapper);
            }

            savegameForm.Show(this);
        }

        private void tradeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////pre-load the tables to prevent additional waiting times when accessing certain information later on
            _fileManager.PreLoadTable("AFFIXES");
            _fileManager.PreLoadTable("ITEMS");
            _fileManager.PreLoadTable("STATS");
            _fileManager.PreLoadTable("Strings_Affix");
            _fileManager.PreLoadTable("GAME_GLOBALS");

            ItemTransfer.ItemTransferForm itemTransfer = new ItemTransfer.ItemTransferForm(_fileManager);
            itemTransfer.DisplayWarningMessage(null, null);
            itemTransfer.Show(this);
        }

        private void animationTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnimationTestForm test = new AnimationTestForm(_fileManager);
            test.Show(this);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomControls.HGLForm form = new CustomControls.HGLForm(_fileManager);
            form.Show(this);
        }

        //private void saveSinglePlayerFilesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    // Select Dump location
        //    FolderBrowserDialog folderBrower = new FolderBrowserDialog();
        //    folderBrower.SelectedPath = Config.HglDataDir;
        //    DialogResult dialogResult = folderBrower.ShowDialog();
        //    if (dialogResult == DialogResult.Cancel) return;

        //    // cache all tables
        //    ProgressForm cacheTableProgress = new ProgressForm(_LoadAllExcelTables, null);
        //    cacheTableProgress.SetLoadingText("Caching all tables.");
        //    cacheTableProgress.ShowDialog();

        //    // generate all tables
        //    ProgressForm generateTableProgress = new ProgressForm(_SaveSinglePlayerFiles, folderBrower.SelectedPath);
        //    generateTableProgress.SetLoadingText("Saving all single player files, this will take a while.");
        //    generateTableProgress.SetStyle(ProgressBarStyle.Marquee);
        //    generateTableProgress.ShowDialog();

        //    MessageBox.Show("Complete");
        //}
    }
}