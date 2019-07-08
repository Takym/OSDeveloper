using System;
using OSDeveloper.IO;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;
using TakymLib.IO;
using Yencon;
using Yencon.Extension;

namespace OSDeveloper.Projects
{
	public class ProjectItem : IComparable<ProjectItem>
	{
		#region プロパティ
		public    string     Name     { get; private   set; }
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
			this.Logger   = Logger.Get("prj/sln_mngr");
			this.Parent   = parent;
			this.Name     = name;
			this.Solution = root;
			this.HintPath = parent.HintPath.Bond(name);
			this.Logger.Trace($"constructing new project item \"{name}\" with the type: {this.GetType().FullName}...");
		}

		#endregion

		#region 情報取得/設定

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

		public void Rename(string newname)
		{
			this.GetMetadata().Rename(newname);
			(this.GetMetadata() as FolderMetadata)?.Refresh();
			this.Name = newname;
			if (this is Solution) {
				this.HintPath = this.GetFullPath();
			} else {
				this.HintPath = this.Parent.HintPath.Bond(newname);
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

			section.SetNodeAsString("Name", this.Name);
			section.SetNodeAsString("Hint", this.HintPath);
			section.SetNodeAsString("Type", this.GetType().FullName);

			this.Logger.Trace($"completed {nameof(ProjectItem)}.{nameof(this.WriteTo)} ({this.Name})");
		}

		/// <exception cref="System.ArgumentException" />
		public virtual void ReadFrom(YSection section)
		{
			this.Logger.Trace($"executing {nameof(ProjectItem)}.{nameof(this.ReadFrom)} ({this.Name})...");

			var name = section.GetNodeAsString("Name");
			if (name == this.Name) {
				var hint = section.GetNodeAsString("Hint");
				if (!string.IsNullOrEmpty(hint)) {
					this.HintPath = new PathString(hint);
				}
			} else {
				throw new ArgumentException(string.Format(
					ErrorMessages.ProjectItem_ReadFrom_DoesNotMatch,
					this.Name,
					name
				), nameof(section));
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

		internal sealed override bool IsTransient()
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

			var typeName = section.GetNodeAsString("Type");
			try {
				this.Type = Type.GetType(typeName, true);
			} catch (Exception e) {
				throw new ArgumentException(string.Format(
					ErrorMessages.ProjectItem_ReadFrom_InvalidType,
					this.Name,
					typeName
				), nameof(section), e);
			}
		}
	}

	#endregion
}
