using System.Collections.Generic;
using System.IO;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ディレクトリのメタ情報を表します。
	/// </summary>
	public class DirMetadata : FileMetadata
	{
		private DirectoryInfo _dinfo;
		private Dictionary<string, DirMetadata> _dirs;
		private Dictionary<string, FileMetadata> _files;

		/// <summary>
		///  このファイルのフォーマットを取得します。
		/// </summary>s
		public override FileFormat Format
		{
			get
			{
				return FileFormat.Directory;
			}
		}

		/// <summary>
		///  ディレクトリのフルパスを指定して、
		///  型'<see cref="OSDeveloper.Core.FileManagement.DirMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="dirname">読み込むディレクトリの名前です。</param>
		public DirMetadata(string dirname) : base(dirname, FileFormat.Directory)
		{
			if (Directory.Exists(dirname)) {
				_dinfo = new DirectoryInfo(dirname);
			} else {
				_dinfo = Directory.CreateDirectory(dirname);
			}

			_dirs = new Dictionary<string, DirMetadata>();
			_files = new Dictionary<string, FileMetadata>();
			this.LoadFiles();
		}

		/// <summary>
		///  ディレクトリ情報(<see cref="System.IO.DirectoryInfo"/>)を指定して、
		///  型'<see cref="OSDeveloper.Core.FileManagement.DirMetadata"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="dinfo">読み込むディレクトリの情報が格納されているオブジェクトです。</param>
		public DirMetadata(DirectoryInfo dinfo) : base(dinfo.FullName, FileFormat.Directory)
		{
			_dinfo = dinfo;
			_dirs = new Dictionary<string, DirMetadata>();
			_files = new Dictionary<string, FileMetadata>();
			this.LoadFiles();
		}

		private void LoadFiles()
		{
			foreach (var dir in _dinfo.GetDirectories()) {
				_dirs.Add(dir.Name, new DirMetadata(dir));
			}

			foreach (var file in _dinfo.GetFiles()) {
				_files.Add(file.Name, new FileMetadata(file.FullName, FileFormat.BinaryFile));
			}
		}

		/// <summary>
		///  ディレクトリをこのディレクトリから取得します。
		///  指定されたディレクトリが存在しなければ、新しく作成します。
		/// </summary>
		/// <param name="name">開くディレクトリのディレクトリ名です。</param>
		/// <returns>取得したディレクトリのメタ情報です。</returns>
		public DirMetadata CreateDirectory(string name)
		{
			if (_dirs.ContainsKey(name)) {
				return _dirs[name];
			} else {
				DirMetadata result = new DirMetadata(this.FileName.Bond(name));
				_dirs.Add(name, result);
				return result;
			}
		}

		/// <summary>
		///  ファイルをこのディレクトリから取得します。
		///  指定されたファイルが存在しなければ、新しく作成します。
		/// </summary>
		/// <param name="name">開くファイルのファイル名です。</param>
		/// <returns>取得したファイルのメタ情報です。</returns>
		public FileMetadata CreateFile(string name)
		{
			if (_files.ContainsKey(name)) {
				return _files[name];
			} else {
				FileMetadata result = new FileMetadata(this.FileName.Bond(name));
				_files.Add(name, result);
				return result;
			}
		}

		/// <summary>
		///  指定されたディレクトリが存在するかどうかを判定します。
		/// </summary>
		/// <param name="name">判定するディレクトリ名です。</param>
		/// <returns>存在する場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public bool ExistsDirectory(string name)
		{
			return _dirs.ContainsKey(name);
		}

		/// <summary>
		///  指定されたファイルが存在するかどうかを判定します。
		/// </summary>
		/// <param name="name">判定するファイル名です。</param>
		/// <returns>存在する場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public bool ExistsFile(string name)
		{
			return _files.ContainsKey(name);
		}

		/// <summary>
		///  このディレクトリに格納されている全てのディレクトリを取得します。
		/// </summary>
		/// <returns>ディレクトリ名のみが格納されている文字列配列です。</returns>
		public string[] GetDirectories()
		{
			var keys = _dirs.Keys;
			string[] result = new string[keys.Count];
			int i = 0;
			foreach (var item in keys) {
				result[i] = item;
				++i;
			}
			return result;
		}

		/// <summary>
		///  このディレクトリに格納されている全てのファイルを取得します。
		/// </summary>
		/// <returns>ファイル名のみが格納されている文字列配列です。</returns>
		public string[] GetFiles()
		{
			var keys = _files.Keys;
			string[] result = new string[keys.Count];
			int i = 0;
			foreach (var item in keys) {
				result[i] = item;
				++i;
			}
			return result;
		}

		/// <summary>
		///  ディレクトリからストリームを作成する事はできない為、
		///  常に<see langword="null"/>を返します。
		/// </summary>
		/// <returns><see langword="null"/>値です。</returns>
		public override Stream CreateStream()
		{
			return null;
		}
	}
}
