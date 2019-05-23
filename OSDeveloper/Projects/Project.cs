using System;
using System.Collections.Generic;
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

			this.SavedVersion = IDEVersion.GetCurrentVersion();
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

		#region 企画/計画設定ファイルの読み書き

		public override void WriteTo(YSection section)
		{
			base.WriteTo(section);
			this.Logger.Trace($"executing {nameof(Project)}.{nameof(this.WriteTo)} ({this.Name})...");

			this.SavedVersion = IDEVersion.GetCurrentVersion();
			section.Add(this.SavedVersion.GetYSection());

			var items = new YSection() { Name = "Items" };
			for (int i = 0; i < _contents.Count; ++i) {
				var item = new YSection() { Name = _contents[i].Name };
				_contents[i].WriteTo(item);
				items.Add(item);
			}
			section.Add(items);

			this.Logger.Trace($"completed {nameof(Project)}.{nameof(this.WriteTo)} ({this.Name})");
		}

		/// <exception cref="System.ArgumentException" />
		public override void ReadFrom(YSection section)
		{
			base.ReadFrom(section);
			this.Logger.Trace($"executing {nameof(Project)}.{nameof(this.ReadFrom)} ({this.Name})...");

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
				_contents.Clear();
				for (int i = 0; i < keys.Length; ++i) {
					if (keys[i] is YSection k) {
						var item = this.LoadItem(keys[i].Name, k);
						item.ReadFrom(k);
						_contents.Add(item);
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

			this.Logger.Trace($"completed {nameof(Project)}.{nameof(this.ReadFrom)} ({this.Name})...");
		}

		private ProjectItem LoadItem(string name, YSection section)
		{
			this.Logger.Info($"{this.Name}: loading {name}...");

			var pitem = new TentativeProjectItem(this.Solution, this, name);
			pitem.ReadFrom(section);
			return Activator.CreateInstance(pitem.Type, this.Solution, this, name) as ProjectItem;
		}

		#endregion
	}
}
