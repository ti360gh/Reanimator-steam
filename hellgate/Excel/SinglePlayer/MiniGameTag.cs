using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MiniGameTag
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MINIGAME_TYPE")]//table C3
        public Int32 miniGameType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "DAMAGETYPES")]//table 1E
        public Int32 goalDamageType;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 goalUnitType;
        public Int32 minNeeded;
        public Int32 maxNeeded;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string frameName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string achievedFrameName;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 toolTip;
    }
}
