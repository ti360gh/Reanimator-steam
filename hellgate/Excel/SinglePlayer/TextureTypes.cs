using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class TextureTypes
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        public Int32 backgroundPriority;
        public Int32 unitPriority;
        [ExcelOutput(DebugIgnoreConstantCheck = true)]
        public Int32 particlePriority;
        [ExcelOutput(DebugIgnoreConstantCheck = true)]
        public Int32 uiPriority;
        public Int32 wardrobePriority;
    }
}