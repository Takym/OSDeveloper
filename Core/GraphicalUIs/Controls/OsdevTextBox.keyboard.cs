using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		// TODO: OsdevTextBox.keyboard: 仮コード

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.OnKeyDown"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.KeyEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnKeyDown)}...");
			base.OnKeyDown(e);

			switch (e.KeyCode) {
				case Keys.Escape: { // ESCキー、コマンドタブを開く
					e.Handled = true;
					MainWindowBase.Current.OpenTerminalTab(this.CommandTab);
				}
				break;
				case Keys.Tab: {

				}
				break;
				default: {
					e.Handled = true;
					char k = ((char)(e.KeyCode & Keys.KeyCode));
					if (k == '\b') {
						this.RemoveStringFrom(_row_ss, _col_ss, 1);
					} else {
						this.AddStringTo(_row_ss, _col_ss, k.ToString());
					}
					++_col_ss;
				}
				break;
			}



			/*

			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				MainWindowBase.Current.OpenTerminalTab(this.CommandTab);
			} else if (e.KeyCode == Keys.Tab) {
				e.Handled = true;
				this.Text += '\t';
				_row_sb = _lines.Length - this.Height / _font.Height + 2;
				if (_row_sb < 0) _row_sb = 0;
				vScrollBar.Value = _row_sb;
				this.Invalidate();
			}

			//*/

			_logger.Trace($"completed {nameof(OnKeyDown)}");
		}

		/*

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
			} else if (e.KeyChar >= ' ' || e.KeyChar == '\n' || e.KeyChar == '\r' || e.KeyChar == '\t') {
				this.Text += e.KeyChar;
			} else {
				goto end;
			}
			_row_sb = _lines.Length - this.Height / _font.Height + 2;
			if (_row_sb < 0) _row_sb = 0;
			vScrollBar.Value = _row_sb;
			this.Invalidate();

end:
			e.Handled = true;
			_logger.Trace($"completed {nameof(OnKeyPress)}");
		}

		//*/
	}
}
