namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  <see cref="OSDeveloper.Core.FileManagement.IKeyNode{T}"/>の値を表します。
	/// </summary>
	public interface IKeyNodeValue
	{
		/// <summary>
		///  このオブジェクトの値を<see langword="CLR"/>のオブジェクトとして取得します。
		/// </summary>
		/// <returns>このオブジェクトの値を表すオブジェクトです。</returns>
		object GetValue();
	}
}
