namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルのフォーマットを表します。
	/// </summary>
	public enum FileFormat
	{
		/// <summary>
		///  不明なファイルを表します。
		/// </summary>
		Unknown,

		/// <summary>
		///  ディレクトリを表します。
		///  この値を持つ<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>は
		///  <see cref="OSDeveloper.Core.FileManagement.DirMetadata"/>を必ず継承している必要があります。
		/// </summary>
		Directory,

		/// <summary>
		///  バイナリ形式のファイルを表します。
		/// </summary>
		BinaryFile,

		/// <summary>
		///  テキスト形式のファイルを表します。
		/// </summary>
		TextFile,

		/// <summary>
		///  機械語にコンパイルされたプログラムを表します。
		/// </summary>
		Program,

		/// <summary>
		///  ソースコードを表します。
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