namespace Biometric
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
            this.Panel = new System.Windows.Forms.Panel();
            this.ErrorLbl = new System.Windows.Forms.Label();
            this.SwipeStatus = new System.Windows.Forms.Label();
            this.SwipeLab = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.Panel.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.Panel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Title, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
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
            // Panel
            // 
            this.Panel.Controls.Add(this.ErrorLbl);
            this.Panel.Controls.Add(this.SwipeStatus);
            this.Panel.Controls.Add(this.SwipeLab);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(119, 131);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(929, 379);
            this.Panel.TabIndex = 11;
            // 
            // ErrorLbl
            // 
            this.ErrorLbl.AutoSize = true;
            this.ErrorLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLbl.ForeColor = System.Drawing.Color.Red;
            this.ErrorLbl.Location = new System.Drawing.Point(13, 440);
            this.ErrorLbl.Margin = new System.Windows.Forms.Padding(0);
            this.ErrorLbl.Name = "ErrorLbl";
            this.ErrorLbl.Size = new System.Drawing.Size(0, 55);
            this.ErrorLbl.TabIndex = 12;
            // 
            // SwipeStatus
            // 
            this.SwipeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwipeStatus.ForeColor = System.Drawing.Color.White;
            this.SwipeStatus.Location = new System.Drawing.Point(109, 208);
            this.SwipeStatus.Margin = new System.Windows.Forms.Padding(0);
            this.SwipeStatus.Name = "SwipeStatus";
            this.SwipeStatus.Size = new System.Drawing.Size(820, 171);
            this.SwipeStatus.TabIndex = 6;
            this.SwipeStatus.Text = " ";
            // 
            // SwipeLab
            // 
            this.SwipeLab.AutoSize = true;
            this.SwipeLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SwipeLab.ForeColor = System.Drawing.Color.White;
            this.SwipeLab.Location = new System.Drawing.Point(18, 99);
            this.SwipeLab.Margin = new System.Windows.Forms.Padding(0);
            this.SwipeLab.Name = "SwipeLab";
            this.SwipeLab.Size = new System.Drawing.Size(343, 55);
            this.SwipeLab.TabIndex = 0;
            this.SwipeLab.Text = "Swipe Status : ";
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
            this.Title.Text = "Biometric Test";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 642);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Form1";
            this.Text = "Biometric";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button RetryBtn;
        private System.Windows.Forms.Label ErrorLbl;
        private System.Windows.Forms.Label SwipeStatus;
        private System.Windows.Forms.Label SwipeLab;
    }
}

