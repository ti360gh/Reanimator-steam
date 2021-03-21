﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class BadgeRewards
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string badgeRewardName;
        [ExcelOutput(SortColumnOrder = 2)]
        public Int32 code;
        public BadgeName badgeName;//count1	type24
        [ExcelOutput(IsTableIndex = true, TableStringId = "ITEMS")]
        public Int32 item;//idx
        //[ExcelOutput(IsTableIndex = true, TableStringId = "")]//table B1
        public Int32 dontApplyIfPlayerHasRewardItemFor;
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 state;//idx
		public Int32 minUnitVersion;
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 unitTypeLimiter;//idx
		public Int32 unitTypeLimitPerPlayer;
        [ExcelOutput(IsBool = true)]
		public Int32 forceOnRespec;
        [ExcelOutput(IsScript = true)]
		public Int32 filter;
        [ExcelOutput(IsTableIndex = true, TableStringId = "TREASURE")]
        public Int32 treasure;//idx

        public enum BadgeName
        {
            Null = -1,
            FSSPing0 = 2,
            CSR = 3,
            Subscriber = 4,
            QA = 6,
            EA = 9,
            Trial = 64,
            Standard = 65,
            Lifetime = 66,
            BestBuy = 68,
            Walmart = 69,
            Gamestop = 70,
            Generic = 71,
            CE = 73,
            KoreanDyeKit1 = 74,
            KoreanDyeKit2 = 75,
            KoreanDyeKit3 = 76,
            KoreanDyeKit4 = 77,
            KoreanDyeKit5 = 78,
            KoreanDyeKit6 = 79,
            KoreanDyeKit7 = 80,
            KoreanDyeKit8 = 81,
            KoreanDyeKit9 = 82,
            KoreanDyeKit10 = 83,
            KoreanDyeKit11 = 84,
            KoreanDyeKit12 = 85,
            PCGamer = 86,
            IAH = 90,
            HBSCocoPet = 91,
            PCBangBonus = 92,
            HBSHalloween = 93,
            ExpBonusBang10 = 94,
            Reward1 = 95,
            Reward2 = 96,
            RewardOldUser = 97,
            RewardNewUser = 98
        }
		
    }
}
