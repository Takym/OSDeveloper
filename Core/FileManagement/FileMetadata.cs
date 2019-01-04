using System;
using System.IO;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルのメタ情報を表します。
	/// </summary>
	public class FileMetadata
	{
		/// <summary>
		///  このオブジェクトが参照しているファイルのファイルパスを取得します。
		/// </summary>
		public PathString FilePath { get; }

		/// <summary>
		///  このオブジェクトが参照しているファイルの名前を取得します。
		/// </summary>
		public string Name
		{
			get
			{
				return Path.GetFileName(this.FilePath);
			}
		}


		/// <summary>
		///  このオブジェクトが参照しているファイルの親ディレクトリのファイルパスを取得します。
		/// </summary>
		public PathString ParentPath
		{
			get
			{
				return new PathString(Path.GetDirectoryName(this.FilePath));
			}
		}

		/// <summary>
		///  このオブジェクトが参照しているファイルのフォーマットを取得します。
		/// </summary>
		public virtual FileFormat Format { get; }

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="filename">読み込むファイルの名前です。</param>
		public FileMetadata(string filename)
		{
			var ftype = FileTypes.CheckFileType(Path.GetExtension(filename).Substring(1));
			this.FilePath = new PathString(Path.GetFullPath(filename));
			this.Format = ftype?.Format ?? FileFormat.Unknown;
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.FileManagement.FileMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="filename">読み込むファイルの名前です。</param>
		/// <param name="format">読み込むファイルの種類です。</param>
		/// <exception cref="System.ArgumentException"/>
		public FileMetadata(string filename, FileFormat format)
		{
			if (format == FileFormat.Directory && !typeof(DirMetadata).IsAssignableFrom(this.GetType())) {
				throw new ArgumentException(ErrorMessages.FileMetadata_FileCannotBeDir, nameof(format));
			}

			this.FilePath = new PathString(Path.GetFullPath(filename));
			this.Format = format;
		}

		/// <summary>
		///  ストリームを新たに生成して返します。
		/// </summary>
		/// <returns>生成されたストリームです。</returns>
		public virtual Stream CreateStream()
		{
			return new FileStream(this.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>
		///  ファイル名を変更します。
		/// </summary>
		/// <param name="newName">変更後の新しいファイル名です。</param>
		/// <exception cref="System.ArgumentException" />
		/// <exception cref="System.ArgumentNullException" />
		/// <exception cref="System.UnauthorizedAccessException" />
		/// <exception cref="System.IO.IOException" />
		/// <exception cref="System.IO.PathTooLongException" />
		public virtual void Rename(string newName)
		{
			if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
				throw new ArgumentException(string.Format(ErrorMessages.IO_InvalidFileNameString, newName), nameof(newName));
			}
			File.Move(this.FilePath, Path.Combine(Path.GetDirectoryName(this.FilePath), newName));
		}

		/// <summary>
		///  このファイルを編集するためのエディタウィンドウを生成します。
		/// </summary>
		/// <param name="mwndbase">このエディタのMDI親ウィンドウです。</param>
		/// <returns>新しく生成されたエディタウィンドウオブジェクトです。</returns>
		public virtual EditorWindow CreateEditor(MainWindowBase mwndbase)
		{
			//var result = new EditorWindow(mwndbase);
			var result = new TextEditor(mwndbase);
			result.TargetFile = this;
			return result;
		}
	}
}
