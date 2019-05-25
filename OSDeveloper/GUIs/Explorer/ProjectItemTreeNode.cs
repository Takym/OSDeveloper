using OSDeveloper.IO.Logging;
using OSDeveloper.Projects;

namespace OSDeveloper.GUIs.Explorer
{
	public class ProjectItemTreeNode : FileTreeBox.FileTreeNode
	{
		private Logger                _logger;
		private ProjectItem           _project_item;

		public ProjectItem ProjectItem { get => _project_item; }

		public ProjectItemTreeNode(ProjectItem projectItem) : base(projectItem.GetMetadata())
		{
			_logger = Logger.Get(nameof(SolutionTreeNode));

			_project_item = projectItem;
			this.Text = _project_item.Name;

			_logger.Trace($"constructed {nameof(SolutionTreeNode)}");
		}

		public virtual void Update(FileTreeBox parent)
		{
			parent.UpdatePItemNode(this);
		}
	}
}
