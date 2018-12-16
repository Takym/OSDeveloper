using System.ComponentModel;
using System.Drawing;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see langword="OSDeveloper"/>独自のコントロールで利用されるカラーテーマを定義します。
	///  このカラーテーマには三つの基本色が含まれます。
	/// </summary>
	[TypeConverter(typeof(OsdevColorThemeConverter))]
	public class OsdevColorTheme
	{
		/// <summary>
		///  初期状態のカラーテーマを取得します。
		/// </summary>
		public static OsdevColorTheme Default
		{
			get
			{
				if (_inst == null) {
					_inst = new OsdevColorTheme();
				}
				return _inst;
			}
		}
		private static OsdevColorTheme _inst;

		internal virtual string KnownName
		{
			get
			{
				return this.GetType() == typeof(OsdevColorTheme)
					? "Gray"
					: "Unknown";
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevColorTheme"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		protected OsdevColorTheme() { } // コンストラクタ隠蔽

		/// <summary>
		///  通常の色を取得します。
		/// </summary>
		public virtual Color Normal { get { return Color.FromArgb(0x80, 0x80, 0x80); } }

		/// <summary>
		///  通常より明るい(薄い)色を取得します。
		/// </summary>
		public virtual Color Light { get { return Color.FromArgb(0xC0, 0xC0, 0xC0); } }

		/// <summary>
		///  通常より暗い(濃い)色を取得します。
		/// </summary>
		public virtual Color Dark { get { return Color.FromArgb(0x40, 0x40, 0x40); } }

		/// <summary>
		///  このカラーテーマを判読可能な文字列に変換します。
		/// </summary>
		/// <returns>このカラーテーマの内容を表す文字列です。</returns>
		public override string ToString()
		{
			if (this.KnownName == "Unknown") {
				return $"N:{this.Normal}, L:{this.Light}, D:{this.Dark}";
			} else {
				return this.KnownName;
			}
		}
	}
}
