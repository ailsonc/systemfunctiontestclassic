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
using System.Resources;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using DllComponent;
using DllLog;

namespace Battery
{
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        TimeSpan Timecurr = new TimeSpan(DateTime.Now.Ticks);

        double batteryNow = 0;
        double batteryInput = 0;
        long initialCapacity = 0;
        uint prepwrstate = 0;

        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();

            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;

            /*Update Batt info per 5 sec*/
            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(Update);
            t.AutoReset = true;
            t.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            QueryBattery();
            registerPowerSettingNotification();

            // If autopass argument was given:
            if (Program.ProgramArgs.Length > 0)
            {
                batteryInput = float.Parse(Program.ProgramArgs[0].ToString(), CultureInfo.InvariantCulture);

                if (batteryNow < batteryInput)
                    Program.ExitApplication(255);
                else
                    Program.ExitApplication(0);
            }
        }
        public void Update(object source, System.Timers.ElapsedEventArgs e)
        {
            QueryBattery();
        }
        private void QueryBattery()
        {
            BatteryInfo info = new BatteryInfo();
            CoreComponent component = null;
            try
            {
                component = new CoreComponent();
                info = component.QueryBatteryInfo();
            }
            catch (Exception ex)
            {
                Log.LogError("Cannot get battery info from DllComponent " + ex.ToString());
            }
            finally
            {
                if (component != null)
                {
                    component.Dispose();
                    component = null;
                }
            }

            if (info.Result != 0)
            {
                Log.LogError("Cannot get battery info from DllComponent. Result: " + info.Result);
                PwrStatus.Text = LocRM.GetString("NotFound");
                ChargeStatus.Text = LocRM.GetString("NotFound");
                BatCap.Text = LocRM.GetString("NotFound");
                CharRate.Text = LocRM.GetString("NotFound");
                return;
            }
            // update Power and Charging status
            if (prepwrstate == 0)
            {
                Update_PwrState(info.PowerState);
                Update_ChgRate(info.PowerState, info.Capacity, info.FullCapcity);
                Update_Capcity(info.Capacity, info.FullCapcity);
            }
            else if (prepwrstate != info.PowerState)
            {
                Update_PwrState(info.PowerState);
                Update_ChgRate(info.PowerState, info.Capacity, info.FullCapcity);
                prepwrstate = info.PowerState;
            }
            else
            {
                Update_ChgRate(info.PowerState, info.Capacity, info.FullCapcity);
                Update_Capcity(info.Capacity, info.FullCapcity);
            }
        }

        private void Update_PwrState(uint pwr_state)
        {
            switch (pwr_state)
            {
                case 1:
                    PwrStatus.Text = LocRM.GetString("BatteryOnline");
                    ChargeStatus.Text = LocRM.GetString("BatteryDischarging");
                    break;
                case 2:
                    PwrStatus.Text = LocRM.GetString("BatteryOffline");
                    ChargeStatus.Text = LocRM.GetString("BatteryDischarging");
                    break;
                case 3:
                    PwrStatus.Text = LocRM.GetString("BatteryOnline");
                    ChargeStatus.Text = LocRM.GetString("BatteryDischarging");
                    break;
                case 5:
                    PwrStatus.Text = LocRM.GetString("BatteryOnline");
                    ChargeStatus.Text = LocRM.GetString("BatteryCharging");
                    break;
            }
        }
        private void Update_Capcity(uint info_Capacity, uint info_FullCapcity)
        {
            /*update Battery % */
            batteryNow = (Convert.ToDouble(info_Capacity) / Convert.ToDouble(info_FullCapcity)) * 100;
            if (batteryNow > 100)
                batteryNow = 100;

            Invoke(new MethodInvoker(delegate
            {
                BatCap.Text = batteryNow.ToString("0.00", CultureInfo.InvariantCulture) + "%";
            }));
        }
        private void Update_ChgRate(uint pwr_state, uint info_Capacity, uint info_FullCapcity)
        {
            /*update Charge rate*/
            if (initialCapacity == 0)
                initialCapacity = info_Capacity;
            TimeSpan TimecurrEnd = new TimeSpan(DateTime.Now.Ticks);
            double TimeInterval = (float)TimecurrEnd.Subtract(Timecurr).Duration().TotalSeconds;
            double Charspeed = (Convert.ToInt32(info_Capacity) - initialCapacity) * 1000 / TimeInterval;

            Invoke(new MethodInvoker(delegate
            {
                CharRate.Text = Charspeed.ToString("0.0000", CultureInfo.InvariantCulture) + " mWh";
            }));
        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RetryBtn_Click(object sender, EventArgs e)
        {
            QueryBattery();
            initialCapacity = 0;
            Timecurr = new TimeSpan(DateTime.Now.Ticks);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            //_timerChar.Stop();
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }


        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("Battery") + LocRM.GetString("Test");
            this.Text = LocRM.GetString("Battery");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");
            BatteryLab.Text = LocRM.GetString("Battery_bat");
            PowerLab.Text = LocRM.GetString("Battery_pwrStatus");
            ChargeLab.Text = LocRM.GetString("Battery_chargeStatus");
            ChargingRateLab.Text = LocRM.GetString("Battery_chargeRate");
            NoteLbl.Text = LocRM.GetString("BatteryNote");
        }


        [DllImport(@"User32", SetLastError = true,
            EntryPoint = "RegisterPowerSettingNotification",
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr RegisterPowerSettingNotification(
            IntPtr hRecipient,
            ref Guid PowerSettingGuid,
            Int32 Flags);

        private const int WM_POWERBROADCAST = 0x0218;
        static Guid GUID_BATTERY_PERCENTAGE_REMAINING = new Guid("A7AD8041-B45A-4CAE-87A3-EECBB468A9E1");
        static Guid GUID_ACDC_POWER_SOURCE = new Guid("5d3e9a59-e9D5-4b00-a6bd-ff34ff516548");
        private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0x00000000;

        private void registerPowerSettingNotification()
        {
            IntPtr hWnd = this.Handle;
            IntPtr ret1 = RegisterPowerSettingNotification(hWnd,
                ref GUID_BATTERY_PERCENTAGE_REMAINING,
                DEVICE_NOTIFY_WINDOW_HANDLE);
            IntPtr ret2 = RegisterPowerSettingNotification(hWnd,
                ref GUID_ACDC_POWER_SOURCE,
                DEVICE_NOTIFY_WINDOW_HANDLE);
        }

         //<summary>
         //Gets Windows message. Catch for battery changes. 
         //</summary>
         //<param name="m">The Windows Message to process.</param>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_POWERBROADCAST:
                    QueryBattery();
                    break;
                default:
                    break;
            }

            // Call parent WndProc for default message processing.
            base.WndProc(ref m);
        }


    }

}

