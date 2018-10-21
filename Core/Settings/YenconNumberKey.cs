﻿namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  数値キーを表します。
	/// </summary>
	public class YenconNumberKey : YenconValue
	{
		/// <summary>
		///  このキーが保持している数値データです。
		/// </summary>
		public ulong Count { get; set; }

		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public override YenconType Kind
		{
			get
			{
				return YenconType.NumberKey;
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Settings.YenconNumberKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconNumberKey()
		{
			this.Count = 0;
		}

		/// <summary>
		///  保持する数値を指定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconNumberKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="value">保持する数値です。</param>
		public YenconNumberKey(long value)
		{
			this.Count = unchecked((ulong)(value));
		}

		/// <summary>
		///  保持する数値を指定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconNumberKey"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="value">保持する数値です。</param>
		public YenconNumberKey(ulong value)
		{
			this.Count = value;
		}

		/// <summary>
		///  このセクションまたはキーの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public override object GetValue()
		{
			return this.Count;
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			return "#" + this.Count;
		}
	}
}
