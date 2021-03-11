using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Fatigue
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 penaltyState;
        public Int32 firstMessageInMinutes;
        public Int32 lastMessageInMinutes;
        public Int32 messageRepeatTimeInMinutes;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 message;
    }
}
