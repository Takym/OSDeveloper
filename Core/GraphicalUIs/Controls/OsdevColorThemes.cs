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
		///  何も指定されていない場合の限定の設定です。
		/// </summary>
		public static OsdevColorTheme Gray { get { return OsdevColorTheme.Default; } }

		/// <summary>
		///  鮭色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.GridColor"/>向けに設計されました。
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

		/// <summary>
		///  爽やかな青系のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.ButtonColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme FreshBlue { get { return OCT_FRESH_BLUE._inst; } }
		private sealed class OCT_FRESH_BLUE : OsdevColorTheme
		{
			private OCT_FRESH_BLUE() { }
			public override Color Normal { get { return Color.Aqua; } }
			public override Color Light { get { return Color.DeepSkyBlue; } }
			public override Color Dark { get { return Color.LightSkyBlue; } }
			internal readonly static OCT_FRESH_BLUE _inst = new OCT_FRESH_BLUE();
		}

		/// <summary>
		///  シアン色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.MouseActionButtonColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme Cyan { get { return OCT_CYAN._inst; } }
		private sealed class OCT_CYAN : OsdevColorTheme
		{
			private OCT_CYAN() { }
			public override Color Normal { get { return Color.Cyan; } }
			public override Color Light { get { return Color.LightCyan; } }
			public override Color Dark { get { return Color.DarkCyan; } }
			internal readonly static OCT_CYAN _inst = new OCT_CYAN();
		}
	}
}
