using System;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	/// <summary>
	///  <see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>の
	///  コマンドモードで利用するタブページです。
	/// </summary>
	public partial class OsdevTextBoxTab : TabPage
	{
		private Logger _logger;
		private OsdevTextBox _target;

		/// <summary>
		///  このタブページと関連付けられているテキストボックスを取得します。
		/// </summary>
		public OsdevTextBox OwnerTextBox
		{
			get
			{
				return _target;
			}
		}

		/// <summary>
		///  このコントロールのキャプションを取得または設定します。
		///  この値はこのタブページと関連付けられているテキストボックスの親コントロールと同じ名前になります。
		/// </summary>
		public override string Text
		{
			get
			{
				return _target?.Parent?.Text;
			}

			set
			{
				base.Text = value;
				if (_target?.Parent != null) {
					_target.Parent.Text = value;
				}
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBoxTab"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="target">操作対象のテキストボックスです。</param>
		public OsdevTextBoxTab(OsdevTextBox target)
		{
			_logger = Logger.GetSystemLogger(nameof(OsdevTextBoxTab));

			this.InitializeComponent();
			this.Controls.Add(output);
			this.Controls.Add(input);

			_target = target;
			_logger.Info("target textbox = " + _target.Name);

			if (!this.IsDesignMode()) {
				output.Font = input.Font = FontResources.CreateGothic();
			}

			_logger.Trace(nameof(OsdevTextBoxTab) + " is constructed");
		}

		/// <summary>
		///  <see cref="System.Windows.Forms.Control.GotFocus"/>を発生させます。
		/// </summary>
		/// <param name="e">
		///  イベントデータを格納している<see cref="System.EventArgs"/>オブジェクトです。
		/// </param>
		protected override void OnGotFocus(EventArgs e)
		{
			_logger.Trace($"executing {nameof(OnGotFocus)}...");
			base.OnGotFocus(e);
			input.Focus();
			_logger.Trace($"completed {nameof(OnGotFocus)}");
		}

		private void input_KeyUp(object sender, KeyEventArgs e)
		{
			_logger.Trace($"executing {nameof(input_KeyUp)}...");

			if (e.KeyData == Keys.Enter) {
				e.Handled = true;
				string cmd = input.Text.Trim();
				input.Text = string.Empty;
				if (!string.IsNullOrEmpty(cmd)) {
					this.Write($"\r\nUser> {cmd}\r\n");
					_target.SendCommand(cmd);
					_target.Focus();
				}
			}

			_logger.Trace($"completed {nameof(input_KeyUp)}");
		}

		/// <summary>
		///  出力先のテキストボックスに指定された文字列を書き込みます。
		/// </summary>
		/// <param name="str">書き込む文字列です。</param>
		public void Write(string str)
		{
			output.Text += str;
			output.SelectionStart = output.Text.Length;
			output.ScrollToCaret();
			_logger.Notice(str);
		}

		/// <summary>
		///  出力先のテキストボックスに改行を書き込みます。
		/// </summary>
		public void WriteLine()
		{
			output.Text += "\r\n";
			output.SelectionStart = output.Text.Length;
			output.ScrollToCaret();
		}

		/// <summary>
		///  出力先のテキストボックスに指定された文字列を改行付きで書き込みます。
		/// </summary>
		/// <param name="str">書き込む文字列です。</param>
		public void WriteLine(string str)
		{
			output.Text += $"{str}\r\n";
			output.SelectionStart = output.Text.Length;
			output.ScrollToCaret();
			_logger.Notice(str);
		}
	}
}
