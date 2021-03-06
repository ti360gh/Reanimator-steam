using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;

namespace Hellgate.Excel.TestCentre
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class LevelsRoomIndexTCv4
    {
        ExcelFile.RowHeader header;

        [ExcelOutput(SortColumnOrder = 1)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string name;
        [ExcelOutput(IsBool = true)]
        public Int32 outDoor;//bool;
        [ExcelOutput(IsBool = true)]
        public Int32 outDoorVisibility;//bool;
        [ExcelOutput(IsBool = true)]
        public Int32 computeAmbientOcclusion;//bool;
        [ExcelOutput(IsBool = true)]
        public Int32 noMonsterSpawn;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 noCollision_tcv4;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 noAdventures;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 noSubLevelEntrance;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 occupiesNodes;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 raisesNodes;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 fullCollision;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 useMatId_tcv4;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 dontObstructSound;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 dontOccludeVisibility;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 thirdPersonCameraIgnore;//bool
        [ExcelOutput(IsBool = true)]
        public Int32 rtsCameraIgnore;//bool
        public Int32 canopy_tcv4;
        public Int32 clutter_tcv4;
        public Int32 roomMessage_tcv4;
        public Int32 havokSliceType;
        public Int32 roomVersion;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass1_tcv4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass2_tcv4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass3_tcv4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass4_tcv4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SPAWN_CLASS")]
        public Int32 spawnClass5_tcv4;
        public Int32 runSpawnClassXTimes1_tcv4;
        public Int32 runSpawnClassXTimes2_tcv4;
        public Int32 runSpawnClassXTimes3_tcv4;
        public Int32 runSpawnClassXTimes4_tcv4;
        public Int32 runSpawnClassXTimes5_tcv4;
        public float nodeBuffer;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        Int32[] undefined1;
        public Int32 undefined_tcv4_1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string reverbEnvironment;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string overrideNodes_tcv4;
        public Int32 monsterLevelOverride_tcv4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        Int32[] undefined2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined_tcv4_2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "BACKGROUNDSOUNDS")]
        public Int32 backGroundSound;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "PROPS")]
        public Int32 noGore;//idx;
        [ExcelOutput(IsTableIndex = true, TableStringId = "PROPS")]
        public Int32 noHumans;//idx
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined3;
    }
}