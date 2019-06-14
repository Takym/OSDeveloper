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
	}
}
