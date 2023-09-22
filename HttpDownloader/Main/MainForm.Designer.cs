namespace HttpDownloader
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.MenuStrip menuStrip1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.log = new System.Windows.Forms.RichTextBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.consoleToolStripMenuItem,
            this.cleanToolStripMenuItem,
            this.configToolStripMenuItem});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(626, 25);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
			// 
			// consoleToolStripMenuItem
			// 
			this.consoleToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.consoleToolStripMenuItem.Checked = true;
			this.consoleToolStripMenuItem.CheckOnClick = true;
			this.consoleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
			this.consoleToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
			this.consoleToolStripMenuItem.Text = "*&Console";
			this.consoleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ConsoleCheckedChanged);
			// 
			// cleanToolStripMenuItem
			// 
			this.cleanToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
			this.cleanToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
			this.cleanToolStripMenuItem.Text = "Clean(&K)";
			this.cleanToolStripMenuItem.Click += new System.EventHandler(this.OnClean);
			// 
			// configToolStripMenuItem
			// 
			this.configToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.configToolStripMenuItem.Name = "configToolStripMenuItem";
			this.configToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
			this.configToolStripMenuItem.Text = "Con&fig";
			this.configToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
			// 
			// mainContainer
			// 
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 25);
			this.mainContainer.Name = "mainContainer";
			this.mainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.log);
			this.mainContainer.Size = new System.Drawing.Size(626, 513);
			this.mainContainer.SplitterDistance = 308;
			this.mainContainer.TabIndex = 1;
			this.mainContainer.TabStop = false;
			// 
			// log
			// 
			this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.log.Dock = System.Windows.Forms.DockStyle.Fill;
			this.log.Location = new System.Drawing.Point(0, 0);
			this.log.Name = "log";
			this.log.ReadOnly = true;
			this.log.Size = new System.Drawing.Size(626, 201);
			this.log.TabIndex = 0;
			this.log.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 538);
			this.Controls.Add(this.mainContainer);
			this.Controls.Add(menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = menuStrip1;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "MainForm";
			this.Text = "Http Downloader";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			this.mainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
			this.mainContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.RichTextBox log;
		private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
	}
}

