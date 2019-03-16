using System;
using System.IO;
using OSDeveloper.Resources;

namespace OSDeveloper.IO.ItemManagement
{
	public abstract class ItemMetadata
	{
		private          PathString     _path;
		public           PathString     Path         { get => _path; }
		public           string         Name         { get => _path.GetFileName(); }
		public  abstract FileSystemInfo Info         { get; }
		public  virtual  FileAttributes Attributes   { get => this.Info.Attributes; }
		public  virtual  bool           CanAccess    { get => true; }
		public           bool           IsRemoved    { get; private set; }

		public virtual FolderMetadata Parent
		{
			get
			{
				if (_parent == null) {
					_parent = new FolderMetadata(_path.GetDirectory());
				}
				return _parent;
			}
		}
		private FolderMetadata _parent;

		protected ItemMetadata(PathString path)
		{
			_path = path;
		}

		/// <exception cref="System.ArgumentException"/>
		protected ItemMetadata(PathString path, FolderMetadata parent)
		{
			_path   = path;
			_parent = parent;
			if (parent != null && _path.GetDirectory() != parent._path) {
				throw new ArgumentException(ErrorMessages.ItemMetadata_Argument);
			}
		}

		public virtual bool Rename(string newName)
		{
			_path = _path.ChangeFileName(newName);
			return true;
		}

		public virtual ItemMetadata Copy(PathString path)
		{
			return null;
		}

		public virtual bool Delete()
		{
			this.IsRemoved = true;
			return true;
		}

		/*
		public abstract EditorWindow CreateEditor();
		public abstract PropertyTab  CreatePropTab();
		*/
	}
}
