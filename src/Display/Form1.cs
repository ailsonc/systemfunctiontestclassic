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
using System.Resources;
using System.Windows.Forms;

namespace Display
{
    
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        private bool inversion_2;
        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            
            InitializeComponent();
            SetString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            this.tableLayoutPanel1.Enabled = false;
            this.tableLayoutPanel1.Visible = false;
            this.inversion_2 = false;

        }
        
        
        /// <summary>
        /// Control.Click Event handler. It will display different color while we keep clicking the screen.
        /// The initial cplor is green.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void Form1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Visible = false;

            if (!this.inversion_2)
            {
                Screen screen = Screen.PrimaryScreen;
                this.pictureBox1.Location = new Point(0, 0);
                this.pictureBox1.BackgroundImage = Properties.Resources.inversion_2;
                this.pictureBox1.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);
                this.pictureBox1.BackgroundImageLayout = ImageLayout.Tile;
                this.pictureBox1.Visible = true;
                this.inversion_2 = true;
                this.Controls.Add(this.pictureBox1);
            } else
            {
                if (Program.ProgramArgs.Count > 0)
                {
                    string hex = Program.ProgramArgs[0];
                    this.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
                    Program.ProgramArgs.RemoveAt(0);
                }

                if (Program.ProgramArgs.Count == 0)
                {
                    this.tableLayoutPanel1.Enabled = true;
                    this.tableLayoutPanel1.Visible = true;
                    foreach (Control c in this.tableLayoutPanel1.Controls)
                    {
                        c.Enabled = true;
                        c.Visible = true;
                    }
                }
            }
            
        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PASS_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
            Application.Exit();
        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FAIL_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
            Application.Exit();
        }
        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            this.Text = LocRM.GetString("Display");
            PASS.Text = LocRM.GetString("Pass");
            FAIL.Text = LocRM.GetString("Fail");
        }

    }
}
