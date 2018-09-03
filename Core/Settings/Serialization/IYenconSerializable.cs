namespace OSDeveloper.Core.Settings.Serialization
{
	/// <summary>
	///  <see langword="Yencon"/>のシリアル化と逆シリアル化を提供します。
	/// </summary>
	public interface IYenconSerializable
	{
		/// <summary>
		///  指定された<see langword="Yencon"/>のセクションにこのオブジェクトのデータを書き込みます。
		/// </summary>
		/// <param name="section">オブジェクトの保存先です。</param>
		void WriteValues(YenconSection section);

		/// <summary>
		///  指定された<see langword="Yencon"/>のセクションからこのオブジェクトのデータを読み込みます。
		/// </summary>
		/// <param name="section">オブジェクトの読み込み元です。</param>
		void ReadValues(YenconSection section);
	}
}
