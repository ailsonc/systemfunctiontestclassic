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
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Windows.Media.Capture;
using Windows.Media.Devices;

namespace AudioPlaybackTest
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        private MediaCapture mediaCapture;
        private FileStream outputStream = null;
        private string soundFile;
        private string soundFilePath = Directory.GetCurrentDirectory();
        private string playFilePath;
        private Windows.Media.MediaProperties.MediaEncodingProfile encodingProfile;
        private bool recording; //flag: if currently recording
        private bool recorded; //flag: if recording complete
        private int numberApproved;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();
            InitializePlayingAudio();
            InitializeRecording();
            SetString();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            PassBtn.Visible = false;
            ResetBtn.Visible = false;
        }

        #endregion // Constructor

        /// <summary>
        /// Initialize Playback function. Set audio file path (playFilePath) from application arguments. 
        /// </summary>
        private void InitializePlayingAudio()
        {
            //todo mudar para controle de audio
            if (Program.ProgramArgs == null)
            {

                PlayAudioLbl.Text = LocRM.GetString("AudioFileNotFound");
                InstructionLbl.Text = LocRM.GetString("AudioSpeakerRecord");

                PlayBtn_L.Visible = false;
                PlayBtn_R.Visible = false;
            }
            else if (Program.ProgramArgs.Count < 2)
            {
                PlayAudioLbl.Text = LocRM.GetString("AudioFileNotFound");
                InstructionLbl.Text = LocRM.GetString("AudioSpeakerRecord");

                PlayBtn_L.Visible = false;
                PlayBtn_R.Visible = false;
            }
            else
            {
                playFilePath = soundFilePath + @"\" + Program.ProgramArgs[1];

                CleanProcess();
            }
        }

        /// <summary>
        /// Initialize Recording function. 
        /// </summary>
        private async void InitializeRecording()
        {
            mediaCapture = new Windows.Media.Capture.MediaCapture();
            
            mediaCapture.Failed += MediaCaptureFailed;

            var settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;

            settings.AudioDeviceId = MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Default);

            await mediaCapture.InitializeAsync(settings);

            encodingProfile = Windows.Media.MediaProperties.MediaEncodingProfile.CreateWav(
                Windows.Media.MediaProperties.AudioEncodingQuality.Auto);

            recorded = false; //no audio recorded yet
            recording = false; //recording not started
        }

        /// <summary>
        /// Start recording. Set audio file path (soundFilePath) to Windows.Storage.KnownFolders.MusicLibrary
        /// </summary>
        private async void StartCapture()
        {
            soundFile = @"\AudioTest_" + DateTime.Now.ToString("HHmmss") + ".wav";
            string audioFile = soundFilePath + soundFile;

            try
            {
                FileStream stream = File.OpenWrite(audioFile);
                outputStream = stream;
                DllLog.Log.LogComment(DllLog.Log.LogLevel.Info,"Output file : " + audioFile);
                await mediaCapture.StartRecordToStreamAsync(encodingProfile, stream.AsRandomAccessStream());
                recording = true;
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError("StartCaptureAudioAsync() Exception: " + ex.Message);
                if (outputStream != null)
                {
                    outputStream.Close();
                    outputStream.Dispose();
                    outputStream = null;
                }
                recording = false;
            }
           
        }

        /// <summary>
        /// Stop recording
        /// </summary>
        private async void StopCapture()
        {
            recording = false;

            try
            {
               
                await mediaCapture.StopRecordAsync();
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError("StopCaptureAudioAsync() Exception: " + ex.Message);
            }
            finally
            {
                if (outputStream != null)
                {
                    outputStream.Flush();
                    outputStream.Close();
                    outputStream.Dispose();
                    outputStream = null;
                }
            }

            recorded = true;
            return;
        }

        /// <summary>
        /// Playback audio file based on audio file path (soundFilePath)
        /// </summary>
        private void PlayBack(string playAudio, int balance)
        {
            this.KeyPreview = true;
            this.numberApproved = GenerateNumber();

            WMPLib.IWMPMediaCollection medialist;
            WMPLib.IWMPMedia mediaSRC;
            string audioFile = soundFilePath + soundFile;

            try
            {
                medialist = Player1.mediaCollection;
                mediaSRC = medialist.add(playAudio);
                Player1.currentPlaylist.clear();
                Player1.currentPlaylist.appendItem(mediaSRC);
                Player1.settings.balance = balance;
                Player1.settings.setMode("loop", true);
                Player1.Ctlcontrols.play();
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError("Media play Failed: " + ex.Message);
                PlayBtn_L.Text = "Error";
                PlayBtn_R.Text = "Error";
                PlayBtn_L.BackColor = Color.Crimson;
                PlayBtn_R.BackColor = Color.Crimson;
            }
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Play button.
        /// This will play either the audio file from application argument (input audio file) or the recorded audio file
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PlayBtn_L_Click(object sender, EventArgs e)
        {
            PlayBtn_Button_Click(-100);
        }

        private void PlayBtn_R_Click(object sender, EventArgs e)
        {
            PlayBtn_Button_Click(100);
        }

        private void PlayBtn_Button_Click(int balance)
        {
            this.KeyPreview = true;
            this.numberApproved = GenerateNumber();
            PlayBack($"{soundFilePath}\\M{this.numberApproved}.mp3", balance);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Record button.
        /// If state is "NOT RECORDING" & "NOT RECORDED" then application can start capture recording
        /// If state is "NOT RECORDING" & "COMPLETE RECORDED" then application can playback recorded file
        /// If state is "RECORDING" then application stops recording
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RecordBtn_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                if (!recorded)
                {
                    this.BackColor = Color.Gray;
                    RecordBtn.Text = LocRM.GetString("AudioStop");
                    StartCapture();
                }
                else
                {
                    string audioFile = soundFilePath + soundFile;

                    PlayBack(audioFile, 0);
                    PassBtn.Visible = true;
                    ResetBtn.Visible = true;
                }
            }
            else
            {
                StopCapture();
                this.BackColor = Color.FromArgb(64, 64, 64);
                RecordBtn.Text = LocRM.GetString("AudioPlay"); ;
            }
        }


        /// <summary>
        /// Control.Click Event handler. Where control is the Reset/Retry button.
        /// Resets the test
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            recorded = false; //no audio recorded yet
            recording = false; //recording not started
            SetString();
            InitializePlayingAudio();
        }


        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button.
        /// Pass the test and close app
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
        }


        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button.
        /// Fail the test and close app
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Windows.Media.Capture.MediaCapture fail Event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void MediaCaptureFailed(MediaCapture sender, MediaCaptureFailedEventArgs e)
        {
            DllLog.Log.LogError("Media Capture Failed: " + e.Message);

            throw new InvalidOperationException(e.Message);
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("Speaker") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            ResetBtn.Text = LocRM.GetString("Retry");
            RecordingLbl.Text = LocRM.GetString("AudioRecording") + ":";
            RecordingNowLbl.Text = LocRM.GetString("AudioRecordingNow");
            RecordBtn.Text = LocRM.GetString("AudioStart");
            PlayBtn_L.Text = LocRM.GetString("AudioPlayL");
            PlayBtn_R.Text = LocRM.GetString("AudioPlayR");
        }

        private int GenerateNumber()
        {
            Random random = new Random();
            return random.Next(0, 9);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.KeyPreview = false;
            if (e.KeyData.ToString().Equals($"D{this.numberApproved}") && PlayBtn_L.Enabled)
            {
                PlayBtn_L.BackColor = Color.Green;
                PlayBtn_R.BackColor = Color.DodgerBlue;
                PlayBtn_L.Enabled = false;
                PlayBtn_R.Enabled = true;
                PlayBtn_Button_Click(100);
            } 
            else if (e.KeyData.ToString().Equals($"D{this.numberApproved}") && PlayBtn_R.Enabled)
            {
                PlayBtn_L.BackColor = Color.Green;
                PlayBtn_R.BackColor = Color.Green;
                PlayBtn_L.Enabled = false;
                PlayBtn_R.Enabled = false;
                PlayAudioLbl.Enabled = false;
                RecordBtn.Enabled = true;
                RecordingLbl.Enabled = true;
                Player1.Ctlcontrols.stop();
                InstructionLbl.Text = LocRM.GetString("AudioSpeakerRecord");
            }
            else
            {
                CleanProcess();
            }
        }

        private void CleanProcess()
        {
            PlayBtn_L.BackColor = Color.DodgerBlue;
            PlayBtn_R.BackColor = Color.Gray;

            PlayAudioLbl.Text = LocRM.GetString("AudioPlayAudio") + ":";
            InstructionLbl.Text = LocRM.GetString("AudioSpeakerPlay");
            Player1.Ctlcontrols.stop();
            PlayAudioLbl.Enabled = false;

            PlayBtn_L.Enabled = true;
            PlayBtn_R.Enabled = false;
            RecordBtn.Enabled = false;
            RecordingLbl.Enabled = false;
        }
    }
}