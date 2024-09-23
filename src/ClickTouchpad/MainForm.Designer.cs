
namespace ClickTouchpad
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RightLbl1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.LeftLblCount = new System.Windows.Forms.Label();
            this.LeftLbl1 = new System.Windows.Forms.Label();
            this.RightLblCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.RightLblCount, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.RightLbl1, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Title, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ExitBtn, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.LeftLblCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.LeftLbl1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1216, 657);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // RightLbl1
            // 
            this.RightLbl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RightLbl1.AutoSize = true;
            this.RightLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightLbl1.ForeColor = System.Drawing.Color.White;
            this.RightLbl1.Location = new System.Drawing.Point(818, 477);
            this.RightLbl1.Margin = new System.Windows.Forms.Padding(0);
            this.RightLbl1.Name = "RightLbl1";
            this.RightLbl1.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.RightLbl1.Size = new System.Drawing.Size(154, 94);
            this.RightLbl1.TabIndex = 2;
            this.RightLbl1.Text = "RIGHT CLICK";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Crimson;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(732, 162);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(481, 312);
            this.panel2.TabIndex = 1;
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Right_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.GreenYellow;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 162);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(480, 312);
            this.panel1.TabIndex = 0;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Left_MouseClick);
            // 
            // Title
            // 
            this.Title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Title.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Title, 3);
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(370, 43);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(474, 73);
            this.Title.TabIndex = 1;
            this.Title.Text = "Touchpad Test";
            // 
            // ExitBtn
            // 
            this.ExitBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExitBtn.BackColor = System.Drawing.Color.LightSlateGray;
            this.ExitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitBtn.ForeColor = System.Drawing.Color.White;
            this.ExitBtn.Location = new System.Drawing.Point(535, 480);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(144, 100);
            this.ExitBtn.TabIndex = 2;
            this.ExitBtn.Text = "EXIT";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // LeftLblCount
            // 
            this.LeftLblCount.AutoSize = true;
            this.LeftLblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftLblCount.ForeColor = System.Drawing.Color.White;
            this.LeftLblCount.Location = new System.Drawing.Point(243, 477);
            this.LeftLblCount.Margin = new System.Windows.Forms.Padding(0);
            this.LeftLblCount.Name = "LeftLblCount";
            this.LeftLblCount.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.LeftLblCount.Size = new System.Drawing.Size(45, 52);
            this.LeftLblCount.TabIndex = 2;
            this.LeftLblCount.Text = "0";
            // 
            // LeftLbl1
            // 
            this.LeftLbl1.AutoSize = true;
            this.LeftLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftLbl1.ForeColor = System.Drawing.Color.White;
            this.LeftLbl1.Location = new System.Drawing.Point(0, 477);
            this.LeftLbl1.Margin = new System.Windows.Forms.Padding(0);
            this.LeftLbl1.Name = "LeftLbl1";
            this.LeftLbl1.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.LeftLbl1.Size = new System.Drawing.Size(137, 94);
            this.LeftLbl1.TabIndex = 0;
            this.LeftLbl1.Text = "LEFT CLICK";
            // 
            // RightLblCount
            // 
            this.RightLblCount.AutoSize = true;
            this.RightLblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightLblCount.ForeColor = System.Drawing.Color.White;
            this.RightLblCount.Location = new System.Drawing.Point(972, 477);
            this.RightLblCount.Margin = new System.Windows.Forms.Padding(0);
            this.RightLblCount.Name = "RightLblCount";
            this.RightLblCount.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.RightLblCount.Size = new System.Drawing.Size(45, 52);
            this.RightLblCount.TabIndex = 3;
            this.RightLblCount.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 657);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "ClickTouchpadTest";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label RightLbl1;
        private System.Windows.Forms.Label LeftLbl1;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label LeftLblCount;
        private System.Windows.Forms.Label RightLblCount;
    }
}

