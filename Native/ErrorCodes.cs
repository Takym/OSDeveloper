namespace OSDeveloper.Native
{
	/// <summary>
	///  <see langword="HResult"/>の一覧です。
	/// </summary>
	public static class ErrorCodes
	{
		/// <summary>
		///  この操作を正しく終了しました。
		/// </summary>
		public const int ERROR_SUCCESS = 0;

		/// <summary>
		///  ファンクションが間違っています。
		/// </summary>
		public const int ERROR_INVALID_FUNCTION = 1;

		/// <summary>
		///  指定されたファイルが見つかりません。
		/// </summary>
		public const int ERROR_FILE_NOT_FOUND = 2;
	}
}
