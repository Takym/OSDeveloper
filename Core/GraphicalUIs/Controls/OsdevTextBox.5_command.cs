using System.Collections.Generic;
using System.Text;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox
	{
		private bool _is_vim_mode;

		/// <summary>
		///  コマンドをこのテキストボックスに対して送信します。
		/// </summary>
		/// <param name="cmd">送信するコマンド文字列です。</param>
		protected internal virtual void SendCommand(string cmd)
		{
			_logger.Trace($"executing {nameof(SendCommand)}...");
			_logger.Info($"Received the command: {cmd}");

			// コマンド分割
			List<string> args = new List<string>();
			StringBuilder tmp = new StringBuilder();
			for (int i = 0; i < cmd.Length; ++i) {
				if (char.IsWhiteSpace(cmd[i])) {
					string s = tmp.ToString().Trim();
					if (!string.IsNullOrEmpty(s)) {
						args.Add(tmp.ToString().Unescape());
					}
				} else {
					tmp.Append(cmd[i]);
				}
			}

			// 空だったら
			if (args.Count == 0) {
				this.CommandTab.WriteLine();
			}

			// コマンドモード変更
			if (args[0] == "vim") {
				if (args.Count >= 2) {
					if (args[1] == "on") {
						_is_vim_mode = true;
					} else if (args[1] == "off") {
						_is_vim_mode = false;
					}
				} else {
					_is_vim_mode = !_is_vim_mode;
				}
				this.CommandTab.WriteLine($"VIM MODE = {_is_vim_mode}");
			}

			// VIMモードならコマンド変換
			if (_is_vim_mode) {
				args = this.ConvertFromVim(args);
				StringBuilder sb = new StringBuilder();
				foreach (var item in args) {
					sb.Append("\"");
					sb.Append(item);
					sb.Append("\", ");
				}
				_logger.Info($"Compiled from vim: {{ {sb.ToString()}}}");
			}

			// 実行
			this.CommandTab.WriteLine("Not supported yet.");

			_logger.Trace($"completed {nameof(SendCommand)}");
		}

		/// <summary>
		///  <see langword="VIM"/>コマンドから<see cref="OSDeveloper.Core.GraphicalUIs.Controls.OsdevTextBox"/>専用コマンドに変換します。
		/// </summary>
		/// <param name="cmd">変換前のトークンに分割された<see langword="VIM"/>コマンドです。</param>
		/// <returns>変換結果の分割済みのコマンドトークンです。</returns>
		protected virtual List<string> ConvertFromVim(List<string> cmd)
		{
			// TODO: いつかVIMコマンドから変換できる様にする。
			return cmd;
		}
	}
}
