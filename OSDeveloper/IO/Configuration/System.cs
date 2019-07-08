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
				_ = VisualStyle;         // 起動設定
				_ = Language;            // 起動設定
				_ = MainWindowPosition;  // 起動設定
				_ = UseEXDialog;         // 環境設定
				_ = UseWSLCommand;       // 環境設定
				_ = TerminalTabSizeMode; // 起動設定
				_ = RiskySettings;       // 起動設定
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
					_y_system_inix.SetNodeAsString(KeyOfVisualStyle, value.ToString());
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
					_y_system_inix.SetNodeAsString(KeyOfLanguage, value.Name);
				}
			}
			public const string KeyOfLanguage = "lang";
			public readonly static CultureInfo DefaultLanguage = Globalization.GetDefaultCulture();

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
					_y_system_inix.SetNodeAsBoolean(KeyOfUseEXDialog, value);
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
					_y_system_inix.SetNodeAsBoolean(KeyOfUseWSLCommand, value);
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
					_y_system_inix.SetNodeAsString(KeyOfTerminalTabSizeMode, value.ToString());
				}
			}
			public const string KeyOfTerminalTabSizeMode = "term_szmd";
			public const TabSizeMode DefaultTerminalTabSizeMode = TabSizeMode.Normal;

			public static DangerSettings RiskySettings
			{
				get
				{
					var node   = GetKey(_y_system_inix, _y_system_cfg, KeyOfRiskySettings, () => DefaultRiskySettings.ToYencon());
					var result = DangerSettings.FromYencon(node);
					if (result.AllowDangerSettings) {
						return result;
					} else {
						return DefaultRiskySettings;
					}
				}

				set
				{
					_y_system_inix.Add(value.ToYencon());
				}
			}
			public const string KeyOfRiskySettings = "risky_settings";
			public readonly static DangerSettings DefaultRiskySettings = DangerSettings.GetDefault();

			public readonly struct DangerSettings
			{
				public bool AllowDangerSettings { get; }
				public const string KeyOfAllowDangerSettings = "allow";

				public bool ShowDeleteMenu { get; }
				public const string KeyOfShowDeleteMenu = "show_delmenu";

				public DangerSettings(bool allowDangerSettings, bool showDeleteMenu)
				{
					this.AllowDangerSettings = allowDangerSettings;
					this.ShowDeleteMenu      = showDeleteMenu;
				}

				public static DangerSettings GetDefault()
				{
					return new DangerSettings(
						allowDangerSettings: false,
						showDeleteMenu:      false
					);
				}

				public YSection ToYencon()
				{
					var result = new YSection() { Name = KeyOfRiskySettings };
					result.SetNodeAsBoolean(KeyOfAllowDangerSettings, this.AllowDangerSettings);
					result.SetNodeAsBoolean(KeyOfShowDeleteMenu,      this.ShowDeleteMenu);
					return result;
				}

				public static DangerSettings FromYencon(YSection section)
				{
					bool allow    = section.GetNodeAsBoolean(KeyOfAllowDangerSettings) ?? false;
					bool show_del = section.GetNodeAsBoolean(KeyOfShowDeleteMenu)      ?? false;
					return new DangerSettings(allow, show_del);
				}
			}
		}
	}
}
