using OSDeveloper.Projects;
using Yencon;

namespace OSDeveloper.GUIs.Explorer
{
	public class SolutionTreeNode : ProjectItemTreeNode
	{
		private YenconStringConverter _converter;
		public  Solution              Solution { get; }

		public SolutionTreeNode(Solution sln) : base(sln)
		{
			_converter      = YenconFormatRecognition.StringConverter;
			this.Solution   = sln;
			this.ImageIndex = IconList.Solution;
			this.Logger.Trace($"constructed {nameof(SolutionTreeNode)}, name:{sln.Name}");
		}

		public void Load()
		{
			var data = _converter.Load(this.Folder.GetSolutionFilePath());
			if (data.SubKeys.Length > 0) { // キーが存在する＝空ではない。
				this.Solution.ReadFrom(data);
			}
		}

		public void Save()
		{
			var data = new YSection();
			this.Solution.WriteTo(data);
			_converter.Save(this.Folder.GetSolutionFilePath(), data);
		}
	}
}
