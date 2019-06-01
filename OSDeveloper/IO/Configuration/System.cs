using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Yencon;
using Yencon.Extension;

namespace OSDeveloper.IO.Configuration
{
	partial class SettingManager
	{
		public static class System
		{
			internal static void EnsureAvailable()
			{
				_ = VisualStyle;
				_ = Language;
				_ = MainWindowPosition;
				_ = UseEXDialog;
				_ = UseWSLCommand;
				_ = TerminalTabSizeMode;
			}

			public static VisualStyleState VisualStyle
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfVisualStyle,
						() => new YString() { Text = DefaultVisualStyle.ToString() });
					if (Enum.TryParse(node.Text, out VisualStyleState result)) {
						return result;
					}
					return DefaultVisualStyle;
				}

				set
				{
					_y_system_inix.Add(new YString() { Name = KeyOfVisualStyle, Text = value.ToString() });
				}
			}
			public const string KeyOfVisualStyle = "visualstyle";
			public const VisualStyleState DefaultVisualStyle = VisualStyleState.ClientAndNonClientAreasEnabled;

			public static CultureInfo Language
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfLanguage,
						() => new YString() { Text = DefaultLanguage.Name });
					try {
						return CultureInfo.GetCultureInfo(node.Text) ?? DefaultLanguage;
					} catch (Exception e) {
						_logger.Exception(e);
						return DefaultLanguage;
					}
				}

				set
				{
					_y_system_inix.Add(new YString() { Name = KeyOfLanguage, Text = value.Name });
				}
			}
			public const string KeyOfLanguage = "lang";
			public readonly static CultureInfo DefaultLanguage = CultureInfo.InstalledUICulture;

			public static Rectangle MainWindowPosition
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfMainWindowPosition,
						() => DefaultMainWindowPosition.ToYSection());
					return node?.ToRectangle() ?? DefaultMainWindowPosition;
				}

				set
				{
					_y_system_inix.Add(value.ToYSection(KeyOfMainWindowPosition));
				}
			}
			public const string KeyOfMainWindowPosition = "mwndpos";
			public readonly static Rectangle DefaultMainWindowPosition = new Rectangle(-1, -1, 1000, 750);

			public static bool UseEXDialog
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfUseEXDialog,
						() => new YBoolean() { Flag = DefaultUseEXDialog });
					return node.Flag;
				}

				set
				{
					_y_system_inix.Add(new YBoolean() { Name = KeyOfUseEXDialog, Flag = value });
				}
			}
			public const string KeyOfUseEXDialog = "use_exdialog";
			public const bool DefaultUseEXDialog = true;

			public static bool UseWSLCommand
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfUseWSLCommand,
						() => new YBoolean() { Flag = DefaultUseWSLCommand });
					return node.Flag;
				}

				set
				{
					_y_system_inix.Add(new YBoolean() { Name = KeyOfUseWSLCommand, Flag = value });
				}
			}
			public const string KeyOfUseWSLCommand = "use_wslexe";
			public const bool DefaultUseWSLCommand = false;

			public static TabSizeMode TerminalTabSizeMode
			{
				get
				{
					var node = GetKey(_y_system_inix, _y_system_cfg, KeyOfTerminalTabSizeMode,
						() => new YString() { Text = DefaultTerminalTabSizeMode.ToString() });
					if (Enum.TryParse(node.Text, out TabSizeMode result)) {
						return result;
					}
					return DefaultTerminalTabSizeMode;
				}

				set
				{
					_y_system_inix.Add(new YString() { Name = KeyOfTerminalTabSizeMode, Text = value.ToString() });
				}
			}
			public const string KeyOfTerminalTabSizeMode = "term_szmd";
			public const TabSizeMode DefaultTerminalTabSizeMode = TabSizeMode.Normal;
		}
	}
}
