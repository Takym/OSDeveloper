using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Controls.SettingPanels.Configuration
{
	public partial class StartupSettings : UserControl
	{
		private readonly Logger       _logger;
		private readonly FormSettings _parent;

		public StartupSettings(FormSettings parent)
		{
			_logger = Logger.Get(nameof(StartupSettings));
			_parent = parent;
			this.InitializeComponent();
			this.SuspendLayout();
			labelDesc    .Text = FormSettingsRes.Startup_labelDesc;
			visualstyle  .Text = FormSettingsRes.Startup_visualstyle;
			labelLang    .Text = FormSettingsRes.Startup_labelLang;
			riskySettings.Text = FormSettingsRes.Startup_riskySettings;
			allowRisky   .Text = FormSettingsRes.Startup_allowRisky;
			showDelMenu  .Text = FormSettingsRes.Startup_showDelMenu;
			this         .Text = FormSettingsRes.Startup_Caption + "(system)";
			this.ResumeLayout(false);
			this.PerformLayout();
			_logger.Trace($"constructed {nameof(StartupSettings)}");
		}

		private void StartupSettings_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(StartupSettings_Load)}...");

			switch (SettingManager.System.VisualStyle) {
				case VisualStyleState.ClientAndNonClientAreasEnabled:
					visualstyle.CheckState = CheckState.Checked;
					break;
				case VisualStyleState.NoneEnabled:
					visualstyle.CheckState = CheckState.Unchecked;
					break;
				default:
					visualstyle.CheckState = CheckState.Indeterminate;
					break;
			}

#if DEBUG
			var clist = Globalization.GetSupportedCultures();
#else
			var clist = Globalization.GetInstalledCultures();
#endif
			for (int i = 0; i < clist.Length; ++i) {
				var l = new Locale(clist[i]);
				cmbxLang.Items.Add(l);
				if (clist[i].Name == SettingManager.System.Language.Name) {
					cmbxLang.SelectedItem = l;
				}
			}
			if (cmbxLang.SelectedItem == null) {
				var l = new Locale(SettingManager.System.Language);
				cmbxLang.Items.Add(l);
				cmbxLang.SelectedItem = l;
			}

			allowRisky.Checked  = SettingManager.System.RiskySettings.AllowDangerSettings;
			showDelMenu.Enabled = SettingManager.System.RiskySettings.AllowDangerSettings;
			showDelMenu.Checked = SettingManager.System.RiskySettings.ShowDeleteMenu;

			_logger.Trace($"completed {nameof(StartupSettings_Load)}");
		}

		private void visualstyle_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(visualstyle_CheckedChanged)}...");

			if (visualstyle.Checked) {
				SettingManager.System.VisualStyle = VisualStyleState.ClientAndNonClientAreasEnabled;
			} else {
				SettingManager.System.VisualStyle = VisualStyleState.NoneEnabled;
			}

			_logger.Trace($"completed {nameof(visualstyle_CheckedChanged)}");
		}

		private void cmbxLang_SelectedIndexChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(cmbxLang_SelectedIndexChanged)}...");

			if (cmbxLang.SelectedItem is Locale l) {
				SettingManager.System.Language = l.Culture;
			}

			_logger.Trace($"completed {nameof(cmbxLang_SelectedIndexChanged)}");
		}

		private void allowRisky_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(allowRisky_CheckedChanged)}...");

			var rs = SettingManager.System.RiskySettings;
			SettingManager.System.RiskySettings = new SettingManager.System.DangerSettings(
				allowDangerSettings: allowRisky.Checked,
				showDeleteMenu:      rs.ShowDeleteMenu
			);
			showDelMenu.Enabled = allowRisky.Checked;
			showDelMenu.Checked = SettingManager.System.RiskySettings.ShowDeleteMenu;

			_logger.Trace($"completed {nameof(allowRisky_CheckedChanged)}");
		}

		private void showDelMenu_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(showDelMenu_CheckedChanged)}...");

			var rs = SettingManager.System.RiskySettings;
			SettingManager.System.RiskySettings = new SettingManager.System.DangerSettings(
				allowDangerSettings: rs.AllowDangerSettings,
				showDeleteMenu:      showDelMenu.Checked
			);

			_logger.Trace($"completed {nameof(showDelMenu_CheckedChanged)}");
		}

		private class Locale
		{
			public CultureInfo Culture { get; }

			public Locale(CultureInfo culture)
			{
				this.Culture = culture;
			}

			public override string ToString()
			{
				return $"{this.Culture.NativeName}/{this.Culture.DisplayName}" +
					$"/{this.Culture.EnglishName}/{this.Culture.Name}({this.Culture.LCID})";
			}
		}
	}
}
