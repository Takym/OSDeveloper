using System;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の論理値を表します。
	/// </summary>
	public class YBoolean : YNode
	{
		/// <summary>
		///  このキーが保持している論理値を取得または設定します。
		/// </summary>
		public bool Flag { get; set; }

		/// <summary>
		///  このキーが保持している論理値を取得します。
		/// </summary>
		/// <returns>このキーが保持している論理値です。</returns>
		public override object GetValue()
		{
			return this.Flag;
		}

		/// <summary>
		///  このキーに指定された論理値を設定します。
		/// </summary>
		/// <param name="value">このキーに設定する新たな論理値です。</param>
		/// <exception cref="System.InvalidCastException">
		///  型'<see cref="bool"/>'に変換できない型が渡された場合に発生します。
		/// </exception>
		public override void SetValue(object value)
		{
			this.Flag = ((bool)(value));
		}

		/// <summary>
		///  この論理値を表すテキスト形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この論理値のテキスト形式を返します。</returns>
		public override string ToString()
		{
			if (this.Flag) {
				return "T";
			} else {
				return "F";
			}
		}

		/// <summary>
		///  この論理値を表すバイナリ形式のヱンコン値を返します。
		/// </summary>
		/// <returns>この論理値のバイナリ形式を返します。</returns>
		public override byte[] ToBinary()
		{
			if (this.Flag) {
				return new byte[] { 0x04 };
			} else {
				return new byte[] { 0x05 };
			}
		}
	}
}
