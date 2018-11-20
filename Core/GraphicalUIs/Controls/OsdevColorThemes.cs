using System.Drawing;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevColorTheme"/>のインスタンスを取得する為の静的クラスです。
	/// </summary>
	public static class OsdevColorThemes
	{
		// "OCT" stands for the Osdev Color Theme.

		/// <summary>
		///  灰色のカラーテーマを取得します。
		/// </summary>
		public static OsdevColorTheme Gray { get { return OsdevColorTheme.Default; } }

		/// <summary>
		///  鮭色のカラーテーマを取得します。
		/// </summary>
		public static OsdevColorTheme Salmon { get { return OCT_SALMON._inst; } }
		private sealed class OCT_SALMON : OsdevColorTheme
		{
			private OCT_SALMON() { }
			public override Color Normal { get { return Color.Salmon; } }
			public override Color Light  { get { return Color.LightSalmon; } }
			public override Color Dark   { get { return Color.DarkSalmon; } }
			internal readonly static OCT_SALMON _inst = new OCT_SALMON();
		}
	}
}
