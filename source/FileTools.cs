﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Reanimator
{
    public class FileTools
    {
        public static byte[] Extract(FileStream dataBuffer, int offset, int compressedSize, int uncompressedSize)
        {
            if (compressedSize > 0)
            {
                byte[] buffer = new byte[compressedSize];
                dataBuffer.Seek(offset, SeekOrigin.Begin);
                dataBuffer.Read(buffer, 0, compressedSize);

                ManagedZLib.CompressionStream ms = new ManagedZLib.CompressionStream(new MemoryStream(buffer), ManagedZLib.CompressionOptions.Decompress);
                return StreamToByteArray(ms);
            }
            else
            {
                byte[] buffer = new byte[uncompressedSize];
                //buffer = StreamToByteArray(dataBuffer);
                return buffer;
            }
        }

        public static byte[] Compress(byte[] buffer)
        {
            ManagedZLib.CompressionStream ms = new ManagedZLib.CompressionStream(new MemoryStream(buffer), ManagedZLib.CompressionOptions.Compress);
            byte[] buffer2 = StreamToByteArray(ms);
            return buffer2;
        }

        public static byte[] StreamToByteArray(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[1024];
                int bytes;
                while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytes);
                }
                byte[] output = ms.ToArray();
                return output;
            }
        }

        public static void ByteArrayToStructure(byte[] byteArray, ref object obj, int offset, int length)
        {
            if (length == 0)
            {
                length = Marshal.SizeOf(obj);
            }

            IntPtr i = Marshal.AllocHGlobal(length);
            Marshal.Copy(byteArray, offset, i, length);

            obj = Marshal.PtrToStructure(i, obj.GetType());

            Marshal.FreeHGlobal(i);
        }

        public static object ByteArrayToStructure(byte[] byteArray, Type type, int offset)
        {
            object obj = Activator.CreateInstance(type);
            ByteArrayToStructure(byteArray, ref obj, offset, 0);
            return obj;
        }

        public static object ByteArrayToStructure(byte[] byteArray, Type type, int offset, int length)
        {
            object obj = Activator.CreateInstance(type);
            ByteArrayToStructure(byteArray, ref obj, offset, length);
            return obj;
        }

        public static Int32[] ByteArrayToInt32Array(byte[] byteArray, int offset, int count)
        {
            Int32[] int32Array = new Int32[count];

            IntPtr bytePtr = Marshal.UnsafeAddrOfPinnedArrayElement(byteArray, offset);
            Marshal.Copy(bytePtr, int32Array, 0, count);

            return int32Array;
        }

        public static int ByteArrayContains(byte[] byteArray, byte[] searchFor)
        {
            for (int i = 0; i < byteArray.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < searchFor.Length; j++)
                {
                    if (searchFor[j] == 0x90)
                    {
                        continue;
                    }

                    if (byteArray[i + j] != searchFor[j])
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}