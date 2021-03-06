using System;
using System.Runtime.InteropServices;
using ExcelOutput = Hellgate.ExcelFile.OutputAttribute;
using RowHeader = Hellgate.ExcelFile.RowHeader;

namespace Hellgate.Excel
{
    [Serializable, StructLayout(LayoutKind.Sequential, Pack = 1)]
    class SoundsTCv4
    {
        RowHeader header;
        [ExcelOutput(IsStringOffset = true, SortColumnOrder = 1)]
        public Int32 name; //pchar
        Int32 undefined1;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 extension; //pchar
        Int32 undefined2;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 lqExtension; //pchar
        Int32 undefined3a;
        Int32 undefined3b;
        public PickType pickType;//unk
        //[ExcelOutput(IsTableIndex = true, TableStringId = "LANGUAGE")]
        public Language language;//idx
        public Int32 volume;
        public float undefined2a;
        public float minRange;
        public float maxRange;
        public RollOffType rollOffType;
        public Int32 reverbSend;
        [ExcelOutput(IsBitmask = true, DefaultBitmask = 0)]
        public BitMask01 bitmask01;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 directory;//pchar
        Int32 undefined5;
        Int32 fileName1;
        Int32 undefined6a;
        Int32 undefined6b;
        public Int32 undefined6c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined6d;//information
        Int32 undefined6e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined6f;//information
        Int32 undefined6g;
        public float weight1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined7;
        Int32 fileName2;
        Int32 undefined8a;
        Int32 undefined8b;
        public Int32 undefined8c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined8d;//information
        Int32 undefined8e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined8f;//information
        Int32 undefined8g;
        public float weight2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined9;
        Int32 fileName3;
        Int32 undefined10a;
        Int32 undefined10b;
        public Int32 undefined10c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined10d;//information
        Int32 undefined10e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined10f;//information
        Int32 undefined10g;
        public float weight3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined11;
        Int32 fileName4;
        Int32 undefined12a;
        Int32 undefined12b;
        public Int32 undefined12c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined12d;//information
        Int32 undefined12e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined12f;//information
        Int32 undefined12g;
        public float weight4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined13;
        Int32 fileName5;
        Int32 undefined14a;
        Int32 undefined14b;
        public Int32 undefined14c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined14d;//information
        Int32 undefined14e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined14f;//information
        Int32 undefined14g;
        public float weight5;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined15;
        Int32 fileName6;
        Int32 undefined16a;
        Int32 undefined16b;
        public Int32 undefined16c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined16d;//information
        Int32 undefined16e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined16f;//information
        Int32 undefined16g;
        public float weight6;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined17;
        Int32 fileName7;
        Int32 undefined18a;
        Int32 undefined18b;
        public Int32 undefined18c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined18d;//information
        Int32 undefined18e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined18f;//information
        Int32 undefined18g;
        public float weight7;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined19;
        Int32 fileName8;
        Int32 undefined20a;
        Int32 undefined20b;
        public Int32 undefined20c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined20d;//information
        Int32 undefined20e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined20f;//information
        Int32 undefined20g;
        public float weight8;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined21;
        Int32 fileName9;
        Int32 undefined22a;
        Int32 undefined22b;
        public Int32 undefined22c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined22d;//information
        Int32 undefined22e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined22f;//information
        Int32 undefined22g;
        public float weight9;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined23;
        Int32 fileName10;
        Int32 undefined24a;
        Int32 undefined24b;
        public Int32 undefined24c;//information
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDS")]
        public Int32 undefined24d;//information
        Int32 undefined24e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined24f;//information
        Int32 undefined24g;
        public float weight10;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined25;
        Int32 fileName11;
        Int32 undefined26a;
        Int32 undefined26b;
        public Int32 undefined26c;//information
        [ExcelOutput(ConstantValue = -1)]
        Int32 undefined26d;//information
        Int32 undefined26e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined26f;//information
        Int32 undefined26g;
        public float weight11;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined27;
        Int32 fileName12;
        Int32 undefined28a;
        Int32 undefined28b;
        public Int32 undefined28c;//information
        [ExcelOutput(ConstantValue = -1)]
        Int32 undefined28d;//information
        Int32 undefined28e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined28f;//information
        Int32 undefined28g;
        public float weight12;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined29;
        Int32 fileName13;
        Int32 undefined30a;
        Int32 undefined30b;
        public Int32 undefined30c;//information
        [ExcelOutput(ConstantValue = -1)]
        Int32 undefined30d;//information
        Int32 undefined30e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined30f;//information
        Int32 undefined30g;
        public float weight13;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined31;
        Int32 fileName14;
        Int32 undefined32a;
        Int32 undefined32b;
        public Int32 undefined32c;//information
        [ExcelOutput(ConstantValue = -1)]
        Int32 undefined32d;//information
        Int32 undefined32e;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 undefined32f;//information
        Int32 undefined32g;
        public float weight14;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        Int32[] undefined33;
        public Int32 freqVar;
        Int32 undefined34;
        public Int32 volVar;
        public Int32 undefined123;//needs labeling
        public Int32 undefined1234;//needs labeling
        public Int32 maxWithInRad;
        public float radius;
		[ExcelOutput(DebugIgnoreConstantCheck = true)]
        public Int32 hardwarePriority;
        public Int32 gamePriority;
        public float fadeOutTime;
        public Int32 undefined40;
        public float fadeInTime;
        public Int32 undefined41;
        public float frontSend;
        public float centerSend;
        public float rearSend;
        public float sideSend;
        public Int32 lfeSend;//lfe ?
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUND_MIXSTATES")]
        public Int32 mixState;//idx
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDBUSES")]
        public Int32 bus;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas1;//list
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas2;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas3;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas4;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas5;
        [ExcelOutput(IsTableIndex = true, TableStringId = "SOUNDVCAS")]
        public Int32 vcas6;
		[ExcelOutput(DebugIgnoreConstantCheck = true)]
        public Int32 vcas7;
		[ExcelOutput(DebugIgnoreConstantCheck = true)]
        public Int32 vcas8;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MUSIC_REF")]
        public Int32 musicRef;//idx
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined44;
        [ExcelOutput(IsTableIndex = true, TableStringId = "MUSICSTINGERS")]
        public Int32 stingerRef;//idx
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined45;
        [ExcelOutput(IsStringOffset = true)]
        public Int32 effects;//pchar
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        Int32[] undefined46;
        public Int32 adsr_tcv4;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public Int32[] undefined47;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public Int32[] undefined48;

        [FlagsAttribute]
        public enum BitMask01 : uint
        {
            nonblock = (1 << 0),
            stream = (1 << 1),
            is3D = (1 << 2),//it is in fact called just 3D, but that isn't liked.
            unk01 = (1 << 3),
            loops = (1 << 4),
            canCutoff = (1 << 5),
            highlander = (1 << 6),
            groupHighlander = (1 << 7),
            software = (1 << 8),
            headRelative = (1 << 9),
            isMusic = (1 << 10),
            dontRandomizeStart = (1 << 11),
            dontCrossfadeVariations = (1 << 12),
            unk02 = (1 << 13),
            loadAtStartup = (1 << 14),
            useGlobalLights = (1 << 15),
            backupTransSpecular = (1 << 16),
            emitsGpuParticles = (1 << 17),
            isScreenEffect = (1 << 18),
            loadAllTechniques = (1 << 19),
            receiveRain = (1 << 20),
            oneParticleSystem = (1 << 21),
            usesPortals = (1 << 22),
            requiresHavokFx = (1 << 23),
            directionalInSH = (1 << 24),
            emissivediffuse = (1 << 25)
        }

        public enum PickType
        {
            Null = -1,
            Rand = 0,
            All = 1
        }
        public enum Language
        {
            Null = -1,
            English = 0,
            Korean = 1,
            ChineseSimplified = 2,
            ChineseTraditional = 3,
            Japanese = 4,
            French = 5,
            Spanish = 6,
            German = 7,
            Italian = 8,
            Polish = 9,
            Czech = 10,
            Hungarian = 11,
            Russian = 12,
            Thai = 13,
            Vietnamese = 14
        }
        public enum RollOffType
        {
            Null = -1,
            None = 0,
            Log = 1,
            Linear = 2,
            Inverse = 3
        }
    }
}