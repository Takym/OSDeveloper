using System;
using System.Text;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定のコメントを表します。
	/// </summary>
	public class YComment : YNode
	{
		private string _remark = string.Empty;

		/// <summary>
		///  このコメントが名前を持つキーかどうかを表す論理値を取得または設定します。
		///  名前を持つ場合は<see langword="true"/>、持たない場合は<see langword="false"/>です。
		/// </summary>
		public bool HasName { get; set; }

		/// <summary>
		///  このキーが保持しているコメントを取得します。
		/// </summary>
		/// <returns>このキーが保持しているコメント値です。</returns>
		public override object GetValue()
		{
			return _remark;
		}

		/// <summary>
		///  このキーに指定されたコメントを設定します。
		///  改行は全て半角空白に変換されます。
		/// </summary>
		/// <param name="value">このキーに設定する新たなコメントです。</param>
		public override void SetValue(object value)
		{
			_remark = value.ToString()
				.Replace("\r\n", " ").Replace("\n\r", " ").Replace("\r", " ").Replace("\n", " ");
		}

		/// <summary>
		///  このコメントを表すテキスト形式のヱンコン値を返します。
		/// </summary>
		/// <returns>このコメントのテキスト形式を返します。</returns>
		public override string ToString()
		{
			return "; " + _remark;
		}

		/// <summary>
		///  このコメントを表すバイナリ形式のヱンコン値を返します。
		/// </summary>
		/// <returns>このコメントのバイナリ形式を返します。</returns>
		public override byte[] ToBinary()
		{
			byte[] str = Encoding.Unicode.GetBytes(_remark);
			byte[] len = BitConverter.GetBytes((uint)(str.Length));
			byte[] result = new byte[5 + str.Length];
			result[0] = 0xFF;
			len.CopyTo(result, 1);
			str.CopyTo(result, 5);
			return result;
		}
	}
}
