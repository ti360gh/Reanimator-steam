﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Reanimator.Excel
{
    public class ItemLevels : ExcelTable
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class ItemLevelsTable
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            byte[] unknown;
            public Int32 level;
            public Int32 baseDamageMultiplyer;
            public Int32 armor;
            public Int32 armorBuffer;
            public Int32 armorRegen;
            public Int32 shields;
            public Int32 shieldsBuffer;
            public Int32 shieldsRegen;
            public Int32 sfxAttackAbility;
            public Int32 sfxDefenceAbility;
            public Int32 interruptAttack;
            public Int32 interruptDefence;
            public Int32 stealthAttack;
            public Int32 aiChangerAttack;
            public Int32 feed;
            public Int32 levelRequirement;
            public Int32 itemLevelMin;
            public Int32 itemLevelMax;
            public Int32 buyPriceBase;
            public Int32 sellPriceBase;
            public Int32 augmentCostCommon;
            public Int32 augmentCostRare;
            public Int32 augmentCostLegendary;
            public Int32 augmentCostRandom;
            public Int32 scrapUpgradeQuantity;
            public Int32 specialScrapUpgradeQuantity;
            public Int32 itemLevelsPerUpgrade;
        }

        List<ItemLevelsTable> itemLevels;

        public ItemLevels(byte[] data) : base(data) { }

        public override object GetTableArray()
        {
            return itemLevels.ToArray();
        }

        protected override void ParseTables(byte[] data)
        {
            itemLevels = ExcelTables.ReadTables<ItemLevelsTable>(data, ref offset, Count);
        }
    }
}