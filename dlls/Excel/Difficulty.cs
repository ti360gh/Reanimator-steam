﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Reanimator.Excel
{
    public class Difficulty : ExcelTable
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class DifficultyTable
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            Int32[] header;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public String difficulty;

            public Int32 id;
            public Int32 unknown;
        }

        public Difficulty(byte[] data) : base(data) { }

        protected override void ParseTables(byte[] data)
        {
            ReadTables<DifficultyTable>(data, ref offset, Count);
        }
    }
}
