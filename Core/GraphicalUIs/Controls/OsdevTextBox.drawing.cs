using System.Drawing;
using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.Paint"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.PaintEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnPaint(PaintEventArgs e)
		{
			this.SuspendLayout();
			base.OnPaint(e);

			e.Graphics.Clear(this.BackColor);
			{
				int x = _font.Height * 3;
				e.Graphics.DrawLine(Pens.Salmon, x, 0, x, this.Height);
				for (int i = 0; i < this.Width; ++i) {
					x = i * _font.Height / 2;
					if (i % 2 == 0) {
						e.Graphics.DrawLine(Pens.LightSalmon, x, 0, x, _font.Height);
					} else {
						e.Graphics.DrawLine(Pens.DarkSalmon, x, 0, x, _font.Height / 2);
					}
				}
			}
			for (int i = 0; i < _lines.Length; ++i) {
				int y = (i + 1) * _font.Height;
				e.Graphics.DrawString($"{i + 1:D5}", _font, Brushes.Salmon, new Point(0, y));
				e.Graphics.DrawString(_lines[i], _font, new SolidBrush(this.ForeColor), new Point(_font.Height * 3, y));
			}

			this.ResumeLayout(false);
		}
	}
}
