#pragma warning disable CS1591 // 公開されている型またはメンバーの XML コメントがありません
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevColorTheme"/>と文字列を変換します。
	/// </summary>
	public class OsdevColorThemeConverter : TypeConverter
	{
		private Dictionary<string, OsdevColorTheme> _known_values;
		private StandardValuesCollection _svc;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevColorThemeConverter"/>'
		///  の新しいインスタンスを生成します。
		/// </summary>
		public OsdevColorThemeConverter()
		{
			_known_values = new Dictionary<string, OsdevColorTheme>();
			var tmp_svc = new List<string>();
			var p = typeof(OsdevColorThemes).GetProperties(BindingFlags.Public | BindingFlags.Static);
			foreach (var item in p) {
				if (item.GetValue(null) is OsdevColorTheme oct) {
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
				return _known_values[s];
			} else {
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is OsdevColorTheme oct) {
				return oct.KnownName;
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
