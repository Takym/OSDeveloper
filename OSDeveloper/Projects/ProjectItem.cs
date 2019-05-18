using System;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using TakymLib.IO;
using Yencon;

namespace OSDeveloper.Projects
{
	public class ProjectItem : IComparable<ProjectItem>
	{
		#region プロパティ
		public string     Name     { get; }
		public PathString HintPath { get; protected set; }
		public Project    Parent   { get; }
		#endregion

		#region コンストラクタ
		protected ProjectItem(string name)
		{
			this.Name = name;
		}

		public ProjectItem(Project parent, string name)
		{
			this.Parent   = parent;
			this.Name     = name;
			this.HintPath = parent.HintPath.Bond(name);
		}
		#endregion

		#region 情報取得

		public virtual PathString GetFullPath()
		{
			return this.Parent.GetFullPath().Bond(this.Name);
		}

		public virtual ItemMetadata GetMetadata()
		{
			return ItemList.GetItem(this.GetFullPath());
		}

		#endregion

		#region 計画設定ファイルの読み書き

		public virtual void WriteTo(YSection section)
		{
			section.Add(new YString() { Name = "Name", Text = this.Name });
			section.Add(new YString() { Name = "Hint", Text = this.HintPath });
		}

		/// <exception cref="System.ArgumentException" />
		public virtual void ReadFrom(YSection section)
		{
			var node = section.GetNode("Name");
			if (node is YString nameKey) {
				if (nameKey.Text == this.Name) {
					// HintPath を取得
					node = section.GetNode("Hint");
					if (node is YString hintKey) {
						this.HintPath = new PathString(hintKey.Text);
					}
				} else { // ヱンコンに保存されているファイル名が一致しない場合
					throw new ArgumentException(string.Format(
						ErrorMessages.ProjectItem_ReadFrom_DoesNotMatch,
						this.Name,
						node.GetValue()
					));
				}
			} else { // ヱンコンに保存されているファイル名が文字列ではない場合
				throw new ArgumentException(string.Format(
					ErrorMessages.ProjectItem_ReadFrom_InvalidValue,
					this.Name
				));
			}
		}

		#endregion

		#region 継承した処理

		public int CompareTo(ProjectItem other)
		{
			return this.Name.CompareTo(other.Name);
		}

		public sealed override bool Equals(object obj)
		{
			if (obj is DummyProjectItem dummy) {
				return this.Name == dummy.Name;
			} else {
				return base.Equals(obj);
			}
		}

		public sealed override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public sealed override string ToString()
		{
			return this.Name;
		}

		#endregion
	}

	public sealed class DummyProjectItem : ProjectItem
	{
		public DummyProjectItem(string name) : base(name) { }
	}
}
