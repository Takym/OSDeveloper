using System;
using System.Windows.Forms;
using OSDeveloper.GraphicalUIs.Terminal;

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
		public  LogOutput LogOutput { get => _output; }

		private void BuildLogOutput()
		{
			_output = new LogOutput();
		}

		#endregion
	}
}
