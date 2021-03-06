using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Font
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string systemName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string localPath;
        [ExcelOutput(IsBool = true)]
        public Int32 bold;
        [ExcelOutput(IsBool = true)]
        public Int32 italic;
        public Int32 fontSize;
        public Int32 sizeInTexture;
        public Int32 undefined1;
        public Int32 letersAcross;
        public Int32 lettersDown;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 131)]
        Int32[] undefined2;
    }
}
