using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class CmdMenus
    {
        ExcelFile.RowHeader header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cmdMenu;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string chatCommand;
        [ExcelOutput(IsBool = true)]
        public Int32 immediate;
    }
}
