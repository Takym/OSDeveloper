using System.IO;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public abstract class ItemMetadata
	{
		public           PathString         Path           { get => this.Parent?.Path?.Bond(_name) ?? ((PathString)(_name)); }
		private          string             _name;
		public           string             Name           { get => _name; }
		public           FolderMetadata     Parent         { get; }
		public  abstract FileSystemInfo     Info           { get; }
		public  virtual  FileAttributes     Attributes     { get => this.Info.Attributes; }
		public  virtual  bool               CanAccess      { get => true; }
		private          bool               _is_removed;
		public           bool               IsRemoved      { get => _is_removed || (_is_removed = !this.Path.Exists()); }
		private          ItemExtendedDetail _ied;
		public           ItemExtendedDetail ExtendedDetail { get => _ied; internal set { _ied = value ?? _ied; _ied.Metadata = this; } }

		protected private ItemMetadata(PathString path)
		{
			string p = path;
			if (p.Length == 3 && p.EndsWith(":\\")) {
				_name       = p;
			} else {
				this.Parent = ItemList.GetDir(path.GetDirectory());
				_name       = path.GetFileName();
			}
			_is_removed = false;
			this.InitIED();
		}

		private void InitIED()
		{
			_ied = new DefaultItemExtendedDetail();
			_ied.Metadata = this;
		}

		public virtual bool Rename(string newName)
		{
			var oldpath = this.Path;
			_name = newName;
			return ItemList.RenameItem(oldpath, this.Path);
		}

		public virtual ItemMetadata Copy(PathString path)
		{
			return null;
		}

		public virtual bool Delete()
		{
			_is_removed = true;
			return ItemList.RemoveItem(this.Path);
		}

		public virtual bool TrashItem()
		{
			_is_removed = true;
			return ItemList.RemoveItem(this.Path);
		}

		internal virtual void UpdateInfo() { }
	}
}
