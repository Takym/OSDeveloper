using System.Windows.Forms;

namespace OSDeveloper.Core.GraphicalUIs
{
	/// <summary>
	///  アプリケーションのメインウィンドウを表します。
	/// </summary>
	public abstract class MainWindowBase : Form
	{
		/// <summary>
		///  このウィンドウの<see cref="System.Windows.Forms.MdiClient"/>を取得します。
		/// </summary>
		/// <returns>取得した<see cref="System.Windows.Forms.MdiClient"/>です。</returns>
		internal MdiClient GetMdiClient()
		{
			if (_mdi_client == null) {
				foreach (var control in this.Controls) {
					_mdi_client = control as MdiClient;
					if (_mdi_client != null) {
						break; // MdiClient を取得出来たら抜ける
					}
				}
			}
			return _mdi_client;
		}
		private MdiClient _mdi_client = null;
	}
}
