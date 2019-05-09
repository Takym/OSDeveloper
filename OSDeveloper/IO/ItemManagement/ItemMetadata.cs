using System.IO;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public abstract class ItemMetadata
	{
		private          PathString         _path;
		public           PathString         Path           { get => _path; }
		public           string             Name           { get => _path.GetFileName(); }
		public  abstract FileSystemInfo     Info           { get; }
		public  virtual  FileAttributes     Attributes     { get => this.Info.Attributes; }
		public  virtual  bool               CanAccess      { get => true; }
		private          bool               _is_removed;
		public           bool               IsRemoved      { get => _is_removed || (_is_removed = !_path.Exists()); }
		private          ItemExtendedDetail _ied;
		public           ItemExtendedDetail ExtendedDetail { get => _ied; internal set { _ied = value ?? _ied; _ied.Metadata = this; } }

		public FolderMetadata Parent
		{
			get
			{
				if (_parent == null) {
					_parent = ItemList.GetDir(_path.GetDirectory());
				}
				return _parent;
			}
		}
		private FolderMetadata _parent;

		protected private ItemMetadata(PathString path)
		{
			_path       = path;
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
			var oldpath = _path;
			_path = oldpath.ChangeFileName(newName);
			return ItemList.RenameItem(oldpath, _path);
		}

		public virtual ItemMetadata Copy(PathString path)
		{
			return null;
		}

		public virtual bool Delete()
		{
			_is_removed = true;
			return ItemList.RemoveItem(_path);
		}

		public virtual bool TrashItem()
		{
			_is_removed = true;
			return ItemList.RemoveItem(_path);
		}
	}
}
