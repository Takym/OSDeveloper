using System.Drawing.Printing;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  印刷に関する操作をサポートするエディタを表します。
	/// </summary>
	public interface IPrintingFeature
	{
		/// <summary>
		///  ドキュメントの印刷に関する情報を保持するオブジェクトを取得します。
		/// </summary>
		PrintDocument PrintDocument { get; }

		/// <summary>
		///  <see cref="OSDeveloper.Core.Editors.IPrintingFeature.ShowPrintDialog"/>等のメソッドを利用して
		///  独自のダイアログを表示する場合は<see langword="true"/>、
		///  <see cref="OSDeveloper.Core.Editors.IPrintingFeature.PrintDocument"/>プロパティを利用して
		///  限定のダイアログを表示する場合は<see langword="false"/>です。
		/// </summary>
		bool UseCustomDialogs { get; }

		/// <summary>
		///  印刷ダイアログを表示して印刷します。
		/// </summary>
		void ShowPrintDialog();

		/// <summary>
		///  印刷プレビューダイアログを表示します。
		/// </summary>
		void ShowPrintPreviewDialog();

		/// <summary>
		///  ページ設定ダイアログを表示します。
		/// </summary>
		void ShowPageSetupDialog();
	}
}
