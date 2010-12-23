﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class WardrobeModel
    {
        RowHeader header;
        public Int32 undefined1;
        [ExcelOutput(IsTableIndex = true, TableStringId = "WARDROBE_MODEL_GROUP", SortColumnOrder = 1, SecondarySortColumn = "appearanceGroup")]
        public Int32 modelGroup;//idx;
        public Int32 appearanceGroup;//idx;
        public Int32 appearanceGroup2;//idx;
        public Int32 undefined2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string fileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string defaultMaterial;
        public Int32 undefined3;
        public Int32 partGroup;
        public Int32 undefinedBool1;
        public Int32 undefinedBool2;
        public Int32 undefinedBool3;
        public Int32 undefined4;
        public Int32 undefined5;
        public float boxMinX;
        public float boxMinY;
        public float boxMinZ;
        public float boxMaxX;
        public float boxMaxY;
        public float boxMaxZ;
    }
}
