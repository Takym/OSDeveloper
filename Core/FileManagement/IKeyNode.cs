namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  ファイルとデータベースの項目を表します。
	/// </summary>
	/// <typeparam name="T">この項目の値の型です。</typeparam>
	public interface IKeyNode<out T> where T: IKeyNodeValue
	{
		/// <summary>
		///  このオブジェクトの名前を取得または設定します。
		/// </summary>
		string Name { get; }

		/// <summary>
		///  このオブジェクトの値を取得します。
		/// </summary>
		T Value { get; }
	}

	/// <summary>
	///  ファイルとデータベースの項目を表します。
	///  項目の値の型を<see cref="OSDeveloper.Core.FileManagement.IKeyNodeValue"/>にする場合は、
	///  このインターフェースを利用してください。
	/// </summary>
	public interface IKeyNode : IKeyNode<IKeyNodeValue> { }
}
