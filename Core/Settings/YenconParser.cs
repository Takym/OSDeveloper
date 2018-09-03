﻿using System.Collections.Generic;
using System.Data;
using System.Text;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.FileManagement;
using static OSDeveloper.Core.MiscUtils.StringUtils;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  内容を解析して型'<see cref="OSDeveloper.Core.Settings.YenconNode"/>'リストに変換します。
	/// </summary>
	public sealed class YenconParser : IKeyNodeParser<YenconNode, YenconValue>
	{
		private readonly bool _throw_error;
		private readonly string _in_src;
		private List<YenconNode> _out;

		/// <summary>
		///  ヱンコン環境設定ファイルの内容として利用する文字列です。
		/// </summary>
		public string Source
		{
			get
			{
				return _in_src;
			}
		}

		string IKeyNodeParser.Source
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Settings.YenconParser"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="src">解析する文字列です。</param>
		public YenconParser(string src)
		{
			_throw_error = false;
			_in_src = src;
			_out = new List<YenconNode>();
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Settings.YenconParser"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="src">解析する文字列です。</param>
		/// <param name="throwError">解析中にエラーが発生した場合に例外を投げるかどうかを指定します。</param>
		public YenconParser(string src, bool throwError)
		{
			_throw_error = throwError;
			_in_src = src;
			_out = new List<YenconNode>();
		}

		/// <summary>
		///  解析した設定データを取得します。
		///  この関数は<see cref="OSDeveloper.Core.Settings.YenconParser.Analyze"/>を実行してから呼び出してください。
		/// </summary>
		/// <returns>型'<see cref="OSDeveloper.Core.Settings.YenconNode"/>'リストです。</returns>
		public IReadOnlyList<YenconNode> GetNodes()
		{
			return new List<YenconNode>(_out);
		}

		IReadOnlyList<IKeyNode<IKeyNodeValue>> IKeyNodeParser.GetNodes()
		{
			var result = new List<IKeyNode<IKeyNodeValue>>();
			foreach (var item in _out) {
				result.Add(item);
			}
			return result;
		}

		/// <summary>
		///  このインスタンスに設定されている文字列を解析します。
		/// </summary>
		/// <exception cref="System.Data.SyntaxErrorException" />
		public void Analyze()
		{
			_out.Clear();
			string src = _in_src.CRtoLF();
			StringBuilder name = new StringBuilder();
			StringBuilder value = new StringBuilder();
			for (int i = 0; i < src.Length;) {
				if (src[i] == ';') { // コメントアウト
					while (i < src.Length && src[i] != '\n') ++i;
				} else if (this.IsWhitespace(src[i])) { // 空白文字無視
					++i;
				} else if (this.IsIdentifier(src[i])) { // キー or セクション
					name.Clear();
					while (i < src.Length && this.IsIdentifier(src[i])) {
						name.Append(src[i]);
						++i;
					}
					YenconNode node = new YenconNode();
					node.Name = name.ToString();
					if (src[i] == '=') {
						++i;
						value.Clear();
						switch (src[i]) { // キーの種類を判別
							case '{': { // セクション
								++i;
								int brkt = 1;
								while (i < src.Length) {
									if (src[i] == '{') {
										++brkt;
									} else if (src[i] == '}') {
										--brkt;
										if (brkt == 0) break;
									}
									value.Append(src[i]);
									++i;
								}
								++i;
								var p = new YenconParser(value.ToString());
								p.Analyze();
								node.Value = new YenconSection(p._out);
								_out.Add(node);
								break;
							}
							case '\"': { // 文字列キー
								++i;
								while (i < src.Length && src[i] != '\"') {
									value.Append(src[i]);
									++i;
								}
								++i;
								node.Value = new YenconStringKey() { Text = value.ToString() };
								_out.Add(node);
								break;
							}
							case '#': { // 数値キー
								++i;
								while (i < src.Length && IsNumber(src[i])) {
									value.Append(src[i]);
									++i;
								}
								++i;
								node.Value = new YenconNumberKey() { Count = uint.Parse(value.ToString()) };
								_out.Add(node);
								break;
							}
							case 'T': { // 論理値キー (true)
								node.Value = new YenconBooleanKey() { Flag = true };
								++i;
								_out.Add(node);
								break;
							}
							case 'F': { // 論理値キー (false)
								node.Value = new YenconBooleanKey() { Flag = false };
								++i;
								_out.Add(node);
								break;
							}
							case '_': { // NULLキー
								node.Value = new YenconNullValue();
								++i;
								_out.Add(node);
								break;
							}
							default: { // それ以外
								if (_throw_error) {
									// 例外を投げても良いので、エラーを発生させる
									throw new SyntaxErrorException($"{ErrorMessages.YenconParser_UnknownCharacter} {src[i]}");
								} else {
									// 取り敢えず無視
									node.Value = new YenconNullValue();
									++i;
									_out.Add(node);
								}
								break;
							}
						}
					} else {
						if (_throw_error) {
							// 例外を投げても良いので、エラーを発生させる
							throw new SyntaxErrorException($"{ErrorMessages.YenconParser_UnknownCharacter} {src[i]}");
						} else {
							// 取り敢えず無視
							node.Value = new YenconNullValue();
							++i;
							_out.Add(node);
						}
					}
				} else { // 対応していない文字
					if (_throw_error) {
						// 例外を投げても良いので、エラーを発生させる
						throw new SyntaxErrorException($"{ErrorMessages.YenconParser_UnknownCharacter} {src[i]}");
					} else {
						++i; // 取り敢えず無視
					}
				}
			}
		}

		// TODO: YenconParser: これより下に定義されている関数は全て StringUtils に移動する。

		private bool IsWhitespace(char c)
		{
			return c == ' '
				|| c == '　'
				|| c == '\t'
				|| c == '\r'
				|| c == '\n';
		}

		private bool IsIdentifier(char c)
		{
			return IsAlphabet(c)
				|| IsNumber(c)
				|| c == '_';
		}

		private bool IsAlphabet(char c)
		{
			return ('A' <= c && c <= 'Z')
				|| ('a' <= c && c <= 'z');
		}

		private bool IsNumber(char c)
		{
			return '0' <= c && c <= '9';
		}
	}
}
