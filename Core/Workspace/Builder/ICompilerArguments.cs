using System.IO;

namespace OSDeveloper.Core.Workspace.Builder
{
	/// <summary>
	///  コンパイラに渡す追加の引数を表します。
	/// </summary>
	public interface ICompilerArguments
	{
		/// <summary>
		///  コンパイラが利用する標準入力ストリームを取得します。
		/// </summary>
		TextReader StandardInputStream { get; }

		/// <summary>
		///  コンパイラが利用する標準出力ストリームを取得します。
		/// </summary>
		TextWriter StandardOutputStream { get; set; }

		/// <summary>
		///  コンパイラが利用する標準エラーストリームを取得します。
		/// </summary>
		TextWriter StandardErrorStream { get; set; }
	}
}