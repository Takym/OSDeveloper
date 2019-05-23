using TakymLib.IO;
using Yencon;

namespace OSDeveloper.Projects
{
	public sealed class Solution : Project
	{
		#region プロパティ

		public PathString Workspace { get; }

		#endregion

		#region コンストラクタ

		public Solution(PathString cwd, string name) : base(name)
		{
			this.Workspace = cwd;
		}

		#endregion

		#region 情報取得

		public override PathString GetFullPath()
		{
			return this.Workspace.Bond(this.Name);
		}

		#endregion

		#region 企画/計画設定ファイルの読み書き

		public override void WriteTo(YSection section)
		{
			base.WriteTo(section);
		}

		/// <exception cref="System.ArgumentException" />
		public override void ReadFrom(YSection section)
		{
			base.ReadFrom(section);
		}

		#endregion
	}
}
