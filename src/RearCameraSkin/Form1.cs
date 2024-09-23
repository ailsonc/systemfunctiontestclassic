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

namespace FrontCameraSkin
{
    
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
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
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            this.Text = LocRM.GetString("RearCamera");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            ResetBtn.Text = LocRM.GetString("Retry");
            InstructionLbl.Text = LocRM.GetString("RearCameraHint");
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
            this.Hide();
            CameraTest();
            this.Show();
        }
        /// <summary>
        /// Execute the RearCamera.exe that generate from RearCaptureEngine project.
        /// </summary>
        void CameraTest()
        {
            string PhotoPath;
            string filePath = System.Environment.CurrentDirectory;
            string Camera_filename = "RearCamera.exe";
            string camera_friendlyName = null;

            //Set background color
            string hex = Program.ProgramArgs[1];
            panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);

            // Delete *.jpg, if it exits
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Directory.GetCurrentDirectory();
            PhotoPath = path + "\\CameraTest.jpg";
            if (System.IO.File.Exists(PhotoPath))
                System.IO.File.Delete(PhotoPath);

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
                PassBtn.Visible = false;
                PhotoPath = System.Windows.Forms.Application.StartupPath + "\\ConnectToCameraFail.jpg";
            }
            else
            {
                PassBtn.Visible = true;

            }

            Bitmap bmPhoto = null;
            if (!System.IO.File.Exists(PhotoPath)) return;

            try
            {
                bmPhoto = new Bitmap(PhotoPath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.LoadAsync(PhotoPath);
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError("Cannot load image or close the *Camera.exe invalid. : " + ex.ToString());
            }
            finally
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                    bmPhoto.Dispose();
                    bmPhoto = null;
                }
            }
        }

    }
}
