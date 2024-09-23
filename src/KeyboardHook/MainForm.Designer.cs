//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
namespace KeyboardHook
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.FailBtn = new System.Windows.Forms.Button();
            this.PassBtn = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VolumeDown = new System.Windows.Forms.Label();
            this.pictureBoxUp = new System.Windows.Forms.PictureBox();
            this.VolumeUP = new System.Windows.Forms.Label();
            this.pictureBoxDown = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Monitor = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.Panel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Panel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1140, 522);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.FailBtn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.PassBtn, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(117, 420);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(906, 99);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.YellowGreen;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(10, 99);
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(281, 1);
            this.button1.TabIndex = 6;
            this.button1.Text = "Pass";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // FailBtn
            // 
            this.FailBtn.BackColor = System.Drawing.Color.Crimson;
            this.FailBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailBtn.ForeColor = System.Drawing.Color.Transparent;
            this.FailBtn.Location = new System.Drawing.Point(311, 10);
            this.FailBtn.Margin = new System.Windows.Forms.Padding(10);
            this.FailBtn.Name = "FailBtn";
            this.FailBtn.Size = new System.Drawing.Size(282, 69);
            this.FailBtn.TabIndex = 5;
            this.FailBtn.Text = "Fail";
            this.FailBtn.UseVisualStyleBackColor = false;
            this.FailBtn.Click += new System.EventHandler(this.FailBtn_Click);
            // 
            // PassBtn
            // 
            this.PassBtn.BackColor = System.Drawing.Color.YellowGreen;
            this.PassBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PassBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PassBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassBtn.ForeColor = System.Drawing.Color.Transparent;
            this.PassBtn.Location = new System.Drawing.Point(10, 10);
            this.PassBtn.Margin = new System.Windows.Forms.Padding(10);
            this.PassBtn.Name = "PassBtn";
            this.PassBtn.Size = new System.Drawing.Size(281, 69);
            this.PassBtn.TabIndex = 4;
            this.PassBtn.Text = "Pass";
            this.PassBtn.UseVisualStyleBackColor = false;
            this.PassBtn.Visible = false;
            this.PassBtn.Click += new System.EventHandler(this.PassBtn_Click);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.tableLayoutPanel3);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.ForeColor = System.Drawing.Color.White;
            this.Panel.Location = new System.Drawing.Point(117, 107);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(906, 307);
            this.Panel.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel3.Controls.Add(this.Title, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(906, 307);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.Title, 2);
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(3, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(900, 80);
            this.Title.TabIndex = 0;
            this.Title.Text = "Buttons";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Monitor);
            this.panel1.Controls.Add(this.VolumeDown);
            this.panel1.Controls.Add(this.pictureBoxUp);
            this.panel1.Controls.Add(this.VolumeUP);
            this.panel1.Controls.Add(this.pictureBoxDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 221);
            this.panel1.TabIndex = 1;
            // 
            // VolumeDown
            // 
            this.VolumeDown.AutoSize = true;
            this.VolumeDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VolumeDown.ForeColor = System.Drawing.Color.LightGray;
            this.VolumeDown.Location = new System.Drawing.Point(78, 164);
            this.VolumeDown.Name = "VolumeDown";
            this.VolumeDown.Size = new System.Drawing.Size(240, 42);
            this.VolumeDown.TabIndex = 22;
            this.VolumeDown.Text = "VolumeDown";
            // 
            // pictureBoxUp
            // 
            this.pictureBoxUp.Image = global::KeyboardHook.Properties.Resources.volumeUp;
            this.pictureBoxUp.ImageLocation = "";
            this.pictureBoxUp.Location = new System.Drawing.Point(34, 101);
            this.pictureBoxUp.Name = "pictureBoxUp";
            this.pictureBoxUp.Size = new System.Drawing.Size(38, 40);
            this.pictureBoxUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUp.TabIndex = 21;
            this.pictureBoxUp.TabStop = false;
            // 
            // VolumeUP
            // 
            this.VolumeUP.AutoSize = true;
            this.VolumeUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VolumeUP.ForeColor = System.Drawing.Color.LightGray;
            this.VolumeUP.Location = new System.Drawing.Point(78, 99);
            this.VolumeUP.Name = "VolumeUP";
            this.VolumeUP.Size = new System.Drawing.Size(196, 42);
            this.VolumeUP.TabIndex = 20;
            this.VolumeUP.Text = "VolumeUP";
            // 
            // pictureBoxDown
            // 
            this.pictureBoxDown.Image = global::KeyboardHook.Properties.Resources.volumeDown;
            this.pictureBoxDown.ImageLocation = "";
            this.pictureBoxDown.Location = new System.Drawing.Point(34, 164);
            this.pictureBoxDown.Name = "pictureBoxDown";
            this.pictureBoxDown.Size = new System.Drawing.Size(38, 40);
            this.pictureBoxDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDown.TabIndex = 4;
            this.pictureBoxDown.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KeyboardHook.Properties.Resources.brilho;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(34, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // Monitor
            // 
            this.Monitor.AutoSize = true;
            this.Monitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Monitor.ForeColor = System.Drawing.Color.LightGray;
            this.Monitor.Location = new System.Drawing.Point(78, 42);
            this.Monitor.Name = "Monitor";
            this.Monitor.Size = new System.Drawing.Size(142, 42);
            this.Monitor.TabIndex = 23;
            this.Monitor.Text = "Monitor";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 522);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "GyrometerTest";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.Panel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBoxDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label VolumeUP;
        private System.Windows.Forms.Label VolumeDown;
        private System.Windows.Forms.PictureBox pictureBoxUp;
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Monitor;
    }
}

