using System;
using System.IO;
using System.Linq;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public sealed class FolderMetadata : ItemMetadata
	{
		private          FolderMetadata [] _folders;
		private          FileMetadata   [] _files;
		private readonly DirectoryInfo     _dinfo;
		public  override FileSystemInfo    Info      { get => _dinfo; }
		public  override bool              CanAccess { get; }
		public           DriveInfo         Drive     { get; }
		public           FolderFormat      Format    { get; }
		public           long              Count     { get => Directory.EnumerateFileSystemEntries(this.Path).LongCount(); }

		public FolderMetadata(PathString path) : this(path, null) { }

		/// <exception cref="System.ArgumentException"/>
		public FolderMetadata(PathString path, FolderMetadata parent) : base(path, parent)
		{
			try {
				_dinfo = new DirectoryInfo(path);
				if (_dinfo.Attributes.HasFlag(FileAttributes.ReparsePoint)) {
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

		public FolderMetadata[] GetFolders()
		{
			if (_folders == null) {
				var dirs = _dinfo.GetDirectories();
				_folders = new FolderMetadata[dirs.Length];
				for (int i = 0; i < dirs.Length; ++i) {
					_folders[i] = new FolderMetadata((PathString)(dirs[i].FullName));
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
				Directory.Move(this.Path, this.Path.ChangeFileName(newName));
				return base.Rename(newName);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public override ItemMetadata Copy(PathString path)
		{
			try {
				Directory.CreateDirectory(path);
				var dirs = this.GetFolders();
				for (int i = 0; i < dirs.Length; ++i) {
					dirs[i].Copy(path.Bond(dirs[i].Name));
				}
				var files = this.GetFiles();
				for (int i = 0; i < files.Length; ++i) {
					files[i].Copy(path.Bond(files[i].Name));
				}
				return new FolderMetadata(path);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
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
				// ディレクトリを作成する。
				Directory.CreateDirectory(dpath);
				// ディレクトリのメタ情報を生成する。
				var result = dirs[_folders.Length] = new FolderMetadata(dpath);
				// 新しいファイル一覧をフィールド変数へ代入する。
				_folders = dirs;
				// 生成したメタ情報を呼び出し元へ返す。
				return result;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
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
					return ft[0].CreateMetadata(((PathString)(info.FullName)), this);
				} else { // なければ、通常の FileMetadata を生成
					return new FileMetadata(
						((PathString)(info.FullName)), this, FileFormat.Unknown);
				}
			} else { // 拡張子が無い場合は、通常の FileMetadata を生成
				return new FileMetadata(
					((PathString)(info.FullName)), this, FileFormat.Unknown);
			}
		}
	}
}
