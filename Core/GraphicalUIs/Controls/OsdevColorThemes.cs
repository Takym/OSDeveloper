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
		public static OsdevColorTheme Gray { get => OsdevColorTheme.Default; }

		/// <summary>
		///  鮭色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.GridColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme Salmon { get => OCT_SALMON._inst; }
		private sealed class OCT_SALMON : OsdevColorTheme
		{
			private OCT_SALMON() { }
			public override Color Normal       { get => Color.Salmon; }
			public override Color Light        { get => Color.LightSalmon; }
			public override Color Dark         { get => Color.DarkSalmon; }
			internal override string KnownName { get => nameof(Salmon); }
			internal readonly static OCT_SALMON _inst = new OCT_SALMON();
		}

		/// <summary>
		///  爽やかな青系の色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.ButtonColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme FreshBlue { get => OCT_FRESH_BLUE._inst; }
		private sealed class OCT_FRESH_BLUE : OsdevColorTheme
		{
			private OCT_FRESH_BLUE() { }
			public override Color Normal       { get => Color.Aqua; }
			public override Color Light        { get => Color.DeepSkyBlue; }
			public override Color Dark         { get => Color.LightSkyBlue; }
			internal override string KnownName { get => nameof(FreshBlue); }
			internal readonly static OCT_FRESH_BLUE _inst = new OCT_FRESH_BLUE();
		}

		/// <summary>
		///  爽やかなシアン系の色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.MdiChildrenTab.MouseActionButtonColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme FreshCyan { get => OCT_FRESH_CYAN._inst; }
		private sealed class OCT_FRESH_CYAN : OsdevColorTheme
		{
			private OCT_FRESH_CYAN() { }
			public override Color Normal       { get => Color.AliceBlue; }
			public override Color Light        { get => Color.LightCyan; }
			public override Color Dark         { get => Color.DarkCyan; }
			internal override string KnownName { get => nameof(FreshCyan); }
			internal readonly static OCT_FRESH_CYAN _inst = new OCT_FRESH_CYAN();
		}

		/// <summary>
		///  ハイライト色のカラーテーマを取得します。
		///  これは<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox.SelectionColor"/>向けに設計されました。
		/// </summary>
		public static OsdevColorTheme Highlight { get => OCT_HIGHLIGHT._inst; }
		private sealed class OCT_HIGHLIGHT : OsdevColorTheme
		{
			private OCT_HIGHLIGHT() { }
			public override Color Normal       { get => Color.LightCyan; }
			public override Color Light        { get => Color.SlateBlue; }
			public override Color Dark         { get => Color.BlueViolet; }
			internal override string KnownName { get => nameof(Highlight); }
			internal readonly static OCT_HIGHLIGHT _inst = new OCT_HIGHLIGHT();
		}
	}
}
