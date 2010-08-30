﻿using System;
using Microsoft.Win32;

namespace Reanimator
{
    public abstract class Config
    {
        const string Key = @"SOFTWARE\Reanimator";
        static readonly RegistryKey RootKey = Registry.CurrentUser.CreateSubKey(Key);
        static readonly RegistryKey Configkey = RootKey.CreateSubKey("config");

        private static T GetValue<T>(string name, T defaultValue)
        {
            if (typeof(T) == typeof(Boolean))
            {
                Object ret;
                if ((bool)(Object)defaultValue)
                {
                    ret = Configkey.GetValue(name, 1);
                }
                else
                {
                    ret = Configkey.GetValue(name, 0);
                }

                return (T)(Object)((int)ret == 0 ? false : true);
            }

            return (T)Configkey.GetValue(name, defaultValue);
        }

        private static void SetValue(string name, Object value)
        {
            if (value.GetType() == typeof(String))
            {
                Configkey.SetValue(name, value, RegistryValueKind.String);
            }
            else if (value.GetType() == typeof(Int32) || value.GetType() == typeof(Int16))
            {
                Configkey.SetValue(name, value, RegistryValueKind.DWord);
            }
            else if (value.GetType() == typeof(Int64))
            {
                Configkey.SetValue(name, value, RegistryValueKind.QWord);
            }
            else if (value.GetType() == typeof(String[]))
            {
                Configkey.SetValue(name, value, RegistryValueKind.MultiString);
            }
            else if (value.GetType() == typeof(Boolean))
            {
                SetValue(name, ((bool) value) ? 1 : 0);
            }

            Configkey.Flush();
        }

        public static string HglDir
        {
            get { return GetValue("HglDir", @"C:\Program Files\Flagship Studios\Hellgate London"); }
            set { SetValue("HglDir", value); }
        }

        public static string HglDataDir
        {
            get { return GetValue("HglDir", @"C:\Program Files\Flagship Studios\Hellgate London\data"); }
            set { SetValue("HglDataDir", value); }
        }

        public static string SaveDir
        {
            get { return GetValue("SaveDir", String.Format(@"C:\Users\{0}\Documents\My Games\Hellgate\Save\Singleplayer", Environment.UserName)); }
            set { SetValue("SaveDir", value); }
        }

        public static string ScriptDir
        {
            get { return GetValue("ScriptDir", @"C:\Program Files\Flagship Studios\Hellgate London\Reanimator\Scripts"); }
            set { SetValue("ScriptDir", value); }
        }

        public static string GameClientPath
        {
            get { return GetValue("GameClientPath", @"C:\Program Files\Flagship Studios\Hellgate London\SP_x32\hellgate_sp_dx9_x32.exe"); }
            set { SetValue("GameClientPath", value); }
        }

        public static int ClientHeight
        {
            get { return GetValue("ClientHeight", 500); }
            set { SetValue("ClientHeight", value); }
        }

        public static int ClientWidth
        {
            get { return GetValue("ClientWidth", 700); }
            set { SetValue("ClientWidth", value); }
        }

        public static string IntPtrCast
        {
            get { return GetValue("IntPtrCast", "hex"); }
            set { SetValue("IntPtrCast", value); }
        }
    }
}
