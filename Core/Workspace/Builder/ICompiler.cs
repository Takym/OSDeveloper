using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Workspace.Builder
{
	/// <summary>
	///  コンパイラを提供します。
	/// </summary>
	public interface ICompiler
	{
		/// <summary>
		///  プロジェクトファイル等で識別子として利用する名前を取得します。
		/// </summary>
		string Name { get; }

		/// <summary>
		///  コンパイラの表示名を取得します。
		/// </summary>
		string DisplayName { get; }

		/// <summary>
		///  コンパイラの説明を取得します。
		/// </summary>
		string Description { get; }

		/// <summary>
		///  コンパイラの実行可能ファイル名を取得します。
		/// </summary>
		string CompilerPath { get; }

		/// <summary>
		///  コンパイラの種類を取得します。
		/// </summary>
		CompilerType Kind { get; }

		/// <summary>
		///  入力ファイルの拡張子の一覧を取得します。
		/// </summary>
		(string type, string[] exts) InputFileType { get; }

		/// <summary>
		///  出力ファイルの拡張子の一覧を取得します。
		/// </summary>
		(string type, string[] exts) OutputFileType { get; }

		/// <summary>
		///  入力ファイルのフォーマットの種類を取得します。
		/// </summary>
		InputFormat InputFormat { get; }

		/// <summary>
		///  出力ファイルのフォーマットの種類を取得します。
		/// </summary>
		OutputFormat OutputFormat { get; }

		/// <summary>
		///  複数の入力ファイルから一つの出力ファイルを生成します。
		/// </summary>
		/// <param name="infiles">入力ファイルのパス文字列配列です。</param>
		/// <param name="outfile">出力ファイルのパス文字列です。</param>
		/// <param name="args">コンパイラに渡す追加の引数です。</param>
		/// <returns>コンパイラから返された戻り値です。</returns>
		int Convert(PathString[] infiles, PathString outfile, ICompilerArguments args);
	}
}
