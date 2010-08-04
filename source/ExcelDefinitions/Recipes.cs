﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Reanimator.ExcelFile.ExcelOutputAttribute;

namespace Reanimator.ExcelDefinitions
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class RecipesRow
    {
        ExcelFile.TableHeader header;

        [ExcelOutput(SortId = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string recipe;
        public Int32 String;
        [ExcelOutput(SortId = 2)]
        public Int32 code;
        [ExcelOutput(IsBool = true)]
        public Int32 cubeRecipe;
        [ExcelOutput(IsBool = true)]
        public Int32 alwaysKnown;
        [ExcelOutput(IsBool = true)]
        public Int32 dontRequireExactIngredients;
        [ExcelOutput(IsBool = true)]
        public Int32 allowInRandomSingleUse;
        [ExcelOutput(IsBool = true)]
        public Int32 removeOnLoad;
        public Int32 weight;
        Int32 experienceEarned;
        Int32 goldReward;
        public Int32 resultQualityModifiesIngredientQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient1ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient1UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient1ItemQuality;
        public Int32 ingredient1MinQuantity;
        public Int32 ingredient1MaxQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient2ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient2UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient2ItemQuality;
        public Int32 ingredient2MinQuantity;
        public Int32 ingredient2MaxQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient3ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient3UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient3ItemQuality;
        public Int32 ingredient3MinQuantity;
        public Int32 ingredient3MaxQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient4ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient4UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient4ItemQuality;
        public Int32 ingredient4MinQuantity;
        public Int32 ingredient4MaxQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient5ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient5UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient5ItemQuality;
        public Int32 ingredient5MinQuantity;
        public Int32 ingredient5MaxQuantity;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEMS", Column = "name")]
        public Int32 ingredient6ItemClass;
        [ExcelOutput(IsTableIndex = true, TableId = "UNITTYPES", Column = "type")]
        public Int32 ingredient6UnitType;
        [ExcelOutput(IsTableIndex = true, TableId = "ITEM_QUALITY", Column = "quality")]
        public Int32 ingredient6ItemQuality;
        public Int32 ingredient6MinQuantity;
        public Int32 ingredient6MaxQuantity;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        Int32[] craftResult;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        Int32[] treasureResult;
        [ExcelOutput(IsTableIndex = true, TableId = "INVLOC", Column = "name")]
        public Int32 mustPlaceInInvSlot;
        [ExcelOutput(IsBitmask = true, DefaultBitmask = 0)]
        public Recipes.BitMask01 bitmask;
        Int32 spawnLevelMin;
        Int32 spawnLevelMax;

        public abstract class Recipes
        {
            [FlagsAttribute]
            public enum BitMask01 : uint
            {
                canBeLearned = 1,
                canSpawn = 2
            }
        }
    }
}