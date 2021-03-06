﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ConsoleApplication1
{


    class Program
    {
        [DllImport("wininet.dll")]
        public static extern Boolean InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        static void Main(string[] args)
        {

            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            int value = int.Parse(registry.GetValue("ProxyEnable").ToString());
            value = 1 - value;
            registry.SetValue("ProxyEnable", value);
            registry.Close();

            bool settingsReturn, refreshReturn;
            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);

            System.Console.WriteLine("ProxyEnable is set to " + value);
        }
    }
}
