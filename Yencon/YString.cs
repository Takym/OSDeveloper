using System;
using System.Text;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の文字列を表します。
	/// </summary>
	public class YString : YNode
	{
		private static readonly (string e, char v)[] _escape_map = new (string e, char v)[] {
			("\\'", '\''), ("\\\\", '\\'), ("\\q", '\"'), ("\\s", ' '), ("\\S", '　'), ("\\t", '\t'), ("\\n", '\n'), ("\\r", '\r')
		};

		private string _s = string.Empty;

		/// <summary>
		///  このキーが保持している文字列を取得または設定します。
		/// </summary>
		public string Text
		{
			get
			{
				StringBuilder sb = new StringBuilder(_s);
				for (int i = 0; i < _escape_map.Length; ++i) {
					sb.Replace(_escape_map[i].e, _escape_map[i].v.ToString());
				}
				return sb.ToString();
			}

			set
			{
				StringBuilder sb = new StringBuilder(value);
				for (int i = 0; i < _escape_map.Length; ++i) {
					sb.Replace(_escape_map[i].v.ToString(), _escape_map[i].e);
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
