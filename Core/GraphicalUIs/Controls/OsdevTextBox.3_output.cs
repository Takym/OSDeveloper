using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.Assets;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		/// <summary>
		///  スクロールバーの位置を表します。
		///  <see cref="_row_sb"/>は行(垂直,<see cref="vScrollBar"/>)を表し、
		///  <see cref="_col_sb"/>は列(水平,<see cref="hScrollBar"/>,未使用)を表します。
		/// </summary>
		private int _row_sb, _col_sb;

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.Paint"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.PaintEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			this.SuspendLayout();
			base.OnPaint(e);

			e.Graphics.Clear(this.BackColor);
			int fh = _font.Height, fw = fh / 2;

			using (Pen a = new Pen(_grid_col.Light))
			using (Pen b = new Pen(_grid_col.Dark))
			using (Pen c = new Pen(_grid_col.Normal)) {
				this.DrawGrid(e.Graphics, fh, fw, a, b, c);
			}

			using (SolidBrush l = new SolidBrush(_grid_col.Normal))
			using (SolidBrush s = new SolidBrush(_sel_col.Normal))
			using (SolidBrush t = new SolidBrush(this.ForeColor)) {
				int x = 0, y = 1;
				for (int i = 0; i < _text.Count; ++i) {
					if (x == 0) {
						this.DrawLine(e.Graphics, ref x, y, fw, fh, l);
					}
					this.DrawChar(e.Graphics, ref x, ref y, fw, fh, s, t, _text[i]);
				}
			}

			this.ResumeLayout(false);
			_logger.Trace($"completed {nameof(OnPaint)}");
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
						g.DrawLine(a, i,      0, i, fh);
						g.DrawLine(p, i, fh + 1, i, this.Height);
					} else {
						g.DrawLine(b, i,      0, i, fw);
						g.DrawLine(p, i, fw + 1, i, this.Height);
					}
				}
			}
		}

		private void DrawLine(Graphics g, ref int x, int y, int fw, int fh, Brush a)
		{
			g.DrawString($"{y:D5} ", _font, a, x * fw, y * fh);
			x = 6;
		}

		private void DrawChar(Graphics g, ref int x, ref int y, int fw, int fh, Brush a, Brush b, uint c)
		{
			if (c == 0x000A) {
				x = 0;
				++y;
			} else if (c == 0x0009) {
				do ++x; while (x % 4 != 0);
			} else if (c == 0x0020) {
				++x;
			} else if (c == 0x3000) {
				x += 2;
			} else {
				g.DrawString(this.GetTextPrivate(c), _font, b, x * fw, y * fh);
				var t = EastAsianWidth.Current.GetValue(c);
				switch (t) {
					case EAWType.Fullwidth:
					case EAWType.Wide:
					case EAWType.Ambiguous:
						x += 2;
						break;
					case EAWType.Halfwidth:
					case EAWType.Narrow:
					case EAWType.Neutral:
						++x;
						break;
				}
			}
		}
	}
}
