using System;
using System.Collections.Generic;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using Yencon;
using Yencon.Extension;

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

			this.Logger.Trace($"constructed the project item \"{this.Name}\" with the type: {this.GetType().FullName}");
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

		#region 項目の追加/削除

		public ProjectItem AddItem(ItemMetadata meta)
		{
			var result = new ProjectItem(this.Solution, this, meta.Path.GetRelativePath(this.GetFullPath()));
			_contents.Add(result);
			return result;
		}

		public void RemoveItem(ItemMetadata meta)
		{
			var item = this.GetItem(meta.Path.GetRelativePath(this.GetFullPath()));
			_contents.Remove(item);
		}

		public void Compact()
		{
			_contents.RemoveAll((ProjectItem item) => item.IsTransient());
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
				var item = new YSection() { Name = i.ToString() };
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
			if (node is YSection verKey) { // セクションかどうか判定
				var version = new IDEVersion(verKey);
				if (version.HasCompatible()) { // 互換性が現在のバージョンとあるかどうか判定
					this.SavedVersion = version;
				} else { // 互換性が無い
					throw new ArgumentException(string.Format(
						ErrorMessages.Project_ReadFrom_NonCompatibleVersion,
						IDEVersion.GetCurrentVersion(),
						version
					));
				}
			} else { // ヱンコン値が不正
				throw new ArgumentException(ErrorMessages.Project_ReadFrom_InvalidVersion);
			}

			// Contents
			node = section.GetNode("Items");
			_contents.Sort(); // 二分探索の為に並び替え
			var addlist = new List<ProjectItem>(); // 新しく追加する項目一覧
			if (node is YSection itemKey) { // セクションかどうか
				var keys = itemKey.SubKeys;
				for (int i = 0; i < keys.Length; ++i) {
					if (keys[i] is YSection k) { // セクションかどうか
						string name = k.GetNodeAsString("Name");
						int j = _contents.BinarySearch(new DummyProjectItem(name));
						if (0 <= j && j < _contents.Count) { // 既に同名の項目が有る場合
							_contents[j].ReadFrom(k); // 情報を再読み込みする
						} else { // 同名のアイテムが無い場合
							var item = this.LoadItem(k.GetNodeAsString("Name"), k);
							item.ReadFrom(k);
							addlist.Add(item); // 新しく生成して追加する
						}
					} else { // ヱンコン値が不正
						throw new ArgumentException(string.Format(
							ErrorMessages.Project_ReadFrom_InvalidItemKey,
							keys[i].Name
						));
					}
				}
			} else { // ヱンコン値が不正
				throw new ArgumentException(ErrorMessages.Project_ReadFrom_InvalidItems);
			}
			_contents.AddRange(addlist); // 新しい項目を追加
			this.Compact();

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
