using System.Windows.Forms;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.GraphicalUIs.ToolStrips
{
	/// <summary>
	///  名前空間'<see cref="OSDeveloper.Core.GraphicalUIs.ToolStrips"/>'以下に存在する
	///  メニューの状態を管理します。このクラスは静的です。
	/// </summary>
	public static partial class MenuStripManager
	{
		// メニュー状態の管理は建前でこのクラスの本当の目的は、
		// メニューコントロールがコンポーネントデザイナで表示されるのを回避する事。
		// ファイルの先頭にコンポーネントクラスが無いとコンポーネントデザイナで、
		// ファイルを開くことが出来ないので、このクラスの宣言を各メニュークラスの先頭に設置する。
		// OSDeveloper.App.FormMain でも似たような仕組みを使っている。

		internal static readonly Logger _logger;
		internal static bool _use_ex_dialog;

		/// <summary>
		///  印刷ダイアログの拡張モードを有効にするかどうかを表す値を取得します。
		/// </summary>
		public static bool UseEXDialog
		{
			get
			{
				return _use_ex_dialog;
			}
		}

		static MenuStripManager()
		{
			_logger = Logger.GetSystemLogger(nameof(MenuStripManager));
		}
	}

	/// <summary>
	///  メインメニューを表す<see cref="System.Windows.Forms.MenuStrip"/>の項目です。
	/// </summary>
	public abstract class MainMenuItem : ToolStripMenuItem
	{
		internal MainMenuItem() { } // コンストラクタの隠蔽
	}
}
