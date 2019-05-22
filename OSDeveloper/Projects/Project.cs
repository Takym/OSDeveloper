using System;
using System.Collections.Generic;
using System.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using Yencon;

namespace OSDeveloper.Projects
{
	public class Project : ProjectItem
	{
		#region プロパティ
		public    IDEVersion                 SavedVersion           { get; protected set; }
		public    IReadOnlyList<ProjectItem> Contents               { get => _contents_as_readonly; }
		private   IReadOnlyList<ProjectItem> _contents_as_readonly;
		private   List<ProjectItem>          _contents;
		protected FolderMetadata             Directory              { get => _folder; }
		private   FolderMetadata             _folder;
		#endregion

		#region コンストラクタ

		private protected Project(string name) : base(name)
		{
			this.Init();
		}

		public Project(Solution root, Project parent, string name) : base(root, parent, name)
		{
			this.Init();
		}

		#region 初期化

		private void Init()
		{
			_contents             = new List<ProjectItem>();
			_contents_as_readonly = _contents.AsReadOnly();
			_folder               = ItemList.GetDir(this.GetFullPath());

			this.LoadItems(_folder, string.Empty);
			this.SavedVersion = IDEVersion.GetCurrentVersion();
		}

		private void LoadItems(FolderMetadata folder, string path)
		{
			var dirs = _folder.GetFolders();
			for (int i = 0; i < dirs.Length; ++i) {
				_contents.Add(this.LoadItem(dirs[i].Name));
				this.LoadItems(dirs[i], path + Path.DirectorySeparatorChar + dirs[i].Name);
			}
			var files = _folder.GetFiles();
			for (int i = 0; i < files.Length; ++i) {
				_contents.Add(this.LoadItem(files[i].Name));
			}
		}

		private ProjectItem LoadItem(string name)
		{
			return new ProjectItem(this.Solution, this, name);
		}

		#endregion

		#endregion

		#region 情報取得

		public sealed override ItemMetadata GetMetadata()
		{
			// プロジェクトは必ずディレクトリなので、FolderMetadataを返す。
			return _folder;
		}

		public ProjectItem GetItem(string name)
		{
			_contents.Sort();
			int j = _contents.BinarySearch(new DummyProjectItem(name));
			if (0 <= j && j < _contents.Count) {
				return _contents[j];
			} else {
				return null;
			}
		}

		#endregion

		#region 計画設定ファイルの読み書き

		public override void WriteTo(YSection section)
		{
			base.WriteTo(section);
			this.SavedVersion = IDEVersion.GetCurrentVersion();
			section.Add(this.SavedVersion.GetYSection());

			var items = new YSection() { Name = "Items" };
			for (int i = 0; i < _contents.Count; ++i) {
				var item = new YSection() { Name = _contents[i].Name };
				_contents[i].WriteTo(item);
				items.Add(item);
			}
			section.Add(items);
		}

		/// <exception cref="System.ArgumentException" />
		public override void ReadFrom(YSection section)
		{
			base.ReadFrom(section);

			// SavedVersion
			var node = section.GetNode(IDEVersion.DefaultKeyName);
			if (node is YSection verKey) {
				var version = new IDEVersion(verKey);
				if (version.HasCompatible()) {
					this.SavedVersion = version;
				} else {
					throw new ArgumentException(string.Format(
						ErrorMessages.Project_ReadFrom_NonCompatibleVersion,
						IDEVersion.GetCurrentVersion(),
						version
					));
				}
			} else {
				throw new ArgumentException(ErrorMessages.Project_ReadFrom_InvalidVersion);
			}

			// Contents
			node = section.GetNode("Items");
			if (node is YSection itemKey) {
				var keys     = itemKey.SubKeys;
				var newitems = new List<ProjectItem>();
				_contents.Sort();
				for (int i = 0; i < keys.Length; ++i) {
					if (keys[i] is YSection k) {
						int j = _contents.BinarySearch(new DummyProjectItem(keys[i].Name));
						if (0 <= j && j < _contents.Count) {
							_contents[j].ReadFrom(k);
						} else {
							var item = this.LoadItem(keys[i].Name);
							item.ReadFrom(k);
							newitems.Add(item);
						}
					} else {
						throw new ArgumentException(string.Format(
							ErrorMessages.Project_ReadFrom_InvalidItemKey,
							keys[i].Name
						));
					}
				}
				_contents.AddRange(newitems);
			} else {
				throw new ArgumentException(ErrorMessages.Project_ReadFrom_InvalidItems);
			}
		}

		#endregion
	}
}
