using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class PvpRanks
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string rankName;
        public Int32 code;
        public Int32 pvpExpMin;
        public Int32 maxPercentile;
        public Int32 maxRankPlayer;
        [ExcelOutput(IsBool = true)]
        public Int32 minusExpEnable;//bool
        [ExcelOutput(IsStringIndex = true)]
        public Int32 characterSheetString;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string iconTextureName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string characterSheetIcon;
    }
}
