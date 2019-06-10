using System;
using System.Windows.Forms;
using OSDeveloper.GUIs.Controls.SettingPanels.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Controls
{
	public sealed partial class FormSettings : Form
	{
		private readonly Logger      _logger;
		private          UserControl _current;

		public FormSettings()
		{
			_logger = Logger.Get(nameof(FormSettings));
			this.InitializeComponent();
			this.SuspendLayout();
			this.Icon = Libosdev.GetIcon(Libosdev.Icons.FormSettings, out uint v0);
			this.Text = FormSettingsRes.Caption;
			this.ResumeLayout(false);
			this.PerformLayout();
			_logger.Trace($"constructed {nameof(FormSettings)}");
		}

		private void FormSettings_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(FormSettings_Load)}...");

			var node_cfg = treeView.Nodes.Add(FormSettingsRes.Config);
			node_cfg.Nodes.Add(new PanelTreeNode(new StartupSettings(this)));

			var node_ext = treeView.Nodes.Add(FormSettingsRes.Extension);

			_logger.Trace($"completed {nameof(FormSettings_Load)}");
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterSelect)}...");

			if (_current != null) {
				panel.Controls.Remove(_current);
			}
			if (e.Node is PanelTreeNode ptn) {
				_current = ptn.UserControl;
				panel.Controls.Add(_current);
			}

			_logger.Trace($"completed {nameof(treeView_AfterSelect)}");
		}

		private sealed class PanelTreeNode : TreeNode
		{
			public UserControl UserControl { get; }

			public PanelTreeNode(UserControl userControl) : base(userControl.Text)
			{
				this.UserControl = userControl;
			}
		}
	}
}
