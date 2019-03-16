using System;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の数値を表します。
	/// </summary>
	public class YNumber : YNode
	{
		/// <summary>
		///  このキーが保持している符号無し64ビット整数値を取得または設定します。
		/// </summary>
		public ulong UInt64Value { get; set; }

		/// <summary>
		///  このキーが保持している符号付き64ビット整数値を取得または設定します。
		/// </summary>
		public long SInt64Value
		{
			get
			{
				return unchecked((long)(this.UInt64Value));
			}

			set
			{
				this.UInt64Value = unchecked((ulong)(value));
			}
		}

		/// <summary>
		///  このキーが保持している数値を取得します。
		/// </summary>
		/// <returns>このキーが保持している数値です。</returns>
		public override object GetValue()
		{
			return this.UInt64Value;
		}

		/// <summary>
		///  このキーに指定された数値を設定します。
		/// </summary>
		/// <param name="value">このキーに設定する新たな数値です。</param>
		/// <exception cref="System.InvalidCastException">
		///  型'<see cref="ulong"/>'に変換できない型が渡された場合に発生します。
		/// </exception>
		public override void SetValue(object value)
		{
			this.UInt64Value = ((ulong)(value));
		}

		/// <summary>
		///  この数値を表すテキスト形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この数値のテキスト形式を返します。</returns>
		public override string ToString()
		{
			return "#" + this.UInt64Value;
		}

		/// <summary>
		///  この数値を表すバイナリ形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この数値のバイナリ形式を返します。</returns>
		public override byte[] ToBinary()
		{
			byte[] result = new byte[9];
			byte[] len = BitConverter.GetBytes(this.UInt64Value);
			result[0] = 0x03;
			len.CopyTo(result, 1);
			return result;
		}
	}
}
