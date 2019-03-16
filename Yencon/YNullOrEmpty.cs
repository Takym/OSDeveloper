#pragma warning disable CS0809 // 旧形式のメンバーが、旧形式でないメンバーをオーバーライドします
using System;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の空の値(<see langword="null"/>)を表します。
	/// </summary>
	public class YNullOrEmpty : YNode
	{
		/// <summary>
		///  このキーは空値を表している為、
		///  常に<see langword="null"/>値を返します。
		/// </summary>
		/// <returns><see langword="null"/>値です。</returns>
		public override object GetValue()
		{
			return null;
		}

		/// <summary>
		///  このキーは空値を表している為、
		///  指定された値は設定されず、この関数は何も行いません。
		/// </summary>
		/// <param name="value">利用されていません。</param>
		[Obsolete()]
		public override void SetValue(object value)
		{
			// 何もしない
		}

		/// <summary>
		///  空値を表すテキスト形式のヱンコン値を返します。
		/// </summary>
		/// <returns>常に下線(<c><see langword="_"/></c>)を返します。</returns>
		public override string ToString()
		{
			return "_";
		}

		/// <summary>
		///  空値を表すバイナリ形式のヱンコン値を返します。
		/// </summary>
		/// <returns>常に<c>{0x00}</c>を持つ1次元で長さが1のバイト配列を返します。</returns>
		public override byte[] ToBinary()
		{
			return new byte[] { 0x00 };
		}
	}
}
