using System;
using System.IO;
using System.Text;
using Yencon.Binary;

namespace Yencon
{
	/// <summary>
	///  ヱンコンをバイナリ形式に変換します。
	/// </summary>
	public class YenconBinaryConverter : IYenconConverter<byte[]>
	{
		/// <summary>
		///  最後に読み書きしたヱンコンファイルのヘッダー情報を取得または設定します。
		/// </summary>
		public YenconBinaryHeader Header;

		/// <summary>
		///  型'<see cref="Yencon.YenconBinaryConverter"/>'の新しいインスタンスを生成します。
		/// </summary>
		public YenconBinaryConverter()
		{
			Header = YenconBinaryHeader.DEFAULT;
		}

		/// <summary>
		///  指定されたファイルからバイナリ形式のヱンコンを読み取ります。
		/// </summary>
		/// <param name="filename">読み込み元のファイルのパスです。</param>
		/// <returns>読み込んだ全ての情報を保持するセクションオブジェクトです。</returns>
		/// <exception cref="System.ArgumentNullException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.FileNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.IOException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.Security.SecurityException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.UnauthorizedAccessException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ObjectDisposedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.Read(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="Yencon.Exceptions.InvalidHeaderException">
		///  ヘッダー情報が不正な場合に発生します。
		/// </exception>
		public YSection Load(string filename)
		{
			byte[] data;
			using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				// TODO: 64ビットの読み書きができない
				data = new byte[fs.Length];
				fs.Read(data, 0, data.Length);
			}
			return ToYencon(data);
		}

		/// <summary>
		///  指定されたファイルに指定されたヱンコン値をバイナリ形式で保存します。
		/// </summary>
		/// <param name="filename">書き込み先のファイルのパスです。</param>
		/// <param name="obj">書き込む全ての情報を保持するセクションオブジェクトです。</param>
		/// <exception cref="System.ArgumentNullException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.FileNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.IOException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.Security.SecurityException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.UnauthorizedAccessException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.IO.PathTooLongException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.FileStream(string, FileMode, FileAccess, FileShare)"/>と
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		/// <exception cref="System.ObjectDisposedException">
		///  この関数で利用しているオブジェクトがこの例外を発生させる可能性があります。
		///  <see cref="System.IO.FileStream.Write(byte[], int, int)"/>が原因に成り得ます。
		/// </exception>
		public void Save(string filename, YSection obj)
		{
			byte[] data = ToObject(obj);
			using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)) {
				fs.Write(data, 0, data.Length);
			}
		}

		/// <summary>
		///  指定されたバイト配列をヱンコンセクションに変換します。
		/// </summary>
		/// <param name="obj">ヱンコンセクションに変換するバイト配列です。</param>
		/// <returns>
		///  変換後のデータを保持するヱンコンセクションです。
		///  バージョンが一致しない場合は<see langword="null"/>が返ります。
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="obj"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		/// <exception cref="Yencon.Exceptions.InvalidHeaderException">
		///  ヘッダー情報が不正な場合に発生します。
		/// </exception>
		public YSection ToYencon(byte[] obj)
		{
			//// == null だと、正しく動作しない可能性がある
			//if (obj is null) throw new ArgumentNullException(nameof(obj));
			// byte[] は == の偽装 null の可能性はない、 is null ではなく == null を使う
			if (obj == null) throw new ArgumentNullException(nameof(obj));

			YSection result;

			using (var ms = new MemoryStream(obj))
			using (var br = new BinaryReader(ms)) {
				// ヘッダー情報の取得
				Header.FromBinary(br.ReadBytes(12));
				// バージョンを確認
				if (Header.CheckVersion()) {
					// ルートセクションを取得
					result = ReadSection(br);
				} else {
					// バージョンが合わない場合は読み込みを停止する
					result = null;
				}
			}

			return result;
		}

		/// <summary>
		///  指定されたヱンコンセクションをバイナリ形式に変換します。
		/// </summary>
		/// <param name="obj">バイナリ形式に変換するヱンコンセクションです。</param>
		/// <returns>変換後のデータを保持するバイト配列です。</returns>
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="obj"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
		public byte[] ToObject(YSection obj)
		{
			obj = obj ?? throw new ArgumentNullException(nameof(obj));
			byte[] head = Header.ToBinary();
			byte[] data = obj.ToBinary();
			byte[] result = new byte[head.Length + data.Length];
			head.CopyTo(result, 0);
			data.CopyTo(result, head.Length);
			return result;
		}

		private YSection ReadSection(BinaryReader br)
		{
			var result = new YSection();

			// キーの数を取得
			uint kc = br.ReadUInt32();

			// キーの数だけ繰り返す
			for (uint i = 0; i < kc; ++i) {
				// 名前の文字列長を取得
				ulong nmsz;
				switch (Header.KeyNameSize) {
					// 8ビット
					case KeyNameSize.HWord:
						nmsz = br.ReadByte();
						break;
					// 16ビット
					case KeyNameSize.Word:
						nmsz = br.ReadUInt16();
						break;
					// 32ビット
					case KeyNameSize.DWord:
						nmsz = br.ReadUInt32();
						break;
#if false
					// 64ビット
					case KeyNameSize.QWord:
						nmsz = br.ReadUInt64();
						break;
#endif
					default:
						// ここには来ない筈
						nmsz = 0;
						break;
				}
				// 文字列値分のバイト配列を取得
				byte[] bName = br.ReadBytes(unchecked((int)(nmsz)));
				// バイト配列をデコードして名前に変換
				string name = Header.GetEncoding().GetString(bName);
				// 型識別子の取得
				byte t = br.ReadByte();
				// 値の取得
				YNode node;
				switch (t) {
					// セクション
					case 0x01:
						node = ReadSection(br);
						break;
					// 文字列
					case 0x02:
						var str = new YString();
						int sz = br.ReadInt32();
						byte[] bStr = br.ReadBytes(sz);
						string val = Encoding.Unicode.GetString(bStr);
						str.SetEscapedText(val);
						node = str;
						break;
					// 整数値
					case 0x03:
						node = new YNumber() { UInt64Value = br.ReadUInt64() };
						break;
					// 真理値
					case 0x04:
						node = new YBoolean() { Flag = true };
						break;
					// 偽理値
					case 0x05:
						node = new YBoolean() { Flag = false };
						break;
					// NULL値 | 空値
					default: // case 0x00:
						node = new YNullOrEmpty();
						break;
				}
				// 名前を設定して現在のセクションに追加
				node.Name = name;
				result.Add(node);
			}

			// 作成したセクションを呼び出し元に返す
			return result;
		}
	}
}
