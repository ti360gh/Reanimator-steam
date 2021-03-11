using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ChatInstancedChannels
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 28)]
        public string name;
        [ExcelOutput(IsBool = true)]
        public Int32 optOut;
        public Int32 maxMembers;
        [ExcelOutput(IsScript = true)]
        public Int32 code;
    }
}
