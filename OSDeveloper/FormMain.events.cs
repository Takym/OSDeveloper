using System;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.IO;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;

namespace OSDeveloper
{
	partial class FormMain
	{
		#region 継承されたイベント

		protected override void OnLoad(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnLoad)}...");
			base.OnLoad(e);

#if DEBUG
			// TODO: デバッグコード、後で削除
			Button btn = new Button();
			btn.Text = "ﾌｧｲﾙｦﾋﾗｸ";
			btn.Location = new System.Drawing.Point(200, 150);
			btn.Click += new EventHandler((a, b) => {
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = FileTypeRegistry.CreateFullSPFs();
				ofd.ShowDialog();
			});
			this.Controls.Add(btn);
#endif

			// このタイミングでディレクトリ設定
			_explorer.Directory = new FolderMetadata(SystemPaths.Workspace);

			_logger.Trace($"completed {nameof(OnLoad)}...");
		}

		protected override void OnShown(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnShown)}...");
			base.OnShown(e);

			this.StatusMessageLeft = FormMainRes.Status_Ready;

			_logger.Trace($"completed {nameof(OnShown)}...");
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnFormClosing)}...");
			base.OnFormClosing(e);

			this.WindowState = FormWindowState.Normal;
			SettingManager.System.MainWindowPosition = new Rectangle(this.Location, this.ClientSize);

			_logger.Trace($"completed {nameof(OnFormClosing)}...");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_logger.Trace($"executing {nameof(OnPaint)}...");
			base.OnPaint(e); // OnPaintのタイミングが知りたいからログだけ出力する。
			_logger.Trace($"completed {nameof(OnPaint)}...");
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnSizeChanged)}...");

			base.OnSizeChanged(e);
			this.SetStatusLabelLocation();

			_logger.Trace($"completed {nameof(OnSizeChanged)}...");
		}

		protected override void OnMdiChildActivate(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnMdiChildActivate)}...");

			base.OnMdiChildActivate(e);
			if (this.ActiveMdiChild == null) {
				this.StatusMessageLeft = FormMainRes.Status_Ready;
			} else {
				_logger.Debug($"the active MDI child form was changed to:\'{this.ActiveMdiChild.Name}/{this.ActiveMdiChild.Text}\'");
				this.StatusMessageLeft = string.Format(FormMainRes.Status_WindowChanged, this.ActiveMdiChild.Text);
			}

			_logger.Trace($"completed {nameof(OnMdiChildActivate)}...");
		}

		#endregion

		#region 割り込みハンドラ

		#endregion
	}
}
