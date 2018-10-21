using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)を
	///  バイナリ形式で読み込みます。このクラスは静的です。
	/// </summary>
	public static class YenconBinaryFile
	{
		private static Encoding _ascii = new ASCIIEncoding();
		private static Encoding _utf16 = new UnicodeEncoding();

		/// <summary>
		///  指定されたバイナリデータを解析してその結果を取得します。
		/// </summary>
		/// <param name="buf">読み取り元のバイト配列です。</param>
		/// <param name="header">バイナリデータのヘッダー情報を表すオブジェクトです。</param>
		/// <returns><see cref="OSDeveloper.Core.Settings.YenconNode"/>のリストオブジェクトです。</returns>
		/// <exception cref="System.ArgumentNullException"/>
		public static List<YenconNode> Scan(byte[] buf, YenconHeader header)
		{
			List<YenconNode> result;
			using (MemoryStream ms = new MemoryStream(buf)) {
				if (ms.Length == 0) {
					return null;
				}
				result = Scan(ms, header);
			}
			return result;
		}

		/// <summary>
		///  指定されたパスからファイルを読み取り、そのバイナリデータを解析してその結果を取得します。
		/// </summary>
		/// <param name="fname">読み取り元のファイルのパスです。</param>
		/// <param name="header">バイナリデータのヘッダー情報を表すオブジェクトです。</param>
		/// <returns><see cref="OSDeveloper.Core.Settings.YenconNode"/>のリストオブジェクトです。</returns>
		/// <exception cref="System.ArgumentNullException"/>
		/// <exception cref="System.IO.IOException"/>
		public static List<YenconNode> Scan(string fname, YenconHeader header)
		{
			List<YenconNode> result;
			if (!File.Exists(fname)) return null;
			using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				if (fs.Length == 0) {
					return null;
				}
				result = Scan(fs, header);
			}
			return result;
		}

		/// <summary>
		///  指定されたヱンコンデータをバイト配列に変換します。
		/// </summary>
		/// <param name="nodes">変換するヱンコンデータです。</param>
		/// <param name="header">バイナリデータのヘッダー情報を表すオブジェクトです。</param>
		/// <returns>
		///  変換結果のバイナリデータです。読み取るには
		///  <see cref="OSDeveloper.Core.Settings.YenconBinaryFile.Scan(byte[], YenconHeader)"/>
		///  を利用します。
		/// </returns>
		public static byte[] ConvertToBinary(List<YenconNode> nodes, YenconHeader header)
		{
			if (header == null) throw new ArgumentNullException(nameof(header));

			byte[] result;
			using (MemoryStream ms = new MemoryStream())
			using (BinaryWriter bw = new BinaryWriter(ms)) {
				// シグネチャ 書き込み
				bw.Write(new byte[] { 0x62, 0x59, 0x43, 0x4E });
				// キー名モード 書き込み
				byte keynamemode;
				switch (header.KeyNameSize) {
					case 8:
						keynamemode = 0b00000000;
						break;
					case 16:
						keynamemode = 0b01000000;
						break;
					case 32:
						keynamemode = 0b10000000;
						break;
					default:
						keynamemode = 0b11000000;
						break;
				}
				if (header.UseAsciiKeyName) {
					keynamemode |= 0b00100000;
				}
				bw.Write(keynamemode);
				// フォーマットバージョン 書き込み
				bw.Write(header.Version.major);
				bw.Write(header.Version.minor);
				bw.Write(header.Version.other);
				// 予約済み
				bw.Write(0xFFFFFFFF);
				// 実際のデータ 書き込み
				if (header.Version.major == 0x00 && header.Version.minor == 0x00 && header.Version.other == 0x00) {
					Convert_00_00_00(bw, nodes, header);
				}

				result = ms.ToArray();
			}
			return result;
		}

		private static List<YenconNode> Scan(Stream s, YenconHeader header)
		{
			if (header == null) throw new ArgumentNullException(nameof(header));

			using (BinaryReader br = new BinaryReader(s)) {
				// シグネチャ認識
				byte[] sig = br.ReadBytes(4);
				if (sig[0] == 0x62 && sig[1] == 0x59 && sig[2] == 0x43 && sig[3] == 0x4E) {
					// キー名モード取得
					byte keynamemode = br.ReadByte();
					switch (keynamemode & 0b11000000) {
						case 0b00000000:
							header.KeyNameSize = 8;
							break;
						case 0b01000000:
							header.KeyNameSize = 16;
							break;
						case 0b10000000:
							header.KeyNameSize = 32;
							break;
						default:
							return null;
					}
					header.UseAsciiKeyName = (keynamemode & 0b00100000) == 0b00100000;
					// フォーマットバージョン取得
					byte[] ver = br.ReadBytes(3);
					header.Version = (ver[0], ver[1], ver[2]);
					if (ver[0] == 0x00 && ver[1] == 0x00 && ver[2] == 0x00) {
						br.ReadBytes(4); // 予約済みの領域を無視
						return Scan_00_00_00(br, header);
					} else {
						// 対応してないバージョンなら
						return null;
					}
				} else {
					// シグネチャが違う
					return null;
				}
			}
		}

		private static List<YenconNode> Scan_00_00_00(BinaryReader br, YenconHeader header)
		{
			List<YenconNode> nodes = new List<YenconNode>();

			uint keys = br.ReadUInt32();
			for (uint i = 0; i < keys; ++i) {
				// 名前取得
				int length;
				if (header.KeyNameSize == 8) {
					length = br.ReadByte();
				} else if (header.KeyNameSize == 16) {
					length = br.ReadUInt16();
				} else /*if (header.KeyNameSize == 32)*/ {
					length = br.ReadInt32();
				}
				string namet;
				byte[] nameb = br.ReadBytes(length);
				if (header.UseAsciiKeyName) {
					namet = _ascii.GetString(nameb);
				} else {
					namet = _utf16.GetString(nameb);
				}

				// ノード生成
				YenconNode yn = new YenconNode();
				yn.Name = namet;

				// 型と値の取得
				byte t = br.ReadByte();
				switch (t) {
					case 0x00: // NULL
						yn.Value = new YenconNullValue();
						break;
					case 0x01: // セクション
						yn.Value = new YenconSection(Scan_00_00_00(br, header));
						break;
					case 0x02: // 文字列
						var str = new YenconStringKey();
						int len = br.ReadInt32();
						byte[] vs = br.ReadBytes(len);
						str.Text = _utf16.GetString(vs).Escape();
						yn.Value = str;
						break;
					case 0x03: // 数値
						var num = new YenconNumberKey();
						num.Count = br.ReadUInt64();
						yn.Value = num;
						break;
					case 0x04: // 論理値 TRUE
						yn.Value = new YenconBooleanKey() { Flag = true };
						break;
					case 0x05: // 論理値 FALSE
						yn.Value = new YenconBooleanKey() { Flag = false };
						break;
					default: // 不明な型
						nodes.Clear();
						return null;
				}

				// ノード追加
				nodes.Add(yn);
			}

			return nodes;
		}

		private static void Convert_00_00_00(BinaryWriter bw, List<YenconNode> nodes, YenconHeader header)
		{
			// コメント削除
			List<YenconNode> nodes2 = new List<YenconNode>();
			foreach (var item in nodes) {
				if (!(item is YenconComment)) {
					nodes2.Add(item);
				}
			}

			bw.Write(nodes2.Count);
			for (int i = 0; i < nodes2.Count; ++i) {
				var item = nodes2[i];

				// 名前設定
				byte[] nameb;
				if (header.UseAsciiKeyName) {
					nameb = _ascii.GetBytes(item.Name);
				} else {
					nameb = _utf16.GetBytes(item.Name);
				}
				switch (header.KeyNameSize) {
					case 8:
						bw.Write((byte)(nameb.Length));
						break;
					case 16:
						bw.Write((ushort)(nameb.Length));
						break;
					case 32:
						bw.Write((uint)(nameb.Length));
						break;
					default:
						throw new InvalidDataException(string.Format(ErrorMessages.YenconBinaryFile_InvalidBitSize, header.KeyNameSize));
				}
				bw.Write(nameb);

				// 型と値の設定
				switch (item.Kind) {
					case YenconType.Section:
						bw.Write((byte)(0x01)); // 型識別子
						var r = new List<YenconNode>(((YenconSection)(item.Value)).Children.Values);
						Convert_00_00_00(bw, r, header);
						break;
					case YenconType.StringKey:
						bw.Write((byte)(0x02)); // 型識別子
						byte[] vs = _utf16.GetBytes(((YenconStringKey)(item.Value)).Text.Unescape());
						bw.Write(vs.Length);
						bw.Write(vs);
						break;
					case YenconType.NumberKey:
						bw.Write((byte)(0x03)); // 型識別子
						bw.Write(((YenconNumberKey)(item.Value)).Count);
						break;
					case YenconType.BooleanKey:
						if (((YenconBooleanKey)(item.Value)).Flag) {
							bw.Write((byte)(0x04)); // 型識別子
						} else {
							bw.Write((byte)(0x05)); // 型識別子
						}
						break;
					default:
						bw.Write((byte)(0x00)); // 型識別子
						break;
				}
			}
		}
	}

	/// <summary>
	///  バイナリ形式のヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)のヘッダー情報を取得します。
	///  この型は継承できません。
	/// </summary>
	public sealed class YenconHeader
	{
		/// <summary>
		///  キー名の大きさを指定する整数型のビット数を取得または設定します。
		/// </summary>
		public int KeyNameSize { get; set; }

		/// <summary>
		///  キー名でASCIIを利用するかどうかを表す論理値を取得または設定します。
		/// </summary>
		public bool UseAsciiKeyName { get; set; }

		/// <summary>
		///  キー名でUnicode(UTF-16)を利用するかどうかを表す論理値を取得または設定します。
		/// </summary>
		public bool UseUtf16KeyName
		{
			get
			{
				return !this.UseAsciiKeyName;
			}

			set
			{
				this.UseAsciiKeyName = !value;
			}
		}

		/// <summary>
		///  ファイルの解析に利用するパーサーのバージョンを取得または設定します。
		/// </summary>
		public (byte major, byte minor, byte other) Version { get; set; }
	}
}
