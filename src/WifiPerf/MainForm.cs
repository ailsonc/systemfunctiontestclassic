using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace WifiPerf
{
    public partial class MainForm : Form
    {
        private static ResourceManager LocRM;
        private Thread thread;
        private string sConnectionName = "";
        private bool bIfConnect = false;
        private int iSignalSpec = 0;
        private double transferLimit, bandwidthLimit;

        public MainForm(string[] WiFiPara)
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();
            SetFullScreen();
            Wifi(WiFiPara);
            Runnable();
        }

        private void Wifi(string[] WiFiPara)
        {
            //WifiPara sample: pt-BR PC2 70 80000 400000
            if (WiFiPara != null && WiFiPara.Length >= 1)
            {
                if (WiFiPara[0].Length > 0 && (!WiFiPara[0].Contains("NULL")))
                {
                    sConnectionName = WiFiPara[0].Trim();
                    bIfConnect = true;
                }
            }
            if (WiFiPara != null && WiFiPara.Length >= 3)
            {
                if (WiFiPara[1].Length > 0 && (!WiFiPara[1].Contains("NULL")))
                    iSignalSpec = Int32.Parse(WiFiPara[1].ToString(), CultureInfo.InvariantCulture);
            }
            if (WiFiPara != null && WiFiPara.Length >= 4)
            {
                if (WiFiPara[2].Length > 0 && (!WiFiPara[2].Contains("NULL")))
                    transferLimit = Int32.Parse(WiFiPara[2].ToString());

                if (WiFiPara[3].Length > 0 && (!WiFiPara[3].Contains("NULL")))
                    bandwidthLimit = Int32.Parse(WiFiPara[3].ToString());
            }
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("WiFi") + LocRM.GetString("Test");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            Retry.Text = LocRM.GetString("Retry");
        }

        /// <summary>
        /// Full screen and no title
        /// </summary>
        private void SetFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Runnable
        /// </summary>
        public void Runnable()
        {
            Connect.SetWifi(sConnectionName);
            Iperf iIperf = new Iperf(lb_transfer_down, lb_bandwidth_down, chart_tb_down, PassBtn, transferLimit, bandwidthLimit);
            thread = new Thread(new ThreadStart(iIperf.Run));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
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

        private void Retry_Click(object sender, EventArgs e)
        {
            Runnable();
        }

    }
}
