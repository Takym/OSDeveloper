using System;
using System.Collections.Generic;
using System.Text;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の文字列を表します。
	/// </summary>
	public class YString : YNode
	{
		private static readonly Dictionary<char, char> _escape_map = new Dictionary<char, char>() {
			['p'] = '\'',
			['y'] = '\\',
			['q'] = '\"',
			['s'] = ' ' ,
			['S'] = '　',
			['t'] = '\t',
			['n'] = '\n',
			['r'] = '\r',
		};

		private string _s = string.Empty;

		/// <summary>
		///  このキーが保持している文字列を取得または設定します。
		/// </summary>
		public string Text
		{
			get
			{
				var sb = new StringBuilder();
				for (int i = 0; i < _s.Length; ++i) {
					if (_s[i] == '\\' && (i + 1) < _s.Length) {
						++i;
						sb.Append(_escape_map[_s[i]]);
					} else {
						sb.Append(_s[i]);
					}
				}
				return sb.ToString();
			}

			set
			{
				var sb = new StringBuilder();
				for (int i = 0; i < value.Length; ++i) {
					if (_escape_map.ContainsValue(value[i])) {
						foreach (var item in _escape_map) {
							if (item.Value == value[i]) {
								sb.Append($"\\{item.Key}");
							}
						}
					} else {
						sb.Append(value[i]);
					}
				}
				_s = sb.ToString();
			}
		}

		/// <summary>
		///  このキーが保持しているエスケープ解除済みの文字列を取得します。
		/// </summary>
		/// <returns>このキーが保持しているエスケープ解除済みの文字列値です。</returns>
		public override object GetValue()
		{
			return this.Text;
		}

		/// <summary>
		///  このキーに指定されたエスケープ解除済みの文字列を設定します。
		/// </summary>
		/// <param name="value">このキーに設定する新たなエスケープ解除済みの文字列です。</param>
		public override void SetValue(object value)
		{
			this.Text = value.ToString();
		}

		/// <summary>
		///  このキーが保持しているエスケープ済みの文字列を取得します。
		/// </summary>
		/// <returns>このキーが保持しているエスケープ済みの文字列値です。</returns>
		public string GetEscapedText()
		{
			return _s;
		}

		/// <summary>
		///  このキーに指定されたエスケープ済みの文字列を設定します。
		/// </summary>
		/// <param name="value">
		///  このキーに設定する新たなエスケープ済みの文字列です。
		///  エスケープされていない文字が渡された場合、正しいヱンコン値が生成される可能性があります。
		/// </param>
		public void SetEscapedText(string value)
		{
			_s = value;
		}

		/// <summary>
		///  この文字列を表すテキスト形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この文字列のテキスト形式を返します。</returns>
		public override string ToString()
		{
			return $"\"{_s}\"";
		}

		/// <summary>
		///  この文字列を表すバイナリ形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この文字列のバイナリ形式を返します。</returns>
		public override byte[] ToBinary()
		{
			byte[] str = Encoding.Unicode.GetBytes(_s);
			byte[] len = BitConverter.GetBytes((uint)(str.Length));
			byte[] result = new byte[5 + str.Length];
			result[0] = 0x02;
			len.CopyTo(result, 1);
			str.CopyTo(result, 5);
			return result;
		}
	}
}
