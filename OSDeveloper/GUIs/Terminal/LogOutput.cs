using System;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Terminal
{
	// TODO: ログをリアルタイムで表示できる様にする

	public partial class LogOutput : TabPage
	{
		private readonly Logger _logger;

		public LogOutput()
		{
			_logger = Logger.Get(nameof(LogOutput));

			this.InitializeComponent();
			this.SuspendLayout();

			// コントローラパネル初期化
			controller.Controls.Add(lblCount);
			controller.Controls.Add(nudCount);
			controller.Controls.Add(lblLevel);
			controller.Controls.Add(cmbLevel);
			controller.Controls.Add(lblLogger);
			controller.Controls.Add(cmbLogger);
			controller.Controls.Add(btnRefresh);

			// 表示文字列設定
			col_CreatedDate .Text = LogOutputTexts.col_CreatedDate;
			col_Level       .Text = LogOutputTexts.col_Level;
			col_Logger      .Text = LogOutputTexts.col_Logger;
			col_Message     .Text = LogOutputTexts.col_Message;
			lblCount        .Text = LogOutputTexts.lblCount;
			lblLevel        .Text = LogOutputTexts.lblLevel;
			lblLogger       .Text = LogOutputTexts.lblLogger;
			btnRefresh      .Text = LogOutputTexts.btnRefresh;

			// コントローラの位置設定
			lblCount    .Location = new Point(                                   4, 4);
			nudCount    .Location = new Point(                 lblCount .Width + 4, 0);
			lblLevel    .Location = new Point(nudCount .Left + nudCount .Width + 4, 4);
			cmbLevel    .Location = new Point(lblLevel .Left + lblLevel .Width + 4, 0);
			lblLogger   .Location = new Point(cmbLevel .Left + cmbLevel .Width + 4, 4);
			cmbLogger   .Location = new Point(lblLogger.Left + lblLogger.Width + 4, 0);
			btnRefresh  .Location = new Point(cmbLogger.Left + cmbLogger.Width + 4, 0);

			// 限定値設定
			nudCount.Value     = 75;
			nudCount.Increment = 10;
			nudCount.Minimum   =  0;
			nudCount.Maximum   = int.MaxValue;
			cmbLevel.Items.Add("All");
			cmbLevel.Items.Add(LogLevel.Notice);
			cmbLevel.Items.Add(LogLevel.Trace);
			cmbLevel.Items.Add(LogLevel.Debug);
			cmbLevel.Items.Add(LogLevel.Info);
			cmbLevel.Items.Add(LogLevel.Warn);
			cmbLevel.Items.Add(LogLevel.Error);
			cmbLevel.Items.Add(LogLevel.Fatal);
			cmbLevel.SelectedIndex = 0;
			this.Text = LogOutputTexts.Caption;
			this.SetComboBoxOptions();

			// コントロール追加
			this.Controls.Add(listView);
			this.Controls.Add(controller);

			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(LogOutput)}");
		}

		private void refreshLogsList(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(refreshLogsList)} from {(sender as Control)?.Name}...");
			this.RefreshLogList();
			_logger.Trace($"completed {nameof(refreshLogsList)} from {(sender as Control)?.Name}");
		}

		private void SetComboBoxOptions()
		{
			string tmp = cmbLogger.Text;
			cmbLogger.Items.Clear();
			string[] loggers = Logger.GetLoggerNames();
			for (int i = 0; i < loggers.Length; ++i) {
				cmbLogger.Items.Add(loggers[i]);
			}
			cmbLogger.Text = tmp;
		}

		private void RefreshLogList()
		{
			listView.Items.Clear();
			this.SetComboBoxOptions();
			var logs = LogFile.GetLogs();
			int nud_val = ((int)(nudCount.Value));
			int start = nud_val == 0 ? 0 : Math.Max(logs.Length - nud_val, 0);
			for (int i = start; i < logs.Length; ++i) {
				this.AddLogToList(logs[i]);
			}
		}

		private void AddLogToList(LogData log)
		{
			if (Enum.TryParse(cmbLevel.SelectedItem?.ToString(), out LogLevel lglvl)) {
				if (log.Level != lglvl) return;
			}
			if (string.IsNullOrEmpty(cmbLogger.Text) ||
				log.Logger.LongName == cmbLogger.Text) {
				ListViewItem lvi = new ListViewItem();
				lvi.Text = log.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss.fff");
				lvi.SubItems.Add(log.Level.ToString());
				lvi.SubItems.Add(log.Logger.LongName);
				lvi.SubItems.Add(log.Message);
				listView.Items.Add(lvi);
			}
		}
	}
}
