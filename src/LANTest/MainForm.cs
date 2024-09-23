using DllLog;
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
using System.Resources;
using System.Windows.Forms;
using System.Net.NetworkInformation;

using System.Linq;
using System.Runtime.InteropServices;
using NativeWifi;

public static class WlanRadio
{
    public static string[] GetInterfaceNames()
    {
        using (var client = new WlanClient())
        {
            return client.Interfaces.Select(x => x.InterfaceName).ToArray();
        }
    }

    public static bool TurnOn(string interfaceName)
    {
        var interfaceGuid = GetInterfaceGuid(interfaceName);
        if (!interfaceGuid.HasValue)
            return false;

        return SetRadioState(interfaceGuid.Value, Wlan.Dot11RadioState.On);
    }

    public static bool TurnOff(string interfaceName)
    {
        var interfaceGuid = GetInterfaceGuid(interfaceName);
        if (!interfaceGuid.HasValue)
            return false;

        return SetRadioState(interfaceGuid.Value, Wlan.Dot11RadioState.Off);
    }

    private static Guid? GetInterfaceGuid(string interfaceName)
    {
        using (var client = new WlanClient())
        {
            return client.Interfaces.FirstOrDefault(x => x.InterfaceName == interfaceName)?.InterfaceGuid;
        }
    }

    private static bool SetRadioState(Guid interfaceGuid, Wlan.Dot11RadioState radioState)
    {
        var state = new Wlan.WlanPhyRadioState
        {
            dwPhyIndex = (int)Wlan.Dot11PhyType.Any,
            dot11SoftwareRadioState = radioState,
        };
        var size = Marshal.SizeOf(state);

        var pointer = IntPtr.Zero;
        try
        {
            pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(state, pointer, false);

            var clientHandle = IntPtr.Zero;
            try
            {
                uint negotiatedVersion;
                var result = Wlan.WlanOpenHandle(
                    Wlan.WLAN_CLIENT_VERSION_LONGHORN,
                    IntPtr.Zero,
                    out negotiatedVersion,
                    out clientHandle);
                if (result != 0)
                    return false;

                result = Wlan.WlanSetInterface(
                    clientHandle,
                    interfaceGuid,
                    Wlan.WlanIntfOpcode.RadioState,
                    (uint)size,
                    pointer,
                    IntPtr.Zero);

                return (result == 0);
            }
            finally
            {
                Wlan.WlanCloseHandle(
                    clientHandle,
                    IntPtr.Zero);
            }
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }

    public static string[] GetAvailableNetworkProfileNames(string interfaceName)
    {
        using (var client = new WlanClient())
        {
            var wlanInterface = client.Interfaces.FirstOrDefault(x => x.InterfaceName == interfaceName);
            if (wlanInterface == null)
            {
                string[] emptyStringArray = new string[0];
                return emptyStringArray;
            }

            return wlanInterface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles)
                .Select(x => x.profileName)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
        }
    }

    public static void ConnectNetwork(string interfaceName, string profileName)
    {
        using (var client = new WlanClient())
        {
            var wlanInterface = client.Interfaces.FirstOrDefault(x => x.InterfaceName == interfaceName);
            if (wlanInterface == null)
                return;

            wlanInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }
    }
}

namespace LANTest
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        public Boolean IsAutoPass = false ;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();
            
            SetString();

            //LANTest();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion // Constructor

        private void LANTest()
        {
            if (Program.ProgramArgs == null || Program.ProgramArgs.Length == 0)
            {
                PassBtn.Visible = false;
                ResultLbl.Text = "Please provide correct value in SFTConfig";
                Log.LogError("LANTest Fail: Please provide correct value in SFTConfig");
            }
            else if (Program.ProgramArgs[0].ToString().ToUpper() == "NULL")
            {
                //Autopass not enabled, display buttons
                PassBtn.Visible = true;
                FailBtn.Visible = true;
                RetryBtn.Visible = true;
                IsAutoPass = false;
                Ping(IsAutoPass, Program.ProgramArgs[1].ToString(), 0);
            }
            else
            {
                PassBtn.Visible = false;
                FailBtn.Visible = false;
                RetryBtn.Visible = false;
                IsAutoPass = true;
                Ping(IsAutoPass, Program.ProgramArgs[1].ToString(), Int32.Parse(Program.ProgramArgs[0].ToString()));
            }
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("LAN") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");

        }

        private void RetryBtn_Click(object sender, EventArgs e)
        {
            ResultLbl.Text = "";

            if(TurnRadioOnOff("Off"))
            {
                LANTest();
                TurnRadioOnOff("On");
            }
            else
            {
                PassBtn.Visible = false;
                ResultLbl.Text = "Failed to turn off Wifi";
                Log.LogError("LANTest Fail: Can't turn off Wifi");
            }




        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (TurnRadioOnOff("Off"))
            {
                LANTest();
                TurnRadioOnOff("On");
            }
            else
            {
                PassBtn.Visible = false;
                ResultLbl.Text = "Failed to turn off Wifi";
                Log.LogError("LANTest Fail: Can't turn off Wifi");
            }
        }

        private bool TurnRadioOnOff(string OnOrOff)
        {
            bool bSuccess = true;
            string[] enumDevice = WlanRadio.GetInterfaceNames();
            if (OnOrOff == "Off")
            {
                foreach (string deviceName in enumDevice)
                {
                    bSuccess &= WlanRadio.TurnOff(deviceName);
                }
            }
            else if (OnOrOff == "On")
            {
                foreach (string deviceName in enumDevice)
                {
                    bSuccess &= WlanRadio.TurnOn(deviceName);
                }
            }
            return bSuccess;
        }



        private void Ping(Boolean IsAutoPass, string IPAddress, int timeout)
        {
            Ping p = new Ping();
            PingReply r;

            try
            {
                if (IsAutoPass == true)
                {
                    r = p.Send(IPAddress, timeout);
                    if (r.Status == IPStatus.Success)
                    {
                        Log.LogError("LANTest Pass: " + "Ping to " + IPAddress.ToString() + " Successful");
                        Program.ExitApplication(0);

                    }
                    else
                    {
                        Log.LogError("LANTest Fail: " + "Fail to Ping " + IPAddress.ToString());
                        Program.ExitApplication(255);
                    }
                }
                else
                {
                    r = p.Send(IPAddress);
                    if (r.Status == IPStatus.Success)
                    {
                        ResultLbl.Text = "Ping to " + IPAddress.ToString() + " Successful, "
                           + " Response delay = " + r.RoundtripTime.ToString() + " ms" + "\n";
                    }
                    else
                    {
                        ResultLbl.Text = "Fail to Ping " + IPAddress.ToString() + "\n";
                    }
                }
            }
            catch (Exception e)
            {
                ResultLbl.Text = "Fail to Ping " + IPAddress.ToString() + "\n";
                Log.LogError(e.ToString());

                if (IsAutoPass == true)
                {
                    Program.ExitApplication(255);
                }

            }
        }
    }
}
