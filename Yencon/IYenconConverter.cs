namespace Yencon
{
	/// <summary>
	///  ヱンコンを別のフォーマットに変換する機能を提供します。
	/// </summary>
	/// <typeparam name="T">変換対象のオブジェクトの種類です。</typeparam>
	public interface IYenconConverter<T>
	{
		/// <summary>
		///  指定されたファイルからデータを読み取り、
		///  <see cref="Yencon.YSection"/>に変換します。
		/// </summary>
		/// <param name="filename">読み取るファイルのファイル名です。</param>
		/// <returns>生成された型'<see cref="Yencon.YSection"/>'のオブジェクト値です。</returns>
		YSection Load(string filename);

		/// <summary>
		///  指定された<see cref="Yencon.YSection"/>を指定されたファイルに書き込みます。
		/// </summary>
		/// <param name="filename">書き込み先のファイルのファイル名です。</param>
		/// <param name="obj">変換する型'<see cref="Yencon.YSection"/>'のオブジェクト値です。</param>
		void Save(string filename, YSection obj);

		/// <summary>
		///  指定された<see cref="Yencon.YSection"/>を<typeparamref name="T"/>に変換します。
		/// </summary>
		/// <param name="obj">変換する型'<see cref="Yencon.YSection"/>'のオブジェクト値です。</param>
		/// <returns>変換結果の新たに生成されたオブジェクトインスタンスです。</returns>
		T ToObject(YSection obj);

		/// <summary>
		///  指定された<typeparamref name="T"/>を<see cref="Yencon.YSection"/>に変換します。
		/// </summary>
		/// <param name="obj">変換する型'<typeparamref name="T"/>'のオブジェクト値です。</param>
		/// <returns>変換結果の新たに生成されたオブジェクトインスタンスです。</returns>
		YSection ToYencon(T obj);
	}
}
