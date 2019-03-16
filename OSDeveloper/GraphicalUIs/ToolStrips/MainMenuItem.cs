using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using OSDeveloper.IO.Logging;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	[DesignerCategory("")] // デザイナ避け
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public partial class MainMenuItem : ToolStripMenuItem
	{
		protected readonly Logger   _logger;
		protected readonly FormMain _mwnd;

		// 外部アセンブリからの継承を阻止
		internal MainMenuItem(FormMain mwnd)
		{
			_logger = Logger.Get(this.GetType().Name);
			_mwnd   = mwnd;
			this.ShowShortcutKeys = true;
		}

		protected override void OnDropDownShow(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDropDownShow)}...");

			base.OnDropDownShow(e);

			_logger.Trace($"completed {nameof(OnDropDownShow)}");
		}

		protected override void OnDropDownHide(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnDropDownHide)}...");

			base.OnDropDownHide(e);

			_logger.Trace($"completed {nameof(OnDropDownHide)}");
		}
	}
}
