using System.Windows.Forms;
using OSDeveloper.IO.Logging;
using OSDeveloper.Projects;
using Yencon;

namespace OSDeveloper.GUIs.Explorer
{
	public class SolutionTreeNode : TreeNode
	{
		private Logger                _logger;
		private YenconStringConverter _converter;
		private Solution              _solution;

		public SolutionTreeNode(Solution solution)
		{
			_logger = Logger.Get(nameof(SolutionTreeNode));

			_converter = new YenconStringConverter();
			_solution  = solution;

			this.Text = _solution.Name;

			_logger.Trace($"constructed {nameof(SolutionTreeNode)}");
		}

		public void Load()
		{
			// TODO: 企画設定の読み込み処理はまだ暫定的。
			var data = _converter.Load(_solution.GetFullPath().Bond(_solution.Name + ".osdev_sln.tycn"));
			_solution.ReadFrom(data);
		}

		public void Save()
		{
			// TODO: 企画設定の書き込み処理はまだ暫定的。
			var data = new YSection();
			_solution.WriteTo(data);
			_converter.Save(_solution.GetFullPath().Bond(_solution.Name + ".osdev_sln.tycn"), data);
		}
	}
}
