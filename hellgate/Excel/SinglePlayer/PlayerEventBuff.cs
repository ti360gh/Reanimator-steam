using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class PlayerEventBuff
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        public Int32 activate;
        public Int32 startMonth;
        public Int32 startDay;
        public Int32 endMonth;
        public Int32 endDay;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 eventState;
        public Int32 eventStartHour;
        public Int32 eventEndHour;
    }
}
