using System.Windows.Forms;
using OSDeveloper.Core.Editors;

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

		/// <summary>
		///  このウィンドウの<see cref="System.Windows.Forms.MdiClient"/>を取得します。
		/// </summary>
		protected MdiClient MdiClient
		{
			get
			{
				if (_mdi_client == null) {
					return this.GetMdiClient();
				} else {
					return _mdi_client;
				}
			}
		}

		/// <summary>
		///  現在利用されているエディタウィンドウ(MDI子ウィンドウ)を取得します。
		/// </summary>
		/// <returns>
		///  型'<see cref="OSDeveloper.Core.Editors.EditorWindow"/>'に変換可能なフォームオブジェクトです。
		///  変換不可能または利用しているエディタが無い場合は<see langword="null"/>が返されます。
		/// </returns>
		public EditorWindow GetActiveEditor()
		{
			return this.ActiveMdiChild as EditorWindow;
		}

		/// <summary>
		///  このウィンドウのステータスメッセージを設定します。
		/// </summary>
		/// <param name="msg">ウィンドウ下部に表示されるメッセージです。</param>
		public abstract void SetStatusMessage(string msg);
	}
}
