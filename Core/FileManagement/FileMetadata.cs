namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルデータを表します。
	/// </summary>
	public class FileMetadata
	{
		/// <summary>
		///  このファイルのファイル名を取得します。
		/// </summary>
		public PathString FileName { get; }

		/// <summary>
		///  このファイルのフォーマットを取得します。
		/// </summary>
		public virtual FileFormat Format { get; }

		public virtual void Read(byte[] buf, int offset, int length)
		{

		}
	}
}
