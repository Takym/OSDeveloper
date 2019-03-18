using System;
using System.Windows.Forms;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.Controls
{
	public partial class TabPageInfo : UserControl
	{
		private Logger _logger;

		public TabPage TabPage { get; }

		public TabPageInfo(TabPage page)
		{
			_logger = Logger.Get(nameof(TabPageInfo));

			this.InitializeComponent();
			this.TabPage = page;
			btnDispose.Text = TerminalTexts.TabPageInfo_btnDispose;

			_logger.Trace($"constructed {nameof(TabPageInfo)}");
		}

		private void TabPageInfo_Load(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(TabPageInfo_Load)}...");

			lblTitle.Text = this.TabPage.Text;

			_logger.Trace($"completed {nameof(TabPageInfo_Load)}");
		}

		private void btnDispose_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnDispose_Click)}...");

			this.TabPage.Dispose();

			_logger.Trace($"completed {nameof(btnDispose_Click)}");
		}
	}
}
