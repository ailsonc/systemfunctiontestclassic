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
using System.Resources;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Windows.Controls;
using System.Drawing;
using System.Threading;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi;
using DllLog;

namespace Video
{

    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        private string FilePath = Directory.GetCurrentDirectory();
        private string path;
        private Speak speak;
        private bool isScreenConnected = false;
        private MediaElement MediaPlayer = new MediaElement();
        private int numberApproved;
        private Thread thread;
        private CoreAudioController coreAudioController = new CoreAudioController();
        private CoreAudioDevice defaultOutputDevice;
        private string deviceName = "HDMI";

        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        /// <param name="videoPath">The path of media file and it can be set on the SFTConfig.xml.</param>
        public Form1()
        {
            try
            {
                LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
                InitializeComponent();
                SetString();
                path = FilePath + @"\" + Program.ProgramFile.ToString();
                Log.LogComment(DllLog.Log.LogLevel.Info, "Video path: " + Program.ProgramFile.ToString());
                this.deviceName = Program.ProgramDevice.ToString();
                Log.LogComment(DllLog.Log.LogLevel.Info, "Video deviceName xml: " + this.deviceName);
                /*Init ElementHost*/
                elementHost1.Child = MediaPlayer;
                MediaPlayer.LoadedBehavior = MediaState.Manual;
                MediaPlayer.UnloadedBehavior = MediaState.Close;
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
                Program.ExitApplication(255);
                Application.ExitThread();
                Application.Exit();
            }
        }

        /// <summary>
        /// Play the media.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
                this.WindowState = FormWindowState.Maximized;
                InitMediaPlayer();
                if (isScreenConnected)
                {
                    SetDefaultDevice();
                    UptadeDefaultDevices();
                    Sound();
                }
            } 
            catch (Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
                Program.ExitApplication(255);
                Application.ExitThread();
                Application.Exit();
            }      
        }

        /// <summary>
        /// Set Default Devices.
        /// </summary>
        private void SetDefaultDevice()
        {
            foreach(var device in this.coreAudioController.GetPlaybackDevices(DeviceState.Active))
            {
                if (device.FullName.Contains(deviceName))
                {
                    Log.LogComment(DllLog.Log.LogLevel.Info, $"Dispositivo selecionado: {device.FullName}");
                    device.SetAsDefault();
                }
                else
                {
                    Log.LogComment(DllLog.Log.LogLevel.Info, $"Dispositivo: {device.FullName}");
                }
            }
        }

        /// <summary>
        /// Uptade Default Devices.
        /// </summary>
        private void UptadeDefaultDevices()
        {
            this.defaultOutputDevice = this.coreAudioController.DefaultPlaybackDevice;
            this.DeviceNameLbl.Text = this.defaultOutputDevice.FullName;
            Log.LogComment(DllLog.Log.LogLevel.Info, "Video deviceName: " + this.defaultOutputDevice.FullName);
        }

        /// <summary>
        /// Sound.
        /// </summary>
        private void Sound()
        {
            this.numberApproved = GenerateNumber(1, 10);
            speak = new Speak(Convert.ToString(this.numberApproved));
            this.thread = new Thread(new ThreadStart(speak.Run));
            this.thread.Start();
        }

        /// <summary>
        /// GenerateNumber.
        /// </summary>
        private int GenerateNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        /// <summary>
        /// InitMediaPlayer.
        /// </summary>
        private void InitMediaPlayer()
        {
            try
            {
                if (!isScreenConnected)
                {
                    Process DisplayDuplicate = new Process
                    {
                        StartInfo =
                        {
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = "DisplaySwitch.exe",
                            Arguments = "/clone"
                        }
                    };

                    ManagementObjectSearcher monitorObjectSearch = new ManagementObjectSearcher("select * from Win32_PnPEntity where service = 'monitor'");
                    int monitorCount = monitorObjectSearch.Get().Count;
                    if (monitorCount > 1)
                    {
                        isScreenConnected = true;
                        DisplayDuplicate.Start(); //Switch display mode to duplicate
                    }
                    else
                    {
                        ErrorLbl.Text = LocRM.GetString("ExtDisplay_notfound");
                        ErrorLbl.Visible = true;
                        PassBtn.Visible = false;
                    }

                }

                if (isScreenConnected)
                {
                    if (!System.IO.File.Exists(path))// Cannot load media file
                    {
                        DllLog.Log.LogError("Cannot load media file");
                        ErrorLbl.Text = LocRM.GetString("ExtDisplay_Err");
                        ErrorLbl.Visible = true;
                        PassBtn.Visible = false;
                        elementHost1.BackColor = Color.FromArgb(64, 64, 64);
                    }
                    else
                    {
                        elementHost1.BackColor = Color.FromArgb(0, 0, 0);
                        MediaPlayer.Source = new Uri(@path, UriKind.Relative);
                        MediaPlayer.Play();
                        MediaPlayer.Volume = 0.1;
                        ErrorLbl.Visible = false;
                        PassBtn.Visible = false;
                    }
                }  
            } 
            catch (Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
                ErrorLbl.Text = LocRM.GetString("ExtDisplay_Err");
                ErrorLbl.Visible = true;
                PassBtn.Visible = false;
                elementHost1.BackColor = Color.FromArgb(64, 64, 64);
                Program.ExitApplication(255);
                Application.ExitThread();
                Application.Exit();
            }
        }

        /// <summary>
        /// Form1.KeyDown Event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData.ToString().Contains($"{this.numberApproved}") && isScreenConnected)
                {
                    DllLog.Log.LogPass($"Número digitado {e.KeyData} contém {this.numberApproved}");
                    speak.Sleep();
                    MediaPlayer.Stop();
                    DllLog.Log.LogFinish($"Sucesso");
                    Program.ExitApplication(0);
                    Application.Exit();
                }
                else
                {
                    DllLog.Log.LogError($"Número digitado {e.KeyData} não contém {this.numberApproved}");
                    speak.Sleep();
                    DllLog.Log.LogFinish($"Fail");
                    MediaPlayer.Stop();
                    Program.ExitApplication(255);
                    Application.Exit();
                }
            } 
            catch(Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
            }
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PASS_Click(object sender, EventArgs e)
        {
            if (speak is null)
            {
                Program.ExitApplication(0);
                Application.Exit();
            }
            else
            {
                speak.Sleep();
                Program.ExitApplication(0);
                Application.Exit();
            }   
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FAIL_Click(object sender, EventArgs e)
        {
            if (speak is null)
            {
                Program.ExitApplication(255);
                Application.Exit();
            }
            else
            {
                speak.Sleep();
                Program.ExitApplication(255);
                Application.Exit();
            }            
        }

        /// <summary>
        ///  RetryBtn.Click Event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RetryBtn_Click(object sender, EventArgs e)
        {
            ErrorLbl.Visible = false;
            InitMediaPlayer();
            if (isScreenConnected)
                Sound();
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            this.Text = LocRM.GetString("ExtDisplay");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");
        }
    }
}
