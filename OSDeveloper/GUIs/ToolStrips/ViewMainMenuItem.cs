using System;
using System.Windows.Forms;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.ToolStrips
{
	public partial class ViewMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _log_output, _loaded_itemlist;

		public ViewMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "VIEW";
			this.Text = MenuTexts.View;

			_log_output      = new ToolStripMenuItem();
			_loaded_itemlist = new ToolStripMenuItem();

			_log_output.Name          = nameof(_log_output);
			_log_output.Text          = MenuTexts.View_LogOutput;
			_log_output.Click        += this._log_output_Click;
			_log_output.ShortcutKeys  = Keys.Alt | Keys.L;

			_loaded_itemlist.Name          = nameof(_loaded_itemlist);
			_loaded_itemlist.Text          = MenuTexts.View_LoadedItemList;
			_loaded_itemlist.Click        += this._loaded_itemlist_Click;
			_loaded_itemlist.ShortcutKeys  = Keys.Alt | Keys.F;

			this.DropDownItems.Add(_log_output);
			this.DropDownItems.Add(_loaded_itemlist);

			_logger.Trace($"constructed {nameof(ViewMainMenuItem)}");
		}

		private void _log_output_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_log_output_Click)}...");

			if (_mwnd.LogOutput == null) {
				MessageBox.Show(_mwnd,
					MenuTexts.View_LogOutput_CannotShow,
					_mwnd.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			} else {
				_mwnd.OpenTab(_mwnd.LogOutput);
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(_log_output_Click)}");
		}

		private void _loaded_itemlist_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_loaded_itemlist_Click)}...");

			if (_mwnd.LoadedItemList == null) {
				MessageBox.Show(_mwnd,
					MenuTexts.View_LoadedItemList_CannotShow,
					_mwnd.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			} else {
				_mwnd.OpenTab(_mwnd.LoadedItemList);
			}
			_mwnd.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(_loaded_itemlist_Click)}");
		}
	}
}
