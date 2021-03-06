using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel.JapaneseBeta
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class StatesBeta
    {
        RowHeader header;
        [ExcelOutput(IsStringOffset = true, SortColumnOrder = 1)]
        public Int32 name;
        Int32 buffer;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
        Int32 buffer1;              // always 0
        [ExcelOutput(IsStringOffset = true)]
        public Int32 file;
        Int32 buffer2;              // always 0
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA0;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA3;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA5;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA6;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA7;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA8;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 isA9;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 statePreventedBy;
        public Int32 duration;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 onDeath;
        public Int32 skillScriptParam;
        Int32 unknown18;            // always 0
        public Int32 element;
        [ExcelOutput(IsScript = true)]
        public Int32 pulseRateInMs;
        [ExcelOutput(IsScript = true)]
        public Int32 pulseRateInMsClient;            // always 0
        [ExcelOutput(IsStringOffset = true)]
        public Int32 pulseSkill;
        Int32 unknown23;            // always 0
        Int32 unknown24;            // always 0
        Int32 unknown25;            // always 0
        public Int32 iconOrder;            // always 0
        Int32 unknown27;            // always 0
        [ExcelOutput(IsStringOffset = true)]
        public Int32 uiIcon;
        Int32 unknown29;            // always 0
        [ExcelOutput(IsStringOffset = true)]
        public Int32 uiIconTexture;
        Int32 unknown31;            // always 0
        public Int32 uiconColor;     //not defined, even though it's used.
        Int32 unknown33;            // always 0
        [ExcelOutput(IsStringOffset = true)]
        public Int32 UiIconBack;     //undefined as well.
        Int32 unknown35;            // always 0
		Int32 unknown35a;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 unIconFront;
        [ExcelOutput(IsTableIndex = true, TableStringId = "FONTCOLORS")]
        public Int32 iconBackColor;
        Int32 unknown37;            // always 0
        Int32 unknown38;            // always 0
        [ExcelOutput(IsStringIndex = true)]
        public Int32 iconTooltipStringHellgate;//stridx
        [ExcelOutput(IsStringIndex = true)]
        public Int32 iconTooltipStringMythos;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 iconTooltipStringAll;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 assocState1;            // always -1
        //[ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 assocState2;            // always -1
        //[ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 assocState3;            // always -1
        [ExcelOutput(IsBitmask = true, DefaultBitmask = 0)]
        public BitMask01 bitmask01;
        public GameFlag gameFlag;
        [ExcelOutput(IsBitmask = true, DefaultBitmask = 0)]
        public BitMask02 bitmask02;
        Int32 unknown48;            // always 0
        Int32 unknown49;            // always 0
    }

    [FlagsAttribute]
    public enum BitMask01 : uint
    {
        executeAttackScriptMelee = 1,
        executeAttackScriptRanged = 2,
        executeSkillScriptOnRemove = 4,
        executeScriptOnSource = 8,
        pulseOnClientToo = 16
    }
    [FlagsAttribute]
    public enum BitMask02 : uint
    {
        stacks = 2,
        stacksPerSource = 4,
        sendToAll = 8,
        sendToSelf = 16,
        sendStats = 32,
        clientNeedsDuration = 64,
        clientOnly = 128,
        executeParentEvents = 256,
        triggerNotargetOnSet = 512,
        savePositionOnSet = 1024,
        saveWithUnit = 2048,
        flagForLoad = 4096,
        sharingModState = 8192,
        usedInHellgate = 16384,
        usedInTugboat = 32768,
        isBad = 65536,
        pulseOnSource = 131072,
        onChangeRepaintItemUi = 262144,
        saveInUnitfileHeader = 524288,
		persistTimerAcrossGames = 1048576,
        updateChatServerOnChange = 2097152,
		undefined = 4194304,
        triggerDigestSave = 8388608,
		isShared = 16777216,
		isRealTime = 33554432
    }
    public enum GameFlag
    {
        Null = -1,
        Hardcore = 0,
        Elite = 1,
        League = 2,
        PvpWorld = 3
    }
}
