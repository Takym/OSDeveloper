using System;
using System.IO;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public sealed class FileMetadata : ItemMetadata
	{
		private readonly FileInfo       _finfo;
		public  override FileSystemInfo Info      { get => _finfo; }
		public  override bool           CanAccess { get; }
		public           FileFormat     Format    { get; }

		public FileMetadata(PathString path, FileFormat format) : this(path, null, format) { }

		/// <exception cref="System.ArgumentException"/>
		public FileMetadata(PathString path, FolderMetadata parent, FileFormat format) : base(path, parent)
		{
			try {
				_finfo = new FileInfo(path);
				using (_finfo.OpenRead()) { /* アクセス可能か確認 */ }
				this.Format = format;
				this.CanAccess = true;
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				this.CanAccess = false;
			}
		}

		public override bool Rename(string newName)
		{
			try {
				_finfo.MoveTo(this.Path.ChangeFileName(newName));
				return base.Rename(newName);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public override ItemMetadata Copy(PathString path)
		{
			try {
				_finfo.CopyTo(path, true);
				return new FileMetadata(path, this.Format);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public override bool Delete()
		{
			try {
				_finfo.Delete();
				return base.Delete();
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}");
				Program.Logger.Exception(e);
				return false;
			}
		}
	}
}
