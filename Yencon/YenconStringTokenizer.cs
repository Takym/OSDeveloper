using System.Collections.Generic;
using System.Text;
using Yencon.Exceptions;
using Yencon.Text;

namespace Yencon
{
	/// <summary>
	///  テキスト形式のヱンコンの文字列を字句解析します。
	/// </summary>
	public sealed class YenconStringTokenizer
	{
		private readonly string _src;
		private List<Token> _result;

		/// <summary>
		///  解析する文字列を指定して、
		///  型'<see cref="Yencon.YenconStringTokenizer"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="src">解析する文字列です。</param>
		public YenconStringTokenizer(string src)
		{
			_src = src ?? string.Empty;
		}

		/// <summary>
		///  渡された文字列を解析して字句に分割されたリストを取得します。
		/// </summary>
		/// <returns>読み取り専用のリストオブジェクトです。</returns>
		/// <exception cref="Yencon.Exceptions.InvalidSyntaxException">
		///  不正なテキスト形式のヱンコンである場合に発生します。
		/// </exception>
		public IReadOnlyList<Token> Scan()
		{
			// キャッシュがnullなら解析する
			if (_result == null) this.AnalyzeInternal();
			// キャッシュされたリストを返す
			return _result.AsReadOnly();
		}

		private IReadOnlyList<Token> AnalyzeInternal()
		{
			_result = new List<Token>();
			var sb = new StringBuilder();

			for (int i = 0; i < _src.Length;) {
				// 空白を読み飛ばす
				this.SkipSpaces(ref i);
				// ファイルの終端が来たら終わり
				if (i >= _src.Length) break;

				if (IsValidLetter(_src[i])) { // TokenType.Name
					// 有効な名前文字が続く限り文字列を取得し続ける
					do sb.Append(_src[i]); while (IsValidLetter(_src[++i]) && i < _src.Length);
					// 取得した文字列を名前としてリストに追加する
					_result.Add(new Token(sb.ToString(), TokenType.Name));
					// StringBuilderを空にしておく
					sb.Clear();
				} else if (_src[i] == '=') { // TokenType.Equal
					// 等号をリストに追加する
					_result.Add(new Token(_src[i].ToString(), TokenType.Equal));
					// カウンタを加算する
					++i;
					// 空白を読み飛ばす
					this.SkipSpaces(ref i);
					// ファイルの終端が来たら終わり
					if (i >= _src.Length) break;
					// 等号の後ろに下線、T、またはFがある場合
					if (_src[i] == '_') { // TokenType.NullOrEmpty
						// 空値としてリストに追加する
						_result.Add(new Token(_src[i].ToString(), TokenType.NullOrEmpty));
						// カウンタを加算する
						++i;
					} else if (_src[i] == 'T' || _src[i] == 'F') { // TokenType.Boolean
						// 論理値としてリストに追加する
						_result.Add(new Token(_src[i].ToString(), TokenType.Boolean));
						// カウンタを加算する
						++i;
					}
				} else if (_src[i] == ';') { // TokenType.CommentChar // TokenType.CommentText
					// コメントの型指定子をリストに追加する
					_result.Add(new Token(_src[i].ToString(), TokenType.CommentChar));
					// カウンタを加算する
					++i;
					// 行末までの文字列を全て取得
					while (_src[i] != '\n' && _src[i] != '\r' && i < _src.Length) sb.Append(_src[i++]);
					// 取得した文字列をコメントとしてリストに追加する
					_result.Add(new Token(sb.ToString(), TokenType.CommentText));
					// StringBuilderを空にしておく
					sb.Clear();
				} else if (_src[i] == '{') { // TokenType.SectionStart
					// 開始波括弧をリストに追加する
					_result.Add(new Token(_src[i].ToString(), TokenType.SectionStart));
					// カウンタを加算する
					++i;
				} else if (_src[i] == '}') { // TokenType.SectionEnd
					// 終了波括弧をリストに追加する
					_result.Add(new Token(_src[i].ToString(), TokenType.SectionEnd));
					// カウンタを加算する
					++i;
				} else if (_src[i] == '\"') { // TokenType.String
					// カウンタを加算する
					++i;
					// 二重引用符が来るまで文字列を取得する
					while (_src[i] != '\"' && i < _src.Length) sb.Append(_src[i++]);
					// 取得した文字列をコメントとしてリストに追加する
					_result.Add(new Token(sb.ToString(), TokenType.String));
					// StringBuilderを空にしておく
					sb.Clear();
					// カウンタを加算する
					++i;
				} else if (_src[i] == '#') { // TokenType.Number
					// 有効な数字が続く限り文字列を取得し続ける
					while (IsValidNumber(_src[++i]) && i < _src.Length) sb.Append(_src[i]);
					// 取得した文字列を符号無し64ビット整数値としてリストに追加する
					_result.Add(new Token(sb.ToString(), TokenType.Number));
					// StringBuilderを空にしておく
					sb.Clear();
				} else { // 無効な文字
					// リストを削除して不適切な構文の情報がメモリ上に残らない様にする
					_result = null;
					// 取り敢えず、例外を投げる
					throw new InvalidSyntaxException(_src[i]);
				}
			}

			return _result;
		}

		/// <summary>
		///  空白を読み飛ばす
		/// </summary>
		/// <param name="i">添字</param>
		private void SkipSpaces(ref int i)
		{
			// char.IsWhiteSpace は使わずに独自の方法で調べる
			while (i < _src.Length &&
				(_src[i] == ' '  || _src[i] == '　' ||
				 _src[i] == '\t' || _src[i] == '\n' || _src[i] == '\r')
			) ++i;
		}

		/// <summary>
		///  有効な名前文字か？
		/// </summary>
		/// <param name="c">判定する字</param>
		/// <returns><see langword="true"/>：有効、<see langword="false"/>：無効</returns>
		internal static bool IsValidLetter(char c)
		{
			return c == '_' ||
				('0' <= c && c <= '9') ||
				('A' <= c && c <= 'Z') ||
				('a' <= c && c <= 'z');
		}

		/// <summary>
		///  無効な名前文字か？
		/// </summary>
		/// <param name="c">判定する字</param>
		/// <returns><see langword="true"/>：無効、<see langword="false"/>：有効</returns>
		internal static bool IsInvalidLetter(char c)
		{
			return c != '_' &&
				('0' > c || c > '9') &&
				('A' > c || c > 'Z') &&
				('a' > c || c > 'z');
		}

		/// <summary>
		///  有効な数字か？
		/// </summary>
		/// <param name="c">判定する字</param>
		/// <returns><see langword="true"/>：有効、<see langword="false"/>：無効</returns>
		static bool IsValidNumber(char c)
		{
			return '0' <= c && c <= '9';
		}

		/// <summary>
		///  コンストラクタに渡された文字列を返します。
		/// </summary>
		/// <returns>コンストラクタに渡されたテキスト形式のヱンコンです。</returns>
		public override string ToString()
		{
			return _src;
		}
	}

	namespace Text
	{
		/// <summary>
		///  字句を表します。
		/// </summary>
		public struct Token
		{
			/// <summary>
			///  この字句の元になった文字列を取得します。
			/// </summary>
			public string SourceText { get; }

			/// <summary>
			///  この字句の種類を取得します。
			/// </summary>
			public TokenType Type { get; }

			/// <summary>
			///  型'<see cref="Yencon.Text.Token"/>'の
			///  新しいインスタンスを生成します。
			/// </summary>
			/// <param name="src">この字句の文字列です。</param>
			/// <param name="type">この字句の種類です。</param>
			public Token(string src, TokenType type)
			{
				this.SourceText = src;
				this.Type = type;
			}
		}

		/// <summary>
		///  字句の種類を表します。
		/// </summary>
		public enum TokenType : byte
		{
			/// <summary>
			///  識別子を表します。
			/// </summary>
			Name,

			/// <summary>
			///  等号を表します。
			/// </summary>
			Equal,

			/// <summary>
			///  <see langword="null"/>を表します。
			/// </summary>
			NullOrEmpty,

			/// <summary>
			///  コメントの型指定子を表します。
			/// </summary>
			CommentChar,

			/// <summary>
			///  コメントの文字列値を表します。
			/// </summary>
			CommentText,

			/// <summary>
			///  セクションの型指定子を表します。
			/// </summary>
			SectionStart,

			/// <summary>
			///  セクションの型終了子を表します。
			/// </summary>
			SectionEnd,

			/// <summary>
			///  文字列値(リテラル)を表します。
			/// </summary>
			String,

			/// <summary>
			///  符号無し64ビット整数値(リテラル)を表します。
			/// </summary>
			Number,

			/// <summary>
			///  論理値(リテラル)を表します。
			/// </summary>
			Boolean
		}
	}
}
