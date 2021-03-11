using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RecipesCombine
    {
        ExcelFile.RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string type;
        public Int32 code;
        public Int32 priority;
        public ChoiceCheck choiceCheck;
        public SpawnLevelType spawnLevelType;
        public Int32 identified;//bool
        [ExcelOutput(IsTableIndex = true, TableStringId = "TREASURE")]//table 69h
        public Int32 treasureResult;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient1UnitType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
        public Int32 ingredient1ItemQuality;
        public Int32 ingredient1ItemCount;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient2UnitType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
        public Int32 ingredient2ItemQuality;
        public Int32 ingredient2ItemCount;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient3UnitType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
        public Int32 ingredient3ItemQuality;
        public Int32 ingredient3ItemCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public Int32[] undefined1;

        public enum ChoiceCheck
        {
            Null = -1,
            Same = 0,
            Ingredient = 1
        }
        public enum SpawnLevelType
        {
            Avg = 0,
            Min = 1,
            Max = 2,
            Owner = 3
        }
    }
}
