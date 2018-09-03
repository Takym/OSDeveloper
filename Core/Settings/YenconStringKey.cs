using System;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  文字列キーを表します。
	/// </summary>
	public class YenconStringKey : YenconValue
	{
		/// <summary>
		///  このキーが保持している文字列データです。
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public override YenconType Kind
		{
			get
			{
				return YenconType.StringKey;
			}
		}

		/// <summary>
		///  このセクションまたはキーの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public override object GetValue()
		{
			return this.Text.Unescape();
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			return "\"" + this.Text + "\"";
		}
	}
}
