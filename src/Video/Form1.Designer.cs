namespace Video
{
    partial class Form1
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
            this.PassBtn = new System.Windows.Forms.Button();
            this.FailBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RetryBtn = new System.Windows.Forms.Button();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.DeviceNameLbl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PassBtn
            // 
            this.PassBtn.BackColor = System.Drawing.Color.YellowGreen;
            this.PassBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PassBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PassBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassBtn.ForeColor = System.Drawing.Color.Transparent;
            this.PassBtn.Location = new System.Drawing.Point(10, 518);
            this.PassBtn.Margin = new System.Windows.Forms.Padding(10);
            this.PassBtn.Name = "PassBtn";
            this.PassBtn.Size = new System.Drawing.Size(461, 84);
            this.PassBtn.TabIndex = 1;
            this.PassBtn.Text = "PASS";
            this.PassBtn.UseVisualStyleBackColor = false;
            this.PassBtn.Click += new System.EventHandler(this.PASS_Click);
            // 
            // FailBtn
            // 
            this.FailBtn.BackColor = System.Drawing.Color.Crimson;
            this.FailBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailBtn.ForeColor = System.Drawing.Color.White;
            this.FailBtn.Location = new System.Drawing.Point(491, 518);
            this.FailBtn.Margin = new System.Windows.Forms.Padding(10);
            this.FailBtn.Name = "FailBtn";
            this.FailBtn.Size = new System.Drawing.Size(461, 84);
            this.FailBtn.TabIndex = 2;
            this.FailBtn.Text = "FAIL";
            this.FailBtn.UseVisualStyleBackColor = false;
            this.FailBtn.Click += new System.EventHandler(this.FAIL_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.RetryBtn, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.FailBtn, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.PassBtn, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.ErrorLbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.elementHost1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DeviceNameLbl, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1444, 617);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // RetryBtn
            // 
            this.RetryBtn.BackColor = System.Drawing.Color.Gray;
            this.RetryBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RetryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RetryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RetryBtn.ForeColor = System.Drawing.Color.White;
            this.RetryBtn.Location = new System.Drawing.Point(972, 518);
            this.RetryBtn.Margin = new System.Windows.Forms.Padding(10);
            this.RetryBtn.Name = "RetryBtn";
            this.RetryBtn.Size = new System.Drawing.Size(462, 84);
            this.RetryBtn.TabIndex = 12;
            this.RetryBtn.Text = "Retry";
            this.RetryBtn.UseVisualStyleBackColor = false;
            this.RetryBtn.Click += new System.EventHandler(this.RetryBtn_Click);
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.ErrorLbl, 3);
            this.ErrorLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.ErrorLbl.Location = new System.Drawing.Point(3, 0);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(1438, 37);
            this.ErrorLbl.TabIndex = 3;
            this.ErrorLbl.Text = "Error";
            this.ErrorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ErrorLbl.Visible = false;
            // 
            // elementHost1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.elementHost1, 3);
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 40);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1438, 465);
            this.elementHost1.TabIndex = 13;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // DeviceNameLbl
            // 
            this.DeviceNameLbl.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.DeviceNameLbl, 3);
            this.DeviceNameLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.DeviceNameLbl.ForeColor = System.Drawing.Color.White;
            this.DeviceNameLbl.Location = new System.Drawing.Point(3, 500);
            this.DeviceNameLbl.Name = "DeviceNameLbl";
            this.DeviceNameLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DeviceNameLbl.Size = new System.Drawing.Size(1438, 17);
            this.DeviceNameLbl.TabIndex = 14;
            this.DeviceNameLbl.Text = "DeviceName";
            this.DeviceNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "ExternalDisplay";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label ErrorLbl;
        private System.Windows.Forms.Button RetryBtn;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Label DeviceNameLbl;
    }
}