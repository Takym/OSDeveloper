using System;
using System.Windows.Forms;
using OSDeveloper.Resources;

namespace OSDeveloper.GraphicalUIs.ToolStrips
{
	public partial class FileMainMenuItem : MainMenuItem
	{
		private readonly ToolStripMenuItem _exit;

		public FileMainMenuItem(FormMain mwnd) : base(mwnd)
		{
			this.Name = "FILE";
			this.Text = MenuTexts.File;

			_exit = new ToolStripMenuItem();

			_exit.Name          = nameof(_exit);
			_exit.Text          = MenuTexts.File_Exit;
			_exit.Click        += this._exit_Click;
			_exit.ShortcutKeys  = Keys.Alt | Keys.F4;

			this.DropDownItems.Add(_exit);

			_logger.Trace($"constructed {nameof(FileMainMenuItem)}");
		}

		private void _exit_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(_exit_Click)}...");
			Application.Exit();
			_logger.Trace($"completed {nameof(_exit_Click)}");
		}
	}
}
