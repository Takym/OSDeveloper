namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  ログの種類を表します。
	/// </summary>
	public enum LogLevel : byte
	{
		/// <summary>
		///  備考情報を表します。
		///  前回のログに追加情報が必要な場合に利用されます。
		/// </summary>
		Notice = 0,

		/// <summary>
		///  関数等の呼び出し/終了の痕跡を表します。
		/// </summary>
		Trace = 1,

		/// <summary>
		///  デバッグ情報や内部変数の値等を表します。
		/// </summary>
		Debug = 2,

		/// <summary>
		///  通常のログ情報を表します。
		///  これは一般的に動作中の処理の説明に利用されます。
		/// </summary>
		Info = 3,

		/// <summary>
		///  無視しても問題ない警告情報を表します。
		/// </summary>
		Warn = 4,

		/// <summary>
		///  既に例外処理が行われたエラーを表します。
		/// </summary>
		Error = 5,

		/// <summary>
		///  致命的なエラーを表します。
		///  これは、アプリケーションがエラー終了する時に利用されます。
		/// </summary>
		Fatal = 6
	}
}
