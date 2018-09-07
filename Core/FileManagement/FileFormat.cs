namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルのフォーマットを表します。
	/// </summary>
	public enum FileFormat
	{
		/// <summary>
		///  テキスト形式のファイルを表します。
		/// </summary>
		TextFile,

		/// <summary>
		///  バイナリ形式のファイルを表します。
		/// </summary>
		BinaryFile,

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