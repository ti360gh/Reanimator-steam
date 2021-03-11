using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class PlayerCondition
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        public Int32 startCondition;
        public Int32 endCondition;
        public Int32 regenInTownPerSecond;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 eventState;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 eventStateForPCBang;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 eventstring;
    }
}
