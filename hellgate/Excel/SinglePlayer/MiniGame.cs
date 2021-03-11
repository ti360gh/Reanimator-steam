using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MiniGame
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 requiredUnitType;
        [ExcelOutput(IsScript = true)]
        public Int32 condition;
        public Int32 weight;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MINIGAME_TAG")]//table C4
        public Int32 tagName0;
        public Int32 overrideMinNeeded0;
        public Int32 overrideMaxNeeded0;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MINIGAME_TAG")]//table C4
        public Int32 tagName1;
        public Int32 overrideMinNeeded1;
        public Int32 overrideMaxNeeded1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MINIGAME_TAG")]//table C4
        public Int32 tagName2;
        public Int32 overrideMinNeeded2;
        public Int32 overrideMaxNeeded2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "TREASURE")]
        public Int32 treasure;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 sound;
    }
}
