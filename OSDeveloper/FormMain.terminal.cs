using System;
using System.Windows.Forms;
using OSDeveloper.GUIs.Terminal;

namespace OSDeveloper
{
	partial class FormMain
	{
		/// <exception cref="System.ArgumentNullException" />
		public void OpenTab(TabPage tabPage)
		{
			tabPage = tabPage ?? throw new ArgumentNullException(nameof(tabPage));
			if (!_terminal.TabPages.Contains(tabPage)) {
				_terminal.TabPages.Add(tabPage);
			}
			_terminal.SelectedTab = tabPage;
			tabPage.Focus();
		}

		#region ログ出力
		private LogOutput _output;
		public  LogOutput LogOutput
		{
			get
			{
				if (_output == null || _output.IsDisposed) {
					_output = new LogOutput();
				}
				return _output;
			}
		}

		#endregion

		#region 読み込み済みのファイル/フォルダ
		private LoadedItemList _itemlist;
		public  LoadedItemList LoadedItemList
		{
			get
			{
				if (_itemlist == null || _itemlist.IsDisposed) {
					_itemlist = new LoadedItemList();
				}
				return _itemlist;
			}
		}

		#endregion
	}
}
