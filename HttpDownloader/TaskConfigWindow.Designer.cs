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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Button btnBrowse;
			System.Windows.Forms.Button btnStart;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label7;
			System.Windows.Forms.Label label8;
			System.Windows.Forms.Label label9;
			System.Windows.Forms.Label label10;
			System.Windows.Forms.Label label11;
			System.Windows.Forms.Label label12;
			System.Windows.Forms.Label label13;
			System.Windows.Forms.Label label14;
			System.Windows.Forms.Button btnAddConfig;
			System.Windows.Forms.Button btnRemoveConfig;
			this.tbURL = new System.Windows.Forms.TextBox();
			this.tbRefer = new System.Windows.Forms.TextBox();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.tbSecFetchSite = new System.Windows.Forms.TextBox();
			this.tbSecFetchMode = new System.Windows.Forms.TextBox();
			this.tbSecFetchDest = new System.Windows.Forms.TextBox();
			this.tbCacheControl = new System.Windows.Forms.TextBox();
			this.tbPragma = new System.Windows.Forms.TextBox();
			this.tbConnection = new System.Windows.Forms.TextBox();
			this.tbAccept = new System.Windows.Forms.TextBox();
			this.tbUserAgent = new System.Windows.Forms.TextBox();
			this.tbMethod = new System.Windows.Forms.TextBox();
			this.cbResume = new System.Windows.Forms.CheckBox();
			this.cbbConfigs = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			btnBrowse = new System.Windows.Forms.Button();
			btnStart = new System.Windows.Forms.Button();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			btnAddConfig = new System.Windows.Forms.Button();
			btnRemoveConfig = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(13, 42);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 12);
			label1.TabIndex = 0;
			label1.Text = "URL";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 96);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(35, 12);
			label2.TabIndex = 1;
			label2.Text = "Refer";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 69);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(41, 12);
			label3.TabIndex = 2;
			label3.Text = "Output";
			// 
			// btnBrowse
			// 
			btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			btnBrowse.Location = new System.Drawing.Point(460, 64);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new System.Drawing.Size(75, 23);
			btnBrowse.TabIndex = 6;
			btnBrowse.Text = "Browse";
			btnBrowse.UseVisualStyleBackColor = true;
			btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnStart
			// 
			btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			btnStart.Location = new System.Drawing.Point(460, 403);
			btnStart.Name = "btnStart";
			btnStart.Size = new System.Drawing.Size(75, 23);
			btnStart.TabIndex = 7;
			btnStart.Text = "Start";
			btnStart.UseVisualStyleBackColor = true;
			btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(12, 123);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(41, 12);
			label4.TabIndex = 1;
			label4.Text = "Method";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(13, 147);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(41, 12);
			label5.TabIndex = 1;
			label5.Text = "Resume";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(13, 251);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(59, 12);
			label6.TabIndex = 1;
			label6.Text = "UserAgent";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(13, 170);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(41, 12);
			label7.TabIndex = 1;
			label7.Text = "Accept";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(13, 224);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(65, 12);
			label8.TabIndex = 1;
			label8.Text = "Connection";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(13, 197);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(41, 12);
			label9.TabIndex = 1;
			label9.Text = "Pragma";
			// 
			// label10
			// 
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(13, 278);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(83, 12);
			label10.TabIndex = 1;
			label10.Text = "Cache-Control";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(13, 305);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(89, 12);
			label11.TabIndex = 1;
			label11.Text = "Sec-Fetch-Dest";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(13, 332);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(89, 12);
			label12.TabIndex = 1;
			label12.Text = "Sec-Fetch-Mode";
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(13, 359);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(89, 12);
			label13.TabIndex = 1;
			label13.Text = "Sec-Fetch-Site";
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(12, 15);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(47, 12);
			label14.TabIndex = 0;
			label14.Text = "Configs";
			// 
			// btnAddConfig
			// 
			btnAddConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			btnAddConfig.Location = new System.Drawing.Point(460, 10);
			btnAddConfig.Name = "btnAddConfig";
			btnAddConfig.Size = new System.Drawing.Size(46, 23);
			btnAddConfig.TabIndex = 10;
			btnAddConfig.Text = "+";
			btnAddConfig.UseVisualStyleBackColor = true;
			btnAddConfig.Click += new System.EventHandler(this.btnAddConfig_Click);
			// 
			// btnRemoveConfig
			// 
			btnRemoveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			btnRemoveConfig.Location = new System.Drawing.Point(512, 10);
			btnRemoveConfig.Name = "btnRemoveConfig";
			btnRemoveConfig.Size = new System.Drawing.Size(23, 23);
			btnRemoveConfig.TabIndex = 10;
			btnRemoveConfig.Text = "-";
			btnRemoveConfig.UseVisualStyleBackColor = true;
			btnRemoveConfig.Click += new System.EventHandler(this.btnRemoveConfig_Click);
			// 
			// tbURL
			// 
			this.tbURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbURL.Location = new System.Drawing.Point(75, 39);
			this.tbURL.Name = "tbURL";
			this.tbURL.Size = new System.Drawing.Size(460, 21);
			this.tbURL.TabIndex = 3;
			// 
			// tbRefer
			// 
			this.tbRefer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRefer.Location = new System.Drawing.Point(75, 93);
			this.tbRefer.Name = "tbRefer";
			this.tbRefer.Size = new System.Drawing.Size(460, 21);
			this.tbRefer.TabIndex = 4;
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.Location = new System.Drawing.Point(75, 66);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.Size = new System.Drawing.Size(379, 21);
			this.tbOutput.TabIndex = 5;
			// 
			// tbSecFetchSite
			// 
			this.tbSecFetchSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSecFetchSite.Location = new System.Drawing.Point(108, 356);
			this.tbSecFetchSite.Name = "tbSecFetchSite";
			this.tbSecFetchSite.Size = new System.Drawing.Size(427, 21);
			this.tbSecFetchSite.TabIndex = 4;
			// 
			// tbSecFetchMode
			// 
			this.tbSecFetchMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSecFetchMode.Location = new System.Drawing.Point(108, 329);
			this.tbSecFetchMode.Name = "tbSecFetchMode";
			this.tbSecFetchMode.Size = new System.Drawing.Size(427, 21);
			this.tbSecFetchMode.TabIndex = 4;
			// 
			// tbSecFetchDest
			// 
			this.tbSecFetchDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSecFetchDest.Location = new System.Drawing.Point(108, 302);
			this.tbSecFetchDest.Name = "tbSecFetchDest";
			this.tbSecFetchDest.Size = new System.Drawing.Size(427, 21);
			this.tbSecFetchDest.TabIndex = 4;
			// 
			// tbCacheControl
			// 
			this.tbCacheControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbCacheControl.Location = new System.Drawing.Point(108, 275);
			this.tbCacheControl.Name = "tbCacheControl";
			this.tbCacheControl.Size = new System.Drawing.Size(427, 21);
			this.tbCacheControl.TabIndex = 4;
			// 
			// tbPragma
			// 
			this.tbPragma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPragma.Location = new System.Drawing.Point(75, 194);
			this.tbPragma.Name = "tbPragma";
			this.tbPragma.Size = new System.Drawing.Size(460, 21);
			this.tbPragma.TabIndex = 4;
			// 
			// tbConnection
			// 
			this.tbConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbConnection.Location = new System.Drawing.Point(108, 221);
			this.tbConnection.Name = "tbConnection";
			this.tbConnection.Size = new System.Drawing.Size(427, 21);
			this.tbConnection.TabIndex = 4;
			// 
			// tbAccept
			// 
			this.tbAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbAccept.Location = new System.Drawing.Point(75, 167);
			this.tbAccept.Name = "tbAccept";
			this.tbAccept.Size = new System.Drawing.Size(460, 21);
			this.tbAccept.TabIndex = 4;
			// 
			// tbUserAgent
			// 
			this.tbUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbUserAgent.Location = new System.Drawing.Point(108, 248);
			this.tbUserAgent.Name = "tbUserAgent";
			this.tbUserAgent.Size = new System.Drawing.Size(427, 21);
			this.tbUserAgent.TabIndex = 4;
			// 
			// tbMethod
			// 
			this.tbMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbMethod.Location = new System.Drawing.Point(75, 120);
			this.tbMethod.Name = "tbMethod";
			this.tbMethod.Size = new System.Drawing.Size(460, 21);
			this.tbMethod.TabIndex = 4;
			// 
			// cbResume
			// 
			this.cbResume.AutoSize = true;
			this.cbResume.Location = new System.Drawing.Point(75, 147);
			this.cbResume.Name = "cbResume";
			this.cbResume.Size = new System.Drawing.Size(15, 14);
			this.cbResume.TabIndex = 8;
			// 
			// cbbConfigs
			// 
			this.cbbConfigs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbbConfigs.FormattingEnabled = true;
			this.cbbConfigs.Location = new System.Drawing.Point(75, 12);
			this.cbbConfigs.Name = "cbbConfigs";
			this.cbbConfigs.Size = new System.Drawing.Size(379, 20);
			this.cbbConfigs.TabIndex = 9;
			this.cbbConfigs.SelectedIndexChanged += new System.EventHandler(this.cbbConfigs_SelectedIndexChanged);
			// 
			// TaskConfigWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 438);
			this.Controls.Add(btnRemoveConfig);
			this.Controls.Add(btnAddConfig);
			this.Controls.Add(this.cbbConfigs);
			this.Controls.Add(this.cbResume);
			this.Controls.Add(btnStart);
			this.Controls.Add(btnBrowse);
			this.Controls.Add(this.tbOutput);
			this.Controls.Add(this.tbMethod);
			this.Controls.Add(this.tbUserAgent);
			this.Controls.Add(this.tbAccept);
			this.Controls.Add(this.tbConnection);
			this.Controls.Add(this.tbPragma);
			this.Controls.Add(this.tbCacheControl);
			this.Controls.Add(this.tbSecFetchDest);
			this.Controls.Add(this.tbSecFetchMode);
			this.Controls.Add(this.tbSecFetchSite);
			this.Controls.Add(this.tbRefer);
			this.Controls.Add(this.tbURL);
			this.Controls.Add(label3);
			this.Controls.Add(label13);
			this.Controls.Add(label12);
			this.Controls.Add(label11);
			this.Controls.Add(label10);
			this.Controls.Add(label9);
			this.Controls.Add(label8);
			this.Controls.Add(label7);
			this.Controls.Add(label6);
			this.Controls.Add(label5);
			this.Controls.Add(label4);
			this.Controls.Add(label2);
			this.Controls.Add(label14);
			this.Controls.Add(label1);
			this.Name = "TaskConfigWindow";
			this.ShowIcon = false;
			this.Text = "Task Config";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox tbURL;
		private System.Windows.Forms.TextBox tbRefer;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.TextBox tbSecFetchSite;
		private System.Windows.Forms.TextBox tbSecFetchMode;
		private System.Windows.Forms.TextBox tbSecFetchDest;
		private System.Windows.Forms.TextBox tbCacheControl;
		private System.Windows.Forms.TextBox tbPragma;
		private System.Windows.Forms.TextBox tbConnection;
		private System.Windows.Forms.TextBox tbAccept;
		private System.Windows.Forms.TextBox tbUserAgent;
		private System.Windows.Forms.TextBox tbMethod;
		private System.Windows.Forms.CheckBox cbResume;
		private System.Windows.Forms.ComboBox cbbConfigs;
	}
}