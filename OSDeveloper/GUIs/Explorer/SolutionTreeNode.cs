using OSDeveloper.IO.Logging;
using OSDeveloper.Projects;
using Yencon;

namespace OSDeveloper.GUIs.Explorer
{
	public class SolutionTreeNode : ProjectItemTreeNode
	{
		private Logger                _logger;
		private YenconStringConverter _converter;
		private Solution              _solution;

		public Solution Solution { get => _solution; }

		public SolutionTreeNode(Solution solution) : base(solution)
		{
			_logger = Logger.Get(nameof(SolutionTreeNode));

			_converter = YenconFormatRecognition.StringConverter;
			_solution  = solution;

			_logger.Trace($"constructed {nameof(SolutionTreeNode)}");
		}

		public void Load()
		{
			var data = _converter.Load(_solution.GetFullPath().Bond(_solution.Name + ".osdev_sln"));
			if (data.SubKeys.Length > 0) { // キーが存在する＝空ではない。
				_solution.ReadFrom(data);
			}
		}

		public void Save()
		{
			var data = new YSection();
			_solution.WriteTo(data);
			_converter.Save(_solution.GetFullPath().Bond(_solution.Name + ".osdev_sln"), data);
		}

		public override void Update(FileTreeBox parent)
		{
			base.Update(parent);
			var items = _solution.Contents;
			for (int i = 0; i < items.Count; ++i) {
				if (this.Nodes.ContainsKey(items[i].Name)) {
					(this.Nodes[items[i].Name] as ProjectItemTreeNode)?.Update(parent);
				} else {
					var pitn = new ProjectItemTreeNode(items[i]);
					pitn.Name = items[i].Name;
					pitn.Update(parent);
					this.Nodes.Add(pitn);
				}
			}
		}

		public void Refresh(FileTreeBox parent)
		{
			this.Load();
			this.Update(parent);
		}
	}
}
