//*********************************************************
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

namespace Video
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static int iExitCode = 0;
        public static string ProgramDevice = String.Empty;
        public static string ProgramFile = String.Empty;

        [STAThread]
        public static void ExitApplication(int exitCode)
        {
            Program.iExitCode = exitCode;
            Application.Exit();
        }

        [STAThread]
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
            catch (CultureNotFoundException ex)
            {
                DllLog.Log.LogFinish($"CultureNotFoundException: {ex.Message}");
            }
        }

        [STAThread]
        static int Main(String[] args)
        {           
            if (args.Length.Equals(3))
            {
                SetLang(args[0]);
                ProgramFile = args[1].ToString();
                ProgramDevice = args[2].ToString();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                DllLog.Log.LogFinish($"Exit: {iExitCode}");
                return iExitCode;
            }
            else 
            {
                iExitCode = 255;
                DllLog.Log.LogFinish($"Exit: {iExitCode}");
                return iExitCode;
            }
        }
    }
}
