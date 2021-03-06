using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MonsterNames
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 stringKey;//stridx
        [ExcelOutput(IsTableIndex = true, TableStringId = "MONSTER_NAME_TYPES")]
        public Int32 monsterNameType;//idx
        [ExcelOutput(IsBool = true)]
        public Int32 isNameOverride;//bool
    }
}
