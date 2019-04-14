using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
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

		public FileStream OpenStream()
		{
			return new FileStream(this.Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
		}

		#region Fileクラスを使った読み書き

		public byte[] ReadAllBytes()
		{
			return File.ReadAllBytes(this.Path);
		}

		public string[] ReadAllLines()
		{
			return File.ReadAllLines(this.Path);
		}

		public string ReadAllText()
		{
			return File.ReadAllText(this.Path);
		}

		public void WriteAllBytes(byte[] data)
		{
			File.WriteAllBytes(this.Path, data);
		}

		public void ReadAllLines(string[] data)
		{
			File.WriteAllLines(this.Path, data);
		}

		public void ReadAllText(string data)
		{
			File.WriteAllText(this.Path, data);
		}

		#endregion

		public override bool Rename(string newName)
		{
			try {
				if (string.IsNullOrEmpty(newName) || newName == this.Path.GetFileName()) return true;
				_finfo.MoveTo(this.Path.ChangeFileName(newName));
				return base.Rename(newName);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}, newname:{newName}");
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
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}, path:{path}");
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
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}

		public override bool TrashItem()
		{
			try {
				FileSystem.DeleteFile(this.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
				return base.TrashItem();
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}
	}
}
