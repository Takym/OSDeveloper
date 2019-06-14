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
					pitn.LoadItems();
					this.Nodes.Add(pitn);
				}
			}
		}
	}
}
