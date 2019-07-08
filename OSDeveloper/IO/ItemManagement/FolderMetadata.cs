using System;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using OSDeveloper.Projects;
using OSDeveloper.Resources;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public sealed class FolderMetadata : ItemMetadata
	{
		private          PathString        _slnfile;
		private          FolderMetadata [] _folders;
		private          FileMetadata   [] _files;
		private readonly DirectoryInfo     _dinfo;
		public  override FileSystemInfo    Info      { get => _dinfo; }
		public  override bool              CanAccess { get; }
		public           DriveInfo         Drive     { get; }
		public           FolderFormat      Format    { get; private set; }
		public           long              Count     { get => Directory.EnumerateFileSystemEntries(this.Path).LongCount(); }

		/// <exception cref="System.ArgumentException"/>
		internal FolderMetadata(PathString path) : base(path)
		{
			try {
				_dinfo   = new DirectoryInfo(path);
				_slnfile = path.Bond(path.GetFileName() + ".osdev_sln");
				if (_slnfile.Exists()) {
					// ソリューションかどうかは一番優先する。
					this.Format = FolderFormat.Solution;
				} else if (_dinfo.Attributes.HasFlag(FileAttributes.ReparsePoint)) {
					this.Format = FolderFormat.Junction;
				} else if (path.GetDirectoryName() == null) {
					this.Drive = new DriveInfo(path);
					switch (this.Drive.DriveType) {
						case DriveType.Removable:
							this.Format = FolderFormat.FloppyDisk;
							break;
						case DriveType.Fixed:
						case DriveType.Ram:
							this.Format = FolderFormat.HardDisk;
							break;
						case DriveType.CDRom:
							this.Format = FolderFormat.OpticalDisc;
							break;
						default:
							this.Format = FolderFormat.Unknown;
							break;
					}
				} else {
					this.Format = FolderFormat.Directory;
				}
				Directory.EnumerateFileSystemEntries(this.Path); // アクセス可能か確認
				this.ExtendedDetail = new FolderExtendedDetail();
				this.CanAccess = true;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				this.CanAccess = false;
			}
		}

		public void Refresh()
		{
			// 取り敢えず null にしておく。そうすると、GetFolders()、GetFiles()が読み直してくれる。
			_folders = null;
			_files   = null;
		}

		public bool IsEmpty()
		{
			return !Directory.EnumerateFileSystemEntries(this.Path).Any();
		}

		public PathString GetSolutionFilePath()
		{
			if (_slnfile.Exists()) {
				this.Format = FolderFormat.Solution;
			}
			return _slnfile;
		}

		public FileMetadata GetSolutionFile()
		{
			if (_slnfile.Exists()) {
				this.Format = FolderFormat.Solution;
				return ItemList.GetFile(_slnfile);
			} else {
				return null;
			}
		}

		public Solution GetSolution()
		{
			if (_slnfile.Exists()) {
				this.Format = FolderFormat.Solution;
				return new Solution(this.Name);
			} else {
				return null;
			}
		}

		public FolderMetadata[] GetFolders()
		{
			// キャッシュが null なら
			if (_folders == null) {
				// ディレクトリ情報からフォルダ一覧を取得
				var dirs = _dinfo.GetDirectories();
				// フォルダ数に合わせて配列を生成
				_folders = new FolderMetadata[dirs.Length];
				// フォルダのメタ情報をフォルダの数だけ生成
				for (int i = 0; i < dirs.Length; ++i) {
					_folders[i] = ItemList.GetDir((PathString)(dirs[i].FullName));
				}
			}
			return _folders;
		}

		public FileMetadata[] GetFiles()
		{
			// キャッシュが null なら
			if (_files == null) {
				// ディレクトリ情報からファイル一覧を取得
				var files = _dinfo.GetFiles();
				// ファイル数に合わせて配列を生成
				_files = new FileMetadata[files.Length];
				// ファイルのメタ情報をファイルの数だけ生成
				for (int i = 0; i < files.Length; ++i) {
					_files[i] = this.OpenFile(files[i]);
				}
			}
			return _files;
		}

		public override bool Rename(string newName)
		{
			try {
				if (string.IsNullOrEmpty(newName) || newName == this.Path.GetFileName()) return true;
				Directory.Move(this.Path, this.Path.ChangeFileName(newName));
				var result = base.Rename(newName);
				_slnfile = this.Path.Bond(this.Path.GetFileName() + ".osdev_sln");
				return result;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}, newname:{newName}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public override ItemMetadata Copy(PathString path)
		{
			try {
				path = path.EnsureName();
				Directory.CreateDirectory(path);
				var dirs = this.GetFolders();
				for (int i = 0; i < dirs.Length; ++i) {
					dirs[i].Copy(path.Bond(dirs[i].Name));
				}
				var files = this.GetFiles();
				for (int i = 0; i < files.Length; ++i) {
					files[i].Copy(path.Bond(files[i].Name));
				}
				return ItemList.GetDir(path);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}, path:{path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public override bool Delete()
		{
			try {
				_dinfo.Delete(true);
				return base.Delete();
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public override bool TrashItem()
		{
			try {
				FileSystem.DeleteDirectory(this.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
				if (this.Path.Exists()) return false;
				return base.TrashItem();
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public FileMetadata CreateFile(string filename)
		{
			try {
				// ファイル名が空なら、untitledとする。
				if (string.IsNullOrWhiteSpace(filename)) {
					filename = "untitled";
				}
				// ファイル名をパス文字列に変換する。
				var fpath = this.Path.Bond(filename);
				// 既にファイルが存在する場合は数字を付け足す。
				fpath = fpath.EnsureName();
				// _files を読み込めている状態にする。
				this.GetFiles();
				// 新しいファイルを格納する為の場所を確保する。
				var files = new FileMetadata[_files.Length + 1];
				_files.CopyTo(files, 0);
				// ファイルを作成する。
				var fi = new FileInfo(fpath);
				fi.Create().Close();
				// ファイルのメタ情報を生成する。
				var result = files[_files.Length] = this.OpenFile(fi);
				// 新しいファイル一覧をフィールド変数へ代入する。
				_files = files;
				// 生成したメタ情報を呼び出し元へ返す。
				return result;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public FolderMetadata CreateDir(string dirname)
		{
			try {
				// ディレクトリ名が空なら、untitledとする。
				if (string.IsNullOrWhiteSpace(dirname)) {
					dirname = "untitled";
				}
				// ディレクトリ名をパス文字列に変換する。
				var dpath = this.Path.Bond(dirname);
				// 既にディレクトリが存在する場合は数字を付け足す。
				dpath = dpath.EnsureName();
				// _folders を読み込めている状態にする。
				this.GetFolders();
				// 新しいファイルを格納する為の場所を確保する。
				var dirs = new FolderMetadata[_folders.Length + 1];
				_folders.CopyTo(dirs, 0);
				// ディレクトリを作成する。
				Directory.CreateDirectory(dpath);
				// ディレクトリのメタ情報を生成する。
				var result = dirs[_folders.Length] = ItemList.GetDir(dpath);
				// 新しいディレクトリ一覧をフィールド変数へ代入する。
				_folders = dirs;
				// 生成したメタ情報を呼び出し元へ返す。
				return result;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public bool AddItem(ItemMetadata item)
		{
			try {
				// ディレクトリ名が違う場合は、例外を発生させる。
				// catchで例外が捕捉されてログが出力される。
				if (!item.Parent.Path.Equals(this.Path)) {
					throw new ArgumentException(
						string.Format(
							ErrorMessages.FolderMetadata_AddItem_DirName,
							item.Path.ToString(), this.Path.ToString()),
						nameof(item));
				}
				if (item is FileMetadata file) { // itemがファイルなら
					// _files を読み込めている状態にする。
					this.GetFiles();
					// 新しいファイルを格納する為の場所を確保する。
					var files = new FileMetadata[_files.Length + 1];
					_files.CopyTo(files, 0);
					// 指定されたファイルを代入する。
					files[_files.Length] = file;
					// 新しいファイル一覧をフィールド変数へ代入する。
					_files = files;
				} else if (item is FolderMetadata dir) { // itemがディレクトリなら
					// _folders を読み込めている状態にする。
					this.GetFolders();
					// 新しいディレクトリを格納する為の場所を確保する。
					var dirs = new FolderMetadata[_folders.Length + 1];
					_folders.CopyTo(dirs, 0);
					// 指定されたディレクトリを代入する。
					dirs[_folders.Length] = dir;
					// 新しいディレクトリ一覧をフィールド変数へ代入する。
					_folders = dirs;
				} else {
					// ファイルでもフォルダでもアイテムには対応していない。
					throw new InvalidCastException(
						string.Format(
							ErrorMessages.InvalidCast,
							item.GetType().FullName,
							$"{typeof(FileMetadata).FullName}, {typeof(FolderMetadata).FullName}"));
				}
				// 成功したので、trueを返す。
				return true;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		private FileMetadata OpenFile(FileInfo info)
		{
			// ファイルの拡張を取得
			var ext = info.Extension;
			// 拡張子が空かどうか判定
			if (!string.IsNullOrEmpty(ext)) {
				// 拡張子から FileType を取得
				var ft = FileTypeRegistry.GetByExtension(ext.Remove(0, 1));
				// FileType が一つ以上あれば、FileType からメタ情報を生成
				if (ft.Length != 0) {
					return ft[0].CreateMetadata(((PathString)(info.FullName)));
				} else { // なければ、通常の FileMetadata を生成
					return ItemList.GetFile((PathString)(info.FullName));
				}
			} else { // 拡張子が無い場合は、通常の FileMetadata を生成
				return ItemList.GetFile((PathString)(info.FullName));
			}
		}
	}
}
