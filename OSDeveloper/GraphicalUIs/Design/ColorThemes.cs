using System.Drawing;

namespace OSDeveloper.GraphicalUIs.Design
{
	/// <summary>
	///  <see cref="OSDeveloper.GraphicalUIs.Design.ColorTheme"/>のインスタンスを取得する為の静的クラスです。
	/// </summary>
	public static class ColorThemes
	{
		// "OCT" stands for the Osdev Color Theme.

		/// <summary>
		///  灰色のカラーテーマを取得します。
		///  何も指定されていない場合の限定の設定です。
		/// </summary>
		public static ColorTheme Gray { get => ColorTheme.Default; }

		/// <summary>
		///  鮭色のカラーテーマを取得します。
		/// </summary>
		public static ColorTheme Salmon { get => OCT_SALMON._inst; }
		private sealed class OCT_SALMON : ColorTheme
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
		/// </summary>
		public static ColorTheme FreshBlue { get => OCT_FRESH_BLUE._inst; }
		private sealed class OCT_FRESH_BLUE : ColorTheme
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
		/// </summary>
		public static ColorTheme FreshCyan { get => OCT_FRESH_CYAN._inst; }
		private sealed class OCT_FRESH_CYAN : ColorTheme
		{
			private OCT_FRESH_CYAN() { }
			public override Color Normal       { get => Color.CadetBlue; }
			public override Color Light        { get => Color.LightCyan; }
			public override Color Dark         { get => Color.DarkCyan; }
			internal override string KnownName { get => nameof(FreshCyan); }
			internal readonly static OCT_FRESH_CYAN _inst = new OCT_FRESH_CYAN();
		}

		/// <summary>
		///  ハイライト色のカラーテーマを取得します。
		/// </summary>
		public static ColorTheme Highlight { get => OCT_HIGHLIGHT._inst; }
		private sealed class OCT_HIGHLIGHT : ColorTheme
		{
			private OCT_HIGHLIGHT() { }
			public override Color Normal       { get => Color.LightCyan; }
			public override Color Light        { get => Color.SlateBlue; }
			public override Color Dark         { get => Color.BlueViolet; }
			internal override string KnownName { get => nameof(Highlight); }
			internal readonly static OCT_HIGHLIGHT _inst = new OCT_HIGHLIGHT();
		}

		/// <summary>
		///  紫色のカラーテーマを取得します。
		/// </summary>
		public static ColorTheme Purple { get => OCT_PURPLE._inst; }
		private sealed class OCT_PURPLE : ColorTheme
		{
			private OCT_PURPLE() { }
			public override Color Normal       { get => Color.FromArgb(0xAE, 0x9C, 0xAE); }
			public override Color Light        { get => Color.FromArgb(0xA4, 0x9C, 0xB8); }
			public override Color Dark         { get => Color.FromArgb(0xB8, 0x9C, 0xA4); }
			internal override string KnownName { get => nameof(Purple); }
			internal readonly static OCT_PURPLE _inst = new OCT_PURPLE();
		}
	}
}
