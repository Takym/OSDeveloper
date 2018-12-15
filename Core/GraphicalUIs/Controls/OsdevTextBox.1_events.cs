using System;

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
	}
}
