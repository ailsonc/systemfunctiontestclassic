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
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Biometric
{
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;

        System.Timers.Timer _timer = new System.Timers.Timer();
        DateTime time_start;
        DateTime time_end;
        public Boolean AutoPass;

        const Int64 WINBIO_I_MORE_DATA = 0x90001;
        const Int64 WINBIO_FP_MERGE_FAILURE = 0x0000000A;
        const Int64 WINBIO_E_CANCELED = 0x80098004;

        uint BioHandle = 0;
        uint totalcapCnt = 0;

        Dictionary<Int64, string> msgDic = new Dictionary<Int64, string>();
        CoreComponent component = null;
        public enum SwipeResult { None, Pass, Fail };
        public SwipeResult SR = SwipeResult.None;
        struct msgReply
        {
            public Int64 num;
            public string msg;
        }

        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();

            InitializeBiometric();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;

        }

        /// <summary>
        /// Initialize Biometric timer and update Biometric every 500ms
        /// </summary>
        private void InitializeBiometric()
        {

            //Check if AutoPass enabled
            time_start = DateTime.Now;

            if (Program.ProgramArgs == null || Program.ProgramArgs.Length == 0)
            {
                //Autopass not enabled, display buttons
                PassBtn.Visible = true;
                FailBtn.Visible = true;
                RetryBtn.Visible = true;
                AutoPass = false;
            }
            else //AutoPass enabled
            {
                PassBtn.Visible = false;
                FailBtn.Visible = false;
                RetryBtn.Visible = false;
                AutoPass = true;
            }

            //Set Timer to update Biometric
            _timer.Interval = 500;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateBiometric);
            _timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Transfer message to dictionary*/
            MsgToDictionary();
            /*Start to swipe finger*/
            QueryBiometric();
        }

        private void UpdateBiometric(object sender, EventArgs e)
        {
            time_end = DateTime.Now;
            if (AutoPass == true)
            {
                //If timeout or biometric fail
                if (((TimeSpan)(time_end - time_start)).TotalSeconds > Convert.ToInt32(Program.ProgramArgs[0]) || SR == SwipeResult.Fail)
                {
                    if (component != null)
                        component.DiscardEnroll(BioHandle);
                    _timer.Stop();
                    Program.ExitApplication(255);
                }
                else
                {
                    if (SR == SwipeResult.Pass)// || totalcapCnt >= Convert.ToInt32(Program.ProgramArgs[0]))
                    {
                        if (component != null)
                            component.DiscardEnroll(BioHandle);
                        _timer.Stop();
                        Program.ExitApplication(0);
                    }
                }
            }
        }

        private void MsgToDictionary()
        {
            msgReply[] winbio_msg = {
            new msgReply { num = 0x090001, msg = LocRM.GetString("BioMoredata")},
            new msgReply { num = 0x01, msg = LocRM.GetString("BioHigh") },
            new msgReply { num = 0x02, msg = LocRM.GetString("BioLow") },
            new msgReply { num = 0x03, msg = LocRM.GetString("BioLeft") },
            new msgReply { num = 0x04, msg = LocRM.GetString("BioRight") },
            new msgReply { num = 0x05, msg = LocRM.GetString("BioFast") },
            new msgReply { num = 0x06, msg = LocRM.GetString("BioSlow") },
            new msgReply { num = 0x07, msg = LocRM.GetString("BioPoorQuality") },
            new msgReply { num = 0x08, msg = LocRM.GetString("BioSkew") },
            new msgReply { num = 0x09, msg = LocRM.GetString("BioShort") },
            new msgReply { num = 0x0A, msg = LocRM.GetString("BioMergeFail") }
            };

            foreach (msgReply mr in winbio_msg)
                msgDic.Add(mr.num, mr.msg);
        }

        private string getActionMsg(Int64 num)
        {
            string str = "";
            if(msgDic.ContainsKey(num) ==true)
                str = msgDic[num];
            return str;
        }

        private async void QueryBiometric()
        {
            Int64 hr = 0;
            UInt64 biocount = 0;
            try
            {
                RetryBtn.Visible = false;
                if (component != null)
                {
                    component.Dispose();
                    component = null;
                }

                component = new CoreComponent();
                biocount = component.BioEnum();
                if (biocount == 0)
                {
                    _timer.Stop();
                    SR = SwipeResult.Fail;
                    Update_SwpState(LocRM.GetString("NotFound"));
                    Log.LogError("BioEnum return from DllComponent ");
                    if (AutoPass == true)
                        Program.ExitApplication(255);
                    else
                        return;
                }
                BioHandle = component.BioOpen();
                if (BioHandle == 0)
                {
                    _timer.Stop();
                    SR = SwipeResult.Fail;
                    Update_SwpState(LocRM.GetString("NotFound"));
                    Log.LogError("BioOpen fail from DllComponent ");
                    return;
                }

                hr = 0;
                Update_SwpState(LocRM.GetString("BioSwipe"));
                hr = await Task.Run(() => { return component.BioLocate(BioHandle); });
                if (hr != 0)
                {
                    _timer.Stop();
                    SR = SwipeResult.Fail;
                    Update_SwpState(LocRM.GetString("BioEnrollFail"));
                    return;
                }

                await Task.Run(() => { SwipeBiometric(component, BioHandle); });
            }
            catch (Exception ex)
            {
                _timer.Stop();
                Update_SwpState(LocRM.GetString("BioEnrollFail"));
                DllLog.Log.LogError(ex.ToString());
            }
            finally
            {
                if (component != null)
                {
                    component.DiscardEnroll(BioHandle);
                    component.Dispose();
                    component = null;
                    if (AutoPass == false)
                        RetryBtn.Visible = true;
                }
            }
        }

        private void SwipeBiometric(CoreComponent cmp, uint bhandle)
        {
            Int64 hr = 0;
            while (true)
            {
                hr = (cmp.TestBiometric(bhandle)) & 0x00000000FFFFFFFF;
                //Console.WriteLine("1-SwipeBiometric={0:X}", hr);

                if (hr == WINBIO_I_MORE_DATA)
                {
                    totalcapCnt++;
                    Update_SwpState(getActionMsg(hr) + LocRM.GetString("BioAct") + totalcapCnt.ToString());
                }
                else
                {
                    /*Swipe completed*/
                    if (hr == 0)
                    {
                        SR = SwipeResult.Pass;
                        if (AutoPass == false)
                            Update_SwpState(LocRM.GetString("BioEnrollSuccess"));
                        cmp.DiscardEnroll(bhandle);
                        break;
                    }
                    else if (hr == WINBIO_FP_MERGE_FAILURE || hr == WINBIO_E_CANCELED)
                    {
                        SR = SwipeResult.Fail;
                        if (AutoPass == false)
                            Update_SwpState(LocRM.GetString("BioEnrollFail"));
                        cmp.DiscardEnroll(bhandle);
                        break;
                    }
                    else
                        Update_SwpState(getActionMsg(hr));
                }
            }
        }

        private void Update_SwpState(string str)
        {
            Invoke(new MethodInvoker(delegate
            {               
                SwipeStatus.Text = str;
            }));
            
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RetryBtn_Click(object sender, EventArgs e)
        {
            BioHandle = 0;
            totalcapCnt = 0;
            QueryBiometric();
          
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            Program.ExitApplication(255);
        }


        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("Biometric") + LocRM.GetString("Test");
            this.Text = LocRM.GetString("Biometric");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");
            SwipeLab.Text = LocRM.GetString("BioswpStatus");
        }
    }

}

