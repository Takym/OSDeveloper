#pragma warning disable CS1591 // 公開されている型またはメンバーの XML コメントがありません
using System;
using System.ComponentModel;
using System.Globalization;

namespace OSDeveloper.Core.GraphicalUIs.Controls.Design
{
	public class OsdevColorThemeConverter : TypeConverter
	{
		private readonly StandardValuesCollection _svc;

		public OsdevColorThemeConverter()
		{
			_svc = new StandardValuesCollection(new[] {
				OsdevColorThemes.Gray,
				OsdevColorThemes.Salmon
			});
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
			if (value is string str) {
				switch (str) {
					case nameof(OsdevColorThemes.Gray):
						return OsdevColorThemes.Gray;
					case nameof(OsdevColorThemes.Salmon):
						return OsdevColorThemes.Salmon;
					default:
						return base.ConvertFrom(context, culture, value);
				}
			} else {
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string)) {
				if (value == OsdevColorThemes.Gray) {
					return nameof(OsdevColorThemes.Gray);
				} else if (value == OsdevColorThemes.Salmon) {
					return nameof(OsdevColorThemes.Salmon);
				} else {
					return base.ConvertTo(context, culture, value, destinationType);
				}
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
