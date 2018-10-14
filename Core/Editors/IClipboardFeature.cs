namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  クリップボードに関する操作をサポートするエディタを表します。
	/// </summary>
	public interface IClipboardFeature : ISelectionFeature
	{
		/// <summary>
		///  選択中のオブジェクトをクリップボードにコピーします。
		/// </summary>
		void Copy();

		/// <summary>
		///  選択中のオブジェクトにクリップボードにあるオブジェクトを貼り付けます。
		/// </summary>
		void Paste();

		/// <summary>
		///  選択中のオブジェクトをグリップボードにコピーしその後削除します。
		/// </summary>
		void Cut();

		/// <summary>
		///  選択中のオブジェクトを削除します。
		/// </summary>
		void Delete();
	}
}
