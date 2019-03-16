using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OSDeveloper.IO;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class ToolMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _desktop_themepacks;

		public ToolMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "TOOL";
			this.Text = MenuTexts.Tool;

			_desktop_themepacks = new ToolStripMenuItem();

			_desktop_themepacks.Name = nameof(_desktop_themepacks);
			_desktop_themepacks.Text = MenuTexts.Tool_DesktopThemepacks;
			this.BuildDesktopThemepacksList();

			this.DropDownItems.Add(_desktop_themepacks);

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
					_logger.Trace($"completed {tsmi.Name}");
				};
				_desktop_themepacks.DropDownItems.Add(tsmi);
			}
		}
	}
}
