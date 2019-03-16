using System;
using System.Text;
using Yencon.Binary;
using Yencon.Exceptions;
using Yencon.Resources;

namespace Yencon
{
	/// <summary>
	///  バイナリ形式のヱンコンのヘッダー情報を表します。
	/// </summary>
	public struct YenconBinaryHeader
	{
		/// <summary>
		///  限定のヘッダー情報の値を取得します。
		/// </summary>
		public static readonly YenconBinaryHeader DEFAULT;

		static YenconBinaryHeader()
		{
			DEFAULT = new YenconBinaryHeader();
			DEFAULT.KeyNameSize    = KeyNameSize.Word;
			DEFAULT.KeyNameType    = KeyNameType.Ascii;
			DEFAULT.Implementation = 0x00;
			DEFAULT.Compatibility  = 0x00;
			DEFAULT.Revision       = 0x00;
		}

		/// <summary>
		///  キー名の長さを表す値の大きさを取得または設定します。
		/// </summary>
		public KeyNameSize KeyNameSize { get; set; }

		/// <summary>
		///  キー名の文字コードの種類を取得または設定します。
		/// </summary>
		public KeyNameType KeyNameType { get; set; }

		/// <summary>
		///  ファイルを保存したヱンコンの解析ライブラリの実装の種類を取得または設定します。
		/// </summary>
		public byte Implementation { get; set; }

		/// <summary>
		///  ファイルを保存したバイナリ形式のヱンコンフォーマットの互換性バージョンを取得または設定します。
		/// </summary>
		public byte Compatibility { get; set; }

		/// <summary>
		///  ファイルを保存したヱンコンの解析ライブラリの細かい改定バージョンを取得または設定します。
		/// </summary>
		public byte Revision { get; set; }

		/// <summary>
		///  このヘッダーのバージョンがこのライブラリで読み込めるかどうかを確認します。
		/// </summary>
		/// <returns>読み込める場合は<see langword="true"/>、読み込めない場合は<see langword="false"/>です。</returns>
		public bool CheckVersion()
		{
			return
				0x00 == this.Implementation &&
				0x00 == this.Compatibility  &&
				0x00 == this.Revision;
		}

		/// <summary>
		///  ヱンコンのキーの文字コードの種類を取得します。
		/// </summary>
		/// <returns>エンコーディング情報を表す型'<see cref="System.Text.Encoding"/>'のオブジェクトです。</returns>
		public Encoding GetEncoding()
		{
			switch (this.KeyNameType) {
				case KeyNameType.Utf16:
					return Encoding.Unicode;
				case KeyNameType.Ascii:
					return Encoding.ASCII;
				default:
					// ここには来ない筈だかnull回避の為に一応
					return Encoding.Default; //return null;
			}
		}

		/// <summary>
		///  ヘッダー情報をバイト配列に変換します。
		/// </summary>
		/// <returns>変換されたバイト配列です。</returns>
		public byte[] ToBinary()
		{
			byte[] head = new byte[12] {
				0x62, 0x59, 0x43, 0x4E,
				0x00,
				this.Implementation,
				this.Compatibility,
				this.Revision,
				0xFF, 0xFF, 0xFF, 0xFF
			};

			head[4] = ((byte)(
				((((byte)(this.KeyNameSize)) << 6) & 0b11000000) |
				((((byte)(this.KeyNameType)) << 5) & 0b00100000)));

			return head;
		}

		/// <summary>
		///  指定されたバイト配列から情報を読み取り、ヘッダー情報を現在のインスタンスに格納します。
		/// </summary>
		/// <param name="head">読み込み元のバイト配列です。</param>
		/// <exception cref="Yencon.Exceptions.InvalidHeaderException">
		///  ヘッダー情報が不正な場合に発生します。
		/// </exception>
		public void FromBinary(byte[] head)
		{
			if (head.Length < 12) {
				throw new InvalidHeaderException(ErrorMessages.InvalidHeaderException_Size);
			}

			if (head[0] != 0x62 || head[1] != 0x59 || head[2] != 0x43 || head[3] != 0x4E) {
				throw new InvalidHeaderException(ErrorMessages.InvalidHeaderException_Signature);
			}

			this.KeyNameSize = ((KeyNameSize)((head[4] & 0b11000000) >> 6));
			this.KeyNameType = ((KeyNameType)((head[4] & 0b00100000) >> 5));

			this.Implementation = head[5];
			this.Compatibility  = head[6];
			this.Revision       = head[7];
		}
	}

	namespace Binary
	{
		/// <summary>
		///  ヱンコンのキー名の長さを表す値の大きさを表します。
		/// </summary>
		public enum KeyNameSize : byte
		{
			/// <summary>
			///  符号無し8ビット整数値です。
			/// </summary>
			HWord = 0b00,

			/// <summary>
			///  符号無し16ビット整数値です。
			/// </summary>
			Word = 0b01,

			/// <summary>
			///  符号無し32ビット整数値です。
			/// </summary>
			DWord = 0b10,

			/// <summary>
			///  符号無し64ビット整数値です。
			/// </summary>
			[Obsolete("現在は利用していません。今後のバージョンで利用できる様になる予定です。")]
			QWord = 0b11
		}

		/// <summary>
		///  ヱンコンのキー名の文字コードの種類を表します。
		/// </summary>
		public enum KeyNameType : byte
		{
			/// <summary>
			///  <see langword="UTF-16"/>(<see langword="Unicode"/>)を表します。
			/// </summary>
			Utf16 = 0,

			/// <summary>
			///  <see langword="ASCII"/>を表します。
			/// </summary>
			Ascii = 1
		}
	}
}
