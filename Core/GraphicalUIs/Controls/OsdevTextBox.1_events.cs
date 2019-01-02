using System;
using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.GridColor"/>が変更された時に発生します。
		/// </summary>
		public event EventHandler GridColorChanged;

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.SelectionColorChanged"/>が変更された時に発生します。
		/// </summary>
		public event EventHandler SelectionColorChanged;

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.TextChanged"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnTextChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnTextChanged)}...");

			base.OnTextChanged(e);
			this.Invalidate();

			int x = 0;
			for (int i = 0; i < _text.Count; ++i) {
				if (_text[i] == 0x0A) ++x;
			}
			vScrollBar.Maximum = x;
			if (x < _row_sb) {
				_row_sb = x - 1;
				vScrollBar.Value = x - 1;
			}

			_logger.Trace($"completed {nameof(OnTextChanged)}");
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.GridColorChanged"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected virtual void OnGridColorChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnGridColorChanged)}...");

			var h = this.GridColorChanged;
			if (h != null) {
				h.Invoke(this, e);
			}

			this.Invalidate();

			_logger.Trace($"completed {nameof(OnGridColorChanged)}");
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.SelectionColorChanged"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected virtual void OnSelectionColorChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnSelectionColorChanged)}...");

			var h = this.SelectionColorChanged;
			if (h != null) {
				h.Invoke(this, e);
			}

			this.Invalidate();

			_logger.Trace($"completed {nameof(OnSelectionColorChanged)}");
		}

#pragma warning disable CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
		/// <summary>
		///  背景描画イベントを無効化します。
		/// </summary>
		/// <param name="e">利用されていません。互換性の為に存在しています。</param>
		[Obsolete("このクラスでは背景描画イベントは利用されていません。")]
		protected sealed override void OnPaintBackground(PaintEventArgs e)
		{
			// 処理速度向上のため背景描画停止。
		}
#pragma warning restore CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
	}
}
