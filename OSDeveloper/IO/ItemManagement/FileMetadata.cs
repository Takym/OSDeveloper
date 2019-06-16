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
		public           FileFormat     Format    { get; internal set; }

		/// <exception cref="System.ArgumentException"/>
		internal FileMetadata(PathString path, FileFormat format) : base(path)
		{
			try {
				_finfo = new FileInfo(path);
				using (_finfo.OpenRead()) { /* アクセス可能か確認 */ }
				this.Format    = format;
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
			try {
				return File.ReadAllBytes(this.Path);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public string[] ReadAllLines()
		{
			try {
				return File.ReadAllLines(this.Path);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public string ReadAllText()
		{
			try {
				return File.ReadAllText(this.Path);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return null;
			}
		}

		public void WriteAllBytes(byte[] data)
		{
			try {
				File.WriteAllBytes(this.Path, data);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
			}
		}

		public void WriteAllLines(string[] data)
		{
			try {
				File.WriteAllLines(this.Path, data);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
			}
		}

		public void WriteAllText(string data)
		{
			try {
				File.WriteAllText(this.Path, data);
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
			}
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
				path = path.EnsureName();
				_finfo.CopyTo(path, true);
				return ItemList.GetFile(path, this.Format);
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
				if (this.Path.Exists()) return false;
				return base.TrashItem();
			} catch (Exception e) {
				Program.Logger.Notice($"The exception occurred in {nameof(FileMetadata)}, filename:{this.Path}");
				Program.Logger.Exception(e);
				return false;
			}
		}
	}
}
