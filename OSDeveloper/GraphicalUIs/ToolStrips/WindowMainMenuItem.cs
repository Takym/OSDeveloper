using System;
using System.Windows.Forms;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class WindowMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _l_arricons, _l_cascade, _l_hori, _l_vert;
		private readonly ToolStripMenuItem _switch_wnd, _close_active_wnd, _close_all_wnds;
		private readonly MainMenuItem _screen;

		public WindowMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "WINDOW";
			this.Text = MenuTexts.Window;

			_l_arricons       = new WindowLayoutToolStripMenuItem(_logger, _mwnd, MdiLayout.ArrangeIcons);
			_l_cascade        = new WindowLayoutToolStripMenuItem(_logger, _mwnd, MdiLayout.Cascade);
			_l_hori           = new WindowLayoutToolStripMenuItem(_logger, _mwnd, MdiLayout.TileHorizontal);
			_l_vert           = new WindowLayoutToolStripMenuItem(_logger, _mwnd, MdiLayout.TileVertical);
			_switch_wnd       = new ToolStripMenuItem();
			_close_active_wnd = new ToolStripMenuItem();
			_close_all_wnds   = new ToolStripMenuItem();
			_screen           = new ScreenMainMenuItem(mwnd);

			_l_arricons.Name         = nameof(_l_arricons);
			_l_arricons.Text         = MenuTexts.Window_Arricons;
			_l_arricons.ShortcutKeys = Keys.Alt | Keys.Left;

			_l_cascade.Name         = nameof(_l_cascade);
			_l_cascade.Text         = MenuTexts.Window_Cascade;
			_l_cascade.ShortcutKeys = Keys.Alt | Keys.Up;

			_l_hori.Name         = nameof(_l_hori);
			_l_hori.Text         = MenuTexts.Window_Hori;
			_l_hori.ShortcutKeys = Keys.Alt | Keys.Down;

			_l_vert.Name         = nameof(_l_vert);
			_l_vert.Text         = MenuTexts.Window_Vert;
			_l_vert.ShortcutKeys = Keys.Alt | Keys.Right;

			_switch_wnd.Name          = nameof(_switch_wnd);
			_switch_wnd.Text          = MenuTexts.Window_SwitchWnd;
			_switch_wnd.Click        += this._switch_wnd_Click;
			_switch_wnd.ShortcutKeys  = Keys.Control | Keys.Tab;

			_close_active_wnd.Name          = nameof(_close_active_wnd);
			_close_active_wnd.Text          = MenuTexts.Window_CloseActiveWnd;
			_close_active_wnd.Click        += this._close_active_wnd_Click;
			_close_active_wnd.ShortcutKeys  = Keys.Control | Keys.Delete;

			_close_all_wnds.Name          = nameof(_close_all_wnds);
			_close_all_wnds.Text          = MenuTexts.Window_CloseAllWnds;
			_close_all_wnds.Click        += this._close_all_wnds_Click;
			_close_all_wnds.ShortcutKeys  = Keys.Control | Keys.Shift | Keys.Delete;

			this.DropDownItems.Add(_l_arricons);
			this.DropDownItems.Add(_l_cascade);
			this.DropDownItems.Add(_l_hori);
			this.DropDownItems.Add(_l_vert);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_switch_wnd);
			this.DropDownItems.Add(_close_active_wnd);
			this.DropDownItems.Add(_close_all_wnds);
			this.DropDownItems.Add(new ToolStripSeparator());
			this.DropDownItems.Add(_screen);

			_logger.Trace($"constructed {nameof(WindowMainMenuItem)}");
		}

		private void _switch_wnd_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_switch_wnd_Click)}...");

			var children = _mwnd.MdiChildren;
			for (int i = 0; i < children.Length; ++i) {
				if (children[i] == _mwnd.ActiveMdiChild) {
					if (++i >= children.Length) i = 0;
					children[i].Activate();
					goto end;
				}
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

end:
			_logger.Trace($"completed {nameof(_switch_wnd_Click)}");
		}

		private void _close_active_wnd_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_close_active_wnd_Click)}...");

			_mwnd.ActiveMdiChild?.Close();

			_logger.Trace($"completed {nameof(_close_active_wnd_Click)}");
		}

		private void _close_all_wnds_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_close_all_wnds_Click)}...");

			// for だと動かなかったので foreach を使う
			foreach (var f in _mwnd.MdiChildren) {
				f.Close();
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(_close_all_wnds_Click)}");
		}

		private class WindowLayoutToolStripMenuItem : ToolStripMenuItem
		{
			private readonly Logger    _logger;
			private readonly FormMain  _target;
			private readonly MdiLayout _layout;

			public WindowLayoutToolStripMenuItem(Logger logger, FormMain target, MdiLayout layout)
			{
				_logger = logger;
				_target = target;
				_layout = layout;
				_logger.Trace($"{nameof(WindowLayoutToolStripMenuItem)} is constructed for");
				_logger.Info($"target main form: {_target.Name}/{_target.Text}, layout style: {_layout}.");
			}

			protected override void OnClick(EventArgs e)
			{
				_logger.Trace($"executing {nameof(OnClick)}...");

				base.OnClick(e);
				_target.LayoutMdi(_layout);
				_target.StatusMessageLeft = FormMainRes.Status_Ready;

				_logger.Trace($"completed {nameof(OnClick)}");
			}
		}
	}
}
