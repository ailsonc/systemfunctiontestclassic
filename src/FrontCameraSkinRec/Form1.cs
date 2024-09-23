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
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.IO;

namespace FrontCameraSkinRec
{
    
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        private bool is_playing = false;

        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            InitMeiadPlayer();
            SetString();

            CameraTest();
           
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
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true; 
        }
        /// <summary>
        /// Initializes windows mediaplayer location
        /// </summary>
        private void InitMeiadPlayer()
        {
            int _x = 0;
            int _y = 0;

            //Set background color
            string hex = Program.ProgramArgs[1];
            panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);

            _x = Math.Abs((panel1.Width - axWindowsMediaPlayer1.Width) / 2);
            _y = Math.Abs((panel1.Height - axWindowsMediaPlayer1.Height) / 2);

            axWindowsMediaPlayer1.Location = new Point(_x, _y);
            axWindowsMediaPlayer1.Visible = true;
        }
        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            this.Text = LocRM.GetString("FrontCameraRec");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            ResetBtn.Text = LocRM.GetString("Retry");
            InstructionLbl.Text = LocRM.GetString("FrontCameraRecHint");
            Playbtn.Text = ""; //Play sign
        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            TableLayoutRowStyleCollection tableStyle = tableLayoutPanel1.RowStyles;
            InstructionLbl.Visible = false;
            tableStyle[0].Height = 5;
            if (is_playing)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                axWindowsMediaPlayer1.URL = null;
                axWindowsMediaPlayer1.close();
            }
            this.Hide();
            CameraTest();
            this.Show();
        }
        /// <summary>
        /// Execute the FrontCamera.exe that generate from FrontCaptureEngine project.
        /// </summary>
        void CameraTest()
        {
            string videoPath;
            string filePath = System.Environment.CurrentDirectory;
            string Camera_filename = "FrontCameraRec.exe";
            string camera_friendlyName = null;

            // Delete *.jpg, if it exits
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Directory.GetCurrentDirectory();
            videoPath = path + "\\videoTest.wmv";
            if (System.IO.File.Exists(videoPath))
                System.IO.File.Delete(videoPath);

            /*parameter 2 is camera friendlyname*/
            if (Program.ProgramArgs != null)
            {
                if (Program.ProgramArgs.Count > 2)
                {
                    if (Program.ProgramArgs[2] != "NULL")
                        camera_friendlyName = Program.ProgramArgs[2];
                }
            }

            //Excute camera FrontCamera.exe
            Process proc = null;
            ProcessStartInfo startInfo = null;

            int ReturnVal = 0;
            try
            {
                proc = new Process();
                startInfo = new ProcessStartInfo
                {
                    FileName = Camera_filename,
                    Arguments = camera_friendlyName,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    CreateNoWindow = true
                };
                proc.StartInfo = startInfo;

                proc.Start();
                proc.WaitForExit();
                ReturnVal = proc.ExitCode;
                proc.Close();
                startInfo = null;
            }
            finally
            {
                if (proc != null)
                {
                    proc.Dispose();
                    proc = null;
                }
            }



            // Show photo on the Screen
            if (ReturnVal == 999)
            {
                TableLayoutRowStyleCollection tableStyle = tableLayoutPanel1.RowStyles;
                tableStyle[0].Height = 200;
                InstructionLbl.Visible = true;
                //PhotoPath = System.Windows.Forms.Application.StartupPath + "\\ConnectToCameraFail.jpg";
                PassBtn.Visible = false;
                Playbtn.Visible = false;

                axWindowsMediaPlayer1.Visible = false;

            }
            else
            {
                PassBtn.Visible = true;
                Playbtn.Enabled = true;
                Playbtn.BackColor = Color.Aqua;

            }

            //Double check for playbtn, if video doesn't exist, disable play button
            if (!System.IO.File.Exists(videoPath))
            {
                Playbtn.Enabled = false;
                Playbtn.BackColor = Color.Gray;
                PassBtn.Visible = false;
                return;
            }              
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WinMediaPlayer(is_playing);
            is_playing = !is_playing;
        }

        /// <summary>
        /// To determine if media player needs to be turn on or off.
        /// </summary>
        /// <param name="sw">switch.</param>
        private void WinMediaPlayer(bool sw)
        {
            try
            {
                if (!sw)
                {
                    string videoPath;
                    //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string path = Directory.GetCurrentDirectory();
                    videoPath = path + "\\videoTest.wmv";
                    axWindowsMediaPlayer1.URL = videoPath;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    Playbtn.Text = ""; //Stop sign
                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    Playbtn.Text = ""; //Play sign
                    axWindowsMediaPlayer1.URL = null;
                    axWindowsMediaPlayer1.close();
                }

            }
            catch (Exception ex)
            {
                DllLog.Log.LogError("Cannot load image or close the *Camera.exe invalid. : " + ex.ToString());
            }

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, EventArgs e)
        {
            // Stopped Playback of the current media item is stopped
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                WinMediaPlayer(true);
                is_playing = false;
            }

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                int width = 0;
                int height = 0;
                int _x = 0;
                int _y = 0;

                //Set windows media player size and location                
                width = axWindowsMediaPlayer1.currentMedia.imageSourceWidth;
                height = axWindowsMediaPlayer1.currentMedia.imageSourceHeight;

                if (width > panel1.Width)
                    width = panel1.Width;
                if (height > panel1.Height)
                    height = panel1.Height;

                axWindowsMediaPlayer1.Size = new Size(width, height);
                _x = Math.Abs((panel1.Width - axWindowsMediaPlayer1.Size.Width) / 2);
                _y = Math.Abs((panel1.Height - axWindowsMediaPlayer1.Size.Height) / 2);

                axWindowsMediaPlayer1.Location = new Point(_x, _y);
            }
        }

    }
}
