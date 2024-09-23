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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Windows.Devices.Perception;
using Windows.Graphics.Imaging;
using Windows.Media;
using IRIQCapture;

namespace IRCameraTest
{
    [ComImport]
    [Guid("4ACCE8C4-3A72-4B17-8CFB-771DA0D12AFB")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IClosableByteAccess
    {
        void Lock(out IntPtr buffer, out uint capacity);
        void Unlock();
    }
    public partial class MainForm : Form
    {
        private PerceptionInfraredFrameSourceWatcher _infraredSourceWatcher;
        private List<PerceptionInfraredFrameSource> _infraredSourceList = null;
        private PerceptionInfraredFrameReader _infraredReader = null;
        private PerceptionInfraredFrameSource _infraredSource = null;
        private string Frame1 = "IRIlluminatedFrame.png";
        private string Frame2 = "IRAmbientFrame.png";
        private string SubFrame = Program.ProgramArgs[1].ToString(); 
        private string RefImage = Program.ProgramArgs[0].ToString();
        private string IfFrameServerSupport = Program.ProgramArgs[2].ToString();
        private DispatcherTimer timer;
        private Dispatcher dispatcher;
        private bool isCaptured = false;
        //private bool IsIlluminatedFrameSaved = false;
        //private bool IsAmibientFrameSaved = false;
        FrameProcessor _frameProcessor = null;

        #region Fields

        private static ResourceManager LocRM;
        private object illuminationEnabled;

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

            //Load Reference Image
            string path = Directory.GetCurrentDirectory();
            string ReferenceImagePath = path + "\\"+ RefImage;
            if (System.IO.File.Exists(ReferenceImagePath))
            {
                // Display the result.
                System.Drawing.Image Image = System.Drawing.Image.FromFile(RefImage);
                ReferenceImage.Image = Image;
                ReferenceImage.Height = Image.Height;
                ReferenceImage.Width = Image.Width;

                PassBtn.Visible = false;

            }
            else
            {

                System.Drawing.Image RefImage = System.Drawing.Image.FromFile("ConnectToCameraFail.jpg");
                ReferenceImage.Image = RefImage;
                ReferenceImage.Height = RefImage.Height;
                ReferenceImage.Width = RefImage.Width;

                PassBtn.Visible = false;
                lbl_RefImage.ForeColor = System.Drawing.Color.Red;
                lbl_RefImage.Text = "Please provide the reference image";
                Log.LogError("IRCamera Warning: Please provide the reference image");

            }            

        }

        #endregion // Constructor
        private async void Initialize()
        {
            try
            {
                var infraredAccess = await PerceptionInfraredFrameSource.RequestAccessAsync();
                if (infraredAccess == PerceptionFrameSourceAccessStatus.Allowed)
                {
                    _infraredSourceWatcher = PerceptionInfraredFrameSource.CreateWatcher();
                    EnableInfraredSourceChangedEvents();
                    _infraredSourceWatcher.Start();
                }
            }
            catch(Exception e)
            {

            }

        }

        private void EnableInfraredSourceChangedEvents()
        {
            if (_infraredSourceList == null)
            {
                _infraredSourceList = new List<PerceptionInfraredFrameSource>();
            }

            _infraredSourceWatcher.SourceAdded += InfraredSourceWatcher_SourceAdded;
            _infraredSourceWatcher.SourceRemoved += InfraredSourceWatcher_SourceRemoved;
        }

        private void DisableInfraredSourceChangedEvents()
        {
            if (_infraredSourceWatcher != null)
            {
                _infraredSourceWatcher.SourceAdded -= InfraredSourceWatcher_SourceAdded;
                _infraredSourceWatcher.SourceRemoved -= InfraredSourceWatcher_SourceRemoved;
                if (_infraredSourceList.Count > 0)
                {
                    _infraredSourceList.Clear();
                }
                _infraredSourceList = null;
                _infraredSourceWatcher.Stop();
                _infraredSourceWatcher = null;
            }
        }

        private void InfraredSourceWatcher_SourceRemoved(PerceptionInfraredFrameSourceWatcher sender, PerceptionInfraredFrameSourceRemovedEventArgs args)
        {
            int removeIndex = _infraredSourceList.IndexOf(args.FrameSource);
            lbl_IRImage.ForeColor = System.Drawing.Color.Red;
            lbl_IRImage.Text = "Sensor {0} Unplugged. " + args.FrameSource.DisplayName;
            _infraredSourceList.RemoveAt(removeIndex);
        }

        private void InfraredSourceWatcher_SourceAdded(PerceptionInfraredFrameSourceWatcher sender, PerceptionInfraredFrameSourceAddedEventArgs args)
        {
            _infraredSourceList.Add(args.FrameSource);

            if (_infraredSource == null)
            {                
                if (OpenInfraredDevice(_infraredSourceList[0]))
                {                    
                    isCaptured = true;
                }
                else
                {
                    isCaptured = false;
                }
            }
        }

        private bool OpenInfraredDevice(PerceptionInfraredFrameSource infraredFrameSoruce)
        {
            bool isOpened = false;
            if (_infraredSource == null && infraredFrameSoruce != null)
            {
                try
                {
                    _infraredSource = infraredFrameSoruce;
                    _infraredReader = _infraredSource.OpenReader();
                    _infraredReader.FrameArrived += InfraredReader_FrameArrived;
                    isOpened = true;
                }
                catch
                {
                    isOpened = false;
                }
            }

            return isOpened;
        }

        private void CloseInfraredDevice()
        {
            if (_infraredReader != null)
            {
                _infraredReader.FrameArrived -= InfraredReader_FrameArrived;
                _infraredReader.Dispose();
                _infraredReader = null;
                _infraredSource = null;
            }
        }

        private void Shutdown()
        {
            CloseInfraredDevice();
            DisableInfraredSourceChangedEvents();
        }

        private void InfraredReader_FrameArrived(PerceptionInfraredFrameReader sender, PerceptionInfraredFrameArrivedEventArgs args)
        {
            ProcessInfraredFrame(/*sender,*/ args);
        }

        private void ProcessInfraredFrame(/*PerceptionInfraredFrameReader sender, */PerceptionInfraredFrameArrivedEventArgs args)
        {
            using (var frame = args.TryOpenFrame())
            {
                if (frame != null)
                {
                    using (var videoFrame = frame.VideoFrame)
                    {
                        if (videoFrame != null)
                        {
                            var IsGetIllumination = videoFrame.ExtendedProperties.TryGetValue(KnownPerceptionInfraredFrameSourceProperties.ActiveIlluminationEnabled, out illuminationEnabled);
                            
                            ProcessVideoFrame(videoFrame, Convert.ToBoolean(illuminationEnabled));
                        }
                    }
                }
            }
        }

        private void ProcessVideoFrame(VideoFrame videoFrame, Boolean ifIlluminationEnabled)
        {
            using (var originalBitmap = videoFrame.SoftwareBitmap)
            {
                if (originalBitmap != null)
                {
                    using (var softwareBitmap = SoftwareBitmap.Convert(originalBitmap, BitmapPixelFormat.Bgra8))
                    {
                        using (var convertedBuffer = softwareBitmap.LockBuffer(BitmapBufferAccessMode.Read))
                        {
                            IClosableByteAccess convertedByteAccess = (IClosableByteAccess)(Object)convertedBuffer;
                            IntPtr convertedBytes;
                            uint convertedCapacity = 0;
                            convertedByteAccess.Lock(out convertedBytes, out convertedCapacity);
                            //dispatcher = Dispatcher.CurrentDispatcher;
                            dispatcher.Invoke((Action)delegate ()
                            {
                                WriteableBitmap displayImageSourceInterimBitmap = new WriteableBitmap((int)softwareBitmap.PixelWidth,
                                    (int)softwareBitmap.PixelHeight, 96.0, 96.0, PixelFormats.Bgra32, null);

                                    displayImageSourceInterimBitmap.WritePixels(
                                        new Int32Rect(0, 0, softwareBitmap.PixelWidth, softwareBitmap.PixelHeight),
                                        (IntPtr)convertedBytes, (int)convertedCapacity, convertedBuffer.GetPlaneDescription(0).Stride);

                                    string filename = ifIlluminationEnabled ? Frame1 : Frame2;

                                    using (FileStream stream5 = new FileStream(filename, FileMode.Create))
                                    {
                                        PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                                        encoder5.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(displayImageSourceInterimBitmap.Clone()));
                                        encoder5.Save(stream5);
                                    }
                            });
                            convertedByteAccess.Unlock();
                        }
                    }
                }
            }
        }
        private void ShowIRImage()
        {
            try
            {
                // Load the images.
                Bitmap bm1 = new Bitmap(Frame2);
                Bitmap bm2 = new Bitmap(Frame1);

                // Make a difference image.
                int wid = Math.Min(bm1.Width, bm2.Width);
                int hgt = Math.Min(bm1.Height, bm2.Height);

                // Get the differences.
                int[,] diffs = new int[wid, hgt];
                int max_diff = 0;
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        // Calculate the pixels' difference.
                        System.Drawing.Color color1 = bm1.GetPixel(x, y);
                        System.Drawing.Color color2 = bm2.GetPixel(x, y);
                        diffs[x, y] = (int)(
                            Math.Abs(color1.R - color2.R) +
                            Math.Abs(color1.G - color2.G) +
                            Math.Abs(color1.B - color2.B));
                        if (diffs[x, y] > max_diff)
                            max_diff = diffs[x, y];
                    }
                }

                // Create the difference image.
                Bitmap bm3 = new Bitmap(wid, hgt);
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        int clr = (int)(255.0 / max_diff * diffs[x, y]);
                        bm3.SetPixel(x, y, System.Drawing.Color.FromArgb(clr, clr, clr));
                    }
                }

                if (IRImage.Image != null)
                {
                    IRImage.Image.Dispose();
                }

                bm3.Save(SubFrame);
                //Display the result.
                System.Drawing.Image image = System.Drawing.Image.FromFile(SubFrame);
                IRImage.Image = image;
                IRImage.Height = image.Height;
                IRImage.Width = image.Width;


            }
            catch (Exception e)
            {
                PassBtn.Enabled = false;
                lbl_IRImage.ForeColor = System.Drawing.Color.Red;
                lbl_IRImage.Text = e.ToString();
                Log.LogError("IRCamera Fail: " + e.ToString());
            }

        }

        private void EnqueueTakePhoto() //new API to support FrameServer
        {
            _frameProcessor.WaitCaptureMutex();
            if (_frameProcessor.CaptureSessionPara.CaptureCount < Constant.QueueMaxCount)
            {
                _frameProcessor.CaptureSessionPara.CaptureCount++;
            }
            _frameProcessor.ReleaseCaptureMutex();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (IfFrameServerSupport.ToUpper() == "TRUE")
            {
                File.Delete(Frame1);
                File.Delete(Frame2);
                // new capture method to support FrameServer
                EnqueueTakePhoto();               
                              
                System.Threading.Thread.Sleep(500);

                if (File.Exists(Frame1) && File.Exists(Frame2))
                    isCaptured = true;

                // close Frameprocessor
                _frameProcessor.Dispose();
                _frameProcessor.Shutdown();
                _frameProcessor.StopIRDevice();
                _frameProcessor = null;
                //GC.Collect();

            }
            else
            {   // close iFrame Provider
                Shutdown();
            }

            if (isCaptured == true)
            {

                PassBtn.Visible = true;

                lbl_IRImage.ForeColor = System.Drawing.Color.Green;
                lbl_IRImage.Text = "IR Camera has been Opened.";

                this.WindowState = FormWindowState.Maximized;
                ShowIRImage();

            }
            else
            {


                PassBtn.Visible = false;
                this.WindowState = FormWindowState.Maximized;
                // Display the result.
                System.Drawing.Image image = System.Drawing.Image.FromFile("ConnectToCameraFail.jpg");
                IRImage.Image = image;
                IRImage.Height = image.Height;
                IRImage.Width = image.Width;

                lbl_IRImage.ForeColor = System.Drawing.Color.Red;
                lbl_IRImage.Text = "IR Camera can't be opened";
                Log.LogError("IRCamera Fail: IR Camera can't be opened");

            }
            
            RetryBtn.Enabled = true;
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
            Title.Text = LocRM.GetString("IRCamera") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Capture");

        }

        private void RetryBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            RetryBtn.Text = LocRM.GetString("Retry");

            lbl_IRImage.ForeColor = System.Drawing.Color.Yellow;
            lbl_IRImage.Text = "IR Camera is opening.";
            isCaptured = false;

            RetryBtn.Enabled = false;

            try
            {
                if (IfFrameServerSupport.ToUpper() == "TRUE")
                {
                    isCaptured = false;
                    if (_frameProcessor == null)
                        _frameProcessor = new FrameProcessor(Frame1, Frame2);
                }
                else
                {
                    Initialize();
                }

            }
            catch(Exception ex)
            {
                if (IfFrameServerSupport.ToUpper() == "TRUE")
                {
                    lbl_IRImage.Text = ex.ToString();
                    // close Frameprocessor
                    _frameProcessor.Dispose();
                    _frameProcessor.Shutdown();
                    _frameProcessor.StopIRDevice();
                    _frameProcessor = null;
                }
                else
                {
                    lbl_IRImage.Text = ex.ToString();
                    Shutdown();
                }
            }


            dispatcher = Dispatcher.CurrentDispatcher;
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Tick += timer_Tick;
            timer.Start();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
