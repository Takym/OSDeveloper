using System;
using System.Drawing;

namespace Yencon.Extension
{
	/// <summary>
	///  <see cref="Yencon.YNode"/>と<see cref="object"/>の変換等のヱンコンに関する便利な機能を提供する静的クラスです。
	/// </summary>
	public static class MiscUtils
	{
		/// <summary>
		///  長方形の大きさを表す型'<see cref="System.Drawing.Rectangle"/>'をヱンコンのセクションに変換します。
		/// </summary>
		/// <param name="rect">変換前の長方形です。</param>
		/// <param name="name">変換後のセクションの名前です。</param>
		/// <returns>変換後のヱンコンのセクションを表す新しいインスタンスです。</returns>
		public static YSection ToYSection(this Rectangle rect, string name = "_rect")
		{
			var section = new YSection() { Name = name };
			section.Add(new YNumber() { Name = "x", SInt64Value = rect.X });
			section.Add(new YNumber() { Name = "y", SInt64Value = rect.Y });
			section.Add(new YNumber() { Name = "w", SInt64Value = rect.Width });
			section.Add(new YNumber() { Name = "h", SInt64Value = rect.Height });
			return section;
		}

		/// <summary>
		///  ヱンコンのセクションを長方形の大きさを表す型'<see cref="System.Drawing.Rectangle"/>'に変換します。
		/// </summary>
		/// <param name="section">変換前のセクションです。</param>
		/// <returns>変換後の長方形を表す新しいインスタンスです。</returns>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="section"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		public static Rectangle ToRectangle(this YSection section)
		{
			section = section ?? throw new ArgumentNullException(nameof(section));
			return new Rectangle(
				unchecked((int)((section.GetNode("x") as YNumber)?.SInt64Value)),
				unchecked((int)((section.GetNode("y") as YNumber)?.SInt64Value)),
				unchecked((int)((section.GetNode("w") as YNumber)?.SInt64Value)),
				unchecked((int)((section.GetNode("h") as YNumber)?.SInt64Value)));
		}
	}
}
