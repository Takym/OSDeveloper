using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.VisualStyles;
using Yencon;
using Yencon.Extension;

namespace OSDeveloper.IO.Configuration
{
	partial class SettingManager
	{
		public static class System
		{
			private static T GetKey<T>(string keyName, T defaultValue) where T: YNode
			{
				defaultValue.Name = keyName;
				return _y_system_inix.GetNode(keyName) as T
					?? _y_system_cfg .GetNode(keyName) as T
					?? _y_system_cfg.Add(defaultValue) as T;
			}

			public static VisualStyleState VisualStyle
			{
				get
				{
					var node = GetKey("visualstyle", new YString() { Text = DefaultVisualStyle.ToString() });
					if (Enum.TryParse(node.Text, out VisualStyleState result)) {
						return result;
					}
					return DefaultVisualStyle;
				}

				set
				{
					_y_system_inix.Add(new YString() { Name = "visualstyle", Text = value.ToString() });
				}
			}
			public const VisualStyleState DefaultVisualStyle = VisualStyleState.ClientAndNonClientAreasEnabled;

			public static CultureInfo Language
			{
				get
				{
					var node = GetKey("lang", new YString() { Text = DefaultLanguage.Name });
					try {
						return CultureInfo.GetCultureInfo(node.Text) ?? DefaultLanguage;
					} catch (Exception e) {
						_logger.Exception(e);
						return DefaultLanguage;
					}
				}

				set
				{
					_y_system_inix.Add(new YString() { Name = "lang", Text = value.Name });
				}
			}
			public readonly static CultureInfo DefaultLanguage = CultureInfo.InstalledUICulture;

			public static Rectangle MainWindowPosition
			{
				get
				{
					var node = GetKey("mwndpos", DefaultMainWindowPosition.ToYSection());
					return node?.ToRectangle() ?? DefaultMainWindowPosition;
				}

				set
				{
					_y_system_inix.Add(value.ToYSection("mwndpos"));
				}
			}
			public readonly static Rectangle DefaultMainWindowPosition = new Rectangle(-1, -1, 1000, 750);
		}
	}
}
