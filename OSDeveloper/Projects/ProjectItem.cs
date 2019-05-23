using System;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;
using TakymLib.IO;
using Yencon;

namespace OSDeveloper.Projects
{
	public class ProjectItem : IComparable<ProjectItem>
	{
		#region プロパティ
		public    string     Name     { get; }
		public    PathString HintPath { get; protected set; }
		public    Project    Parent   { get; }
		protected Solution   Solution { get; }
		protected Logger     Logger   { get; }
		#endregion

		#region コンストラクタ

		private protected ProjectItem(string name)
		{
			this.Logger = Logger.Get("prj/sln_mngr");
			this.Name = name;
			if (this is Solution root) {
				this.Solution = root;
				this.HintPath = root.GetFullPath();
			}
			this.Logger.Trace($"constructing new project item \"{name}\" with the type: {this.GetType().FullName}...");
		}

		public ProjectItem(Solution root, Project parent, string name)
		{
			this.Logger = Logger.Get("prj/sln_mngr");
			this.Parent   = parent;
			this.Name     = name;
			this.Solution = root;
			this.HintPath = parent.HintPath.Bond(name);
			this.Logger.Trace($"constructing new project item \"{name}\" with the type: {this.GetType().FullName}...");
		}

		#endregion

		#region 情報取得

		public virtual PathString GetFullPath()
		{
			return this.Parent.GetFullPath().Bond(this.Name);
		}

		public virtual ItemMetadata GetMetadata()
		{
			var path = this.GetFullPath();
			if (path.Exists()) {
				return ItemList.GetItem(path);
			} else if (this.HintPath.Exists()) {
				return ItemList.GetItem(this.HintPath);
			} else {
				return ItemList.CreateNewFile(path, FileFormat.Unknown);
			}
		}

		internal virtual bool IsTransient()
		{
			return false;
		}

		#endregion

		#region 企画/計画設定ファイルの読み書き

		public virtual void WriteTo(YSection section)
		{
			this.Logger.Trace($"executing {nameof(ProjectItem)}.{nameof(this.WriteTo)} ({this.Name})...");

			section.Add(new YString() { Name = "Name", Text = this.Name });
			section.Add(new YString() { Name = "Hint", Text = this.HintPath });
			section.Add(new YString() { Name = "Type", Text = this.GetType().FullName });

			this.Logger.Trace($"completed {nameof(ProjectItem)}.{nameof(this.WriteTo)} ({this.Name})");
		}

		/// <exception cref="System.ArgumentException" />
		public virtual void ReadFrom(YSection section)
		{
			this.Logger.Trace($"executing {nameof(ProjectItem)}.{nameof(this.ReadFrom)} ({this.Name})...");

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

			this.Logger.Trace($"completed {nameof(ProjectItem)}.{nameof(this.ReadFrom)} ({this.Name})...");
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

	#region 一時的な計画項目

	/// <summary>
	///  一時的な<see cref="ProjectItem"/>。
	/// </summary>
	public abstract class TemporaryProjectItem : ProjectItem
	{
		protected TemporaryProjectItem(string name) : base(name) { }
		protected TemporaryProjectItem(Solution root, Project parent, string name) : base(root, parent, name) { }

		internal override bool IsTransient()
		{
			return true;
		}
	}

	/// <summary>
	///  <see cref="ProjectItem"/>のリストを二分検索する為の仮の<see cref="ProjectItem"/>。
	/// </summary>
	public sealed class DummyProjectItem : TemporaryProjectItem
	{
		public DummyProjectItem(string name) : base(name) { }
	}

	/// <summary>
	///  アイテム読み込み時の暫定的な<see cref="ProjectItem"/>。
	/// </summary>
	public sealed class TentativeProjectItem : TemporaryProjectItem
	{
		public Type Type { get; private set; }

		public TentativeProjectItem(Solution root, Project parent, string name) : base(root, parent, name) { }

		public override void ReadFrom(YSection section)
		{
			base.ReadFrom(section);
			var node = section.GetNode("Type");
			if (node is YString typeKey) {
				this.Type = Type.GetType(typeKey.Text, true);
			} else {// ヱンコンに保存されているファイル名が文字列ではない場合
				throw new ArgumentException(string.Format(
					ErrorMessages.ProjectItem_ReadFrom_InvalidValue,
					this.Name
				));
			}
		}
	}

	#endregion
}
