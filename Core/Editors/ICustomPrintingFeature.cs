using System.Drawing.Printing;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  印刷、印刷プレビュー、ページ設定を行う事ができるエディタを表します。
	/// </summary>
	public interface ICustomPrintingFeature
	{
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
