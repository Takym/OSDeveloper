using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TakymLib;
using Yencon;
using YenconCommandLineTool.Resources;

namespace YenconCommandLineTool
{
	static class Program
	{
		static string _fname, _ypath;
		static YSection _root, _current;

		[STAThread()]
		static int Main(string[] args)
		{
			_fname = Messages.Fname_Untitled;
			_ypath = "/";
			_root = _current = new YSection();

			Console.WriteLine(Messages.ToolTitle);
			Console.WriteLine(Messages.FileNotOpened);
			Console.WriteLine();

			while (true) {
				Console.WriteLine(_fname);
				Console.Write(_ypath + "> ");
				string[] cmd = SplitCommand(Console.ReadLine());
				if (cmd.Length > 0) {
					switch (cmd[0]) {
						// 終了
						case "exit":
						case "quit":
							goto end;
						// 説明系
						case "help":
							if (cmd.Length > 1) {
								Manual.Help(cmd[1]);
							} else {
								Manual.Help();
							}
							break;
						case "about":
						case "ver":
							Manual.Version(Assembly.GetExecutingAssembly());
							Manual.Version(Assembly.GetAssembly(typeof(YNode)));
							Manual.Version(Assembly.GetAssembly(typeof(StringUtils)));
							break;
						// ファイルアクセス系
						case "load":
							string fnr = cmd.Length > 1 ? cmd[1] : _fname;
							var objr = FileAccessor.Load(fnr);
							if (objr != null) {
								_root = _current = objr; _fname = fnr; _ypath = "/";
							}
							break;
						case "loadt":
							string fnt = cmd.Length > 1 ? cmd[1] : _fname;
							var objt = FileAccessor.LoadTxt(fnt);
							if (objt != null) {
								_root = _current = objt; _fname = fnt; _ypath = "/";
							}
							break;
						case "loadb":
							string fnb = cmd.Length > 1 ? cmd[1] : _fname;
							var objb = FileAccessor.LoadBin(fnb);
							if (objb != null) {
								_root = _current = objb; _fname = fnb; _ypath = "/";
							}
							break;
						case "save":
							FileAccessor.Save   (_fname = cmd.Length > 1 ? cmd[1] : _fname, _root);
							break;
						case "savet":
							FileAccessor.SaveTxt(_fname = cmd.Length > 1 ? cmd[1] : _fname, _root);
							break;
						case "saveb":
							FileAccessor.SaveBin(_fname = cmd.Length > 1 ? cmd[1] : _fname, _root);
							break;
						case "binhdr":
							FileAccessor.ShowBinHeader();
							break;
						// セクション
						case "into":
							if (cmd.Length > 1) {
								var s = _current.GetNode(cmd[1]) as YSection;
								if (s == null) {
									Console.WriteLine(Messages.SectionNotFound);
								} else {
									_current = s;
									_ypath += s.Name + "/";
								}
								break;
							} else goto default;
						case "goroot":
							_current = _root;
							_ypath = "/";
							break;
						// セクションとキーの制御
						case "list":
							Operator.List(_current);
							break;
						case "set":
							if (cmd.Length > 3) {
								Operator.Set(_current, cmd[1], cmd[2], cmd[3]);
								break;
							} else goto default;
						case "adds":
							if (cmd.Length > 1) {
								Operator.Adds(_current, cmd[1]);
								break;
							} else goto default;
						// コマンドが見つからなかった場合
						default:
							Console.WriteLine(string.Format(Messages.CommandNotFound, cmd[0]));
							break;
					}
				}
				Console.WriteLine();
			}

end:
			Console.WriteLine();
			ConsoleUtils.Pause();
			return 0;
		}

		static string[] SplitCommand(string cmd)
		{
			var result = new List<string>();
			var tmp    = new StringBuilder();
			var ystr   = new YString();
			for (int i = 0; i < cmd.Length; ++i) {
				if (char.IsWhiteSpace(cmd[i])) {
					string s = tmp.ToString();
					if (!string.IsNullOrWhiteSpace(s)) {
						ystr.SetEscapedText(s.Trim());
						result.Add(ystr.Text);
					}
					tmp.Clear();
				} else {
					tmp.Append(cmd[i]);
				}
			}
			{
				string s = tmp.ToString();
				if (!string.IsNullOrWhiteSpace(s)) {
					ystr.SetEscapedText(s.Trim());
					result.Add(ystr.Text);
				}
				tmp.Clear();
			}
			return result.ToArray();
		}

		public static void ShowError(Exception e)
		{
			var c = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(e.ToString());
			Console.ForegroundColor = c;
		}
	}
}
