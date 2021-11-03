namespace HttpDownloader
{
	partial class Downloader
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOther = new System.Windows.Forms.Button();
			this.progress = new HttpDownloader.MyProgressBar();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblName.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblName.Location = new System.Drawing.Point(3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(218, 17);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.FlatAppearance.BorderSize = 0;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancel.Location = new System.Drawing.Point(265, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(32, 44);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "✖";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOther
			// 
			this.btnOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOther.FlatAppearance.BorderSize = 0;
			this.btnOther.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOther.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOther.Location = new System.Drawing.Point(227, 3);
			this.btnOther.Name = "btnOther";
			this.btnOther.Size = new System.Drawing.Size(32, 44);
			this.btnOther.TabIndex = 2;
			this.btnOther.Text = "▶";
			this.btnOther.UseVisualStyleBackColor = true;
			this.btnOther.Click += new System.EventHandler(this.btnOther_Click);
			// 
			// progress
			// 
			this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progress.Location = new System.Drawing.Point(6, 23);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(215, 24);
			this.progress.TabIndex = 1;
			// 
			// Downloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnOther);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.lblName);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "Downloader";
			this.Size = new System.Drawing.Size(300, 50);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOther;
		private MyProgressBar progress;
	}
}
