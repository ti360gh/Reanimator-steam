using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ItemSetItemGroups
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        public Int32 setAffix1NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix1;
        public Int32 setAffix2NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix2;
        public Int32 setAffix3NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix3;
        public Int32 setAffix4NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix4;
        public Int32 setAffix5NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix5;
        public Int32 setAffix6NumRequired;
        [ExcelOutput(IsTableIndex = true, TableStringId = "AFFIXES")]//table 35
        public Int32 setAffix6;
    }
}
