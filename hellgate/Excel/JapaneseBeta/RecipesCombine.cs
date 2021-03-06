using System;
using System.Runtime.InteropServices;

namespace Hellgate.Excel.JapaneseBeta
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RecipesCombineBeta
    {
        ExcelFile.RowHeader header;
        [ExcelFile.OutputAttribute(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string type;
        public Int32 code;
        public Int32 priority;
        public ChoiceCheck choiceCheck;
        public SpawnLevelType spawnLevelType;
        public Int32 identified;//bool
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "TREASURE")]//table 69h
        public Int32 treasureResult;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient1UnitType;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
        public Int32 ingredient1ItemQuality;
        public Int32 ingredient1ItemCount;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient2UnitType;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
        public Int32 ingredient2ItemQuality;
        public Int32 ingredient2ItemCount;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "UNITTYPES")]//table 17h
        public Int32 ingredient3UnitType;
        [ExcelFile.OutputAttribute(IsTableIndex = true, TableStringId = "ITEMQUALITY")]//table 45h
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
