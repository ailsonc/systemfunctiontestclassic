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
using System.Globalization;
using System.Windows.Forms;

namespace KeyboardFullTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                SetLang(args[0]); //first argument always language code
                ProgramArgs = new string[args.Length - 1];
                Array.Copy(args, 1, ProgramArgs, 0, args.Length - 1);
            }
            else
            {
                ProgramArgs = new string[0];
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            return ExitCode;
        }

        static int ExitCode = 255;
        public static string[] ProgramArgs;

        public static void ExitApplication(int exitCode)
        {
            Program.ExitCode = exitCode;
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
    }
}
