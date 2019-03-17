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

			_terminal.TabPages.Add("Empty 1");
			_terminal.TabPages.Add("Empty 2");
			_terminal.TabPages.Add("Empty 3");

			for (int i = 1; i <= 10; ++i) {
				Form f = new Form();
				f.Name = $"Form_{i}";
				f.Text = $"Window {i}";
				f.MdiParent = this;
				f.Show();
			}

			Button btn = new Button();
			btn.Text = "H:\\ﾄﾞﾗｲﾌﾞ";
			btn.Location = new System.Drawing.Point(200, 200);
			btn.Click += new EventHandler((a, b) => _explorer.Directory = new FolderMetadata(new PathString("H:\\")));
			this.Controls.Add(btn);

			Button btn2 = new Button();
			btn2.Text = "ﾌｧｲﾙｦﾋﾗｸ";
			btn2.Location = new System.Drawing.Point(200, 150);
			btn2.Click += new EventHandler((a, b) => {
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = FileTypeRegistry.CreateFullSPFs();
				ofd.ShowDialog();
			});
			this.Controls.Add(btn2);
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
