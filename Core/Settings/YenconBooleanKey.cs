using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  論理値キーを表します。
	/// </summary>
	public class YenconBooleanKey : YenconValue
	{
		/// <summary>
		///  このキーが保持している論理値データです。
		/// </summary>
		public bool Flag { get; set; }

		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public override YenconType Kind
		{
			get
			{
				return YenconType.BooleanKey;
			}
		}

		/// <summary>
		///  このセクションまたはキーの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public override object GetValue()
		{
			return this.Flag;
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			if (this.Flag) {
				return "T";
			} else {
				return "F";
			}
		}
	}
}
