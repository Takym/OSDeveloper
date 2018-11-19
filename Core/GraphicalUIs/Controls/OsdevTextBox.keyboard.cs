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
			base.OnKeyPress(e);

			this.Text += e.KeyChar;
			e.Handled = true;
		}
	}
}
