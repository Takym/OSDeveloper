using System.Collections.Generic;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Controls;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.Terminal
{
	public partial class AllTabPagesList : TabPage
	{
		private Logger             _logger;
		private ClosableTabControl _parent;
		private List<TabPage>      _tabs;

		public AllTabPagesList(ClosableTabControl parent, List<TabPage> tabs)
		{
			_logger = Logger.Get(nameof(AllTabPagesList));

			this.InitializeComponent();
			this.SuspendLayout();

			// コントロール設定
			this.Text = TerminalTexts.AllTabPagesList_Title;

			_parent = parent;
			_tabs   = tabs;
			this.RestoreTabsList();

			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(AllTabPagesList)}");
		}

		private void RestoreTabsList()
		{
			for (int i = 0; i < panel.Controls.Count; ++i) {
				panel.Controls[i].Dispose();
			}
			for (int i = 0; i < _tabs.Count; ++i) {
				panel.Controls.Add(new TabPageInfo(_tabs[i]));
			}
		}
	}
}
