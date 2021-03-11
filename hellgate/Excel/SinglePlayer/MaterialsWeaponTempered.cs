using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class MaterialsWeaponTempered
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string globalMaterialName;
        public float brightnessToMul;
        public Int32 blinkMillis;
        public float shineSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string particle;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string boneToAttach;
        public float shineSizeForMelee;
    }
}
