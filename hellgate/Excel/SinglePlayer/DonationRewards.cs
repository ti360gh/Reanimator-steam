using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DonationRewards
    {
        ExcelFile.RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string donationRewardName;
        public Int32 code;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 item;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 doNotApplyIfPlayerHasRewardItemFor;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 state;
        public Int32 durationInTicks;
        public Int32 weight;
    }
}
