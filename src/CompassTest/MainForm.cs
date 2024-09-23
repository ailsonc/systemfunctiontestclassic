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
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.Foundation;

namespace CompassTest
{
    public partial class MainForm : Form
    {
        #region Fields

        Compass _compass;
        private uint _desiredReportInterval;
        private static Image image;
        private static ResourceManager LocRM;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();
            InitializeCompass();
            SetString();
            Label.CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;

            image = pictureBoxPage.Image;
        }
        #endregion // Constructor

        /// <summary>
        ///  Initialize Compass timer and update Compass every 500ms
        /// </summary>
        private void InitializeCompass()
        {
            _compass = Compass.GetDefault();
            if (_compass != null)
            {
                // Establish the report interval
                uint minReportInterval = _compass.MinimumReportInterval;
                _desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;
                _compass.ReportInterval = _desiredReportInterval;

                _compass.ReadingChanged += new TypedEventHandler<Compass, CompassReadingChangedEventArgs>(ReadingChanged);
            }
            else
            {
                // The device on which the application is running does not support the compass sensor
                MagneticLbl.Text = LocRM.GetString("NotFound");
            }

        }

        /// <summary>
        /// Compass readings event and update UI strings and image
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ReadingChanged(object sender, CompassReadingChangedEventArgs e)
        {
            try
            {
                DispatchedHandler handler = delegate ()
                {
                    CompassReading reading = e.Reading;
                    if (reading != null)
                    {
                        MagneticLbl.Text = String.Format(CultureInfo.CurrentCulture,
                                                         "{0}: {1,5:0.00} (°)\r\n{2}: {3,5:0.00} (°)\r\n{4}: {5}",
                                                         LocRM.GetString("TrueHeading"),
                                                         reading.HeadingTrueNorth,
                                                         LocRM.GetString("MagneticHeading"),
                                                         reading.HeadingMagneticNorth,
                                                         LocRM.GetString("Accuracy"),
                                                         reading.HeadingAccuracy);

                        Rotate(-(float)reading.HeadingMagneticNorth);
                    }
                };
                BeginInvoke(handler);
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
            }
           
        }

        /// <summary>
        /// Rotates the compass image
        /// </summary>
        /// <param name="angle">angle to rotate image by.</param>
        public void Rotate(float angle)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(image.Width, image.Height);
                Graphics g = Graphics.FromImage(bitmap);

                g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
                g.DrawImage(image, image.Width / 2 - image.Height / 2, image.Height / 2 - image.Width / 2, image.Height, image.Width);

                pictureBoxPage.Image = bitmap;
            }
            catch (Exception ex)
            {
                DllLog.Log.LogError(ex.ToString());
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
            Title.Text = LocRM.GetString("Compass") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }

    }
}
