using System;
using System.Collections.Generic;
using System.IO;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)を
	///  バイナリ形式で読み込みます。このクラスは継承できません。
	/// </summary>
	public sealed class YenconBinaryFile
	{
		private readonly byte[] _in_binary;
		private List<YenconNode> _result;

		/// <summary>
		///  読み込むバイナリデータを指定して、
		///  型'<see cref="OSDeveloper.Core.Settings"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="buf">バイナリデータです。</param>
		public YenconBinaryFile(byte[] buf)
		{
			_in_binary = buf;
		}

		/// <summary>
		///  このインスタンスに設定されているバイナリデータを解析してその結果を取得します。
		/// </summary>
		/// <param name="header">バイナリデータのヘッダー情報を表すオブジェクトです。</param>
		/// <returns><see cref="OSDeveloper.Core.Settings.YenconNode"/>のリストオブジェクトです。</returns>
		/// <exception cref="System.ArgumentNullException"/>
		public List<YenconNode> Scan(YenconHeader header)
		{
			if (header == null) throw new ArgumentNullException(nameof(header));
			if (_result != null) return _result;
			List<YenconNode> nodes = new List<YenconNode>();

			using (MemoryStream ms = new MemoryStream(_in_binary)) {
				byte[] sig = new byte[4];
				ms.Read(sig, 0, sig.Length);
				if (sig[0] == 0x62 &&
					sig[1] == 0x59 &&
					sig[2] == 0x43 &&
					sig[3] == 0x4E) {
					byte keynamemode = ((byte)(ms.ReadByte()));
					// TODO: 未完成
				} else {
					return null;
				}
			}

			_result = nodes;
			return nodes;
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
		///  キー名でASCIIを利用するかどうかを表す論理値を取得します。
		///  この値を変更するには<see cref="OSDeveloper.Core.Settings.YenconHeader.UseAsciiKeyName"/>を利用してください。
		/// </summary>
		public bool UseUtf16KeyName
		{
			get
			{
				return !this.UseAsciiKeyName;
			}
		}

		/// <summary>
		///  ファイルの解析に利用するパーサーのバージョンを取得または設定します。
		/// </summary>
		public int Version { get; set; }
	}
}
