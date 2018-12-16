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
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			var t = typeof(OsdevColorThemes);
			var p = t.GetProperties(BindingFlags.Public | BindingFlags.Static);
			List<object> items = new List<object>();
			foreach (var item in p) {
				items.Add(item.GetValue(null));
			}
			return new StandardValuesCollection(items);
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
				var t = typeof(OsdevColorThemes);
				var p = t.GetProperty(s, BindingFlags.Public | BindingFlags.Static);
				return p.GetValue(null);
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
