using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DefenseGameMonsterBuff
    {
        RowHeader header;
        public Int32 defenseObjectDestroyCount;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 state;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 message;
    }
}
