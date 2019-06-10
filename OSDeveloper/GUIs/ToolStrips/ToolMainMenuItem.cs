using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.GUIs.Controls;
using OSDeveloper.IO;
using OSDeveloper.Native;
using OSDeveloper.Resources;
using TakymLib.IO;

namespace OSDeveloper.GUIs.ToolStrips
{
	public partial class ToolMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _desktop_themepacks;
		private readonly ToolStripMenuItem _show_settings;

		internal ToolStripMenuItem ShowSettings => _show_settings;

		public ToolMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "TOOL";
			this.Text = MenuTexts.Tool;

			_desktop_themepacks = new ToolStripMenuItem();
			_show_settings      = new ToolStripMenuItem();

			_desktop_themepacks.Name = nameof(_desktop_themepacks);
			_desktop_themepacks.Text = MenuTexts.Tool_DesktopThemepacks;
			this.BuildDesktopThemepacksList();

			_show_settings.Name          = nameof(_show_settings);
			_show_settings.Text          = MenuTexts.Tool_ShowSettings;
			_show_settings.Click        += this._show_settings_Click;
			_show_settings.ShortcutKeys  = Keys.Alt | Keys.O;
			_show_settings.Image         = Libosdev.GetIcon(Libosdev.Icons.FormSettings, out uint v0).ToBitmap();

			this.DropDownItems.Add(_desktop_themepacks);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_show_settings);

			_logger.Trace($"constructed {nameof(ToolMainMenuItem)}");
		}

		private void BuildDesktopThemepacksList()
		{
			var dir   = SystemPaths.Resources.Bond("DesktopThemepacks");
			var files = Directory.EnumerateFiles(dir, "*.themepack");
			foreach (var pack in files) {
				string name = ((PathString)(pack)).GetFileNameWithoutExtension();
				var tsmi = new ToolStripMenuItem();
				tsmi.Name = $"_desktop_themepacks_{name}";
				tsmi.Text = string.Format(MenuTexts.Tool_DesktopThemepacks_Text, name);
				tsmi.Click += (sender, e) => {
					_logger.Trace($"executing {tsmi.Name}...");
					var dr = MessageBox.Show(_mwnd,
						string.Format(MenuTexts.Tool_DesktopThemepacks_Confirm, name),
						string.Format(MenuTexts.Tool_DesktopThemepacks_Text,    name),
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question);
					if (dr == DialogResult.Yes) {
						Process.Start(pack);
					}
					_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
					_logger.Trace($"completed {tsmi.Name}");
				};
				_desktop_themepacks.DropDownItems.Add(tsmi);
			}
		}

		private void _show_settings_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_show_settings_Click)}...");

			var settings = new FormSettings();
			settings.ShowDialog(_mwnd);

			_logger.Trace($"completed {nameof(_show_settings_Click)}");
		}
	}
}
