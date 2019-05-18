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
		public    IList<Project>             DependsOn              { get => _depends_on; }
		private   List<Project>              _depends_on;
		public    IReadOnlyList<ProjectItem> Contents               { get => _contents_as_readonly; }
		private   IReadOnlyList<ProjectItem> _contents_as_readonly;
		private   List<ProjectItem>          _contents;
		protected FolderMetadata             Directory              { get => _folder; }
		private   FolderMetadata             _folder;
		#endregion

		#region コンストラクタ
		protected Project(string name) : base(name)
		{
			this.Init();
		}

		public Project(Project parent, string name) : base(parent, name)
		{
			this.Init();
		}

		#region 初期化

		private void Init()
		{
			_depends_on           = new List<Project>();
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
				_contents.Add(new ProjectItem(this, dirs[i].Name));
				this.LoadItems(dirs[i], path + Path.DirectorySeparatorChar + dirs[i].Name);
			}
			var files = _folder.GetFiles();
			for (int i = 0; i < files.Length; ++i) {
				_contents.Add(new ProjectItem(this, files[i].Name));
			}
		}

		#endregion

		#endregion

		#region 情報取得

		public sealed override ItemMetadata GetMetadata()
		{
			// プロジェクトは必ずディレクトリなので、FolderMetadataを返す。
			return _folder;
		}

		#endregion

		#region 計画設定ファイルの読み書き

		public override void WriteTo(YSection section)
		{
			base.WriteTo(section);
			this.SavedVersion = IDEVersion.GetCurrentVersion();
			section.Add(this.SavedVersion.GetYSection());

			/*var dependsOn = new YSection() { Name = "DependsOn" };
			for (int i = 0; i < _depends_on.Count; ++i) {

			}
			//*/

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
				var keys = itemKey.SubKeys;
				_contents.Sort();
				for (int i = 0; i < keys.Length; ++i) {
					if (keys[i] is YSection k) {
						int j = _contents.BinarySearch(new DummyProjectItem(keys[i].Name));
						_contents[j].ReadFrom(k);
					} else {
						throw new ArgumentException(string.Format(
							ErrorMessages.Project_ReadFrom_InvalidItemKey,
							keys[i].Name
						));
					}
				}
			} else {
				throw new ArgumentException(ErrorMessages.Project_ReadFrom_InvalidItems);
			}
		}

		#endregion
	}
}
