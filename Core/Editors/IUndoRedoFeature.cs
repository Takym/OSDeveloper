namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  作業履歴の操作をサポートするエディタを表します。
	/// </summary>
	public interface IUndoRedoFeature
	{
		/// <summary>
		///  作業を一つ元に戻せるかどうかを表す論理値を取得します。
		/// </summary>
		bool CanUndo { get; }

		/// <summary>
		///  取り消した作業をやり直せるかどうかを表す論理値を取得します。
		/// </summary>
		bool CanRedo { get; }

		/// <summary>
		///  作業を一つ元に戻します。
		/// </summary>
		void Undo();

		/// <summary>
		///  取り消した作業をやり直します。
		/// </summary>
		void Redo();
	}
}
