﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OSDeveloper.Core.MiscUtils;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Settings.YenconTool
{
	static class Program
	{
		public static List<YenconNode> Root;
		public static YenconHeader Header;
		public static YenconSection Current;
		public static string CurrentPath;

		[STAThread()]
		static int Main(string[] args)
		{
			Console.WriteLine("OSDeveloper - Yencon Command Line Tool");
			Console.WriteLine("Copyright (C) 2018 Takym.\n");

			// 取り敢えず基本設定読み込み
			ConfigManager.ApplySystemSettings();

			// コマンドライン解析
			if (args.Length != 1) {
				Console.WriteLine(Messages.CommandLine_Invalid);
				Console.WriteLine("prompt> yencon.cmd <config-file>");
				return ErrorCodes.ERROR_INVALID_FUNCTION;
			}

			// ファイル読み込み
			string filename = Path.GetFullPath(args[0]);
			if (!File.Exists(filename)) {
				Console.WriteLine(Messages.FileNotFound, filename);
				return ErrorCodes.ERROR_FILE_NOT_FOUND;
			}

			// バイナリ形式
			Header = new YenconHeader();
			Root = YenconBinaryFile.Scan(filename, Header);
			if (Root == null) {
				// テキスト形式
				Header = null;
				using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (StreamReader sr = new StreamReader(fs, Encoding.Unicode)) {
					var yp = new YenconParser(sr.ReadToEnd());
					yp.Analyze();
					Root = new List<YenconNode>(yp.GetNodes());
				}
			}

			// プロンプト
			Console.WriteLine(Messages.Ready + "\n");
			var rootSection = new YenconSection(Root);
			Current = rootSection;
			while (true) {
				// 開いているファイルとセクションの位置を表示
				Console.WriteLine($"{Path.GetDirectoryName(filename)}");
				if (string.IsNullOrEmpty(CurrentPath)) {
					Console.Write($"${Path.GetFileName(filename)}> ");
				} else {
					Console.Write($"${Path.GetFileName(filename)}{CurrentPath}> ");
				}

				// コマンドを解析・実行する
				string cmd = Console.ReadLine().Trim();
				if (cmd == "end" || cmd == "exit" || cmd == "quit") {
					// 終了コマンド
					return ErrorCodes.ERROR_SUCCESS;
				} else if (cmd == "help") {
					// 説明書を表示
					Console.WriteLine(Messages.CmdHelp);
					Console.WriteLine("add              " + Messages.CmdHelp_Add);
					Console.WriteLine("clear, cls       " + Messages.CmdHelp_Clear);
					Console.WriteLine("del, rmv         " + Messages.CmdHelp_DelRmv);
					Console.WriteLine("end, exit, quit  " + Messages.CmdHelp_EndExitQuit);
					Console.WriteLine("goroot           " + Messages.CmdHelp_Goroot);
					Console.WriteLine("gset             " + Messages.CmdHelp_Gset);
					Console.WriteLine("help             " + Messages.CmdHelp_Help);
					Console.WriteLine("into             " + Messages.CmdHelp_Into);
					Console.WriteLine("list             " + Messages.CmdHelp_List);
					Console.WriteLine("list full        " + Messages.CmdHelp_ListFull);
					Console.WriteLine("mks              " + Messages.CmdHelp_Mks);
					Console.WriteLine("reload           " + Messages.CmdHelp_Reload);
					Console.WriteLine("save             " + Messages.CmdHelp_Save);
					Console.WriteLine("saveb            " + Messages.CmdHelp_SaveB);
					Console.WriteLine("savet            " + Messages.CmdHelp_SaveT);
				} else if (cmd == "clear" || cmd == "cls") {
					// 画面を初期化
					Console.Clear();
				} else if (cmd == "list") {
					// 現在読み込まれているエントリを全て出力 コメントは非表示
					list(Current, true);
				} else if (cmd == "list full") {
					// 現在読み込まれているエントリを全て出力 コメントも表示
					list(Current, false);
				} else if (cmd.StartsWith("gset ")) {
					// 指定された名前のエントリの値を取得・設定
					gset(cmd.Substring(5));
				} else if (cmd.StartsWith("into ")) {
					// 指定されたセクションの中に入る
					into(cmd.Substring(5));
				} else if (cmd == "goroot") {
					// 現在位置をルートキーに戻す
					Current = rootSection;
					CurrentPath = string.Empty;
				} else if (cmd == "save") {
					// 現在の設定情報をファイルに保存する
					if (Header == null) {
						// テキスト形式
						using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
						using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode)) {
							sw.Write(rootSection.ToStringWithoutBrace());
						}
					} else {
						// バイナリ形式
						var r = new List<YenconNode>(rootSection.Children.Values);
						var buf = YenconBinaryFile.ConvertToBinary(r, Header);
						using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
						using (BinaryWriter bw = new BinaryWriter(fs)) {
							bw.Write(buf);
						}
					}
					Console.WriteLine(Messages.Saved_Successfully);
				} else if (cmd == "savet") {
					// 現在の設定情報をテキストファイルに保存する
					using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
					using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode)) {
						sw.Write(rootSection.ToStringWithoutBrace());
					}
					Console.WriteLine(Messages.Saved_Successfully);
				} else if (cmd == "saveb") {
					// 現在の設定情報をバイナリファイルに保存する
					if (Header == null) {
						Header = new YenconHeader();
						Header.KeyNameSize = 16;
						Header.UseAsciiKeyName = true;
						Header.Version = (0x00, 0x00, 0x00);
					}
					var r = new List<YenconNode>(rootSection.Children.Values);
					var buf = YenconBinaryFile.ConvertToBinary(r, Header);
					using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
					using (BinaryWriter bw = new BinaryWriter(fs)) {
						bw.Write(buf);
					}
					Console.WriteLine(Messages.Saved_Successfully);
				} else if (cmd == "reload") {
					// 全ての変更を破棄し、ファイルから設定情報を読み込む。
					// バイナリ形式
					Header = new YenconHeader();
					Root = YenconBinaryFile.Scan(filename, Header);
					if (Root == null) {
						// テキスト形式
						Header = null;
						using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
						using (StreamReader sr = new StreamReader(fs, Encoding.Unicode)) {
							var yp = new YenconParser(sr.ReadToEnd());
							yp.Analyze();
							Root = new List<YenconNode>(yp.GetNodes());
						}
					}
					Current = rootSection = new YenconSection(Root);
					CurrentPath = string.Empty;
					Console.WriteLine(Messages.Reloaded_Successfully);
				} else if (cmd.StartsWith("add ")) {
					// 新しいキーを追加または既存のキーをNULLにする
					add(cmd.Substring(4));
					Console.WriteLine(Messages.AddedKey_Successfully);
				} else if (cmd.StartsWith("mks ")) {
					// 新しいセクションを追加または既存のキーを空のセクションにする
					mks(cmd.Substring(4));
					Console.WriteLine(Messages.AddedSection_Successfully);
				} else if (cmd.StartsWith("del ") || cmd.StartsWith("rmv ")) {
					// キーまたはセクションを削除する。
					delete(cmd.Substring(4));
				} else if (!string.IsNullOrEmpty(cmd)) {
					// 不正なコマンド
					Console.WriteLine(Messages.CommandNotFound, cmd);
				}

				Console.WriteLine();
			}
		}

		public static void list(YenconSection section, bool skipComment)
		{
			foreach (var item in section.Children) {
				string str;
				if (item.Value is YenconComment yc) {
					if (skipComment) {
						continue;
					} else {
						str = yc.Text + " (" + Messages.TempID + ":" + yc.Name + ")";
					}
				} else if (item.Value.Value is YenconSection) {
					// セクションは値の数のみ表示
					YenconSection v = item.Value.Value as YenconSection;
					if (v.Children != null && v.Children.Count != 0) {
						str = string.Format(Messages.SectionList_Present, v.Children.Count);
					} else {
						str = Messages.SectionList_Empty;
					}
				} else if (item.Value.Value is YenconNullValue) {
					str = "<NULL>";
				} else {
					str = item.Value.Value.GetValue().ToString().Escape();
				}
				Console.WriteLine($"{item.Key.Abridge(36)}({item.Value?.Kind.ToString().PadRight(12)}): {str}");
			}
		}

		public static void gset(string name)
		{
			var vs = Current.Children;
			YenconNode node = null;
			if (vs.ContainsKey(name)) {
				node = vs[name];
			} else {
				Console.WriteLine(Messages.KeyNotFound, name);
				return;
			}

			Console.WriteLine(Messages.KindOfKey + node.Kind);
			if (node.Kind == YenconType.Section) { // セクションの場合
				list(node.Value as YenconSection, false);
			} else if (node is YenconComment yc) { // コメントの場合
				Console.WriteLine(yc.Text);
			} else { // 普通のキーの場合
				Console.WriteLine(node.Value.GetValue());
				Console.WriteLine();
				Console.Write(Messages.NewValueOfKey);
				string newval = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(newval)) {
					return; // 改変しないで終了
				} else if (uint.TryParse(newval.Trim(), out var num)) {
					// 数値キー
					node.Value = new YenconNumberKey() { Count = num };
				} else if (newval.TryToBoolean(out var flg)) {
					// 論理値キー
					node.Value = new YenconBooleanKey() { Flag = flg };
				} else {
					// 文字列キー
					node.Value = new YenconStringKey() { Text = newval };
				}
			}
		}

		public static void into(string name)
		{
			var vs = Current.Children;
			YenconNode node = null;
			if (vs.ContainsKey(name)) {
				if (vs[name].Kind == YenconType.Section) {
					node = vs[name];
				} else {
					Console.WriteLine(Messages.KeyIsNotSection, name);
					return;
				}
			} else {
				Console.WriteLine(Messages.KeyNotFound, name);
				return;
			}

			Current = node.Value as YenconSection;
			CurrentPath += "/" + node.Name;
		}

		public static void add(string name)
		{
			var vs = Current.Children;
			if (vs.ContainsKey(name)) {
				// 既に存在する場合はNULLに書き換え
				vs[name].Value = new YenconNullValue();
			} else {
				vs.Add(name, new YenconNode() {
					Name = name,
					Value = new YenconNullValue()
				});
			}
		}

		public static void mks(string name)
		{
			var vs = Current.Children;
			if (vs.ContainsKey(name)) {
				// 既に存在する場合は空のセクションに書き換え
				vs[name].Value = new YenconSection();
			} else {
				vs.Add(name, new YenconNode() {
					Name = name,
					Value = new YenconSection()
				});
			}
		}

		public static void delete(string name)
		{
			var vs = Current.Children;
			if (vs.ContainsKey(name)) {
				// 存在している場合のみ削除
				vs.Remove(name);
				Console.WriteLine(Messages.Deleted_Successfully);
			} else {
				// 存在しない場合は警告を表示
				Console.WriteLine(Messages.CannotDelete);
			}
		}
	}
}
