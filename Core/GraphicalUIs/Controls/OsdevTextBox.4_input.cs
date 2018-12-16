using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		#region キーボード
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
						//this.RemoveStringFrom(_row_ss, _col_ss, 1);
					} else {
						//this.AddStringTo(_row_ss, _col_ss, k.ToString());
					}
					++_col_ss;
				}
				break;
			}

			_logger.Trace($"completed {nameof(OnKeyDown)}");
		}
		#endregion

		#region マウス
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseWheel"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseWheel)}...");

			base.OnMouseWheel(e);
			int v = vScrollBar.Value - e.Delta / 50;
			if (v < vScrollBar.Minimum) v = vScrollBar.Minimum;
			if (v > vScrollBar.Maximum) v = vScrollBar.Maximum;
			vScrollBar.Value = v;
			_row_sb = v;
			this.Invalidate();

			_logger.Trace($"completed {nameof(OnMouseWheel)}");
		}
		#endregion

		#region 未実装
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseDown"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseDown)}...");

			base.OnMouseDown(e);

			_logger.Trace($"completed {nameof(OnMouseDown)}");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseMove"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseMove)}...");

			base.OnMouseMove(e);

			_logger.Trace($"completed {nameof(OnMouseMove)}");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseUp"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMouseUp)}...");

			base.OnMouseDown(e);

			_logger.Trace($"completed {nameof(OnMouseUp)}");
		}
		#endregion
	}
}
