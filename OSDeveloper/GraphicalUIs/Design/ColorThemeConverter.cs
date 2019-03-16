using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using OSDeveloper.MiscUtils;

namespace OSDeveloper.GraphicalUIs.Design
{
	/// <summary>
	///  <see cref="OSDeveloper.GraphicalUIs.Design.ColorTheme"/>と文字列を変換します。
	/// </summary>
	public class ColorThemeConverter : TypeConverter
	{
		private readonly Dictionary<string, ColorTheme> _known_values;
		private readonly StandardValuesCollection _svc;

		/// <summary>
		///  型'<see cref="OSDeveloper.GraphicalUIs.Design.ColorThemeConverter"/>'
		///  の新しいインスタンスを生成します。
		/// </summary>
		public ColorThemeConverter()
		{
			_known_values = new Dictionary<string, ColorTheme>();
			var tmp_svc = new List<string>();
			var p = typeof(ColorTheme).GetProperties(BindingFlags.Public | BindingFlags.Static);
			for (int i = 0; i < p.Length; ++i) {
				if (p[i].GetValue(null) is ColorTheme oct) {
					_known_values.Add(oct.KnownName, oct);
					tmp_svc.Add(oct.KnownName);
				}
			}
			_svc = new StandardValuesCollection(tmp_svc);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return _svc;
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string)) {
				return true;
			} else {
				return base.CanConvertFrom(context, sourceType);
			}
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string s) {
				if (_known_values.ContainsKey(s)) {
					// KnownNameが指定された場合はKnownValueを返す
					return _known_values[s];
				} else {
					// UnknownCodeの場合は解析してOsdevColorTheme.Customを返す。
					StringBuilder sb = new StringBuilder();
					(var n, var l, var d) = (Color.Empty, Color.Empty, Color.Empty);
					for (int i = 0; i < s.Length;) {
						char t = s[i];
						if (s[i + 1] == ':') { // 種類と数値がコロンで区切られている場合
							i += 2;
							// 数値を文字列バッファに格納
							while (s[i].IsHexadecimal() && i < s.Length) sb.Append(s[i++]);
							// 文字列バッファを数値に変換する
							if (uint.TryParse(sb.ToString(), out uint v)) {
								// 数値を色に変換する
								var c = Color.FromArgb((int)(v));
								// 色の種類を解析する
								switch (t) {
									case 'N': case 'n': n = c; break;
									case 'L': case 'l': l = c; break;
									case 'D': case 'd': d = c; break;
								}
							}
							sb.Clear();
						} else { // それ以外は全て無視
							++i;
						}
					}
					// 解析した情報を元にOsdevColorTheme.Customを生成して返す
					return new ColorTheme.Custom(n, l, d);
				}
			} else {
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is ColorTheme oct) {
				return oct.KnownName;
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
