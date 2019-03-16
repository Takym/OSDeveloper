using System;
using System.IO;
using System.Linq;

namespace OSDeveloper.IO.ItemManagement
{
	public class FolderMetadata : ItemMetadata
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
				} else if (path.GetDirectoryName() is null) { // == null だと、正しく動作しない可能性がある
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
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}");
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
			// == null だと、正しく動作しない可能性がある
			if (_folders is null) {
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
			// == null だと、正しく動作しない可能性がある
			if (_files is null) {
				var files = _dinfo.GetFiles();
				_files = new FileMetadata[files.Length];
				for (int i = 0; i < files.Length; ++i) {
					var fmt = FileFormat.Unknown;
					var ext = files[i].Extension;
					// TODO: 今後、FileTypeRegistry.GetByExtension(ext.Remove(0, 1))[0].CreateMetadata(); を使うようにする
					if (!string.IsNullOrEmpty(ext)) {
						var ft = FileTypeRegistry.GetByExtension(ext.Remove(0, 1));
						if (ft.Length != 0) {
							fmt = ft[0].Format;
						}
					}
					_files[i] = new FileMetadata(
						((PathString)(files[i].FullName)), this, fmt);
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
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}");
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
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}");
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
				Program.Logger.Notice($"The exception occurred in {nameof(FolderMetadata)}");
				Program.Logger.Exception(e);
				return false;
			}
		}
	}
}
