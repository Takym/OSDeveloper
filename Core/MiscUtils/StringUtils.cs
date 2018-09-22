using System;
using OSDeveloper.Core.Error;

namespace OSDeveloper.Core.MiscUtils
{
	/// <summary>
	///  <see cref="string"/>クラスと<see cref="char"/>構造体の拡張メソッドと便利関数を提供します。
	///  このクラスは静的です。
	/// </summary>
	public static class StringUtils
	{
		/// <summary>
		///  キャリッジリターン<c>('\r')</c>をラインフィード<c>('\n')</c>に変換します。
		///  <c>"\r\n"</c>と<c>"\n\r"</c>も<c>'\n'</c>に変換されます。
		/// </summary>
		/// <param name="text">変換対象の文字列です。</param>
		/// <returns>変換結果の文字列です。</returns>
		public static string CRtoLF(this string text)
		{
			return text.Replace("\r\n", "\n").Replace("\n\r", "\n").Replace("\r", "\n");
		}

		/// <summary>
		///  ラインフィード<c>('\n')</c>をキャリッジリターン<c>('\r')</c>に変換します。
		///  <c>"\r\n"</c>と<c>"\n\r"</c>も<c>'\r'</c>に変換されます。
		/// </summary>
		/// <param name="text">変換対象の文字列です。</param>
		/// <returns>変換結果の文字列です。</returns>
		public static string LFtoCR(this string text)
		{
			return text.Replace("\r\n", "\r").Replace("\n\r", "\r").Replace("\n", "\r");
		}

		/// <summary>
		///  指定された文字列をエスケープします。
		/// </summary>
		/// <param name="s">変換対象の文字列です。</param>
		/// <returns>変換結果の文字列です。</returns>
		public static string Escape(this string s)
		{
			return s
				.Replace("\'", "\\\'")
				.Replace("\\", "\\\\")
				.Replace("\"", "\\q")
				.Replace("\t", "\\t")
				.Replace("\n", "\\n")
				.Replace("\r", "\\r");
		}

		/// <summary>
		///  指定された文字列のエスケープを解除します。
		/// </summary>
		/// <param name="s">変換対象の文字列です。</param>
		/// <returns>変換結果の文字列です。</returns>
		public static string Unescape(this string s)
		{
			return s
				.Replace("\\\'", "\'")
				.Replace("\\\\", "\\")
				.Replace("\\q", "\"")
				.Replace("\\t", "\t")
				.Replace("\\n", "\n")
				.Replace("\\r", "\r");
		}

		/// <summary>
		///  指定した文字列をブール値に変換します。
		/// </summary>
		/// <param name="s">変換対象の文字列です。</param>
		/// <returns>変換結果のブール値です。</returns>
		/// <exception cref="System.ArgumentException"/>
		public static bool ToBoolean(this string s)
		{
			if (s.TryToBoolean(out var result)) {
				return result;
			} else {
				throw new ArgumentException($"{ErrorMessages.StringUtils_ConvertToBoolean} {s}", nameof(s));
			}
		}

		/// <summary>
		///  指定した文字列をブール値に変換できるかどうか試します。
		///  変換結果は<paramref name="result"/>に保持されます。
		/// </summary>
		/// <param name="s">変換対象の文字列です。</param>
		/// <param name="result">変換結果のブール値です。</param>
		/// <returns>
		///  変換に成功した場合は<c>true</c>、失敗した場合は<c>false</c>です。
		/// </returns>
		public static bool TryToBoolean(this string s, out bool result)
		{
			string text = s.ToLower().Trim(' ', '　', '\t', '\r', '\n');
			switch (text) {
				case "true":
				case "yes":
				case "on":
				case "allow":
				case "pos":
				case "positive":
				case "one":
				case "1":
				case "high":
				case "t":
				case "y":
					result = true;
					return true;
				case "false":
				case "no":
				case "off":
				case "deny":
				case "neg":
				case "negative":
				case "zero":
				case "0":
				case "low":
				case "f":
				case "n":
					result = false;
					return true;
				default:
					result = false;
					return false;
			}
		}

		/// <summary>
		///  "<c>0-9,a-z,A-Z</c>"と"<c>!#$%&amp;'()-=^~@`[{]};+,._</c>"をランダムに並べた文字列を生成します。
		///  文字数は8-64の間で生成されます。生成された文字列はファイル名に使用できます。
		/// </summary>
		/// <returns>生成された文字列です。</returns>
		public static string GetRandomText()
		{
			return GetRandomText(8, 64, Environment.TickCount);
		}

		/// <summary>
		///  "<c>0-9,a-z,A-Z</c>"と"<c>!#$%&amp;'()-=^~@`[{]};+,._</c>"をランダムに並べた文字列を生成します。
		///  指定した文字数で生成されます。生成された文字列はファイル名に使用できます。
		/// </summary>
		/// <param name="length">生成する文字列の文字数です。</param>
		/// <returns>生成された文字列です。</returns>
		public static string GetRandomText(int length)
		{
			return GetRandomText(length, length, Environment.TickCount);
		}

		/// <summary>
		///  "<c>0-9,a-z,A-Z</c>"と"<c>!#$%&amp;'()-=^~@`[{]};+,._</c>"をランダムに並べた文字列を生成します。
		///  生成された文字列はファイル名に使用できます。
		/// </summary>
		/// <param name="min">文字数の最小です。</param>
		/// <param name="max">文字数の最大です。</param>
		/// <returns>生成された文字列です。</returns>
		public static string GetRandomText(int min, int max)
		{
			return GetRandomText(min, max, Environment.TickCount);
		}

		/// <summary>
		///  "<c>0-9,a-z,A-Z</c>"と"<c>!#$%&amp;'()-=^~@`[{]};+,._</c>"をランダムに並べた文字列を生成します。
		///  文字数は指定された範囲からランダムで決定します。生成された文字列はファイル名に使用できます。
		/// </summary>
		/// <param name="min">文字数の最小です。</param>
		/// <param name="max">文字数の最大です。</param>
		/// <param name="seed">シード値です。</param>
		/// <returns>生成された文字列です。</returns>
		public static string GetRandomText(int min, int max, int seed)
		{
			Random r = new Random(seed);
			int len = r.Next(min, max);
			string result = string.Empty;
			for (int i = 0; i < len; ++i) {
				result += c[r.Next(0, c.Length)];
			}
			return result;
		}

		private const string c = "!#$0aAbBcCdDeEfF%&'gGhH1234()-=IiJjKkLlMmNnOoPp^~@[{]}5678qrstuv;+QRSTUV9WXYZwxyz,._";

		/// <summary>
		///  指定された文字が空白文字かどうか判定します。
		/// </summary>
		/// <param name="c">判定対象の一文字です。</param>
		/// <returns>空白文字の場合は<see langword="true"/>、それ以外は<see langword="false"/>です。</returns>
		public static bool IsWhitespace(this char c)
		{
			return c == ' '
				|| c == '　'
				|| c == '\t'
				|| c == '\r'
				|| c == '\n';
		}

		/// <summary>
		///  指定された文字がラテン文字かどうか判定します。
		/// </summary>
		/// <param name="c">判定対象の一文字です。</param>
		/// <returns>ラテン文字の場合は<see langword="true"/>、それ以外は<see langword="false"/>です。</returns>
		public static bool IsAlphabet(this char c)
		{
			return ('A' <= c && c <= 'Z')
				|| ('a' <= c && c <= 'z');
		}

		/// <summary>
		///  指定された文字が算用数字かどうか判定します。
		/// </summary>
		/// <param name="c">判定対象の一文字です。</param>
		/// <returns>算用数字の場合は<see langword="true"/>、それ以外は<see langword="false"/>です。</returns>
		public static bool IsNumber(this char c)
		{
			return '0' <= c && c <= '9';
		}


		/// <summary>
		///  指定された文字が英数字またはアンダースコアかどうか判定します。
		/// </summary>
		/// <param name="c">判定対象の一文字です。</param>
		/// <returns>英数字またはアンダースコアの場合は<see langword="true"/>、それ以外は<see langword="false"/>です。</returns>
		public static bool IsIdentifier(this char c)
		{
			return IsAlphabet(c)
				|| IsNumber(c)
				|| c == '_';
		}
	}
}
