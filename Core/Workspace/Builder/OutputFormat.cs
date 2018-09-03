namespace OSDeveloper.Core.Workspace.Builder
{
	/// <summary>
	///  出力形式を表します。
	/// </summary>
	public enum OutputFormat
	{
		/// <summary>
		///  機械語にコンパイルされたプログラムを表します。
		/// </summary>
		Program,

		/// <summary>
		///  自動生成されたソースコードを表します。
		/// </summary>
		SourceCode,

		/// <summary>
		///  リソースとして埋め込まれるオブジェクトを表します。
		/// </summary>
		Resource,

		/// <summary>
		///  ドキュメントとして埋め込まれるオブジェクトを表します。
		/// </summary>
		Document
	}
}
