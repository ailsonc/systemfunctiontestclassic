﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Display
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static int iExitCode = 0;
        public static IList<string> ProgramArgs = new List<string>();

        public static void ExitApplication(int exitCode)
        {
            Program.iExitCode = exitCode;
            Application.Exit();
        }
        private static void SetLang(string langCode)
        {
            try
            {

                CultureInfo newCulture = new CultureInfo(langCode);
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name != newCulture.Name)
                {
                    CultureInfo.DefaultThreadCurrentCulture = newCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = newCulture;
                }
            }
            catch (CultureNotFoundException)
            {
            }
        }
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                
                SetLang(args[0]); //first argument always language code
                for (int i = 0; i < args.Length; i++)
                {
                    ProgramArgs.Add(args[i]);
                }
                ProgramArgs.RemoveAt(0);
            }
            //SetLang("zh-Hans");
            iExitCode = 255;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            return iExitCode;
        }
    }
}
