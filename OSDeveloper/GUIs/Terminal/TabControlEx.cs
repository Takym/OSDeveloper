using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.GUIs.Controls;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Terminal
{
	public partial class TabControlEx : ClosableTabControl
	{
		private readonly Logger _logger;

		public TabControlEx() : base()
		{
			_logger = Logger.Get(nameof(TabControlEx));

			this.InitializeComponent();
			this.SizeMode = SettingManager.System.TerminalTabSizeMode;

			_logger.Trace($"constructed {nameof(TabControlEx)}");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			base.OnPaint(e);

			if (this.TabPages.Count == 0) {
				if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled ||
					Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled) {
					var rect = new Rectangle(e.ClipRectangle.Location, e.ClipRectangle.Size);
					rect.Inflate(-2, -2);
					TabRenderer.DrawTabPage(e.Graphics, rect);
				}
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  10,  8);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  11,  8);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  12,  8);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  10,  9);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  12,  9);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  10, 10);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  11, 10);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Cyan,  12, 10);
				e.Graphics.DrawString(TerminalTexts.TabControlEx_NoPage, this.Font, Brushes.Black, 11,  9);
			}

			_logger.Trace($"completed {nameof(OnPaint)}");
		}
	}
}
