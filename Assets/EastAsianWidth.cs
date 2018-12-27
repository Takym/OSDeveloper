using System;
using System.IO;
using System.Text;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Assets
{
	/// <summary>
	///  <see langword="Unicode"/>の文字幅を表します。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class EastAsianWidth : IDisposable
	{
		/// <summary>
		///  <see langword="Unicode"/>に収録されている文字の総数を表します。
		/// </summary>
		public const int TotalUnicodeCharacters = 0x110000;

		/// <summary>
		///  メモリ上に読み込まれる文字の総数を表します。
		/// </summary>
		public const int MemoryUnicodeCharacters = 0x10000;

		private int _offset;
		private EAWType[] _eaws;
		private FileStream _fs;
		private bool _is_disposed;

		/// <summary>
		///  型'<see cref="OSDeveloper.Assets.EastAsianWidth"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public EastAsianWidth()
		{
			_offset = 0;
			_eaws = new EAWType[MemoryUnicodeCharacters];
			_fs = new FileStream(CreateFileName(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			_is_disposed = false;
			this.CheckHeader();
		}

		private static string CreateFileName()
		{
			var dt = DateTime.Now;
			var wk = ((char)('a' + dt.Day / 7));
			return SystemPaths.Temporary.Bond($"{dt:yyyyMM}{wk}.eaw");
		}

		private void CheckHeader()
		{
			if (_fs.Length != TotalUnicodeCharacters) {
				_fs.SetLength(TotalUnicodeCharacters);
				this.Parse();
			} else {
				for (int i = 0; i < MemoryUnicodeCharacters; ++i) {
					int v = _fs.ReadByte();
					if (0 <= v && v < 6) {
						_eaws[i] = ((EAWType)(v));
					}
				}
			}
		}

		private void Parse()
		{
			using (StreamReader sr = new StreamReader(SystemPaths.Resources.Bond("EastAsianWidth.txt"))) {
				while (!sr.EndOfStream) {
					string s = sr.ReadLine();
					// TODO: EastAsianWidth.txtの解析処理
				}
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Assets.EastAsianWidth"/>'の
		///  現在のインスタンスを破棄します。
		/// </summary>
		~EastAsianWidth()
		{
			this.Dispose(false);
		}

		/// <summary>
		///  現在のオブジェクトで利用されているリソースを開放します。
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		void Dispose(bool disposing)
		{
			if (!_is_disposed) {
				if (disposing) {
				}
				_eaws = null;
				_is_disposed = true;
			}
		}
	}
}
