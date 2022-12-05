using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpDownloader
{
	class MyProgressBar : ProgressBar
	{
		public MyProgressBar()
		{
			// Modify the ControlStyles flags
			//http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		[Bindable(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var rect = ClientRectangle;
			var g = e.Graphics;

			ProgressBarRenderer.DrawHorizontalBar(g, rect);
			var chunks = rect;
			chunks.Inflate(-2, -2);
			chunks.Width = (int)((double)Value / Maximum * chunks.Width);
			ProgressBarRenderer.DrawHorizontalChunks(g, chunks);

			var t = Text;
			var f = Font;
			var size = g.MeasureString(t, f);
			g.DrawString(t, f, SystemBrushes.ControlText, (rect.Width - size.Width) / 2, (rect.Height - size.Height) / 2);
		}
	}
}
