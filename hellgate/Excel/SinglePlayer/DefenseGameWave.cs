using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DefenseGameWave
    {
        RowHeader header;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MONSTERS")]
        public Int32 monsterClass;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass;
        public Int32 spawnTime;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 spawnMessage;
    }
}
