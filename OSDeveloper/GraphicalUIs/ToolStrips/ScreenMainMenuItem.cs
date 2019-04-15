using System;
using System.Windows.Forms;
using OSDeveloper.Native;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class ScreenMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _entire, _active;

		public ScreenMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "SCREEN";
			this.Text = MenuTexts.Screen;

			_entire = new ToolStripMenuItem();
			_active = new ToolStripMenuItem();

			_mwnd.MdiChildActivate += this._mwnd_MdiChildActivate;

			_entire.Name          = nameof(_entire);
			_entire.Text          = MenuTexts.Screen_CaptureEntire;
			_entire.Click        += this._entire_Click;
			_entire.ShortcutKeys  = Keys.Control | Keys.F12;

			_active.Name          = nameof(_active);
			_active.Text          = MenuTexts.Screen_CaptureActive;
			_active.Click        += this._active_Click;
			_active.ShortcutKeys  = Keys.Control | Keys.Shift | Keys.F12;
			_active.Enabled       = _mwnd.ActiveMdiChild != null;

			this.DropDownItems.Add(_entire);
			this.DropDownItems.Add(_active);

			_logger.Trace($"constructed {nameof(ScreenMainMenuItem)}");
		}

		private void _mwnd_MdiChildActivate(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_mwnd_MdiChildActivate)}...");
			_active.Enabled = _mwnd.ActiveMdiChild != null;
			_logger.Trace($"completed {nameof(_mwnd_MdiChildActivate)}");
		}

		private void _entire_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_entire_Click)}...");

			var img = User32.CaptureControl(_mwnd);
			Clipboard.SetImage(img);
			_mwnd.StatusMessageLeft = FormMainRes.Status_CapturedEntire;

			_logger.Trace($"completed {nameof(_entire_Click)}");
		}

		private void _active_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_active_Click)}...");

			if (_mwnd.ActiveMdiChild == null) {
				_logger.Warn("there is no active MDI child window, cannot capture");
				_logger.Warn("this error should not happen");
				_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				var img = User32.CaptureControl(_mwnd.ActiveMdiChild);
				Clipboard.SetImage(img);
				_mwnd.StatusMessageLeft = string.Format(FormMainRes.Status_CapturedActive, _mwnd.ActiveMdiChild.Text);
			}

			_logger.Trace($"completed {nameof(_active_Click)}");
		}
	}
}
