using System.Diagnostics;
using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using static OSDeveloper.Core.GraphicalUIs.ToolStrips.MenuStripManager;

namespace OSDeveloper.Core.GraphicalUIs.ToolStrips
{
	partial class MenuStripManager { } // デザイナ避け

	/// <summary>
	///  ツールを呼び出したり設定を管理するメニューを表します。
	/// </summary>
	public class ToolMenuItem : MainMenuItem
	{
		private readonly MainWindowBase _mwnd_base;
		private readonly ToolStripMenuItem _install_themepack_darkolorfuler;

		/// <summary>
		///  このメニューの操作対象を指定して、
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips.ToolMenuItem"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="mwndBase">このメニューの親ウィンドウです。</param>
		public ToolMenuItem(MainWindowBase mwndBase)
		{
			_mwnd_base = mwndBase;
			_install_themepack_darkolorfuler = new ToolStripMenuItem();

			_install_themepack_darkolorfuler.Text = string.Format(MenuTexts.Tool_InstallThemepack, "Darkolorfuler");
			_install_themepack_darkolorfuler.Click += this._install_themepack_darkolorfuler_Click;

			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_install_themepack_darkolorfuler);
			this.DropDownItems.Add(new ToolStripSeparator());
		}

		private void _install_themepack_darkolorfuler_Click(object sender, System.EventArgs e)
		{
			_logger.Trace($"{nameof(ToolMenuItem)}: Installing Darkolorfuler...");

			// OSDeveloper独自のテーマパックをインストールする
			//  - Darkolorfuler : 全体的に黒っぽいテーマ。
			//                    非クライアント領域にビジュアルスタイルを適応しない状態でIDEを起動すると、
			//                    メインウィンドウとMDI子ウィンドウのタイトルバーがカラフルになる。

			var dr = MessageBox.Show(_mwnd_base,
				string.Format(MenuTexts.Tool_InstallThemepack_Confirm, "Darkolorfuler"),
				_mwnd_base.Text,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (dr == DialogResult.Yes) {
				Process.Start(SystemPaths.Resources.Bond("Darkolorfuler.themepack"));
				_mwnd_base.SetStatusMessage(MenuTexts.Tool_InstallThemepack_StatusMsg);
				_logger.Trace($"{nameof(ToolMenuItem)}: Installed Darkolorfuler");
			} else {
				_mwnd_base.SetStatusMessage(MenuTexts.Tool_InstallThemepack_StatusMsg_Cancel);
				_logger.Trace($"{nameof(ToolMenuItem)}: Canceled to install Darkolorfuler");
			}
		}
	}
}
