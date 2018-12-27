namespace OSDeveloper.Assets
{
	/// <summary>
	///  <see langword="East Asian Width"/>の種類を表します。
	/// </summary>
	public enum EAWType : byte
	{
		/// <summary>
		///  <see cref="OSDeveloper.Assets.EAWType.Fullwidth"/>、
		///  <see cref="OSDeveloper.Assets.EAWType.Halfwidth"/>、
		///  <see cref="OSDeveloper.Assets.EAWType.Narrow"/>、
		///  <see cref="OSDeveloper.Assets.EAWType.Wide"/>、
		///  <see cref="OSDeveloper.Assets.EAWType.Ambiguous"/>に属さない文字を表します。
		/// </summary>
		Neutral,

		/// <summary>
		///  全角文字を表します。
		/// </summary>
		Fullwidth,

		/// <summary>
		///  半角文字を表します。
		/// </summary>
		Halfwidth,

		/// <summary>
		///  狭い文字を表します。
		/// </summary>
		Narrow,

		/// <summary>
		///  広い文字を表します。
		/// </summary>
		Wide,

		/// <summary>
		///  文脈によって文字幅が変わる曖昧な文字を表します。
		/// </summary>
		Ambiguous
	}
}
