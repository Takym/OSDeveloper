﻿using System;
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
			cmd += " ";
			for (int i = 0; i < cmd.Length; ++i) {
				if (char.IsWhiteSpace(cmd[i])) {
					string s = tmp.ToString().Trim();
					tmp.Clear();
					if (!string.IsNullOrEmpty(s)) {
						args.Add(s.Unescape());
					}
				} else {
					tmp.Append(cmd[i]);
				}
			}

			// 空だったら
			if (args.Count == 0) {
				goto end;
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
			}

			// 入力されたコマンドの内部形式出力
			StringBuilder sb = new StringBuilder();
			foreach (var item in args) {
				sb.Append("\"");
				sb.Append(item);
				sb.Append("\", ");
			}
			_logger.Info($"the command token: {{ {sb.ToString()}}}");

			// 実行
			this.RunDebugCommand(args);
			this.CommandTab.WriteLine();
end:
			_logger.Trace($"completed {nameof(SendCommand)}");
		}

		private void RunDebugCommand(List<string> cmd)
		{
			if (cmd.Count == 0) return;
			switch (cmd[0]) {
				case "sel":
					this.CommandTab.WriteLine("This command is now developing.");
					this.CommandTab.WriteLine("usage> sel <int: row start> <int: column start> <int: row end> <int: column end>");
					this.CommandTab.WriteLine("usage> sel <int: selection start> <int: selection end>");
					this.CommandTab.WriteLine("sel: error: out of range");
					this.CommandTab.WriteLine("sel: error: specified numbers are invalid");
					this.CommandTab.WriteLine("sel: error: number of parameters");
					break;
				default:
					this.CommandTab.WriteLine($"The command not found: {cmd[0]}");
					break;
			}
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
