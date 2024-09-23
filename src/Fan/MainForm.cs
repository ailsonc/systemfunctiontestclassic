using System;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Fan
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        public Boolean AutoPass;

        private Thread thread;
        private FanController fan;

        #endregion

        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            RunFan();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        private void RunFan()
        {
            fan = new FanController(lb_fan1, lb_fan2, lb_fan3);
            fan.kill();
            thread = new Thread(new ThreadStart(fan.run));
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
            fan.kill();
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            fan.kill();
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = $"{LocRM.GetString("Fan")} {LocRM.GetString("Test")}";
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }

    }
}
