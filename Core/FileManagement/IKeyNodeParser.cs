using System.Collections.Generic;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  文字列データから<see cref="OSDeveloper.Core.FileManagement.IKeyNode{T}"/>に変換する機能を提供します。
	/// </summary>
	public interface IKeyNodeParser
	{
		/// <summary>
		///  変換前の文字列データを取得します。
		/// </summary>
		string Source { get; }

		/// <summary>
		///  変換結果を取得します。この値は必ず新しいインスタンスになります。
		///  この関数は<see cref="OSDeveloper.Core.FileManagement.IKeyNodeParser.Analyze"/>を呼び出してから実行してください。
		/// </summary>
		/// <returns>変換結果を表す読み取り専用のリストオブジェクトです。</returns>
		IReadOnlyList<IKeyNode<IKeyNodeValue>> GetNodes();

		/// <summary>
		///  変換を行います。
		/// </summary>
		void Analyze();
	}

	/// <summary>
	///  文字列データから<see cref="OSDeveloper.Core.FileManagement.IKeyNode{T}"/>に変換する機能を提供します。
	/// </summary>
	/// <typeparam name="TNode">項目の種類です。</typeparam>
	/// <typeparam name="TValue">項目の値の種類です。</typeparam>
	public interface IKeyNodeParser<out TNode, TValue> : IKeyNodeParser
		where TNode: IKeyNode<TValue>
		where TValue: IKeyNodeValue
	{
		/// <summary>
		///  変換結果を取得します。この値は必ず新しいインスタンスになります。
		///  この関数は<see cref="OSDeveloper.Core.FileManagement.IKeyNodeParser.Analyze"/>を呼び出してから実行してください。
		/// </summary>
		/// <returns>変換結果を表す読み取り専用のリストオブジェクトです。</returns>
		new IReadOnlyList<TNode> GetNodes();
	}
}
