namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  編集内容を検索または置換をサポートするエディタを表します。
	/// </summary>
	public interface ISearchReplaceFeature : ISelectionFeature
	{
		/// <summary>
		///  指定された文字列を検索し何個存在するか求めます。
		/// </summary>
		/// <param name="str">検索する文字列です。</param>
		/// <returns>編集内容に<paramref name="str"/>が何個存在するかを表す値です。</returns>
		int Find(string str);

		/// <summary>
		///  指定された文字列を検索し選択します。
		/// </summary>
		/// <param name="str">検索する文字列です。</param>
		/// <returns>
		///  編集内容に<paramref name="str"/>が存在する場合は<see langword="true"/>、
		///  存在しない場合は<see langword="false"/>です。
		/// </returns>
		bool FindNext(string str);

		/// <summary>
		///  <paramref name="oldStr"/>を編集内容から検索し<paramref name="newStr"/>に置換します。
		/// </summary>
		/// <param name="oldStr">検索する文字列です。</param>
		/// <param name="newStr">置換後の文字列です。</param>
		///  編集内容に<paramref name="str"/>が存在し置換に成功した場合は<see langword="true"/>、
		///  存在しない場合は<see langword="false"/>です。
		/// </returns>
		bool ReplaceNext(string oldStr, string newStr);

		/// <summary>
		///  編集内容に存在する全ての<paramref name="oldStr"/>を<paramref name="newStr"/>に置換します。
		/// </summary>
		/// <param name="oldStr">検索する文字列です。</param>
		/// <param name="newStr">置換後の文字列です。</param>
		void ReplaceAll(string oldStr, string newStr);
	}
}
