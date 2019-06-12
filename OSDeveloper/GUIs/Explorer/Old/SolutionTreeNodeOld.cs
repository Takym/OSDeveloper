using OSDeveloper.IO.Logging;
using OSDeveloper.Projects;
using Yencon;

namespace OSDeveloper.GUIs.Explorer
{
	public class SolutionTreeNodeOld : ProjectItemTreeNodeOld
	{
		private Logger                _logger;
		private YenconStringConverter _converter;
		private Solution              _solution;

		public Solution Solution { get => _solution; }

		public SolutionTreeNodeOld(Solution solution) : base(solution)
		{
			_logger = Logger.Get(nameof(SolutionTreeNodeOld));

			_converter = YenconFormatRecognition.StringConverter;
			_solution  = solution;

			_logger.Trace($"constructed {nameof(SolutionTreeNodeOld)}");
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

		public void Refresh(FileTreeBoxOld parent)
		{
			this.Load();
			this.Update(parent);
		}
	}
}
