//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
namespace Upload
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
            this.Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.FailBtn = new System.Windows.Forms.Button();
            this.PassBtn = new System.Windows.Forms.Button();
            this.Retry = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.result = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.Label();
            this.filepath = new System.Windows.Forms.Label();
            this.uristring = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PassResultLab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(98, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(510, 72);
            this.Title.TabIndex = 0;
            this.Title.Text = "File Upload";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Title, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(953, 551);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.FailBtn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.PassBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Retry, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(98, 443);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(756, 105);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // FailBtn
            // 
            this.FailBtn.BackColor = System.Drawing.Color.Crimson;
            this.FailBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailBtn.ForeColor = System.Drawing.Color.Transparent;
            this.FailBtn.Location = new System.Drawing.Point(261, 10);
            this.FailBtn.Margin = new System.Windows.Forms.Padding(10);
            this.FailBtn.Name = "FailBtn";
            this.FailBtn.Size = new System.Drawing.Size(232, 74);
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
            this.PassBtn.Size = new System.Drawing.Size(231, 74);
            this.PassBtn.TabIndex = 4;
            this.PassBtn.Text = "Pass";
            this.PassBtn.UseVisualStyleBackColor = false;
            this.PassBtn.Click += new System.EventHandler(this.PassBtn_Click);
            // 
            // Retry
            // 
            this.Retry.BackColor = System.Drawing.Color.Gray;
            this.Retry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Retry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Retry.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Retry.ForeColor = System.Drawing.Color.White;
            this.Retry.Location = new System.Drawing.Point(513, 10);
            this.Retry.Margin = new System.Windows.Forms.Padding(10);
            this.Retry.Name = "Retry";
            this.Retry.Size = new System.Drawing.Size(233, 74);
            this.Retry.TabIndex = 12;
            this.Retry.Text = "Retry";
            this.Retry.UseVisualStyleBackColor = false;
            this.Retry.Click += new System.EventHandler(this.Retry_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.result);
            this.panel1.Controls.Add(this.filename);
            this.panel1.Controls.Add(this.filepath);
            this.panel1.Controls.Add(this.uristring);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.PassResultLab);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(98, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 324);
            this.panel1.TabIndex = 6;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.ForeColor = System.Drawing.Color.Red;
            this.result.Location = new System.Drawing.Point(177, 265);
            this.result.Margin = new System.Windows.Forms.Padding(0);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(109, 42);
            this.result.TabIndex = 16;
            this.result.Text = "result";
            // 
            // filename
            // 
            this.filename.AutoSize = true;
            this.filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filename.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.filename.Location = new System.Drawing.Point(240, 210);
            this.filename.Margin = new System.Windows.Forms.Padding(0);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(158, 42);
            this.filename.TabIndex = 15;
            this.filename.Text = "filename";
            // 
            // filepath
            // 
            this.filepath.AutoSize = true;
            this.filepath.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filepath.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.filepath.Location = new System.Drawing.Point(238, 151);
            this.filepath.Margin = new System.Windows.Forms.Padding(0);
            this.filepath.Name = "filepath";
            this.filepath.Size = new System.Drawing.Size(138, 42);
            this.filepath.TabIndex = 14;
            this.filepath.Text = "filepath";
            // 
            // uristring
            // 
            this.uristring.AutoSize = true;
            this.uristring.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uristring.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.uristring.Location = new System.Drawing.Point(238, 94);
            this.uristring.Margin = new System.Windows.Forms.Padding(0);
            this.uristring.Name = "uristring";
            this.uristring.Size = new System.Drawing.Size(150, 42);
            this.uristring.TabIndex = 13;
            this.uristring.Text = "uristring";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 265);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 42);
            this.label3.TabIndex = 12;
            this.label3.Text = "Result : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 209);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 42);
            this.label2.TabIndex = 11;
            this.label2.Text = "File Name : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 151);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 42);
            this.label4.TabIndex = 10;
            this.label4.Text = "File Path : ";
            // 
            // PassResultLab
            // 
            this.PassResultLab.AutoSize = true;
            this.PassResultLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassResultLab.ForeColor = System.Drawing.Color.White;
            this.PassResultLab.Location = new System.Drawing.Point(23, 94);
            this.PassResultLab.Margin = new System.Windows.Forms.Padding(0);
            this.PassResultLab.Name = "PassResultLab";
            this.PassResultLab.Size = new System.Drawing.Size(202, 42);
            this.PassResultLab.TabIndex = 9;
            this.PassResultLab.Text = "Uri String : ";
            this.PassResultLab.Click += new System.EventHandler(this.PassResultLab_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 551);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Retry;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PassResultLab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Label filepath;
        private System.Windows.Forms.Label uristring;
        private System.Windows.Forms.Label label3;
    }
}

