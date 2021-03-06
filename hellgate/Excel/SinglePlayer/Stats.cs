using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Stats
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string stat;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
        public Type type;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 assocStat1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 assocStat2;
        public Int32 regenIntervalInMS;
        public Int32 regenDivisor;
        public Int32 regenDelayOnDec;
        public Int32 regenDelayOnZero;
        public Int32 regenDelayMonster;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 undefined1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 undefined2;
        public Int32 offset;
        public Int32 shift;
        public Int32 minSet;
        public Int32 maxSet;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 undefined3;
        public Int32 minAssert;
        public Int32 maxAssert;
        public Int32 undefined4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 accrueToTypes1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 accrueToTypes2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 unitType;
        [ExcelOutput(IsBitmask = true, DefaultBitmask = 0)]
        public BitMask01 bitmask01;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] undefined5;
        [ExcelOutput(IsBool = true)]
        public Int32 player;
        [ExcelOutput(IsBool = true)]
        public Int32 monster;
        [ExcelOutput(IsBool = true)]
        public Int32 missile;
        [ExcelOutput(IsBool = true)]
        public Int32 item;
        [ExcelOutput(IsBool = true)]
        public Int32 Object;
        public Int32 valbits;
        public Int32 valWindow;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] undefined6;
        public Int32 valShift;
        public Int32 valOffs;
        [ExcelOutput(IsTableIndex = true, TableStringId = "EXCELTABLES")]
        public Int32 valTable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        byte[] undefined7;
        public Int32 param1Bits;
        public Int32 param2Bits;
        public Int32 param3Bits;
        public Int32 param4Bits;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        byte[] undefined8;
        public Int32 param1Shift;
        public Int32 param2Shift;
        public Int32 param3Shift;
        public Int32 param4Shift;
        public Int32 param1Offs;
        public Int32 param2Offs;
        public Int32 param3Offs;
        public Int32 param4Offs;
        [ExcelOutput(IsTableIndex = true, TableStringId = "EXCELTABLES")]
        public Int32 param1Table;
        [ExcelOutput(IsTableIndex = true, TableStringId = "EXCELTABLES")]
        public Int32 param2Table;
        [ExcelOutput(IsTableIndex = true, TableStringId = "EXCELTABLES")]
        public Int32 param3Table;
        [ExcelOutput(IsTableIndex = true, TableStringId = "EXCELTABLES")]
        public Int32 param4Table;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        byte[] undefined9;
        public Int32 minTicksBetweenDbCommand;
        public DatabaseUnitField databaseUnitField;
        public SpecFunc specFunc;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 sfStat1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 sfStat2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 sfStat3;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]
        public Int32 sfStat4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        byte[] undefined10;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 checkAgainstTypes1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 checkAgainstTypes2;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 reqFailString;//stridx
        [ExcelOutput(IsStringIndex = true)]
        public Int32 reqFailStringHellgate;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 reqFailStringMythos;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Int32[] undefined11;
        public Int32 undefined11a;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string versionFunction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Int32[] undefined12;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATS")]//table 
        public Int32 param5Table;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public Int32[] undefined13;

        [FlagsAttribute]
        public enum BitMask01 : uint
        {
            _undefined1 = (1 << 0),
            cur = (1 << 1),
            modList = (1 << 2),
            vector = (1 << 3),
            Float = (1 << 4),
            _undefined2 = (1 << 5),
            accrue = (1 << 6),
            accrueOnceOnly = (1 << 7),
            combat = (1 << 8),
            directDmg = (1 << 9),
            send = (1 << 10),
            sendAll = (1 << 11),
            save = (1 << 12),
            _undefined3 = (1 << 13),
            noMaxCurWhenDead = (1 << 14),
            stateMonitorsC = (1 << 15),
            stateMonitorsS = (1 << 16),
            transfer = (1 << 17),
            transferToMissile = (1 << 18),
            calcRider = (1 << 19),
            calc = (1 << 20),
            updateDatabase = (1 << 21),
            dontTranferToNonWeaponMissile = (1 << 22)
        }

        public enum Type
        {
            None = 0,
            Regen = 1,
            Feed = 2,
            Req = 3,
            ReqLimit = 4,
            PctDegen = 5,
            Alloc = 6,
            Max = 7,
            MaxRatio = 8,
            MaxRatioDecrease = 9,
            MaxPure = 10,
            Degen = 11,
            PctRegen = 12,
            RegenClient = 13,
            DegenCLient = 14
        }
        public enum SpecFunc
        {
            Null = -1,
            None = 0,
            RegenOnGet = 1,
            PctRegenOnGet = 4
        }

        public enum DatabaseUnitField
        {
            Null = -1,
            None = 0,
            Experience = 1,
            Money = 2,
            Quantity = 3,
            InventoryLocation = 4,
            RankExp = 5
        }
    }
}
