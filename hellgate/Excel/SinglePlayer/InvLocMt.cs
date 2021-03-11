using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class InvLocMt
    {
        RowHeader header;
        public Int32 lType;
        public Int32 mType;
        public Int32 sType;
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table 18
        public Int32 location;
    }
}
