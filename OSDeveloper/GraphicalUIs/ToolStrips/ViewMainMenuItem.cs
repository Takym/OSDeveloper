using System;
using System.Windows.Forms;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class ViewMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _log_output;

		public ViewMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "VIEW";
			this.Text = MenuTexts.View;

			_log_output = new ToolStripMenuItem();

			_log_output.Name          = nameof(_log_output);
			_log_output.Text          = MenuTexts.View_LogOutput;
			_log_output.Click        += this._log_output_Click;
			_log_output.ShortcutKeys  = Keys.Alt | Keys.L;

			this.DropDownItems.Add(_log_output);

			_logger.Trace($"constructed {nameof(ViewMainMenuItem)}");
		}

		private void _log_output_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_log_output_Click)}...");

			// == null だと、正しく動作しない可能性がある
			if (_mwnd.LogOutput is null) {
				MessageBox.Show(_mwnd,
					MenuTexts.View_LogOutput_CannotShow,
					_mwnd.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			} else {
				_mwnd.OpenTab(_mwnd.LogOutput);
			}

			_logger.Trace($"completed {nameof(_log_output_Click)}");
		}
	}
}
