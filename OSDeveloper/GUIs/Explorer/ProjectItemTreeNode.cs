using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Projects;

namespace OSDeveloper.GUIs.Explorer
{
	public class ProjectItemTreeNode : FileTreeNode
	{
		public ProjectItem ProjectItem { get; }

		public ProjectItemTreeNode(ProjectItem pitem) : base(pitem.GetMetadata())
		{
			this.ProjectItem = pitem;
			this.Logger.Trace($"constructed {nameof(ProjectItemTreeNode)}, name:{pitem.Name}");
		}

		public void LoadItems()
		{
			if (this.ProjectItem is Project prj) {
				for (int i = 0; i < prj.Contents.Count; ++i) {
					var pitn = new ProjectItemTreeNode(prj.Contents[i]);
					pitn.ContextMenuStrip = this.ContextMenuStrip;
					pitn.LoadItems();
					this.Nodes.Add(pitn);
				}
			}
		}

		public override FileTreeNode CreateFile(string filename)
		{
			ProjectItemTreeNode result;
			if (this.ProjectItem is Project proj) { // 計画である。
				var meta  = this.Folder.CreateFile(filename);
				var pitem = proj.AddItem(meta);
				result = new ProjectItemTreeNode(pitem);
				this.Nodes.Add(result);
			} else { // 項目である。
				var meta  = this.File.Parent.CreateFile(filename);
				var pitem = this.ProjectItem.Parent.AddItem(meta);
				result = new ProjectItemTreeNode(pitem);
				this.Parent.Nodes.Add(result);
			}
			return result;
		}

		public override FileTreeNode CreateDir(string dirname)
		{
			ProjectItemTreeNode result;
			if (this.ProjectItem is Project proj) { // 計画である。
				var meta  = this.Folder.CreateDir(dirname);
				var pitem = proj.AddGroup(meta, ItemGroup.FolderKind.Asset);
				result = new ProjectItemTreeNode(pitem);
				this.Nodes.Add(result);
			} else { // 項目である。
				var meta  = this.File.Parent.CreateDir(dirname);
				var pitem = this.ProjectItem.Parent.AddGroup(meta, ItemGroup.FolderKind.Asset);
				result = new ProjectItemTreeNode(pitem);
				this.Parent.Nodes.Add(result);
			}
			return result;
		}

		public override FileTreeNode AddItem(ItemMetadata meta)
		{
			ProjectItemTreeNode result;
			if (this.ProjectItem is Project proj) { // 計画である。
				meta = meta.Copy(this.Folder.Path.Bond(meta.Name));
				this.Folder.AddItem(meta);
				var pitem = proj.AddItem(meta);
				result = new ProjectItemTreeNode(pitem);
				this.Nodes.Add(result);
			} else { // 項目である。
				meta = meta.Copy(this.File.Parent.Path.Bond(meta.Name));
				this.File.Parent.AddItem(meta);
				var pitem = this.ProjectItem.Parent.AddItem(meta);
				result = new ProjectItemTreeNode(pitem);
				this.Parent.Nodes.Add(result);
			}
			return result;
		}

		public override bool TrashItem()
		{
			bool result = this.Metadata.TrashItem();
			if (result) {
				this.Remove();
				this.ProjectItem.Parent.RemoveItem(this.Metadata);
			}
			return result;
		}

		public override bool Delete()
		{
			this.ProjectItem.Parent.RemoveItem(this.Metadata);
			return base.Delete();
		}

		public override void Rename(string newname)
		{
			this.ProjectItem.Rename(newname);
		}
	}
}
