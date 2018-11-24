using System;
using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseDown"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseMove"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseUp"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.MouseWheel"/>イベントを発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.Windows.Forms.MouseEventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			int v = vScrollBar.Value - e.Delta / 50;
			if (v < vScrollBar.Minimum) v = vScrollBar.Minimum;
			if (v > vScrollBar.Maximum) v = vScrollBar.Maximum;
			vScrollBar.Value = v;
			_line = v;
			this.Invalidate();
		}
	}
}
