using System.Collections.Generic;

namespace OSDeveloper.Core.Workspace
{
	/// <summary>
	///  ソリューションを表します。このクラスは抽象クラスです。
	/// </summary>
	public abstract class Solution
	{
		/// <summary>
		///  このソリューションを格納しているワークスペースを取得します。
		/// </summary>
		public abstract SolutionGroup Workspace { get; }

		/// <summary>
		///  このソリューションの名前を取得します。
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		///  このソリューションが格納している全てのプロジェクトを取得します。
		/// </summary>
		public abstract IList<Project> Projects { get; }

		/// <summary>
		///  ソースコードが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string SourceCodeDirectory { get { return "in-src"; } }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ResourceDirectory { get { return "in-res"; } }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string DocumentDirectory { get { return "in-doc"; } }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string LibraryDirectory { get { return "in-lib"; } }

		/// <summary>
		///  ビルドに利用する追加のツールを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ToolkitDirectory { get { return "in-tol"; } }

		/// <summary>
		///  中間コードの保存先の一時ディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ObjectDirectory { get { return "out-obj"; } }

		/// <summary>
		///  デバッグ時に利用される一時ディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string DebugDirectory { get { return "out-dbg"; } }

		/// <summary>
		///  生成されたプログラムを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string BinaryDirectory { get { return "out-bin"; } }

		/// <summary>
		///  完成したオペレーティングシステムを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string PackageDirectory { get { return "out-pkg"; } }
	}
}
