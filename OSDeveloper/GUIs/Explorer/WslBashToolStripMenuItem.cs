using System;
using System.Diagnostics;
using System.Windows.Forms;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;
using TakymLib;
using TakymLib.IO;

namespace OSDeveloper.GUIs.Explorer
{
	public sealed partial class WslBashToolStripMenuItem : ToolStripMenuItem
	{
		private readonly Logger _logger;

		public override string Text
		{
			get
			{
				if (this.TextModified) {
					return base.Text;
				} else {
					if (WinFormUtils.DesignMode) {
						return "bash";
					} else {
						return SettingManager.System.UseWSLCommand
							? ExplorerTexts.PopupMenu_OpenIn_BashWsl
							: ExplorerTexts.PopupMenu_OpenIn_Bash;
					}
				}
			}

			set
			{
				base.Text = value;
			}
		}

		public bool TextModified { get; set; }

		public WslBashToolStripMenuItem()
		{
			_logger = Logger.Get(nameof(WslBashToolStripMenuItem));
			this.InitializeComponent();
			this.Image = Shell32.GetSmallImageAt(2, false);
			_logger.Trace($"constructed {nameof(WslBashToolStripMenuItem)}");
		}

		protected override void OnClick(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnClick)}...");
			base.OnClick(e);

			PathString wslpath;
			if (SettingManager.System.UseWSLCommand) {
				wslpath = ((PathString)($"{FileTreeBox._sys32}wsl.exe"));
			} else {
				wslpath = ((PathString)($"{FileTreeBox._sys32}bash.exe"));
			}

			if (wslpath.Exists()) {
				Process.Start(wslpath, "--login -i");
			} else {
				MessageBox.Show(ExplorerTexts.Msgbox_WslNonInstalled, ASMINFO.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			_logger.Trace($"completed {nameof(OnClick)}");
		}
	}
}
