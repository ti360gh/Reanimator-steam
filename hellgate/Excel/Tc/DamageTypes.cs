﻿using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using TableHeader = Hellgate.ExcelFile.TableHeader;

namespace Hellgate.Excel.Tc
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class DamageTypesTCv4Row
    {
        ExcelFile.TableHeader header;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string stat;
        public short code;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string miniIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string String;
        short undefined1;
        public Int32 color;
        public Int32 shieldHit;
        public Int32 criticalState;
        public Int32 softHitState;
        public Int32 mediumHitState;
        public Int32 bigHitState;
        public Int32 fumbleHitState;
        public Int32 damageOverTimeState;
        public Int32 fieldMissile;
        public Int32 invulnerableState;
        public Int32 invulnerableSfxState;
        public Int32 thornsState;
        public Int32 vulnerabilityInPVPTugboat;
        public Int32 vulnerabilityInPVPHellgate;
        public Int32 sfxInvulnerabilityDurationMultInPvpTugboat_tcv4;
        public Int32 sfxInvulnerabilityDurationMultInPvpHellgate_tcv4;
    }
}