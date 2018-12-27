using System.Drawing;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  編集中の文字列を選択する操作をサポートするエディタを表します。
	/// </summary>
	public interface ITextSelectionFeature : ISelectionFeature
	{
		/// <summary>
		///  選択されている文字列を取得または設定します。
		/// </summary>
		string SelectedText { get; set; }

		/// <summary>
		///  選択されている文字列の全ての文字列中での位置を取得します。
		/// </summary>
		int SelectionIndex { get; }

		/// <summary>
		///  選択されている文字列の全ての文字列中での最終位置を取得します。
		/// </summary>
		int SelectionLastIndex { get; }

		/// <summary>
		///  選択されている文字列の長さを取得または設定します。
		/// </summary>
		int SelectionLength { get; }
	}
}
