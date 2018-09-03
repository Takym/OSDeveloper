using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Workspace
{
	/// <summary>
	///  ワークスペースを表します。
	///  これは<see cref="OSDeveloper.Core.Workspace.Solution"/>のグループです。
	/// </summary>
	public sealed class SolutionGroup
	{
		/// <summary>
		///  このワークスペースが保管されているディレクトリを取得します。
		/// </summary>
		public PathString Directory { get; }
	}
}
