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
				// TODO: 文字列描画
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
	}
}
