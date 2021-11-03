namespace HttpDownloader
{
	partial class TaskConfigWindow
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
			System.Windows.Forms.ToolStrip toolStrip1;
			System.Windows.Forms.ToolStripLabel toolStripLabel1;
			System.Windows.Forms.ToolStripButton toolStripButton1;
			System.Windows.Forms.ToolStripButton toolStripButton2;
			System.Windows.Forms.ToolStripButton toolStripButton3;
			System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
			System.Windows.Forms.ToolStripButton toolStripButton4;
			this.cbbConfigs = new System.Windows.Forms.ToolStripComboBox();
			this.pgHeader = new System.Windows.Forms.PropertyGrid();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			toolStrip1.AutoSize = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripLabel1,
            this.cbbConfigs,
            toolStripButton1,
            toolStripButton2,
            toolStripButton3,
            toolStripSeparator1,
            toolStripButton4});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(564, 30);
			toolStrip1.TabIndex = 12;
			toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			toolStripLabel1.Name = "toolStripLabel1";
			toolStripLabel1.Size = new System.Drawing.Size(53, 27);
			toolStripLabel1.Text = "Config: ";
			// 
			// cbbConfigs
			// 
			this.cbbConfigs.Name = "cbbConfigs";
			this.cbbConfigs.Size = new System.Drawing.Size(300, 30);
			this.cbbConfigs.SelectedIndexChanged += new System.EventHandler(this.cbbConfigs_SelectedIndexChanged);
			// 
			// toolStripButton1
			// 
			toolStripButton1.AutoSize = false;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(40, 27);
			toolStripButton1.Text = "+";
			toolStripButton1.Click += new System.EventHandler(this.btnAddConfig_Click);
			// 
			// toolStripButton2
			// 
			toolStripButton2.AutoSize = false;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(27, 27);
			toolStripButton2.Text = "-";
			toolStripButton2.Click += new System.EventHandler(this.btnRemoveConfig_Click);
			// 
			// toolStripButton3
			// 
			toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButton3.AutoSize = false;
			toolStripButton3.Name = "toolStripButton3";
			toolStripButton3.Size = new System.Drawing.Size(60, 27);
			toolStripButton3.Text = "Start";
			toolStripButton3.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
			// 
			// toolStripButton4
			// 
			toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButton4.Name = "toolStripButton4";
			toolStripButton4.Size = new System.Drawing.Size(38, 27);
			toolStripButton4.Text = "DNS";
			toolStripButton4.Click += new System.EventHandler(this.btnDNS_Click);
			// 
			// pgHeader
			// 
			this.pgHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgHeader.HelpVisible = false;
			this.pgHeader.Location = new System.Drawing.Point(0, 30);
			this.pgHeader.Name = "pgHeader";
			this.pgHeader.Size = new System.Drawing.Size(564, 405);
			this.pgHeader.TabIndex = 11;
			this.pgHeader.ToolbarVisible = false;
			// 
			// TaskConfigWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 435);
			this.Controls.Add(this.pgHeader);
			this.Controls.Add(toolStrip1);
			this.Name = "TaskConfigWindow";
			this.ShowIcon = false;
			this.Text = "Task Config";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid pgHeader;
		private System.Windows.Forms.ToolStripComboBox cbbConfigs;
	}
}