using System.Drawing;
using System.Windows.Forms;

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
			using (SolidBrush t = new SolidBrush(this.ForeColor)) {
				int y = fh;
				for (int i = _row_sb; i < _lines.Count; ++i) {
					e.Graphics.DrawString($"{i + 1:D5}", _font, l, new Point(0, y));
					this.DrawTextLine(e.Graphics, fh, fw, i, y, t, l);
					y += fh;
					if (y >= this.Height) return;
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
						g.DrawLine(a, i, 0, i, fh);
						g.DrawLine(p, i, fh + 1, i, this.Height);
					} else {
						g.DrawLine(b, i, 0, i, fw);
						g.DrawLine(p, i, fw + 1, i, this.Height);
					}
				}
			}
		}

		private void DrawTextLine(Graphics g, int fh, int fw, int i, int y, SolidBrush t, SolidBrush ws)
		{
			// 一文字ずつ描画
			int x = 0;
			for (int j = 0; j < _lines[i].Length; ++j) {
				char c = _lines[i][j];
				switch (c) {
					case '\t':
						g.DrawString("→", _font, ws, new Point(fw * (x + 6), y));
						break;
					case ' ':
						g.DrawString("・", _font, ws, new Point(fw * (x + 6) - fw / 2, y));
						break;
					case '　':
						g.DrawString("▪", _font, ws, new Point(fw * (x + 6) + fw / 2, y));
						break;
					default:
						g.DrawString(c.ToString(), _font, t, new Point(fw * (x + 6), y));
						break;
				}
				if (_row_ss == i && _col_ss == j) {
					g.DrawLine(Pens.White, new Point(fw * (x + 6), y), new Point(fw * (x + 6), y + fh));
				}
				if (0x20 <= c && c <= 0x7F) { // ASCII
					if (c == '\t') {          // タブ
						x += 4 - (x % 4);
					} else {                  // 半角文字
						++x;
					}
				} else {                      // Unicode
					                          // 全角文字 (一部半角文字)
					x += 2;
				}
			}
		}
	}
}
