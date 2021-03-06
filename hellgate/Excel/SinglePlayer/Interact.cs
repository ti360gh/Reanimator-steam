using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Interact
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsBool = true)]
        public Int32 faceDuringInteraction;
        [ExcelOutput(IsBool = true)]
        public Int32 allowGhost;
        [ExcelOutput(IsBool = true)]
        public Int32 priority;
        [ExcelOutput(IsBool = true)]
        public Int32 setTalkingTo;
        [ExcelOutput(IsBool = true)]
        public Int32 playGreeting;
        [ExcelOutput(IsTableIndex = true, TableStringId = "INTERACT_MENU")]
        public Int32 interactMenu;//index
    }
}
