using System.Drawing.Printing;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  印刷に関する操作をサポートするエディタを表します。
	/// </summary>
	public interface IPrintingFeature
	{
		/// <summary>
		///  ドキュメントの印刷に関する情報を保持するオブジェクトを取得します。
		/// </summary>
		PrintDocument PrintDocument { get; }
	}
}
