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
			int fh = _font.Height, fw = fh / 2;

			using (Pen a = new Pen(this.GridColor.Light))
			using (Pen b = new Pen(this.GridColor.Dark))
			using (Pen c = new Pen(this.GridColor.Normal)) {
				this.DrawGrid(e.Graphics, fh, fw, a, b, c);
			}

			for (int i = 0; i < _lines.Length; ++i) {
				int y = (i + 1) * _font.Height;
				e.Graphics.DrawString($"{i + 1:D5}", _font, Brushes.Salmon, new Point(0, y));
				e.Graphics.DrawString(_lines[i], _font, new SolidBrush(this.ForeColor), new Point(_font.Height * 3, y));
			}

			this.ResumeLayout(false);
		}

		private void DrawGrid(Graphics g, int fh, int fw, Pen a, Pen b, Pen c)
		{
			// 行番号と文字列の間に線を描画
			int x = fw * 6;
			g.DrawLine(c, x, 0, x, this.Height);

			// 上部にフォント幅の目安を描画
			using (Pen p = new Pen(Color.FromArgb(48, this.ForeColor))) {
				for (int i = 0; i < this.Width; i += fw) {
					if (i % fh == 0) {
						g.DrawLine(a, i, 0, i, fh);
						g.DrawLine(p, i, fh + 1, i, this.Height);
					} else {
						g.DrawLine(b, i, 0, i, fw);
						g.DrawLine(p, i, fw + 1, i, this.Height);
					}
				}
			}
		}
	}
}
