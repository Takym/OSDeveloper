using System;
using System.Drawing;
using System.Windows.Forms;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Resources;

namespace OSDeveloper.GUIs.Terminal
{
	public partial class LoadedItemList : TabPage
	{
		private readonly Logger _logger;

		public LoadedItemList()
		{
			_logger = Logger.Get(nameof(LoadedItemList));

			this.InitializeComponent();
			this.SuspendLayout();

			// コントローラパネル初期化
			controller.Controls.Add(btnRefresh);

			// 表示文字列設定
			btnRefresh.Text = TerminalTexts.btnRefresh;
			col_Name.Text   = TerminalTexts.LoadedItemList_col_Name;
			col_Type.Text   = TerminalTexts.LoadedItemList_col_Type;

			// コントローラの位置設定
			btnRefresh.Location = new Point(4, 0);

			// 限定値設定
			this.Text = TerminalTexts.LoadedItemList_Caption;

			// コントロール追加
			this.Controls.Add(listView);
			this.Controls.Add(controller);

			this.ResumeLayout(false);
			this.PerformLayout();

			_logger.Trace($"constructed {nameof(LogOutput)}");
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRefresh_Click)}...");

			listView.Items.Clear();
			var dirs = ItemList.GetLoadedDirs();
			for (int i = 0; i < dirs.Length; ++i) {
				listView.Items.Add(dirs[i]).SubItems.Add(TerminalTexts.LoadedItemList_Directory);
			}
			var files = ItemList.GetLoadedFiles();
			for (int i = 0; i < files.Length; ++i) {
				listView.Items.Add(files[i]).SubItems.Add(TerminalTexts.LoadedItemList_File);
			}

			_logger.Trace($"completed {nameof(btnRefresh_Click)}");
		}
	}
}
