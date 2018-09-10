using System;
using System.Collections.Generic;
using System.IO;
using OSDeveloper.Core.Error;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ディレクトリのメタ情報を表します。
	/// </summary>
	public class DirMetadata : FileMetadata
	{
		private DirectoryInfo _dinfo;
		private SortedDictionary<string, DirMetadata> _dirs;
		private SortedDictionary<string, FileMetadata> _files;

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

			_dirs = new SortedDictionary<string, DirMetadata>();
			_files = new SortedDictionary<string, FileMetadata>();
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
			_dirs = new SortedDictionary<string, DirMetadata>();
			_files = new SortedDictionary<string, FileMetadata>();
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
		///  ディレクトリを再読み込みします。
		/// </summary>
		public void Reload()
		{
			List<DirMetadata> tmpDirs = new List<DirMetadata>(_dirs.Values);
			List<FileMetadata> tmpFiles = new List<FileMetadata>(_files.Values);

			_dirs.Clear();
			_files.Clear();

			foreach (var dir in tmpDirs) {
				dir.Reload();
				_dirs.Add(dir.Name, dir);
			}
			foreach (var file in tmpFiles) {
				_files.Add(file.Name, file);
			}

			foreach (var dir in _dinfo.GetDirectories()) {
				if (!_dirs.ContainsKey(dir.Name)) {
					_dirs.Add(dir.Name, new DirMetadata(dir));
				}
			}
			foreach (var file in _dinfo.GetFiles()) {
				if (!_files.ContainsKey(file.Name)) {
					_files.Add(file.Name, new FileMetadata(file.FullName, FileFormat.BinaryFile));
				}
			}

			tmpDirs.Clear();
			tmpFiles.Clear();
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
				DirMetadata result = new DirMetadata(this.FilePath.Bond(name));
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
				FileMetadata result = new FileMetadata(this.FilePath.Bond(name));
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
		/// <returns>ディレクトリ名のみ(完全なパスではない)が格納されている文字列配列です。</returns>
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
		/// <returns>ファイル名のみ(完全なパスではない)が格納されている文字列配列です。</returns>
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

		/// <summary>
		///  ディレクトリ名を変更します。
		/// </summary>
		/// <param name="newName">変更後の新しいディレクトリ名です。</param>
		/// <exception cref="System.ArgumentException" />
		/// <exception cref="System.ArgumentNullException" />
		/// <exception cref="System.UnauthorizedAccessException" />
		/// <exception cref="System.IO.IOException" />
		/// <exception cref="System.IO.PathTooLongException" />
		public override void Rename(string newName)
		{
			if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1) {
				throw new ArgumentException(string.Format(ErrorMessages.IO_InvalidDirNameString, newName), nameof(newName));
			}
			File.Move(this.FilePath, Path.Combine(Path.GetDirectoryName(this.FilePath), newName));
		}
	}
}
