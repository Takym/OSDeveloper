using System;
using System.Windows.Forms;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Controls.SettingPanels.Configuration
{
	public partial class EnvironmentSettings : UserControl
	{
		private readonly Logger       _logger;
		private readonly FormSettings _parent;

		public EnvironmentSettings(FormSettings parent)
		{
			_logger = Logger.Get(nameof(EnvironmentSettings));
			_parent = parent;
			this.InitializeComponent();
			this.SuspendLayout();
			labelDesc  .Text = FormSettingsRes.Environment_labelDesc;
			useExdialog.Text = FormSettingsRes.Environment_useExdialog;
			useWsl     .Text = FormSettingsRes.Environment_useWsl;
			this       .Text = FormSettingsRes.Environment_Caption;
			this.ResumeLayout(false);
			this.PerformLayout();
			_logger.Trace($"constructed {nameof(EnvironmentSettings)}");
		}

		private void EnvironmentSettings_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(EnvironmentSettings_Load)}...");

			useExdialog.Checked = SettingManager.System.UseEXDialog;
			useWsl     .Checked = SettingManager.System.UseWSLCommand;

			_logger.Trace($"completed {nameof(EnvironmentSettings_Load)}");
		}

		private void useExdialog_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(useExdialog_CheckedChanged)}...");

			SettingManager.System.UseEXDialog = useExdialog.Checked;

			_logger.Trace($"completed {nameof(useExdialog_CheckedChanged)}");
		}

		private void useWsl_CheckedChanged(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(useWsl_CheckedChanged)}...");

			SettingManager.System.UseWSLCommand = useWsl.Checked;

			_logger.Trace($"completed {nameof(useWsl_CheckedChanged)}");
		}
	}
}
