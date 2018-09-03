namespace OSDeveloper.Core.Workspace
{
	/// <summary>
	///  プロジェクトを表します。
	/// </summary>
	public class Project
	{
		/// <summary>
		///  このプロジェクトを格納しているワークスペースを取得します。
		/// </summary>
		public SolutionGroup Workspace
		{
			get
			{
				return this.Parent.Workspace;
			}
		}

		/// <summary>
		///  このプロジェクトを格納しているソリューションを取得します。
		/// </summary>
		public virtual Solution Parent { get; }

		/// <summary>
		///  このプロジェクトの名前を取得します。
		/// </summary>
		public virtual string Name { get; }
	}
}
