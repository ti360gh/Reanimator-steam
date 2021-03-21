﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MonScalingFieldLevel
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String fieldLevel;
        [ExcelOutput(IsBool = true)]
        public Int32 uiActive;
        [ExcelOutput(IsStringIndex = true)]
        public Int32 mapDifficultyString;
        public float playerVsMonsterIncrementPct0;
        public float playerVsMonsterIncrementPct1;
        public Int32 monsterVsPlayerIncrementPct;
        public Int32 experienceTotal;
        public Int32 treasurePerPlayer;
        public Int32 addAffixCount;
        [ExcelOutput(IsScript = true)]
        public Int32 prop1;
        public Int32 overrideChampionChancePct;
        public Int32 adventureChancePct;
        public Int32 moneyIncreasePct;
    }
}