namespace OSDeveloper.Core.Workspace
{
	/// <summary>
	///  ソリューションを表します。このクラスは抽象クラスです。
	/// </summary>
	public abstract class Solution
	{
		/// <summary>
		///  このソリューションが保存されているワークスペースを取得します。
		/// </summary>
		public abstract SolutionGroup Workspace { get; }

		/// <summary>
		///  このソリューションの名前を取得します。
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		///  ソースコードが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string SourceCodeDirectory { get; }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ResourceDirectory { get; }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string DocumentDirectory { get; }

		/// <summary>
		///  リソースが格納されているディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string LibraryDirectory { get; }

		/// <summary>
		///  ビルドに利用する追加のツールを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ToolkitDirectory { get; }

		/// <summary>
		///  中間コードの保存先の一時ディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string ObjectDirectory { get; }

		/// <summary>
		///  デバッグ時に利用される一時ディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string DebugDirectory { get; }

		/// <summary>
		///  生成されたプログラムを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string BinaryDirectory { get; }

		/// <summary>
		///  完成したオペレーティングシステムを格納するディレクトリの名前を取得します。
		///  これは、ソリューションディレクトリからの相対パスです。
		/// </summary>
		public virtual string PackageDirectory { get; }
	}
}
