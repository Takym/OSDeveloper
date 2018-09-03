namespace OSDeveloper.Core.Error
{
	/// <summary>
	///  失敗したオブジェクトのシリアル化と逆シリアル化の状態を表します。
	/// </summary>
	public enum SerializingFailureState
	{
		/// <summary>
		///  原因が不明である事を表します。
		/// </summary>
		Unknown,

		/// <summary>
		///  ルート属性が無視された事を表します。
		/// </summary>
		IgnoredRootElement
	}
}
