using System.Windows.Forms;
using OSDeveloper.Native;

namespace OSDeveloper.App
{
	partial class Program { } // デザイナ避け
	partial class FormMain
	{
		bool CheckLibosdev()
		{
			int count = 0;
			_logger.Info("Started to load \'libosdev.dll\'");
retry:
			var status = Libosdev.CheckStatus(out var ex);
			if (status != LibState.Normal) {
				_logger.Warn("Cannot load \'libosdev.dll\'...");
				_logger.Info("Status = " + status.ToString());
				if (ex != null) {
					_logger.Exception(ex);
				}
				if (count < 10) { // ライブラリの読み込みに遅延があるかもしれないので、試行回数を増やす
					++count;
					goto retry;
				}
				_logger.Fatal("Tried ten times and failed all");
				MessageBox.Show(this,
					DialogMessages.Libosdev_CannotLoad,
					this.Text,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				Application.Exit();
				return false;
			}
			_logger.Info("Success to load \'libosdev.dll\'");
			if (count != 0) {
				_logger.Notice($"This process had {count} times try.");
			}
			return true;
		}
	}
}
