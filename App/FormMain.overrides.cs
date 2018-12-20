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
			if (!this.CheckLibosdev()) {
				return;
			}

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

		public override void OpenTerminalTab(TabPage tab)
		{
			_logger.Trace($"executing {nameof(OpenTerminalTab)}...");
			if (!_terminal_container.TabPages.Contains(tab)) {
				_terminal_container.TabPages.Add(tab);
			}
			_terminal_container.SelectedTab = tab;
			tab.Focus();
			_logger.Trace($"completed {nameof(OpenTerminalTab)}");
		}
	}
}
