using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ItemEvent
    {
        RowHeader header;
        public Int32 level;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 levelUpItem_Name;
        public Int32 levelUpItem_Amount;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 levelUpItem_SendID;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 levelUpItem_MailSubject;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 LevelUpItem_MailBody;
        public Int32 rank;
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 rankUpItem_Name;
        public Int32 rankUpItem_Amount;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 rankUpItem_SendID;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 rankUpItem_MailSubject;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 rankUpItem_MailBody;
    }
}
