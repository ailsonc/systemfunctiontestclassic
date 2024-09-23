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

namespace GyrometerTest
{
    public partial class MainForm : Form
    {
        #region Fields

        private static Image imageX;
        private static Image imageY;
        private static Image imageZ;
        private static ResourceManager LocRM;
        System.Timers.Timer _timer = new System.Timers.Timer();
        DateTime time_start;
        DateTime time_end;
        public Boolean AutoPass;
        private Boolean flag_Action1 = false;
        private Boolean flag_Action2 = false;
        #endregion //Fields

        #region Contructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();            
            SetString();
            InitializeGyrometer();
            Label.CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Landscape;

            imageX = pictureBoxX.Image;
            imageY = pictureBoxY.Image;
            imageZ = pictureBoxZ.Image;
        }

        #endregion //Contructor

        /// <summary>
        /// Initialize Gyrometer timer and update Gyrometer every 500ms
        /// </summary>
        private void InitializeGyrometer()
        {
            //Check if AutoPass enabled
            time_start = DateTime.Now;

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
                Title.Text = LocRM.GetString("Action1");
                DllLog.Log.LogError("AcceleroMeter AutoPass: Execute Action 1");
            }
            //Set Timer to update Gyrometer
            _timer.Interval = 100;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateGyrometer);
            _timer.Start();
            
        }


        /// <summary>
        /// Update Gyrometer readings event and update UI
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void UpdateGyrometer(object sender, EventArgs e)
        {
            try
            {
                Gyrometer gyrometer = Gyrometer.GetDefault();
                if (gyrometer != null)
                {
                    XPanel.Text = LocRM.GetString("XAxis") + ": " +
                         String.Format("{0,5:0.00}", gyrometer.GetCurrentReading().AngularVelocityX) + "(°)/s";
                    YPanel.Text = LocRM.GetString("YAxis") + ": " +
                         String.Format("{0,5:0.00}", gyrometer.GetCurrentReading().AngularVelocityY) + "(°)/s";
                    ZPanel.Text = LocRM.GetString("ZAxis") + ": " +
                         String.Format("{0,5:0.00}", gyrometer.GetCurrentReading().AngularVelocityZ) + "(°)/s";

                    pictureBoxX.Image = Rotate(imageX, Math.Max(-135, Math.Min(135, gyrometer.GetCurrentReading().AngularVelocityX)));
                    pictureBoxY.Image = Rotate(imageY, Math.Max(-135, Math.Min(135, gyrometer.GetCurrentReading().AngularVelocityY)));
                    pictureBoxZ.Image = Rotate(imageZ, Math.Max(-135, Math.Min(135, gyrometer.GetCurrentReading().AngularVelocityZ)));


                    //Run AutoPass if needed
                    time_end = DateTime.Now;

                    if (AutoPass == true)
                    {
                        //RunAction#1
                        if (flag_Action1 == false && flag_Action2 == false && (gyrometer.GetCurrentReading().AngularVelocityX > 0))
                        {

                                
                                if (RoundDecimals(gyrometer.GetCurrentReading().AngularVelocityX, 0) >= Convert.ToDouble(Program.ProgramArgs[0]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityY) <= Convert.ToInt32(Program.ProgramArgs[4]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityZ) <= Convert.ToInt32(Program.ProgramArgs[4]))
                                {
                                    flag_Action1 = true;
                                    Title.Text = LocRM.GetString("Action2");
                                    DllLog.Log.LogError("Gyrometer AutoPass: Execute Action 2");
                                }

                        }
                        //RunAction#2
                        else if (flag_Action1 == true && flag_Action2 == false && (gyrometer.GetCurrentReading().AngularVelocityZ < 0))
                        {

                                if (RoundDecimals(gyrometer.GetCurrentReading().AngularVelocityZ, 0) <= Convert.ToDouble(Program.ProgramArgs[1]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityY) <= Convert.ToInt32(Program.ProgramArgs[4]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityX) <= Convert.ToInt32(Program.ProgramArgs[4]))
                                {
                                    flag_Action2 = true;
                                    Title.Text = LocRM.GetString("Action3");
                                    DllLog.Log.LogError("Gyrometer AutoPass: Execute Action 3");
                                }

                        }
                        //RunAction#3
                        else if (flag_Action1 == true && flag_Action2 == true && gyrometer.GetCurrentReading().AngularVelocityY < 0)
                        {
                                if (RoundDecimals(gyrometer.GetCurrentReading().AngularVelocityY, 0) <= Convert.ToDouble(Program.ProgramArgs[2]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityZ) <= Convert.ToInt32(Program.ProgramArgs[4]) &&
                                    Math.Abs(gyrometer.GetCurrentReading().AngularVelocityX) <= Convert.ToInt32(Program.ProgramArgs[4]))
                                {
                                    DllLog.Log.LogError("Gyrometer AutoTest - Pass");
                                    _timer.Stop();
                                    Program.ExitApplication(0);
                            }

                        }
                        //Time is up
                        else if (((TimeSpan)(time_end - time_start)).TotalSeconds > Convert.ToInt32(Program.ProgramArgs[3]))
                        {
                            _timer.Stop();
                            Program.ExitApplication(255);
                            DllLog.Log.LogError("Gyrometer AutoPass: Fail - Time is up");
                        }
                        ///////////////

                    }
                }
                else
                {
                    XPanel.Text = LocRM.GetString("NotFound");

                    if (AutoPass == true)
                    {
                        _timer.Stop();
                        Program.ExitApplication(255);
                        DllLog.Log.LogError("Gyrometer AutoPass: Fail - Device not found");
                    }
                    else
                    {
                        _timer.Stop();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                XPanel.Text = LocRM.GetString("Error");
                _timer.Stop();

            }
        }

        public double RoundDecimals(double input, int digit)
        {
            if (digit < 0) { return Math.Floor(input); }
            double pow = Math.Pow(10, digit);
            double sign = input >= 0 ? 1 : -1;
            return sign * Math.Floor(sign * input * pow) / pow;
        }
        /// <summary>
        /// Rotates the image
        /// </summary>
        /// <param name="image">image to rotate</param>
        /// <param name="angle">angle to rotate image by.</param>
        public Bitmap Rotate(Image image, double angle)
        {
            Bitmap bitmap = null;

            try
            {
                bitmap = new Bitmap(image.Width, image.Height);
                Graphics g = Graphics.FromImage(bitmap);

                g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                g.RotateTransform((float)angle);
                g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
                g.DrawImage(image, image.Width / 2 - image.Height / 2, image.Height / 2 - image.Width / 2, image.Height, image.Width);
            }
            catch
            {
            }

            return bitmap;
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
            Title.Text = LocRM.GetString("Gyrometer") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }

    }
}
