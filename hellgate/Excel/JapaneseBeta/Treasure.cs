using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel.JapaneseBeta
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class TreasureBeta
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string treasureClass;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public Int32[] allowUnitTypes;
        [ExcelOutput(IsTableIndex = true, TableStringId = "GLOBAL_THEMES")]
        public Int32 globalThemeRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "GLOBAL_THEMES")]
        public Int32 globalThemeProbBoostTheme;
		public Int32 globalThemeBoostPct;
        Int32 unknown02;
        public PickTypes pickType; // XLS_ReadInternalIndex_PickType (XLS_TREASURE_DATA+1D5), 0x08
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public Int32[] picks;
        public float noDrop;
		public Int32 mustDrop;
        [ExcelOutput(IsScript = true)]
        public Int32 levelBoost;
        public float gamblePriceRangeMin;
        public float gamblePriceRangeMax;
        public float moneyChanceMultiplier;
        public float moneyLuckChanceMultiplier;
        public float moneyAmountMultiplier;

        /* first index is type, which determines whether next index is a specific item(01), a unit type(02),
         * another treasure class(03), an item quality(04), or something else yet to be determined */

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item1; // is 0x58 (22x Int32; 21x item, 1x value) in length - is multiple relational index like SPAWN_CLASS
        public Int32 value1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item2;
        public Int32 value2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item3;
        public Int32 value3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item4;
        public Int32 value4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item5;
        public Int32 value5;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item6;
        public Int32 value6;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item7;
        public Int32 value7;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public Int32[] item8;
        public Int32 value8;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8804)]
        byte[] unknown13;
        [ExcelOutput(IsBitmask = true)]
        public Bitmask01 spawnCondition;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 spawnFromMonsterUnitType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "LEVEL_THEMES")]
        public Int32 spawnFromLevelTheme;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction1;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction2;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction3;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction4;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction5;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table C8
        public Int32 gameModeRestriction6;

        [FlagsAttribute]
        public enum Bitmask01 : uint
        {
            CreateForAllPlayersInLevel = (1 << 0),
            RequiredUsableByOperator = (1 << 1),
            RequiredUsableBySpawner = (1 << 2),
            SubscriberOnly = (1 << 3),
            MaxSlots = (1 << 4),
            ResultsNotRequired = (1 << 5),
            StackTreasure = (1 << 6),
            MultiplayerOnly = (1 << 7),
            SinglePlayerOnly = (1 << 8),
            baseOnPlayerLevel = (1 << 9)
        }

        public enum PickTypes
        {
            Null = -1,
            one = 0,
            all = 1,
            modifiers_only = 2,
            ind_percent = 3,
            one_eliminate = 4,
            first_valid = 5
        }
    }
}