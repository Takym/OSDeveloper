using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.KeyPress"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.KeyPressEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnKeyPress)}...");
			base.OnKeyPress(e);

			if (e.KeyChar == '\b') {
				if (this.Text.Length != 0) {
					this.Text = this.Text.Substring(0, this.Text.Length - 1);
				}
			} else {
				this.Text += e.KeyChar;
			}
			_line = _lines.Length - this.Height / _font.Height + 2;
			if (_line < 0) _line = 0;
			vScrollBar.Value = _line;
			this.Invalidate();
			e.Handled = true;

			_logger.Trace($"completed {nameof(OnKeyPress)}");
		}
	}
}
