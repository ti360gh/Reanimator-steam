using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel.JapaneseBeta
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SubLevelBeta
    {
        RowHeader header;
        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsTableIndex = true, TableStringId = "LEVEL_DRLGS")]
        public Int32 drlg;//idx
        [ExcelOutput(IsBool = true)]
        public Int32 allowRecall;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 allowTownPortals;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 allowMonsterDistribution;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 headStoneAtEntranceObject;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 respawnAtEntrance;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 partyPortalsAtEntrance;//bool
        public Type type;
        public float defaultPositionX;
        public float defaultPositionY;
        public float defaultPositionZ;
        public float entranceFlatZTolerance;
        public float entranceFlatRadius;
        public float entranceFlatHeightMin;
        [ExcelOutput(IsBool = true)]
        public Int32 autoCreateEntrance;//bool
        [ExcelOutput(IsTableIndex = true, TableStringId = "OBJECTS")]
        public Int32 objectEntrance;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "OBJECTS")]
        public Int32 objectExit;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 alternativeEntranceUnitType;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "UNITTYPES")]
        public Int32 alternativeExitUnitType;//idx
        [ExcelOutput(IsBool = true)]
        public Int32 allowLayoutMarkersForEntrance;//bool
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string layoutMarkerEntranceName;
        [ExcelOutput(IsBool = true)]
        public Int32 allowPathNodesForEntrance;//bool
        [ExcelOutput(IsTableIndex = true, TableStringId = "SUBLEVEL")]
        public Int32 subLevelNext;//idx
        [ExcelOutput(IsBool = true)]
        public Int32 overRideLevelSpawns;//bool
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "WEATHER_SETS")]
        public Int32 weather;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "STATES")]
        public Int32 stateWhenInsideSubLevel;//idx

        public enum Type
        {
            Null = -1,
            None = 0,
            Hellrift = 1,
            TruthAOld = 2,
            TruthANew = 3,
            TruthBOld = 4,
            TruthBNew = 5,
            TruthCOld = 6,
            TruthCNew = 7,
            TruthDOld = 8,
            TruthDNew = 9,
            TruthEOld = 10,
            TruthENew = 11
        }
    }
}
