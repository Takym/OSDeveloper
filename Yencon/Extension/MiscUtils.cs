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

		/// <summary>
		///  <paramref name="section"/>から子セクションを取得します。
		/// </summary>
		/// <param name="section">取得元の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">取得する子セクションの名前です。</param>
		/// <returns>
		///  指定された名前のノードが、
		///  子セクションの場合はそのセクションを、
		///  それ以外の場合は<see langword="null"/>を返します。
		/// </returns>
		public static YSection GetNodeAsSection(this YSection section, string keyname)
		{
			var node = section.GetNode(keyname);
			if (node is YSection secKey) {
				return secKey;
			} else {
				return null;
			}
		}

		/// <summary>
		///  <paramref name="section"/>から文字列値を取得します。
		/// </summary>
		/// <param name="section">取得元の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">取得する文字列キーの名前です。</param>
		/// <returns>
		///  指定された名前のノードが、
		///  文字列キーの場合はキーが保持している文字列を返し、
		///  それ以外の場合は、キーの値を文字列に変換して返します。
		/// </returns>
		public static string GetNodeAsString(this YSection section, string keyname)
		{
			var node = section.GetNode(keyname);
			if (node is YString strKey) {
				return strKey.Text;
			} else {
				return node?.GetValue()?.ToString() ?? string.Empty;
			}
		}

		/// <summary>
		///  <paramref name="section"/>に文字列値を設定します。
		/// </summary>
		/// <param name="section">設定先の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">設定する文字列キーの名前です。</param>
		/// <param name="value">設定する文字列です。</param>
		public static void SetNodeAsString(this YSection section, string keyname, string value)
		{
			var node = section.GetNode(keyname);
			if (node is YString strKey) {
				strKey.Text = value;
			} else {
				section.Add(new YString() { Name = keyname, Text = value });
			}
		}

		/// <summary>
		///  <paramref name="section"/>から数値を取得します。
		/// </summary>
		/// <param name="section">取得元の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">取得する数値キーの名前です。</param>
		/// <returns>
		///  指定された名前のノードが、
		///  数値キーの場合はキーが保持している符号付き64ビット数値を返し、
		///  それ以外の場合は、<c>0</c>を返します。
		/// </returns>
		public static long GetNodeAsNumber(this YSection section, string keyname)
		{
			var node = section.GetNode(keyname);
			if (node is YNumber numKey) {
				return numKey.SInt64Value;
			} else {
				return 0;
			}
		}

		/// <summary>
		///  <paramref name="section"/>に数値を設定します。
		/// </summary>
		/// <param name="section">設定先の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">設定する数値キーの名前です。</param>
		/// <param name="value">設定する数値です。</param>
		public static void SetNodeAsNumber(this YSection section, string keyname, long value)
		{
			var node = section.GetNode(keyname);
			if (node is YNumber numKey) {
				numKey.SInt64Value = value;
			} else {
				section.Add(new YNumber() { Name = keyname, SInt64Value = value });
			}
		}

		/// <summary>
		///  <paramref name="section"/>から論理値を取得します。
		/// </summary>
		/// <param name="section">取得元の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">取得する論理値キーの名前です。</param>
		/// <returns>
		///  指定された名前のノードが、
		///  論理値キーの場合はキーが保持している論理値を返し、
		///  それ以外の場合は、<see langword="null"/>を返します。
		/// </returns>
		public static bool? GetNodeAsBoolean(this YSection section, string keyname)
		{
			var node = section.GetNode(keyname);
			if (node is YBoolean flgKey) {
				return flgKey.Flag;
			} else {
				return null;
			}
		}

		/// <summary>
		///  <paramref name="section"/>に論理値を設定します。
		/// </summary>
		/// <param name="section">設定先の<see cref="Yencon.YSection"/>です。</param>
		/// <param name="keyname">設定する論理値キーの名前です。</param>
		/// <param name="value">設定する論理値です。</param>
		public static void SetNodeAsBoolean(this YSection section, string keyname, bool value)
		{
			var node = section.GetNode(keyname);
			if (node is YBoolean flgKey) {
				flgKey.Flag = value;
			} else {
				section.Add(new YBoolean() { Name = keyname, Flag = value });
			}
		}
	}
}
