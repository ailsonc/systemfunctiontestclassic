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
using System.Diagnostics;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Geolocation;
using System.Timers;
using System.Drawing;

namespace GeolocationTest
{
    public partial class MainForm : Form
    {

        #region Fields

        private static ResourceManager LocRM;

        Geolocator _geolocator;
        PositionStatus _posStatus;
        int checkTolerence = 0;
        double checkLatitude = 0;
        double checkLongitude = 0;
        int checkMaxTimeout = 60; //change default timeout value to 1 mins
        int sec = 0;
        private System.Timers.Timer timer;
        public Boolean AutoPass = false;
        //ErrorCode 1: The pipe is being closed. (Exception from HRESULT: 0x800700E8)
        //ErrorCode 2: Input Parameter Error(Tolerance) - Cannot parse String to Int
        //ErrorCode 3: Input Parameter Error(LocationMatch) - Cannot parse String to Double
        //ErrorCode 4: Input Parameter Error(AllowedMaxPosFixTime) - Cannot parse String to Int
        //ErrorCode 5: Input Parameter Error
        int ErrorCode = 0;
        //Stopwatch stopWatch = new Stopwatch();

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

            InitializeGeolocation();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion //Contructor

        /// <summary>
        /// Initial for timer settings.
        /// </summary>
        private void InitTimer()
        {
            if (timer != null)
            {
                StopTimer();
                timer.Elapsed -= OnTimedEvent;
                timer = null;
            }
            // Create timer with one second interval.
            timer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
        }
        private void StartTimer()
        {
            if (timer != null && timer.Enabled == false)
            {
                sec = 0;
                timer.Start();
            }
        }
        private void StopTimer()
        {
            if (timer != null && timer.Enabled == true)
            {
                timer.Stop();
            }
        }
        /// <summary>
        /// Timer event
        /// </summary>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            sec++;
            CheckMaxTimeout();
        }
        /// <summary>
        /// Initialize Geolocation and get position
        /// </summary>
        private async void InitializeGeolocation()
        {

            try
            {

                //Check if AutoPass enabled
                if (Program.ProgramArgs == null || Program.ProgramArgs.Length == 0)
                {
                    //Autopass not enabled, display buttons
                    PassBtn.Visible = true;
                    FailBtn.Visible = true;
                    ResetBtn.Visible = true;

                    AutoPass = false;

                    try
                    {
                        InitTimer();
                        _geolocator = new Windows.Devices.Geolocation.Geolocator();
                        _geolocator.DesiredAccuracy = PositionAccuracy.High;
                        _geolocator.DesiredAccuracyInMeters = 10;

                        _geolocator.StatusChanged += OnStatusChanged;
                        _geolocator.PositionChanged += OnPositionChanged;
                        var task = _geolocator.GetGeopositionAsync();
                        Geoposition pos = await _geolocator.GetGeopositionAsync();

                    }
                    catch (Exception e)
                    {
                        ErrorCode = 1;
                        throw e;
                    }
                }
                else if  (Program.ProgramArgs.Length == 3 
                    && Program.ProgramArgs[0] != "NULL" 
                    && Program.ProgramArgs[1] != "NULL"
                    && Program.ProgramArgs[2] != "NULL")
                {
                    AutoPass = true;

                    try
                    {
                        InitTimer();
                        _geolocator = new Windows.Devices.Geolocation.Geolocator();
                        _geolocator.DesiredAccuracy = PositionAccuracy.High;
                        _geolocator.DesiredAccuracyInMeters = 10;

                        _geolocator.StatusChanged += OnStatusChanged;
                        _geolocator.PositionChanged += OnPositionChanged;
                        var task = _geolocator.GetGeopositionAsync();
                        Geoposition pos = await _geolocator.GetGeopositionAsync();

                    }
                    catch (Exception e)
                    {
                        ErrorCode = 1;
                        throw e;
                    }

                    if (Program.ProgramArgs[1].ToLower()  != "any")
                    {
                        if (!Int32.TryParse(Program.ProgramArgs[0], out checkTolerence))
                        {
                            ErrorCode = 2;
                            throw new Exception("Cannot parse String to Int: " + Program.ProgramArgs[0]);

                        }


                        string[] argPos = Program.ProgramArgs[1].Split(',');
                        if (argPos.Length == 2)
                        {
                            if (double.TryParse(argPos[0], out checkLatitude) && double.TryParse(argPos[1], out checkLongitude))
                            {
                                //Successfully parse latitude and longitude
                                Log.LogComment(Log.LogLevel.Info, "Successfully parse latitude and longitude: Latitude = " + checkLatitude + " Longtitude = " + checkLongitude);
                            }
                            else
                            {
                                ErrorCode = 3;
                                throw new Exception("Cannot parse String to Double: " + Program.ProgramArgs[1]);

                            }
                        }

                        if (!Int32.TryParse(Program.ProgramArgs[2], out checkMaxTimeout))
                        {
                            ErrorCode = 4;
                            throw new Exception("Cannot parse String to Int: " + Program.ProgramArgs[2]);

                        }

                    }
                    else
                    {
                        _geolocator.PositionChanged -= OnPositionChanged;
                        _geolocator.StatusChanged -= OnStatusChanged;
                        _geolocator = null;
                        Program.ExitApplication(0);
                    }
                }
                else
                {
                    ErrorCode = 5;
                    throw new Exception("Input parameter error");

                }
                StartTimer();
                //stopWatch.Start();
            }
            catch (Exception e)
            {
                
                //AccuracyLbl.Text = LocRM.GetString("Error") + " " + e.Message;
                if(ErrorCode == 1)
                {
                    Log.LogError("Error: GPS Initialization Fail");
                    Log.LogError(e.ToString());
                    if(AutoPass == false)
                    {
                        AccuracyLbl.Text = LocRM.GetString("NotFound");
                        FailBtn.Visible = true;
                        ResetBtn.Visible = true;
                    }
                    else
                    {
                        if (_geolocator != null)
                        {
                            _geolocator.PositionChanged -= OnPositionChanged;
                            _geolocator.StatusChanged -= OnStatusChanged;
                            _geolocator = null;
                        }
                        Program.ExitApplication(255);
                    }

                }
                else
                {
                    Log.LogError("Error: Input parameter error");
                    Log.LogError(e.ToString());

                    if (_geolocator != null)
                    {
                        _geolocator.PositionChanged -= OnPositionChanged;
                        _geolocator.StatusChanged -= OnStatusChanged;
                        _geolocator = null;
                    }
                    Program.ExitApplication(255);
                }
                

                //FailBtn.Visible = true;
                //ResetBtn.Visible = true;
            }
        }

        private void OnStatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";
            _posStatus = args.Status;
            SetColor(StatusLbl, System.Drawing.Color.LightGray);
            switch (_posStatus)
            {
                case PositionStatus.Disabled:
                    // the application does not have the right capability or the location master switch is off
                    status = "location is disabled in phone settings";
                    SetColor(StatusLbl, System.Drawing.Color.Crimson);
                    break;
                case PositionStatus.Initializing:
                    // the geolocator started the tracking operation
                    status = "initializing";
                    break;
                case PositionStatus.NoData:
                    // the location service was not able to acquire the location
                    status = "no data";
                    SetColor(StatusLbl, System.Drawing.Color.Crimson);
                    break;
                case PositionStatus.Ready:
                    // the location service is generating geopositions as specified by the tracking parameters
                    status = "ready";
                    SetColor(StatusLbl, System.Drawing.Color.YellowGreen);
                    break;
                case PositionStatus.NotAvailable:
                    status = "not available";
                    SetColor(StatusLbl, System.Drawing.Color.Crimson);
                    // not used in WindowsPhone, Windows desktop uses this value to signal that there is no hardware capable to acquire location information
                    break;
                case PositionStatus.NotInitialized:
                    // the initial state of the geolocator, once the tracking operation is stopped by the user the geolocator moves back to this state
                    break;

            }
            SetText(StatusLbl,  status );
            //CheckMaxTimeout();
        }

        private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Geoposition pos = args.Position;

            SetText(AccuracyLbl, LocRM.GetString("GeolocationAccuracy") + ": " + pos.Coordinate.Accuracy.ToString() + " [" + pos.Coordinate.PositionSource + "]");
            SetText(LatitudeLbl, LocRM.GetString("GeolocationLatitude") + ": " + pos.Coordinate.Point.Position.Latitude.ToString("0.000000"));
            SetText(LongitudeLbl, LocRM.GetString("GeolocationLongitude") + ": " + pos.Coordinate.Point.Position.Longitude.ToString("0.000000"));

            if (checkTolerence + checkLatitude + checkLongitude + checkMaxTimeout > 0) //make sure autotest is configured
            {
                if (pos.Coordinate.PositionSource == PositionSource.Satellite)
                {
                    if (isPositionWithinRange(pos.Coordinate.Point.Position, pos.Coordinate.Accuracy))
                    {
                        Log.LogComment(Log.LogLevel.Info, "Distance in range, current position: " + pos.Coordinate.Point.Position.Latitude.ToString("0.000000") + ", " + pos.Coordinate.Point.Position.Longitude.ToString("0.0000"));
                        PassBtn_Click(this, null);
                    }
                    else
                    {
                        Log.LogComment(Log.LogLevel.Warning, "Distance NOT in range, current position: " + pos.Coordinate.Point.Position.Latitude.ToString("0.000000") + ", " + pos.Coordinate.Point.Position.Longitude.ToString("0.0000"));
                    }
                }
            }
            //CheckMaxTimeout();
        }

        private bool isPositionWithinRange(BasicGeoposition pos, double accuracy)
        {
            bool isWithinRange = false;
            double distance = calcDistance(pos);

            Log.LogComment(Log.LogLevel.Info,"Distance to LocationMatch = " + Math.Round(distance) + " m, Tolerance = " + checkTolerence + " m.");

            if (distance <= checkTolerence && accuracy <= checkTolerence)
            {
                isWithinRange = true;
            }
            return isWithinRange;
        }

        private double calcDistance(BasicGeoposition currPos) // in meters
        {
            // haversine formula
            double dist = 0;
            const double earthMeanRadius = 6371000; // average earth radius in meters

            double deltaLatRadians = ((checkLatitude - currPos.Latitude) * Math.PI / 180);
            double deltaLongRadians = ((checkLongitude - currPos.Longitude) * Math.PI / 180);
            double lat1Radians = (currPos.Latitude * Math.PI / 180);
            double lat2Radians = (checkLatitude * Math.PI / 180);

            double a = (Math.Sin(deltaLatRadians / 2) * Math.Sin(deltaLatRadians / 2)) + Math.Cos(lat1Radians) * Math.Cos(lat2Radians) * (Math.Sin(deltaLongRadians / 2) * Math.Sin(deltaLongRadians / 2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            dist = c * earthMeanRadius;

            System.Diagnostics.Debug.WriteLine("Current: " + currPos.Latitude + "," + currPos.Longitude);
            System.Diagnostics.Debug.WriteLine("Check: " + checkLatitude + "," + checkLongitude);
            System.Diagnostics.Debug.WriteLine("Tolerance: " + checkTolerence);
            System.Diagnostics.Debug.WriteLine("Distance: " + dist);
            return dist;
        }

        private void CheckMaxTimeout ()
        {
            if (sec > checkMaxTimeout)
            {
                Log.LogComment(Log.LogLevel.Info, "AllowedMaxPosFixTime reached:  " + checkMaxTimeout + " seconds");
                FailBtn_Click(null, null);
            }

        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            if (_geolocator != null)
            {
                _geolocator.PositionChanged -= OnPositionChanged;
                _geolocator.StatusChanged -= OnStatusChanged;
                _geolocator = null;
            }
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            _geolocator.PositionChanged -= OnPositionChanged;
            _geolocator.StatusChanged -= OnStatusChanged;
            _geolocator = null;
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button.
        /// Reset/Retry and restart the test.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {

            if(_geolocator != null)
            {

                _geolocator.PositionChanged -= OnPositionChanged;
                _geolocator.StatusChanged -= OnStatusChanged;
                _geolocator = null;
            }   

            SetString();
            InitializeGeolocation();
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("GPS") + LocRM.GetString("Test");
            StatusLbl.Text = LocRM.GetString("Wait");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            ResetBtn.Text = LocRM.GetString("Retry");

            AccuracyLbl.Text = "";
            LatitudeLbl.Text = "";
            LongitudeLbl.Text = "";

        }

        private void SetText(Control c, string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () {
                    SetText(c, text);
                }));
            }
            else
            {
                c.Text = text;
            }
        }

        private void SetColor(Control c, System.Drawing.Color color)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate () {
                    SetColor(c, color);
                }));
            }
            else
            {
                c.ForeColor = color;
            }
        }
    }
}
