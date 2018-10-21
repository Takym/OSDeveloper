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
			_logger.Trace($"executing {nameof(OnLoad)}...");
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
			_logger.Trace($"completed {nameof(OnLoad)}");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			base.OnPaint(e);

			_logger.Trace($"completed {nameof(OnPaint)}");
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnClosing)}...");
			base.OnClosing(e);

			_logger.Trace($"completed {nameof(OnClosing)}");
		}

		protected override void OnClosed(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnClosed)}...");
			base.OnClosed(e);

			_logger.Trace($"completed {nameof(OnClosed)}");
		}

		public override void SetStatusMessage(string msg)
		{
			_logger.Trace($"executing {nameof(SetStatusMessage)}...");
			_status_label.Text = msg;
			_logger.Info(msg);
			_logger.Trace($"completed {nameof(SetStatusMessage)}");
		}
	}
}
