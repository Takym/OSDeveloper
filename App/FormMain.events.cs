using System.Threading;
using System.Windows.Forms;

namespace OSDeveloper.App
{
	partial class Program { } // デザイナ避け
	public partial class FormMain
	{
		private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			_logger.Exception(e.Exception, true);
			var dr = MessageBox.Show(this,
				string.Format(DialogMessages.ThreadException, e.Exception.Message),
				this.Text,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Error);
			if (dr == DialogResult.No) {
				MessageBox.Show(this,
					string.Format(DialogMessages.ThreadException_LogFile, _logger.LogFile.FileName),
					this.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				Application.Exit();
			}
		}
	}
}
