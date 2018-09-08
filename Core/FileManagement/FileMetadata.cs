using System.IO;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルのメタ情報を表します。
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

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="filename">読み込むファイルです。</param>
		public FileMetadata(string filename)
		{
			this.FileName = new PathString(filename);
			this.Format = FileFormat.BinaryFile;
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="filename">読み込むファイルです。</param>
		/// <param name="format">読み込むファイルの種類です。</param>
		public FileMetadata(string filename, FileFormat format)
		{
			this.FileName = new PathString(filename);
			this.Format = format;
		}

		/// <summary>
		///  ストリームを新たに生成して返します。
		/// </summary>
		/// <returns>生成されたストリームです。</returns>
		public virtual Stream CreateStream()
		{
			return new FileStream(this.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
		}
	}
}
