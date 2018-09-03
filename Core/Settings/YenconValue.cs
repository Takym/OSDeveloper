using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  セクションまたはキーを表します。
	/// </summary>
	public abstract class YenconValue : IKeyNodeValue
	{
		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public abstract YenconType Kind { get; }

		/// <summary>
		///  このセクションまたはキーの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public abstract object GetValue();

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public abstract override string ToString();

		internal YenconValue() { } // 外部ライブラリから継承を不可能にする。
	}

	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  空の値を表します。
	/// </summary>
	public sealed class YenconNullValue : YenconValue
	{
		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public override YenconType Kind
		{
			get
			{
				return YenconType.NullOrEmpty;
			}
		}

		/// <summary>
		///  このセクションまたはキーの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public override object GetValue()
		{
			return null;
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			return "_";
		}
	}

	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  セクションまたはキーの種類を表します。
	/// </summary>
	public enum YenconType
	{
		/// <summary>
		///  空の値を表します。
		/// </summary>
		NullOrEmpty,

		/// <summary>
		///  セクションを表します。
		/// </summary>
		Section,

		/// <summary>
		///  文字列キーを表します。
		/// </summary>
		StringKey,

		/// <summary>
		///  数値キーを表します。
		/// </summary>
		NumberKey,

		/// <summary>
		///  論理値キーを表します。
		/// </summary>
		BooleanKey
	}
}
