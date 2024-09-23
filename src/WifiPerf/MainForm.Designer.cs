
//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
namespace WifiPerf
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
            this.Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel_dashboard = new System.Windows.Forms.TableLayoutPanel();
            this.panel_chart_down = new System.Windows.Forms.Panel();
            this.chart_tb_down = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel_chart_title_down = new System.Windows.Forms.Panel();
            this.lb_bandwidth_down = new System.Windows.Forms.Label();
            this.lb_title_bandwidth_down = new System.Windows.Forms.Label();
            this.lb_transfer_down = new System.Windows.Forms.Label();
            this.lb_title_transfer_down = new System.Windows.Forms.Label();
            this.tableLayoutPanel_button = new System.Windows.Forms.TableLayoutPanel();
            this.FailBtn = new System.Windows.Forms.Button();
            this.PassBtn = new System.Windows.Forms.Button();
            this.Retry = new System.Windows.Forms.Button();
            this.tableLayoutPanel_main.SuspendLayout();
            this.tableLayoutPanel_dashboard.SuspendLayout();
            this.panel_chart_down.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_tb_down)).BeginInit();
            this.panel_chart_title_down.SuspendLayout();
            this.tableLayoutPanel_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_main
            // 
            this.tableLayoutPanel_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tableLayoutPanel_main.ColumnCount = 3;
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_main.Controls.Add(this.Title, 1, 0);
            this.tableLayoutPanel_main.Controls.Add(this.tableLayoutPanel_dashboard, 1, 1);
            this.tableLayoutPanel_main.Controls.Add(this.tableLayoutPanel_button, 1, 2);
            this.tableLayoutPanel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_main.Name = "tableLayoutPanel_main";
            this.tableLayoutPanel_main.RowCount = 3;
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_main.Size = new System.Drawing.Size(1232, 784);
            this.tableLayoutPanel_main.TabIndex = 1;
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(126, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(873, 72);
            this.Title.TabIndex = 2;
            this.Title.Text = "Wi-Fi Test";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel_dashboard
            // 
            this.tableLayoutPanel_dashboard.ColumnCount = 1;
            this.tableLayoutPanel_dashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_dashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_dashboard.Controls.Add(this.panel_chart_down, 0, 0);
            this.tableLayoutPanel_dashboard.Controls.Add(this.panel_chart_title_down, 0, 1);
            this.tableLayoutPanel_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_dashboard.Location = new System.Drawing.Point(126, 159);
            this.tableLayoutPanel_dashboard.Name = "tableLayoutPanel_dashboard";
            this.tableLayoutPanel_dashboard.RowCount = 2;
            this.tableLayoutPanel_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_dashboard.Size = new System.Drawing.Size(979, 464);
            this.tableLayoutPanel_dashboard.TabIndex = 6;
            // 
            // panel_chart_down
            // 
            this.panel_chart_down.BackColor = System.Drawing.Color.White;
            this.panel_chart_down.Controls.Add(this.chart_tb_down);
            this.panel_chart_down.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_chart_down.Location = new System.Drawing.Point(3, 3);
            this.panel_chart_down.Name = "panel_chart_down";
            this.panel_chart_down.Size = new System.Drawing.Size(973, 226);
            this.panel_chart_down.TabIndex = 6;
            // 
            // chart_tb_down
            // 
            chartArea2.Name = "Upload";
            this.chart_tb_down.ChartAreas.Add(chartArea2);
            this.chart_tb_down.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Upload";
            legend2.Title = "Upload";
            legend2.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.GradientLine;
            this.chart_tb_down.Legends.Add(legend2);
            this.chart_tb_down.Location = new System.Drawing.Point(0, 0);
            this.chart_tb_down.Name = "chart_tb_down";
            series3.ChartArea = "Upload";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Upload";
            series3.Name = "Transfer";
            series4.ChartArea = "Upload";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Upload";
            series4.Name = "Bandwidth";
            this.chart_tb_down.Series.Add(series3);
            this.chart_tb_down.Series.Add(series4);
            this.chart_tb_down.Size = new System.Drawing.Size(973, 226);
            this.chart_tb_down.TabIndex = 2;
            this.chart_tb_down.Text = "chart1";
            // 
            // panel_chart_title_down
            // 
            this.panel_chart_title_down.Controls.Add(this.lb_bandwidth_down);
            this.panel_chart_title_down.Controls.Add(this.lb_title_bandwidth_down);
            this.panel_chart_title_down.Controls.Add(this.lb_transfer_down);
            this.panel_chart_title_down.Controls.Add(this.lb_title_transfer_down);
            this.panel_chart_title_down.Location = new System.Drawing.Point(3, 235);
            this.panel_chart_title_down.Name = "panel_chart_title_down";
            this.panel_chart_title_down.Size = new System.Drawing.Size(973, 226);
            this.panel_chart_title_down.TabIndex = 6;
            // 
            // lb_bandwidth_down
            // 
            this.lb_bandwidth_down.AutoSize = true;
            this.lb_bandwidth_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_bandwidth_down.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lb_bandwidth_down.Location = new System.Drawing.Point(259, 77);
            this.lb_bandwidth_down.Margin = new System.Windows.Forms.Padding(0);
            this.lb_bandwidth_down.Name = "lb_bandwidth_down";
            this.lb_bandwidth_down.Size = new System.Drawing.Size(39, 42);
            this.lb_bandwidth_down.TabIndex = 17;
            this.lb_bandwidth_down.Text = "0";
            // 
            // lb_title_bandwidth_down
            // 
            this.lb_title_bandwidth_down.AutoSize = true;
            this.lb_title_bandwidth_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_title_bandwidth_down.ForeColor = System.Drawing.Color.White;
            this.lb_title_bandwidth_down.Location = new System.Drawing.Point(24, 77);
            this.lb_title_bandwidth_down.Margin = new System.Windows.Forms.Padding(0);
            this.lb_title_bandwidth_down.Name = "lb_title_bandwidth_down";
            this.lb_title_bandwidth_down.Size = new System.Drawing.Size(213, 42);
            this.lb_title_bandwidth_down.TabIndex = 16;
            this.lb_title_bandwidth_down.Text = "Bandwidth: ";
            // 
            // lb_transfer_down
            // 
            this.lb_transfer_down.AutoSize = true;
            this.lb_transfer_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_transfer_down.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lb_transfer_down.Location = new System.Drawing.Point(259, 20);
            this.lb_transfer_down.Margin = new System.Windows.Forms.Padding(0);
            this.lb_transfer_down.Name = "lb_transfer_down";
            this.lb_transfer_down.Size = new System.Drawing.Size(39, 42);
            this.lb_transfer_down.TabIndex = 15;
            this.lb_transfer_down.Text = "0";
            // 
            // lb_title_transfer_down
            // 
            this.lb_title_transfer_down.AutoSize = true;
            this.lb_title_transfer_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_title_transfer_down.ForeColor = System.Drawing.Color.White;
            this.lb_title_transfer_down.Location = new System.Drawing.Point(24, 20);
            this.lb_title_transfer_down.Margin = new System.Windows.Forms.Padding(0);
            this.lb_title_transfer_down.Name = "lb_title_transfer_down";
            this.lb_title_transfer_down.Size = new System.Drawing.Size(177, 42);
            this.lb_title_transfer_down.TabIndex = 14;
            this.lb_title_transfer_down.Text = "Transfer: ";
            // 
            // tableLayoutPanel_button
            // 
            this.tableLayoutPanel_button.ColumnCount = 3;
            this.tableLayoutPanel_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel_button.Controls.Add(this.FailBtn, 1, 0);
            this.tableLayoutPanel_button.Controls.Add(this.PassBtn, 0, 0);
            this.tableLayoutPanel_button.Controls.Add(this.Retry, 2, 0);
            this.tableLayoutPanel_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_button.Location = new System.Drawing.Point(126, 629);
            this.tableLayoutPanel_button.Name = "tableLayoutPanel_button";
            this.tableLayoutPanel_button.RowCount = 2;
            this.tableLayoutPanel_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_button.Size = new System.Drawing.Size(979, 152);
            this.tableLayoutPanel_button.TabIndex = 5;
            // 
            // FailBtn
            // 
            this.FailBtn.BackColor = System.Drawing.Color.Crimson;
            this.FailBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FailBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FailBtn.ForeColor = System.Drawing.Color.Transparent;
            this.FailBtn.Location = new System.Drawing.Point(336, 10);
            this.FailBtn.Margin = new System.Windows.Forms.Padding(10);
            this.FailBtn.Name = "FailBtn";
            this.FailBtn.Size = new System.Drawing.Size(306, 116);
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
            this.PassBtn.Size = new System.Drawing.Size(306, 116);
            this.PassBtn.TabIndex = 4;
            this.PassBtn.Text = "Pass";
            this.PassBtn.UseVisualStyleBackColor = false;
            this.PassBtn.Visible = false;
            this.PassBtn.Click += new System.EventHandler(this.PassBtn_Click);
            // 
            // Retry
            // 
            this.Retry.BackColor = System.Drawing.Color.Gray;
            this.Retry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Retry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Retry.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Retry.ForeColor = System.Drawing.Color.White;
            this.Retry.Location = new System.Drawing.Point(662, 10);
            this.Retry.Margin = new System.Windows.Forms.Padding(10);
            this.Retry.Name = "Retry";
            this.Retry.Size = new System.Drawing.Size(307, 116);
            this.Retry.TabIndex = 12;
            this.Retry.Text = "Retry";
            this.Retry.UseVisualStyleBackColor = false;
            this.Retry.Click += new System.EventHandler(this.Retry_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 784);
            this.Controls.Add(this.tableLayoutPanel_main);
            this.Name = "MainForm";
            this.Text = "WIFI";
            this.tableLayoutPanel_main.ResumeLayout(false);
            this.tableLayoutPanel_dashboard.ResumeLayout(false);
            this.panel_chart_down.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_tb_down)).EndInit();
            this.panel_chart_title_down.ResumeLayout(false);
            this.panel_chart_title_down.PerformLayout();
            this.tableLayoutPanel_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_button;
        private System.Windows.Forms.Button FailBtn;
        private System.Windows.Forms.Button PassBtn;
        private System.Windows.Forms.Button Retry;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_tb_down;
        private System.Windows.Forms.Label lb_transfer_down;
        private System.Windows.Forms.Label lb_title_transfer_down;
        private System.Windows.Forms.Label lb_bandwidth_down;
        private System.Windows.Forms.Label lb_title_bandwidth_down;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_dashboard;
        private System.Windows.Forms.Panel panel_chart_down;
        private System.Windows.Forms.Panel panel_chart_title_down;
    }
}

