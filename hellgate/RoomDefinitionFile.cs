using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Hellgate.Excel.JapaneseBeta;
using Hellgate.Xml;
using Revival.Common;

namespace Hellgate
{
    public class RoomDefinitionFile : HellgateFile
    {
        public new const String Extension = ".rom";
        public new const String ExtensionDeserialised = ".rom.xml";
        private const UInt32 FileMagicWord = 0xEA7A7ABE; // '¾zzê'
        private const UInt32 RequiredVersion = 0x49; // 73

        // total size = 728 bytes (0x2D8)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class RoomDefinitionHeader
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            private String _internalRoomDefinitionName;             // 0x00     0       // is internally set - is null in-file              LoadRoomDefinition+262  898 call    strncpy_s_0 (0x100; 256 bytes)
            private Int32 _internalInt321;                          // 0x100    256     // is internally set to -1                          LoadRoomDefinition+24F  898 or      dword ptr [rbx+100h], 0FFFFFFFFh
            private Int32 _internalUnknown1;                        // 0x104    260     // is all 0 in every file; probably internal/reserved
            private Int32 _internalInt322;                          // 0x108    264     // is internally set to ? shortly after Excel_GetRoomIndex (was 22 (or 0x22? - wasn't paying attention) for character creation) LoadRoomDefinition+28D  898 mov     [rbx+108h], eax
            internal Int32 Count10C;                                // 0x10C    268
            internal Int64 Offset110;                               // 0x110    272
            internal Int32 Count118;                                // 0x118    280
            private Int32 _internalUnknown2;                        // 0x11C    284     // is always 0 in every file; probably internal/reserved
            internal Int64 Offset120;                               // 0x120    288
            public float UnknownFloat1;                             // 0x128    296
            internal Int32 Count12C;                                // 0x12C    300
            internal Int64 Offset130;                               // 0x130    304
            private Int32 _internalUnknown3;                        // 0x138    312     // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown4;                        // 0x13C    316     // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown5;                        // 0x140    320     // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown6;                        // 0x144    324     // is always 0 in every file; probably internal/reserved
            public float MinX;                                      // 0x148    328
            public float MinY;                                      // 0x14C    332
            public float MinZ;                                      // 0x150    336
            public float MaxX;                                      // 0x154    340
            public float MaxY;                                      // 0x158    344
            public float MaxZ;                                      // 0x15C    348
            internal Int32 Count160;                                // 0x160    352
            private Int32 _internalUnknown7;                        // 0x164    356     // is always 0 in every file; probably internal/reserved
            internal Int64 Offset168;                               // 0x168    360     // is offset, but not seen used
            public Int32 UnknownInt321;                             // 0x170    368     // is read in before next big function call         LoadRoomDefinition+4CA  898 mov     r9d, [rbx+170h]
            internal Int32 Count174;                                // 0x174    372
            internal Int64 Offset178;                               // 0x178    376
            public Int32 UnknownInt322;                             // 0x180    384     // is read in before next big function call         LoadRoomDefinition+4E0  898 mov     eax, [rbx+180h]
            internal Int32 Count184;                                // 0x184    388
            internal Int64 Offset188;                               // 0x188    392     // is offset to footer
            private Int64 _internalUnknown8;                        // 0x190    400     // is always 0 in every file; probably internal/reserved
            private Int64 _internalUnknown9;                        // 0x198    408     // is always 0 in every file; probably internal/reserved
            public Int64 UnknownInt323;                             // 0x1A0    416
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 47)]
            private Int32[] _internalUnknowns;                      // 0x198    406     // is all 0's in every file; probably internal/reserved
            public Int32 RoomVersion;                               // 0x264    612     // room version from excel table ROOM_INDEX - file must equal excel value
            internal Int64 VerticesOffset;                          // 0x268    616
            internal Int32 VertexCount;                             // 0x270    624     // this is the "box" vertices of the room (8 points of cube etc)
            private Int32 _internalUnknown10;                       // 0x274    628     // is always 0 in every file; probably internal/reserved
            internal Int64 Offset278;                               // 0x278    632
            internal Int32 Count280;                                // 0x280    640
            private Int32 _internalUnknown11;                       // 0x284    644     // is always 0 in every file; probably internal/reserved
            internal Int64 Offset288;                               // 0x288    648
            public float UnknownFloat8;                             // 0x28C    656
            public float UnknownFloat9;                             // 0x290    660
            internal Int32 Count298;                                // 0x298    664
            internal Int32 Count29C;                                // 0x29C    668
            internal Int64 Offset2A0;                               // 0x2A0    672
            internal Int32 Count2A8;                                // 0x2A8    680
            public float UnknownFloat10;                            // 0x2AC    684
            public float UnknownFloat11;                            // 0x2B0    688
            public float UnknownFloat12;                            // 0x2B4    692
            public Int32 UnknownInt324;                             // 0x2B8    696
            private Int32 _internalUnknown12;                       // 0x2BC    700     // is always 0 in every file; probably internal/reserved
            private Int32 _internalInt323;                          // 0x2C0    704     // internally set to -1                             LoadRoomDefinition+286  898 or      dword ptr [rbx+2C0h], 0FFFFFFFFh
            private Int32 _internalUnknown13;                       // 0x2C4    708     // is always 0 in every file; probably internal/reserved
            private Int64 _internalInt641;                          // 0x2C8    712     // internally set to ptr to fileBytes               LoadRoomDefinition+274  898 mov     [rbx+2C8h], r11
            private Int32 _internalInt324;                          // 0x2D0    720     // internally set to fileSize                       LoadRoomDefinition+201  898 mov     [rbx+2D0h], edx
            private Int32 _internalUnknown14;                       // 0x2D4    724     // is always 0 in every file; probably internal/reserved
            // end of struct                                        // 0x2D8    728
        }

        // total size = 16 bytes (0x10)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class UnknownStruct1
        {
            public float UnknownFloat1;         // 0x00     0
            public float UnknownFloat2;         // 0x04     4
            public float UnknownFloat3;         // 0x08     8
            internal Int32 Index;               // 0x0C     12      // is just a self-index (array[0].Index = 0, array[1].Index = 1, etc)
            // end of struct                    // 0x10     16
        }

        // total size = 64 bytes (0x40)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UnknownStruct2
        {
            public Int64 Struct1Index1;         // 0x00     0       // these are the index to UnknownStruct1 array (above)
            public Int64 Struct1Index2;         // 0x08     8
            public Int64 Struct1Index3;         // 0x10     16
            public float UnknownFloat1;         // 0x18     24
            public float UnknownFloat2;         // 0x1C     28
            public float UnknownFloat3;         // 0x20     32
            public float UnknownFloat4;         // 0x24     36
            public float UnknownFloat5;         // 0x28     40
            public float UnknownFloat6;         // 0x2C     44
            public float UnknownFloat7;         // 0x30     48
            public float UnknownFloat8;         // 0x34     52
            public float UnknownFloat9;         // 0x38     56
            private Int32 _internalUnknown;     // 0x3C     60      // is always 0 in every file; probably internal/reserved
            // end of struct                    // 0x40     64
        }

        // total size = 40 bytes (0x28)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UnknownStruct3
        {
            public Int64 Offset1;               // 0x00     0       // note: these offset values are file bytes offsets to Int32 arrays in _unknownStruct1Int32Array
            public Int64 Offset2;               // 0x08     8
            public Int64 Offset3;               // 0x10     16
            public Int32 Int32Count1;           // 0x18     24      // these are the number of int values at their respective offset locations
            public Int32 Int32Count2;           // 0x1C     28
            public Int32 Int32Count3;           // 0x20     32
            private Int32 _internalUnknown;     // 0x24     36      // is always 0 in every file; probably internal/reserved
            // end of struct                    // 0x28     40
        }

        // total size = 12 bytes (0x0C)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UnknownStruct7Vector3
        {
            public float X;                     // 0x00     0
            public float Y;                     // 0x04     4
            public float Z;                     // 0x08     8
            // end of struct                    // 0x0C     12
        }

        // total size = 12 bytes (0x0C)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class UnknownStruct5
        {
            public Int32 UnknownInt321;         // 0x00     0
            public Int32 UnknownInt322;         // 0x04     4
            public Int32 UnknownInt323;         // 0x08     8
            // end of struct                    // 0x0C     12
        }

        // total size = 136 bytes (0x88)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class UnknownStruct6
        {
            // most of these are guesses - I think some of the Int32s are float, and possible some of the floats are doubles
            // todo: keep an eye on outputs and check for huge weird numbers
            public Int32 UnknownInt32;          // 0x00     0
            public float UnknownFloat1;         // 0x04     4
            public float UnknownFloat2;         // 0x08     8
            public float UnknownFloat3;         // 0x0C     12
            public float UnknownFloat4;         // 0x10     16
            public float UnknownFloat5;         // 0x14     20
            public float UnknownFloat6;         // 0x18     24
            public float UnknownFloat7;         // 0x1C     28
            public float UnknownFloat8;         // 0x20     32
            public float UnknownFloat9;         // 0x24     36
            public float UnknownFloat10;        // 0x28     40
            public float UnknownFloat11;        // 0x2C     44
            public float UnknownFloat12;        // 0x30     48
            private Int32 _internalUnknown1;    // 0x34     52      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown2;    // 0x38     56      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown3;    // 0x3C     60      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown4;    // 0x40     64      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown5;    // 0x44     68      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown6;    // 0x48     72      // is always 0 in every file; probably internal/reserved
            public float UnknownFloat13;        // 0x4C     76
            public float UnknownFloat14;        // 0x50     80
            public float UnknownFloat15;        // 0x54     84
            public float UnknownFloat16;        // 0x58     88
            public float UnknownFloat17;        // 0x5C     92
            public float UnknownFloat18;        // 0x60     96
            public float UnknownFloat19;        // 0x64     100
            public float UnknownFloat20;        // 0x68     104
            public float UnknownFloat21;        // 0x6C     108
            public float UnknownFloat22;        // 0x70     112
            private Int32 _internalUnknown7;    // 0x74     116      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown8;    // 0x78     120      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown9;    // 0x7C     124      // is always 0 in every file; probably internal/reserved
            private Int32 _internalUnknown10;   // 0x80     128      // is always 0 in every file; probably internal/reserved
            internal Int32 FooterInt32;         // 0x84     132      // is always -1 in every file; probably internal/reserved
            // end of struct                    // 0x88     136
        }

        // total size = 6 bytes (0x06)
        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class UnknownStruct7
        {
            public short UnknownShort1;         // 0x00     0
            public short UnknownShort2;         // 0x02     2
            public short UnknownShort3;         // 0x04     4
            // end of struct                    // 0x06     6
        }

        [XmlRoot("RoomDefinition")]
        public class RoomDefinitionStruct
        {
            public RoomDefinitionHeader FileHeader;
            public UnknownStruct1[] UnknownStruct1Array;
            public UnknownStruct2[] UnknownStruct2Array;
            public UnknownStruct3[][] UnknownStruct3Arrays;
            public Int32[] UnknownStruct3Int32Array;
            public Vector3[] Vertices;
            public UnknownStruct5[] UnknownStruct5Array;
            public UnknownStruct6[] UnknownStruct6Array;
            public UnknownStruct7Vector3[] UnknownStruct7Array;
            public UnknownStruct7[] UnknownStruct8Array;
            public UnknownStruct5[] UnknownStructFooter;
        }

        public RoomDefinitionStruct RoomDefinition;
        public float Width { get { return (RoomDefinition.FileHeader.MaxX - RoomDefinition.FileHeader.MinX); } }
        public float Length { get { return (RoomDefinition.FileHeader.MaxY - RoomDefinition.FileHeader.MinY); } }
        public float Height { get { return (RoomDefinition.FileHeader.MaxZ - RoomDefinition.FileHeader.MinZ); } }
        public String FilePath { get; private set; }
        public RoomIndexRow RoomIndexData { get; private set; }
        public RoomLayoutGroupDefinition Layout { get; private set; }

        public RoomDefinitionFile(String filePath)
        {
            FilePath = filePath;
        }

        public RoomDefinitionFile(String filePath, RoomIndexRow roomIndexRow, RoomLayoutGroupDefinition roomLayout)
        {
            FilePath = filePath;
            RoomIndexData = roomIndexRow;
            Layout = roomLayout;
        }

        /// <summary>
        /// Parses a level rules file bytes.
        /// </summary>
        /// <param name="fileBytes">The bytes of the level rules to parse.</param>
        public override void ParseFileBytes(byte[] fileBytes)
        {
            // sanity check
            if (fileBytes == null) throw new ArgumentNullException("fileBytes", "File bytes cannot be null!");
            RoomDefinition = new RoomDefinitionStruct();

            // file header checks
            int offset = 0;
            UInt32 fileMagicWord = FileTools.ByteArrayToUInt32(fileBytes, ref offset);
            if (fileMagicWord != FileMagicWord) throw new Exceptions.UnexpectedMagicWordException();

            UInt32 fileVersion = FileTools.ByteArrayToUInt32(fileBytes, ref offset);
            if (fileVersion != RequiredVersion) throw new Exceptions.NotSupportedFileVersionException();

            // main file header
            RoomDefinition.FileHeader = FileTools.ByteArrayToStructure<RoomDefinitionHeader>(fileBytes, ref offset);
            RoomDefinitionHeader header = RoomDefinition.FileHeader;

            // read UnknownStruct1 array
            offset = (int)header.Offset110;
            int count10C = header.Count10C;
            if (offset > 0 && count10C > 0)
            {
                RoomDefinition.UnknownStruct1Array = _ReadUnknownStruct1Array(fileBytes, ref offset, count10C); // FileTools.ByteArrayToArray<UnknownStruct1>(fileBytes, ref offset, count10C);
            }

            // read UnknownStruct2 array
            offset = (int)header.Offset120;
            int count118 = header.Count118;
            if (offset > 0 && count118 > 0)
            {
                RoomDefinition.UnknownStruct2Array = _ReadUnknownStruct2Array(fileBytes, ref offset, count118); // FileTools.ByteArrayToArray<UnknownStruct2>(fileBytes, ref offset, count118);
            }

            // read UnknownStruct3 arrays
            offset = (int)header.Offset288;
            int count29C = header.Count29C;
            int count298 = header.Count298;
            if (offset > 0 && count29C > 0 && count298 > 0)
            {
                RoomDefinition.UnknownStruct3Arrays = new UnknownStruct3[count29C][];
                for (int i = 0; i < count29C; i++)
                {
                    RoomDefinition.UnknownStruct3Arrays[i] = _ReadUnknownStruct3Array(fileBytes, ref offset, count298); // FileTools.ByteArrayToArray<UnknownStruct3>(fileBytes, ref offset, count298);
                }
            }
            // read UnknownStruct3 Int32 array
            offset = (int)header.Offset2A0;
            int count2A8 = header.Count2A8;
            if (offset > 0 && count2A8 > 0)
            {
                RoomDefinition.UnknownStruct3Int32Array = FileTools.ByteArrayToInt32Array(fileBytes, ref offset, count2A8);
            }

            // read room vertices array
            offset = (int)header.VerticesOffset;
            int vertexCount = header.VertexCount;
            if (offset > 0 && vertexCount > 0)
            {
                RoomDefinition.Vertices = _ReadVector3Array(fileBytes, ref offset, vertexCount); // FileTools.ByteArrayToArray<Vector3>(fileBytes, ref offset, vertexCount);
            }

            // read UnknownStruct5 array
            offset = (int)header.Offset278;
            int count280 = header.Count280;
            if (offset > 0 && count280 > 0)
            {
                RoomDefinition.UnknownStruct5Array = FileTools.ByteArrayToArray<UnknownStruct5>(fileBytes, ref offset, count280);
            }

            // read UnknownStruct6 array
            offset = (int)header.Offset130;
            int count12C = header.Count12C;
            if (offset > 0 && count12C > 0)
            {
                RoomDefinition.UnknownStruct6Array = FileTools.ByteArrayToArray<UnknownStruct6>(fileBytes, ref offset, count12C);
            }

            // read UnknownStruct7 array (not seen read like this - but it works) - has same structure (3xfloat) as UnknownStruct4
            offset = (int)header.Offset178;
            int count174 = header.Count174;
            if (offset > 0 && count174 > 0)
            {
                RoomDefinition.UnknownStruct7Array = _ReadUnknownStruct7Vector3Array(fileBytes, ref offset, count174); // FileTools.ByteArrayToArray<Vector3>(fileBytes, ref offset, count174);
            }

            // read UnknownStruct8 array (not seen read like this - but it works)
            offset = (int)header.Offset188;
            int count184 = header.Count184;
            if (offset > 0 && count184 > 0)
            {
                RoomDefinition.UnknownStruct8Array = _ReadUnknownStruct7Array(fileBytes, ref offset, count184); // FileTools.ByteArrayToArray<UnknownStruct7>(fileBytes, ref offset, count184);
            }

            // read UnknownStruct9 array
            offset = (int)header.Offset168;
            int countUnknown7 = header.Count160;
            if (offset > 0 && countUnknown7 > 0)
            {
                RoomDefinition.UnknownStructFooter = FileTools.ByteArrayToArray<UnknownStruct5>(fileBytes, ref offset, countUnknown7);
            }

            // final debug check
            Debug.Assert(offset == fileBytes.Length);
        }

        private static UnknownStruct1[] _ReadUnknownStruct1Array(byte[] fileBytes, ref int offset, int count) // because Marshal.PtrToStructure IS SO FUCKING SLOW
        {
            UnknownStruct1[] arr = new UnknownStruct1[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = new UnknownStruct1
                {
                    UnknownFloat1 = StreamTools.ReadFloat(fileBytes, ref offset),
                    UnknownFloat2 = StreamTools.ReadFloat(fileBytes, ref offset),
                    UnknownFloat3 = StreamTools.ReadFloat(fileBytes, ref offset),
                    Index = StreamTools.ReadInt32(fileBytes, ref offset)
                };
            }

            return arr;
        }

        private static unsafe UnknownStruct2[] _ReadUnknownStruct2Array(byte[] fileBytes, ref int offset, int count) // because Marshal.PtrToStructure IS SO FUCKING SLOW
        {
            fixed (byte* pBytes = &fileBytes[offset])
            {
                UnknownStruct2[] arr = new UnknownStruct2[count];
                for (int i = 0; i < count; i++)
                {
                    arr[i] = *(UnknownStruct2*) (pBytes + i*sizeof (UnknownStruct2));
                }
                return arr;
            }

            //UnknownStruct2[] arr = new UnknownStruct2[count];

            //fixed (byte* pBytes = &fileBytes[offset])
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        arr[i] = new UnknownStruct2
            //        {
            //            Struct1Index1 = *(((Int64*)pBytes)),
            //            Struct1Index2 = *(((Int64*)pBytes + 8)),
            //            Struct1Index3 = *(((Int64*)pBytes + 16)),
            //            UnknownFloat1 = *(((float*)pBytes + 24)),
            //            UnknownFloat2 = *(((float*)pBytes + 28)),
            //            UnknownFloat3 = *(((float*)pBytes + 32)),
            //            UnknownFloat4 = *(((float*)pBytes + 36)),
            //            UnknownFloat5 = *(((float*)pBytes + 40)),
            //            UnknownFloat6 = *(((float*)pBytes + 44)),
            //            UnknownFloat7 = *(((float*)pBytes + 48)),
            //            UnknownFloat8 = *(((float*)pBytes + 52)),
            //            UnknownFloat9 = *(((float*)pBytes + 56))
            //            // int _internalUnknown @ 60
            //        };

            //        offset += 64;
            //    }
            //}

            //return arr;
        }

        private static unsafe UnknownStruct3[] _ReadUnknownStruct3Array(byte[] fileBytes, ref int offset, int count) // because Marshal.PtrToStructure IS SO FUCKING SLOW
        {
            fixed (byte* pBytes = &fileBytes[offset])
            {
                UnknownStruct3[] arr = new UnknownStruct3[count];
                for (int i = 0; i < count; i++)
                {
                    arr[i] = *(UnknownStruct3*)(pBytes + i * sizeof(UnknownStruct3));
                }
                return arr;
            }

            //UnknownStruct3[] arr = new UnknownStruct3[count];

            //for (int i = 0; i < count; i++)
            //{
            //    arr[i] = new UnknownStruct3
            //    {
            //        Offset1 = StreamTools.ReadInt64(fileBytes, ref offset),
            //        Offset2 = StreamTools.ReadInt64(fileBytes, ref offset),
            //        Offset3 = StreamTools.ReadInt64(fileBytes, ref offset),
            //        Int32Count1 = StreamTools.ReadInt32(fileBytes, ref offset),
            //        Int32Count2 = StreamTools.ReadInt32(fileBytes, ref offset),
            //        Int32Count3 = StreamTools.ReadInt32(fileBytes, ref offset)
            //    };
            //    offset += 4; // _internalUnknown
            //}

            //return arr;
        }

        private static unsafe UnknownStruct7Vector3[] _ReadUnknownStruct7Vector3Array(byte[] fileBytes, ref int offset, int count)
        {
            fixed (byte* pBytes = &fileBytes[offset])
            {
                UnknownStruct7Vector3[] arr = new UnknownStruct7Vector3[count];
                for (int i = 0; i < count; i++)
                {
                    arr[i] = *(UnknownStruct7Vector3*)(pBytes + i * sizeof(UnknownStruct7Vector3));
                }
                return arr;
            }
        }

        private static Vector3[] _ReadVector3Array(byte[] fileBytes, ref int offset, int count) // because Marshal.PtrToStructure IS SO FUCKING SLOW
        {
            Vector3[] arr = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = new Vector3
                {
                    X = StreamTools.ReadFloat(fileBytes, ref offset),
                    Y = StreamTools.ReadFloat(fileBytes, ref offset),
                    Z = StreamTools.ReadFloat(fileBytes, ref offset),
                };
            }

            return arr;
        }

        private static UnknownStruct7[] _ReadUnknownStruct7Array(byte[] fileBytes, ref int offset, int count) // because Marshal.PtrToStructure IS SO FUCKING SLOW
        {
            UnknownStruct7[] arr = new UnknownStruct7[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = new UnknownStruct7
                {
                    UnknownShort1 = StreamTools.ReadInt16(fileBytes, ref offset),
                    UnknownShort2 = StreamTools.ReadInt16(fileBytes, ref offset),
                    UnknownShort3 = StreamTools.ReadInt16(fileBytes, ref offset)
                };
            }

            return arr;
        }

        /// <summary>
        /// Parses and XML document and returns the serialized byte array.
        /// </summary>
        /// <param name="xmlDocument">The XML Document to parse.</param>
        /// <returns>The serialized byte array.</returns>
        public void ParseXmlDocument(XmlDocument xmlDocument)
        {
            if (xmlDocument == null) throw new ArgumentNullException("xmlDocument", "XML Document cannot be null!");

            XmlNodeReader xmlNodeReader = new XmlNodeReader(xmlDocument);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof (RoomDefinitionStruct));
            RoomDefinition = (RoomDefinitionStruct) xmlSerializer.Deserialize(xmlNodeReader);
        }

        public override byte[] ToByteArray()
        {
            if (RoomDefinition == null) throw new Exceptions.NotInitializedException();

            int offset = 0;
            byte[] fileBytes = new byte[1024];

            // write header
            FileTools.WriteToBuffer(ref fileBytes, ref offset, FileMagicWord);
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RequiredVersion);
            offset += Marshal.SizeOf(RoomDefinition.FileHeader); // want to update offsets and counts first

            // write unknown struct 3
            RoomDefinition.FileHeader.Offset110 = offset;
            RoomDefinition.FileHeader.Count10C = RoomDefinition.UnknownStruct1Array.Length;
            for (int i = 0; i < RoomDefinition.UnknownStruct1Array.Length; i++)
            {
                RoomDefinition.UnknownStruct1Array[i].Index = i;
            }
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct1Array);

            // write unknown struct 2
            RoomDefinition.FileHeader.Offset120 = offset;
            RoomDefinition.FileHeader.Count118 = RoomDefinition.UnknownStruct2Array.Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct2Array);

            // write int arrays
            RoomDefinition.FileHeader.Offset2A0 = offset;
            RoomDefinition.FileHeader.Count2A8 = RoomDefinition.UnknownStruct3Int32Array.Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct3Int32Array.ToByteArray());

            // write unknown struct 1
            RoomDefinition.FileHeader.Offset288 = offset;
            RoomDefinition.FileHeader.Count29C = RoomDefinition.UnknownStruct3Arrays.Length;
            RoomDefinition.FileHeader.Count298 = RoomDefinition.UnknownStruct3Arrays[0].Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct3Arrays);

            // write room vertices
            RoomDefinition.FileHeader.VerticesOffset = 0;
            RoomDefinition.FileHeader.VertexCount = 0;
            if (RoomDefinition.Vertices != null)
            {
                RoomDefinition.FileHeader.VerticesOffset = offset;
                RoomDefinition.FileHeader.VertexCount = RoomDefinition.Vertices.Length;
                FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.Vertices);
            }

            // write unknown struct 5
            RoomDefinition.FileHeader.Offset278 = 0;
            RoomDefinition.FileHeader.Count280 = 0;
            if (RoomDefinition.UnknownStruct5Array != null)
            {
                RoomDefinition.FileHeader.Offset278 = offset;
                RoomDefinition.FileHeader.Count280 = RoomDefinition.UnknownStruct5Array.Length;
                FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct5Array);
            }

            // write unknown struct 6
            RoomDefinition.FileHeader.Offset130 = 0;
            RoomDefinition.FileHeader.Count12C = 0;
            if (RoomDefinition.UnknownStruct6Array != null)
            {
                RoomDefinition.FileHeader.Offset130 = offset;
                RoomDefinition.FileHeader.Count12C = RoomDefinition.UnknownStruct6Array.Length;
                foreach (UnknownStruct6 unknownStruct6 in RoomDefinition.UnknownStruct6Array)
                {
                    unknownStruct6.FooterInt32 = -1;
                }
                FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct6Array);
            }

            // write unknown struct 7
            RoomDefinition.FileHeader.Offset178 = offset;
            RoomDefinition.FileHeader.Count174 = RoomDefinition.UnknownStruct7Array.Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct7Array);

            // write unknown struct 8
            RoomDefinition.FileHeader.Offset188 = offset;
            RoomDefinition.FileHeader.Count184 = RoomDefinition.UnknownStruct8Array.Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStruct8Array);

            // write unknown struct footer
            RoomDefinition.FileHeader.Offset168 = offset;
            RoomDefinition.FileHeader.Count160 = RoomDefinition.UnknownStructFooter.Length;
            FileTools.WriteToBuffer(ref fileBytes, ref offset, RoomDefinition.UnknownStructFooter);

            // write our updated header
            FileTools.WriteToBuffer(ref fileBytes, 8, RoomDefinition.FileHeader);

            // and we're done
            Array.Resize(ref fileBytes, offset);
            return fileBytes;
        }

        public override byte[] ExportAsDocument()
        {
            if (RoomDefinition == null) throw new Exceptions.NotInitializedException();

            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(RoomDefinition.GetType());
            xmlSerializer.Serialize(memoryStream, RoomDefinition);

            return memoryStream.ToArray();
        }
    }
}