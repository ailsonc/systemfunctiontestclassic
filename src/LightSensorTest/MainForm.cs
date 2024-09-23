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
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Windows.Devices.Sensors;
using Microsoft.Win32;

namespace LightSensorTest
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        System.Timers.Timer _timer = new System.Timers.Timer();
        DateTime time_start;
        DateTime time_end;
        public Boolean AutoPass;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();
            InitializeLightSensor();
            SetString();
            Label.CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;

            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
            GetOrientation();
        }

        #endregion // Constructor

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs events)
        {
            GetOrientation();
        }

        /// <summary>
        /// Get screen orientation and set label location
        /// </summary>
        private void GetOrientation()
        {
            float percentX = 0.44f;
            float percentY = 0.13f;

            Rectangle theScreenRect = Screen.GetBounds(this);

            if (theScreenRect.Height > theScreenRect.Width)
            {
                //Run the application in portrait, as in:
                percentX = theScreenRect.Width * percentX;
                percentY = theScreenRect.Height * percentY;
                labelLight.Location = new Point((int)percentX, (int)percentY);
            }
            else
            {
                //Run the application in landscape, as in:
                percentX = theScreenRect.Width * percentX;
                percentY = theScreenRect.Height * percentY;
                labelLight.Location = new Point((int)percentX, (int)percentY);
            }
        }


        /// <summary>
        /// Initialize Accelerometer timer and update Accelerometer every 500ms
        /// </summary>
        private void InitializeLightSensor()
        {

            //Set Timer to update Light 
            _timer.Interval = 500;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateLightSensor);
            _timer.Start();
            time_start = DateTime.Now;
            //Check if AutoPass enabled
            if (Program.ProgramArgs == null || Program.ProgramArgs.Length == 0)
            {
                //Autopass not enabled, display buttons
                PassBtn.Visible = true;
                FailBtn.Visible = true;
                AutoPass = false;
            }
            else //AutoPass enabled
            {
                PassBtn.Visible = false;
                FailBtn.Visible = false;
                AutoPass = true;
            }

        }

        /// <summary>
        /// Update Light Sensor readings event and update UI
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void UpdateLightSensor(object sender, EventArgs e)
        {
            try
            {
                LightSensor light = LightSensor.GetDefault();

                    if (light != null )
                    {

                        SensorLabel.Text = LocRM.GetString("ALSReading") + ": " + light.GetCurrentReading().IlluminanceInLux.ToString();

                        int colorValue = (int)Math.Min(light.GetCurrentReading().IlluminanceInLux, 255);
                        labelLight.ForeColor = Color.FromArgb(colorValue, colorValue, 0);

                        time_end = DateTime.Now;

                        if (AutoPass == true)
                        {
                            //If Lux meets the value, then Pass
                            if (light.GetCurrentReading().IlluminanceInLux <= Convert.ToDouble(Program.ProgramArgs[0]))
                            {
                                _timer.Stop();
                                Program.ExitApplication(0);
                            }
                            //Time is up, close the program and fail the item
                            else if (((TimeSpan)(time_end - time_start)).TotalSeconds > Convert.ToDouble(Program.ProgramArgs[1]))
                            {
                                _timer.Stop();
                                Program.ExitApplication(255);
                            }
                        }

                    }
                    else
                    {
                        SensorLabel.Text = LocRM.GetString("NotFound");
                        _timer.Stop();
                    }


            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                _timer.Stop();
            }
        }


        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Program.ExitApplication(0);
        }


        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Program.ExitApplication(255);
        }


        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            //Title.Text = LocRM.GetString("Light") + LocRM.GetString("Test");
            Title.Text = (AutoPass == true) ? LocRM.GetString("HideSensor") : LocRM.GetString("Light") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");

                

        }

    }
}
