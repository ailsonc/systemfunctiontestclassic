namespace Serial
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.RetryBtn = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.faillabe = new System.Windows.Forms.Label();
            this.passlabel = new System.Windows.Forms.Label();
            this.FailResultLab = new System.Windows.Forms.Label();
            this.PassResultLab = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PassBtn
            // 
            this.PassBtn.BackColor = System.Drawing.Color.YellowGreen;
            this.PassBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PassBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PassBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassBtn.ForeColor = System.Drawing.Color.White;
            this.PassBtn.Location = new System.Drawing.Point(10, 10);
            this.PassBtn.Margin = new System.Windows.Forms.Padding(10);
            this.PassBtn.Name = "PassBtn";
            this.PassBtn.Size = new System.Drawing.Size(289, 90);
            this.PassBtn.TabIndex = 6;
            this.PassBtn.Text = "PASS";
            this.PassBtn.UseVisualStyleBackColor = false;
            this.PassBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // FailBtn
            // 
            this.FailBtn.BackColor = System.Drawing.Color.Crimson;
            this.FailBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailBtn.ForeColor = System.Drawing.Color.White;
            this.FailBtn.Location = new System.Drawing.Point(319, 10);
            this.FailBtn.Margin = new System.Windows.Forms.Padding(10);
            this.FailBtn.Name = "FailBtn";
            this.FailBtn.Size = new System.Drawing.Size(289, 90);
            this.FailBtn.TabIndex = 7;
            this.FailBtn.Text = "FAIL";
            this.FailBtn.UseVisualStyleBackColor = false;
            this.FailBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.Title, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1169, 642);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel3.Controls.Add(this.FailBtn, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.PassBtn, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.RetryBtn, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(119, 516);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(929, 123);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // RetryBtn
            // 
            this.RetryBtn.BackColor = System.Drawing.Color.Gray;
            this.RetryBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RetryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RetryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RetryBtn.ForeColor = System.Drawing.Color.White;
            this.RetryBtn.Location = new System.Drawing.Point(628, 10);
            this.RetryBtn.Margin = new System.Windows.Forms.Padding(10);
            this.RetryBtn.Name = "RetryBtn";
            this.RetryBtn.Size = new System.Drawing.Size(291, 90);
            this.RetryBtn.TabIndex = 8;
            this.RetryBtn.Text = "Retry";
            this.RetryBtn.UseVisualStyleBackColor = false;
            this.RetryBtn.Click += new System.EventHandler(this.RetryBtn_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(119, 55);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(929, 73);
            this.Title.TabIndex = 9;
            this.Title.Text = "SerialPort Test";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.faillabe);
            this.panel1.Controls.Add(this.passlabel);
            this.panel1.Controls.Add(this.FailResultLab);
            this.panel1.Controls.Add(this.PassResultLab);
            this.panel1.Location = new System.Drawing.Point(119, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(929, 379);
            this.panel1.TabIndex = 10;
            // 
            // faillabe
            // 
            this.faillabe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.faillabe.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faillabe.ForeColor = System.Drawing.Color.White;
            this.faillabe.Location = new System.Drawing.Point(36, 236);
            this.faillabe.Name = "faillabe";
            this.faillabe.Size = new System.Drawing.Size(883, 120);
            this.faillabe.TabIndex = 4;
            // 
            // passlabel
            // 
            this.passlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passlabel.ForeColor = System.Drawing.Color.White;
            this.passlabel.Location = new System.Drawing.Point(36, 63);
            this.passlabel.Name = "passlabel";
            this.passlabel.Size = new System.Drawing.Size(883, 120);
            this.passlabel.TabIndex = 3;
            // 
            // FailResultLab
            // 
            this.FailResultLab.AutoSize = true;
            this.FailResultLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailResultLab.ForeColor = System.Drawing.Color.White;
            this.FailResultLab.Location = new System.Drawing.Point(32, 194);
            this.FailResultLab.Margin = new System.Windows.Forms.Padding(0);
            this.FailResultLab.Name = "FailResultLab";
            this.FailResultLab.Size = new System.Drawing.Size(224, 42);
            this.FailResultLab.TabIndex = 2;
            this.FailResultLab.Text = "Fail Result : ";
            // 
            // PassResultLab
            // 
            this.PassResultLab.AutoSize = true;
            this.PassResultLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassResultLab.ForeColor = System.Drawing.Color.White;
            this.PassResultLab.Location = new System.Drawing.Point(29, 21);
            this.PassResultLab.Margin = new System.Windows.Forms.Padding(0);
            this.PassResultLab.Name = "PassResultLab";
            this.PassResultLab.Size = new System.Drawing.Size(248, 42);
            this.PassResultLab.TabIndex = 1;
            this.PassResultLab.Text = "Pass Result : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 642);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Form1";
            this.Text = "Battery";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button RetryBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FailResultLab;
        private System.Windows.Forms.Label PassResultLab;
        private System.Windows.Forms.Label passlabel;
        private System.Windows.Forms.Label faillabe;
    }
}

