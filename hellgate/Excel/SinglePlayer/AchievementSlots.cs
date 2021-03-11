using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class AchievementSlots
    {
        ExcelFile.RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String slotName;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
		public Int32 unlocksAtLevel;
        [ExcelOutput(IsScript = true)]
        public Int32 conditionalScript;
        [ExcelOutput(IsBool = true)]
		public Int32 unlocksAtPCBang;
		
    }
}
