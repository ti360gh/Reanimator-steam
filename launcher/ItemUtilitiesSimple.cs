﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Reanimator.Excel;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace Reanimator.Forms
{
    public class UnitHelpFunctionsSimple
    {
        TableDataSet _dataSet;
        DataTable _itemNames;
        List<ExcelTable> _excelTables;

        public UnitHelpFunctionsSimple(ref TableDataSet dataSet)
        {
            _dataSet = dataSet;
            _itemNames = _dataSet.XlsDataSet.Tables[0];
            _excelTables = new List<ExcelTable>();

            string excelTableFolder = Config.DataDirsRoot + "\\data_common\\excel\\";

            _excelTables.Add(LoadSpecificTable(excelTableFolder, "items", "ITEMS"));
        }

        public void LoadSimpleCharacterValues(Unit unit)
        {
            GenerateSimpleUnitNameStrings(unit.Items.ToArray());
        }

        private void GenerateSimpleUnitNameStrings(Unit[] units)
        {
                         //DataRow[] row = _itemNames.Select(string.Format("ReferenceId LIKE {0}", unit.unitCode));
            try
            {
                foreach (Unit unit in units)
                {
                    DataView view = new DataView(_itemNames);

                    foreach (DataColumn column in _itemNames.Columns)
                    {
                        view.RowFilter = string.Format("Convert({0}, 'System.String') LIKE '{1}'", "ReferenceId", unit.unitCode);
                        view.Sort = column.ColumnName;

                        if (view.Count > 0)
                        {
                            unit.Name = view[0].Row[0].ToString();
                            continue;
                        }
                    }
                    //GenerateSimpleUnitNameStrings(unit.Items.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GenerateUnitNameStrings");
            }
        }

        public string MapIdToString(Unit.StatBlock.Stat stat, int tableId, int lookupId)
        {
            string value = string.Empty;

            if (stat.values.Length != 0)
            {
                String select = String.Format("code = '{0}'", lookupId);
                DataTable table = _dataSet.GetExcelTable(tableId);
                DataRow[] row;

                if (table != null)
                {
                    row = table.Select(select);

                    if (row != null && row.Length != 0)
                    {
                        value = (string)row[0][1];
                    }
                }
            }

            return value;
        }

        public static Unit OpenCharacterFile(string fileName)
        {
            Unit unit = null;

            FileStream heroFile;
            try
            {
                heroFile = new FileStream(fileName, FileMode.Open);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to open file: " + fileName + "\n\n" + e, "OpenCharacterFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            BitBuffer bitBuffer = new BitBuffer(FileTools.StreamToByteArray(heroFile)) { DataByteOffset = 0x2028 };

            unit = new Unit(bitBuffer);
            unit.ParseUnit();

            heroFile.Close();

            return unit;
        }

        private void PopulateItems(Unit unit)
        {
            bool canGetItemNames = true;
            DataTable itemsTable = _dataSet.GetExcelTable(27953);
            DataTable affixTable = _dataSet.GetExcelTable(30512);
            if (itemsTable != null && affixTable != null)
            {
                if (!itemsTable.Columns.Contains("code1") || !itemsTable.Columns.Contains("String_string"))
                    canGetItemNames = false;
                if (!affixTable.Columns.Contains("code") || !affixTable.Columns.Contains("setNameString_string") ||
                    !affixTable.Columns.Contains("magicNameString_string"))
                    canGetItemNames = false;
            }
            else
            {
                canGetItemNames = false;
            }


            List<Unit> items = unit.Items;
            for (int i = 0; i < items.Count; i++)
            {
                Unit item = items[i];
                if (item == null) continue;


                // assign default name
                item.Name = "Item Id: " + item.unitCode;
                if (!canGetItemNames)
                {
                    continue;
                }
                if (item.unitCode == 25393)
                {
                    //string a;
                }


                // get item name
                DataRow[] itemsRows = itemsTable.Select(String.Format("code1 = '{0}'", item.unitCode));
                if (itemsRows.Length == 0)
                {
                    continue;
                }
                item.Name = itemsRows[0]["String_string"] as String;


                // does it have an affix/prefix
                String affixString = String.Empty;
                for (int s = 0; s < item.Stats.Length; s++)
                {
                    // "applied_affix"
                    if (item.Stats[s].Id == 0x7438)
                    {
                        int affixCode = item.Stats[s].values[0].Stat;
                        DataRow[] affixRows = affixTable.Select(String.Format("code = '{0}'", affixCode));
                        if (affixRows.Length > 0)
                        {
                            String replaceString = affixRows[0]["setNameString_string"] as String;
                            if (String.IsNullOrEmpty(replaceString))
                            {
                                replaceString = affixRows[0]["magicNameString_string"] as String;
                                if (String.IsNullOrEmpty(replaceString))
                                {
                                    break;
                                }
                            }

                            affixString = replaceString;
                        }
                    }

                    // "item_quality"
                    if (item.Stats[s].Id == 0x7832)
                    {
                        // is unique || is mutant then no affix
                        int itemQualityCode = item.Stats[s].values[0].Stat;
                        if (itemQualityCode == 13616 || itemQualityCode == 13360)
                        {
                            affixString = String.Empty;
                            break;
                        }
                    }
                }

                if (affixString.Length > 0)
                {
                    item.Name = affixString.Replace("[item]", item.Name);
                }

                if (item.Items.Count > 0)
                {
                    PopulateItems(item);
                }
            }
        }

        public static int GetSimpleValue(Unit unit, int valueId)
        {
            for (int counter = 0; counter < unit.Stats.Length; counter++)
            {
                Unit.StatBlock.Stat unitStats = unit.Stats[counter];

                if (unitStats.Id == valueId)
                {
                    return unitStats.values[0].Stat;
                }
            }
            //MessageBox.Show("Field \"" + valueName + "\" not present in unit " + unit.Name + "!");
            return 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct MainHeader
        {
            public Int32 Flag;
            public Int32 Version;
            public Int32 DataOffset1;
            public Int32 DataOffset2;
        };

        public static void SaveCharacterFile(Unit unit, string filePath)
        {
            DialogResult dr = DialogResult.Retry;
            while (dr == DialogResult.Retry)
            {
                try
                {
                    using (FileStream saveFile = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        // main header
                        MainHeader mainHeader;
                        mainHeader.Flag = 0x484D4752; // "RGMH"
                        mainHeader.Version = 1;
                        mainHeader.DataOffset1 = 0x2028;
                        mainHeader.DataOffset2 = 0x2028;
                        byte[] data = FileTools.StructureToByteArray(mainHeader);
                        saveFile.Write(data, 0, data.Length);

                        // hellgate string (is this needed?)
                        const string hellgateString = "Hellgate: London";
                        byte[] hellgateStringBytes = FileTools.StringToUnicodeByteArray(hellgateString);
                        saveFile.Seek(0x28, SeekOrigin.Begin);
                        saveFile.Write(hellgateStringBytes, 0, hellgateStringBytes.Length);

                        // char name (not actually used in game though I don't think)  (is this needed?)
                        string charString = unit.Name;
                        byte[] charStringBytes = FileTools.StringToUnicodeByteArray(charString);
                        saveFile.Seek(0x828, SeekOrigin.Begin);
                        saveFile.Write(charStringBytes, 0, charStringBytes.Length);

                        // no detail string (is this needed?)
                        const string noDetailString = "No detail";
                        byte[] noDetailStringBytes = FileTools.StringToUnicodeByteArray(noDetailString);
                        saveFile.Seek(0x1028, SeekOrigin.Begin);
                        saveFile.Write(noDetailStringBytes, 0, noDetailStringBytes.Length);

                        // load char string (is this needed?)
                        const string loadCharacterString = "Load this Character";
                        byte[] loadCharacterStringBytes = FileTools.StringToUnicodeByteArray(loadCharacterString);
                        saveFile.Seek(0x1828, SeekOrigin.Begin);
                        saveFile.Write(loadCharacterStringBytes, 0, loadCharacterStringBytes.Length);

                        // main character data
                        saveFile.Seek(0x2028, SeekOrigin.Begin);
                        byte[] saveData = unit.GenerateSaveData(charStringBytes);
                        saveFile.Write(saveData, 0, saveData.Length);
                    }

                    MessageBox.Show("Character saved successfully!", "Saved", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    break;
                }
                catch (Exception e)
                {
                    dr = MessageBox.Show("Failed to save character file!Try again?\n\n" + e, "Error",
                                         MessageBoxButtons.RetryCancel);
                }
            }
        }

        public ExcelTable LoadSpecificTable(String folder, string fileName, string stringId)
        {
            String filePath = String.Format("{0}\\{1}.txt.cooked", folder, fileName);

            if (String.IsNullOrEmpty(filePath)) return null;

            if (!File.Exists(filePath))
            {
                filePath = filePath.Replace("_common", string.Empty);
                if (!File.Exists(filePath)) return null;
            }

            // parse file
            try
            {
                using (FileStream cookedFile = new FileStream(filePath, FileMode.Open))
                {
                    byte[] buffer = FileTools.StreamToByteArray(cookedFile);

                    ExcelTable excelTable = new ExcelTables.ExcelTableManagerManager().CreateTable(stringId, buffer);
                    if (excelTable != null)
                    {
                        if (!excelTable.IsNull)
                        {
                            return excelTable;
                        }
                    }
                    MessageBox.Show(string.Format("Error parsing the file {0}!", filePath));
                }
            }
            catch (ExcelTableException e)
            {
                MessageBox.Show("Unexpected parsing error!\n\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BadHeaderFlag e)
            {
                MessageBox.Show("File data tokens not aligned!\n\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to open file for reading!\n\n" + filePath + "\n\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
    }

    //public enum ItemQuality
    //{
    //    Normal = 12336,
    //    NormalMod = 14384,

    //    Uncommon = 12592,

    //    Rare = 18480,
    //    RareMod = 18736,

    //    Legendary = 13104,
    //    LegendaryMod = 16944,

    //    Mutant = 13360,
    //    MutantMod = 17200,

    //    Unique = 13616,
    //    UniqueMod = 17456
    //};

    //public enum ItemValueNames
    //{
    //    accuracy = 18224,
    //    accuracy_feed = 16690,
    //    achievement_points_cur = 24900,
    //    achievement_points_total = 23108,
    //    achievement_progress = 22852,
    //    ai_change_attack = 20793,
    //    ai_change_duration = 24889,
    //    ai_last_attacker_id = 27186,
    //    applied_affix = 29752,
    //    armor = 18737,
    //    armor_buffer_cur = 19761,
    //    armor_buffer_max = 19505,
    //    armor_buffer_regen = 20273,
    //    attached_affix_hidden = 19512,
    //    attack_rating = 25392,
    //    badge_reward_received = 23107,
    //    base_dmg_max = 25904,
    //    base_dmg_min = 25648,
    //    critical_chance = 12849,
    //    critical_mult = 14385,
    //    cube_known_recipes = 12869,
    //    damage_increment = 27696,
    //    damage_increment_field = 17473,
    //    damage_increment_radial = 17729,
    //    damage_max = 27440,
    //    damage_min = 27184,
    //    difficulty_max = 25667,
    //    energy_decrease_source = 27970,
    //    energy_increase_source = 29250,
    //    energy_max = 28226,
    //    experience = 13874,
    //    experience_next = 14386,
    //    experience_prev = 14130,
    //    faction_score = 13622,
    //    firing_error_decrease_source = 12610,
    //    firing_error_decrease_source_weapon = 12866,
    //    firing_error_increase_source = 12354,
    //    firing_error_increase_source_weapon = 30529,
    //    firing_error_max = 31297,
    //    firing_error_max_weapon = 31041,
    //    gold = 13618,
    //    hp_cur = 13360,
    //    hp_max = 13616,
    //    hp_regen = 14384,
    //    identified = 16951,
    //    interrupt_attack = 17713,
    //    inventory_height = 12595,
    //    inventory_width = 12339,
    //    item_augmented_count = 21315,
    //    item_difficulty_spawned = 17989,
    //    item_level_limit = 13123,
    //    item_level_req = 28739,
    //    item_look_group = 25401,
    //    item_pickedup = 12357,
    //    item_quality = 30770,
    //    item_quantity = 21300,
    //    item_quantity_max = 21044,
    //    item_slots = 26163,
    //    item_spawner_level = 28726,
    //    item_upgraded_count = 21059,
    //    last_trigger = 28982,
    //    level = 12336,
    //    level_def_return = 27700,
    //    level_def_start = 27444,
    //    level_req = 12848,
    //    minigame_category_needed = 26180,
    //    newest_headstone_unit_id = 27457,
    //    no_trade = 24898,
    //    no_tutorial_tips = 20548,
    //    offerid = 27703,
    //    offweapon_melee_speed = 29236,
    //    pet_damage_bonus = 22081,
    //    played_time_in_seconds = 26690,
    //    player_visited_level_bitfield = 21061,
    //    power_cost_pct_skillgroup = 17716,
    //    power_cur = 16944,
    //    power_max = 17200,
    //    power_regen = 17456,
    //    previous_weaponconfig = 17718,
    //    quest_global_fix_flags = 31044,
    //    quest_player_tracking = 24899,
    //    quest_reward = 14657,
    //    reward_original_location = 31286,
    //    save_quest_data_version = 22596,
    //    save_quest_hunt_version = 29497,
    //    save_quest_state_1 = 30260,
    //    save_quest_state_2 = 30516,
    //    save_quest_state_3 = 30772,
    //    save_quest_state_4 = 31028,
    //    save_quest_state_5 = 31284,
    //    save_quest_state_6 = 12341,
    //    save_quest_state_7 = 12597,
    //    save_quest_state_8 = 12853,
    //    save_quest_state_9 = 13109,
    //    save_quest_status = 17973,
    //    save_quest_version = 18229,
    //    save_task_count = 18741,
    //    save_task_version = 18485,
    //    sfx_attack = 28977,
    //    sfx_attack_focus = 29251,
    //    sfx_defense_bonus = 28721,
    //    sfx_duration_in_ticks = 30769,
    //    sfx_strength_pct = 12338,
    //    shield_buffer_cur = 25137,
    //    shield_buffer_max = 24881,
    //    shield_buffer_regen = 25649,
    //    shield_overload_pct = 26417,
    //    shields = 23089,
    //    skill_is_selected = 17204,
    //    skill_level = 21043,
    //    skill_points = 30004,
    //    skill_right = 20787,
    //    skill_shift_enabled = 28225,
    //    splash_increment = 30515,
    //    stamina = 19248,
    //    stamina_feed = 17202,
    //    stat_points = 14642,
    //    strength = 21808,
    //    strength_feed = 20274,
    //    unlimited_in_merchant_inventory = 20536,
    //    waypoint_flags = 20532,
    //    willpower = 19760,
    //    willpower_feed = 17458
    //};

    //public enum InventoryTypes
    //{
    //    Inventory = 19760,
    //    Stash = 28208,
    //    //SharedStash = 26160,
    //    QuestRewards = 26928,
    //    Cube = 22577,
    //    CurrentWeaponSet = 25904
    //};
}
