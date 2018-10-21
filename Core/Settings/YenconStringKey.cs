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
		///  型'<see cref="OSDeveloper.Core.Settings.YenconStringKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconStringKey()
		{
			this.Text = string.Empty;
		}

		/// <summary>
		///  保持する文字列を指定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconStringKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="str">保持する文字列です。</param>
		public YenconStringKey(string str)
		{
			this.Text = str?.Escape() ?? string.Empty;
		}

		/// <summary>
		///  保持する文字列を指定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconStringKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="obj">保持する文字列に変換可能なオブジェクトです。</param>
		public YenconStringKey(object obj)
		{
			this.Text = obj?.ToString()?.Escape() ?? string.Empty;
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
