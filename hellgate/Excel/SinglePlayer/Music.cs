using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Music
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;//this seems to be unique names, since it starts with an empty entry, like similar indices
        public Int32 undefined1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MUSICCONDITIONS")]
        public Int32 baseCondition;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "MUSIC_REF")]
        public Int32 musicRef;//idx
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined2;
    }
}