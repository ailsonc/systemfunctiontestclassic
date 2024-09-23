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
using Windows.Devices.Sms;

using System.Net;   /*add to reference*/
using System.IO;    /*add to reference*/
using System.Xml;



namespace Upload
{
     public partial class MainForm : Form
    {

        private static ResourceManager LocRM;
        /// <summary>
        /// Initialize SIM device and update UI Labels with result
        /// </summary>
        /// 
         public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);

            InitializeComponent();
            InitializeUpload();
            SetString();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
       

        }
        private async void InitializeUpload()
        {
            result.Text = "";
            Retry.Text = LocRM.GetString("Retry");

            GetUploadInfo();
            

        }

        public void GetUploadInfo()
        {
            string xmlPath = Application.StartupPath+"\\"+"SFTConfig.xml";//"D:\\ms_Server\\CommonProj\\SystemFunctionTestClassic\\SystemFunctionTestClassic\\SFTConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            FailBtn.Enabled = false;
            PassBtn.Enabled = false;
            uristring.Text = "";
            filepath.Text = "";
            filename.Text = "";


            XmlNodeList uri = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase/TestMenu/MenuItem/Uri");
          //  XmlNodeList fpath = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase/TestMenu/MenuItem/FilePath");
          //  XmlNodeList fname = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase/TestMenu/MenuItem/FileName");

            uristring.Text = uri[0].InnerText;
            filepath.Text = Application.StartupPath + "\\";//"D:\\ms_Server\\CommonProj\\SystemFunctionTestClassic\\OutputDebug\\" ; //fpath[0].InnerText; // Application.StartupPath;
            filename.Text = "SFTClassicLog.txt"; //fname[0].InnerText; //"SFTClassicLog.txt";
                                                 // MessageBox.Show(filepath.Text+ filename.Text);

            if (uristring.Text == "")
            {
              //  uristring.Text = "Please modify Uri string in SFTConfig.xml";
                FailBtn.Enabled = true;
                result.Text = "Fail !";
                return;
            }

            else if (!File.Exists(filepath.Text + "\\" + filename.Text))
            {
                Retry.Text = LocRM.GetString("Retry");
                filepath.Text = "";
                filename.Text = "File Not Found . Please Check.";
				result.Text = "Fail !";
                return;
            }
            else
            {
                UploadFile();
            }
         


        }
        public void UploadFile()
        {
            DateTime t = DateTime.Now;

            string time = t.Year.ToString() + t.Month.ToString() + t.Day.ToString() + "-" +
                          t.Hour.ToString() + t.Minute.ToString() + t.Second.ToString() + "-" + t.Millisecond.ToString();

            if (File.Exists(uristring.Text + time + filename.Text))
            {
                FailBtn.Enabled = true;
                result.Text = "Fail !";
                return;
            }
            else
            {
                WebClient myWebClient = new WebClient();
                byte[] responseArray = myWebClient.UploadFile(uristring.Text + time + filename.Text, "PUT", filepath.Text + filename.Text);

            }
            if (File.Exists(filepath.Text + filename.Text))
            {
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                // Upload the file to the URI.
                
                    byte[] responseArray = myWebClient.UploadFile(uristring.Text + time + filename.Text, "PUT", filepath.Text + filename.Text);

               
            }

            

            if (File.Exists(uristring.Text + time + filename.Text))
            {
                PassBtn.Enabled = true;
                result.Text = "Success ! ";
                //   Upload.Text = LocRM.GetString("Retry");
            }

            else
            {
                FailBtn.Enabled = true;
                result.Text = "Fail !";
             //   Retry.Text = LocRM.GetString("Retry");
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
            //   Title.Text = LocRM.GetString("SIM") + LocRM.GetString("Test");
            Title.Text = LocRM.GetString("File") + LocRM.GetString("Upload");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            //  Upload.Text = LocRM.GetString("Retry");
            Retry.Text = LocRM.GetString("Retry");

        }

     //   private void Upload_Click(object sender, EventArgs e)
     //   {
           
     //   }

        

        private void PassResultLab_Click(object sender, EventArgs e)
        {

        }

        private void Retry_Click(object sender, EventArgs e)
        {

            FailBtn.Enabled = false;
            PassBtn.Enabled = false;
            uristring.Text = "";
            GetUploadInfo();
        }
    }
}
