using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class TutorialStrings
    {
        RowHeader header;
        [ExcelOutput(IsTableIndex = true, TableStringId = "QUEST")]//table 9E
        public Int32 relatedQuest;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsTableIndex = true, TableStringId = "TUTORIALSTRINGS")]//table C6 labeled Tutorials, perhaps really tutorialtrings
        public Int32 nextRow;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string linkData;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        Int32[] undefined1;
        public DataType dataType;
        public Int32 duration;
        public Int32 delay;
        public StartCondition startCondition;

        public enum DataType
        {
            Null = -1,
            String = 0,
            Communicator = 1,
            Image = 2
        }

        public enum StartCondition
        {
            Null = -1,
            Activate = 0,
            Complete = 1
        }

    }
}
