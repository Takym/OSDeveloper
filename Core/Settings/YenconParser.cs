using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  内容を解析して型'<see cref="OSDeveloper.Core.Settings.YenconNode"/>'リストに変換します。
	///  このクラスは継承できません。
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
					value.Clear();
					while (i < src.Length && src[i] != '\n') {
						// 必ず ; から始まるので、前置インクリメントを使う
						value.Append(src[++i]);
					}
					YenconComment yc = new YenconComment(value.ToString());
					_out.Add(yc);
				} else if (src[i].IsWhitespace()) { // 空白文字無視
					++i;
				} else if (src[i].IsIdentifier()) { // キー or セクション
					name.Clear();
					while (i < src.Length && src[i].IsIdentifier()) {
						name.Append(src[i]);
						++i;
					}
					while (src[i].IsWhitespace()) {
						++i; // 空白文字無視
					}
					YenconNode node = new YenconNode();
					node.Name = name.ToString();
					if (src[i] == '=') {
						++i;
						while (src[i].IsWhitespace()) {
							++i; // 空白文字無視
						}
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
								while (i < src.Length && src[i].IsNumber()) {
									value.Append(src[i]);
									++i;
								}
								++i;
								node.Value = new YenconNumberKey() { Count = ulong.Parse(value.ToString()) };
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

		/// <summary>
		///  指定されたファイルをヱンコン形式として読み込みます。
		/// </summary>
		/// <param name="path">読み込み元のファイルのパスです。</param>
		/// <returns>読み込んだファイルから生成された<see cref="OSDeveloper.Core.Settings.YenconSection"/>オブジェクトです。</returns>
		public static YenconSection Load(string path)
		{
			YenconParser parser;
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
			using (StreamReader sr = new StreamReader(fs, Encoding.Unicode)) {
				parser = new YenconParser(sr.ReadToEnd());
				parser.Analyze();
			}
			return new YenconSection(parser.GetNodes());
		}

		/// <summary>
		///  指定されたファイルに指定された<see cref="OSDeveloper.Core.Settings.YenconSection"/>オブジェクトの内容を保存します。
		/// </summary>
		/// <param name="path">書き込み先のファイルのパスです。</param>
		/// <param name="section">保存する<see cref="OSDeveloper.Core.Settings.YenconSection"/>オブジェクトです。</param>
		public static void Save(string path, YenconSection section)
		{
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode)) {
				sw.Write(section.ToStringWithoutBrace());
			}
		}
	}
}
