using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Emotes
    {
        ExcelFile.RowHeader header;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 commandString;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 decriptionString;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 textString;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 commandStringEnglish;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 descriptionStringEnglish;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 testStringEnglish;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITMODES")]//table 22
        public Int32 unitMode;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ACHIEVEMENTS")]//table B7
        public Int32 requiresAchievement;
    }
}
