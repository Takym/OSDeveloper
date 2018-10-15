using System;
using System.ComponentModel;
using System.Windows.Forms;
using OSDeveloper.Core.GraphicalUIs;

namespace OSDeveloper.App
{
	partial class Program { } // デザイナ避け
	public partial class FormMain
	{
		protected override void OnLoad(EventArgs e)
		{
			_logger.Trace("The OnLoad event of FormMain was called");
			base.OnLoad(e);

			// libosdev.dll の動作確認
			if (!CheckLibosdev()) {
				return;
			}

#if false//DEBUG
			// TODO: 以下のコードは必要が無くなったら削除する。
			for (int i = 0; i < 10; ++i) {
				var f = new OSDeveloper.Core.Editors.EditorWindow(this);
				f.Show();
			}

			Button btn = new Button();
			btn.Text = "ﾌｧｲﾙｦﾋﾗｸ";
			btn.Location = new System.Drawing.Point(200, 250);
			btn.Click += new EventHandler((sender, e2) => {
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = Core.FileManagement.FileTypes.CreateFullSPFs();
				ofd.ShowDialog(this);
			});
			this.Controls.Add(btn);

			var rde = new OSDeveloper.Core.Editors.RegistryDraftEditor(this);
			rde.Show();
#endif

			this.SetStatusMessage(MainWindowStatusMessage.Ready());
			_logger.Trace("Finished OnLoad event of FormMain");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace("The OnPaint event of FormMain was called");
			base.OnPaint(e);

			_logger.Trace("Finished OnPaint event of FormMain");
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			_logger.Trace("The OnClosing event of FormMain was called");
			base.OnClosing(e);

			_logger.Trace("Finished OnClosing event of FormMain");
		}

		protected override void OnClosed(EventArgs e)
		{
			_logger.Trace("The OnClosed event of FormMain was called");
			base.OnClosed(e);

			_logger.Trace("Finished OnClosed event of FormMain");
		}

		public override void SetStatusMessage(string msg)
		{
			_logger.Trace("The SetStatusMessage method of FormMain was called");
			_status_label.Text = msg;
			_logger.Info(msg);
			_logger.Trace("Finished SetStatusMessage method of FormMain");
		}
	}
}
