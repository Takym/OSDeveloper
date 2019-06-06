using System;
using System.Windows.Forms;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.GUIs.Controls
{
	public sealed partial class FormSettings : Form
	{
		private readonly Logger _logger;

		public FormSettings()
		{
			_logger = Logger.Get(nameof(FormSettings));
			this.InitializeComponent();
			this.SuspendLayout();
			this.Icon = Libosdev.GetIcon(Libosdev.Icons.FormSettings, out uint v0);
			this.ResumeLayout(false);
			this.PerformLayout();
			_logger.Trace($"constructed {nameof(FormSettings)}");
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(FormSettings_Load)}...");

			_logger.Trace($"completed {nameof(FormSettings_Load)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}
	}
}
