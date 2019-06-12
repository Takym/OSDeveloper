using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Projects;

namespace OSDeveloper.GUIs.Explorer
{
	public class ProjectItemTreeNodeOld : FileTreeBoxOld.FileTreeNode
	{
		private Logger                _logger;
		private ProjectItem           _project_item;

		public ProjectItem ProjectItem { get => _project_item; }

		public ProjectItemTreeNodeOld(ProjectItem projectItem) : base(projectItem.GetMetadata())
		{
			_logger = Logger.Get(nameof(SolutionTreeNodeOld));

			_project_item = projectItem;
			this.Text = _project_item.Name;

			_logger.Trace($"constructed {nameof(SolutionTreeNodeOld)}");
		}

		public void Update(FileTreeBoxOld parent)
		{
			parent.UpdatePItemNode(this);
			var items = (_project_item as Project)?.Contents;
			if (items != null) {
				for (int i = 0; i < items.Count; ++i) {
					if (this.Nodes.ContainsKey(items[i].Name)) {
						(this.Nodes[items[i].Name] as ProjectItemTreeNodeOld)?.Update(parent);
					} else {
						var pitn = this.CreateNode(items[i]);
						pitn.Update(parent);
						this.Nodes.Add(pitn);
					}
				}
			}
		}

		public ProjectItemTreeNodeOld AddItem(ItemMetadata item)
		{
			var prj  = _project_item as Project ?? _project_item.Parent;
			var pi   = prj.AddItem(item);
			var pitn = this.CreateNode(pi);

			if (_project_item is Project) {
				this.Nodes.Add(pitn);
			} else {
				this.Parent.Nodes.Add(pitn);
			}

			return pitn;
		}

		public void RemoveItem(ItemMetadata item)
		{
			var prj  = _project_item as Project ?? _project_item.Parent;
			prj.RemoveItem(item);
		}

		private ProjectItemTreeNodeOld CreateNode(ProjectItem pItem)
		{
			var pitn = new ProjectItemTreeNodeOld(pItem);
			pitn.Name = pItem.Name;
			pitn.Text = pItem.Name;
			return pitn;
		}
	}
}
