namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  編集中のオブジェクトを選択する操作をサポートするエディタを表します。
	/// </summary>
	public interface ISelectionFeature
	{
		/// <summary>
		///  エディタ内にある全てのオブジェクトを選択します。
		/// </summary>
		void SelectAll();

		/// <summary>
		///  選択を解除します。
		/// </summary>
		void ClearSelection();
	}
}
