using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Procs
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String name;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
        [ExcelOutput(IsBool = true)]
        public Int32 verticalCenter;//bool
        public float coolDownInSeconds;
        public Int32 targetInstrumentOwner;//a single bit
        public float delayeProcTimeInSeconds;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SKILLS")]
        public Int32 skill1;//idx
        public Int32 skill1Param;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SKILLS")]
        public Int32 skill2;//idx
        public Int32 skill2Param;
    }
}
