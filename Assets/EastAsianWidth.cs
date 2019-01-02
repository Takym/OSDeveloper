using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Assets
{
	/// <summary>
	///  <see langword="Unicode"/>の文字幅を表します。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class EastAsianWidth : IDisposable
	{
		#region 動的

		private int _offset;
		private EAWType[] _eaws;
		private FileStream _fs;
		private StringBuilder _sb;
		private bool _is_disposed;

		/// <summary>
		///  型'<see cref="OSDeveloper.Assets.EastAsianWidth"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.Data.SyntaxErrorException"/>
		private EastAsianWidth()
		{
			_offset = 0;
			_eaws = new EAWType[MemoryUnicodeCharacters];
			_fs = new FileStream(CreateFileName(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			_sb = new StringBuilder();
			_is_disposed = false;
			this.CheckHeader();
			_logger.Trace($"{nameof(EastAsianWidth)} was initialized");
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
					int num1, num2;

					// 空白無視
ignore_ws:
					char c = ((char)(sr.Peek()));
					if (c.IsWhitespace()) {
						sr.Read();
						goto ignore_ws;
					}

					// コメントを無視
					if (c == '#') {
						sr.ReadLine();
						continue;
					}

					// 数値1を取得
					num1 = this.ReadInt(sr);
					if (num1 == -1) {
						throw new SyntaxErrorException();
					}

					c = ((char)(sr.Read()));
					if (c == '.' && ((char)(sr.Read())) == '.') {
						// 数値2を取得
						num2 = this.ReadInt(sr);
						if (num2 == -1) {
							throw new SyntaxErrorException();
						}
						// セミコロンをスキップ
						c = ((char)(sr.Read()));
						if (c != ';') {
							throw new SyntaxErrorException();
						}
						// 値を設定
						var t = this.ReadType(sr);
						for (; num1 <= num2; ++num1) {
							this.SetValue(num1, t);
						}
					} else if (c == ';') {
						// 値を設定
						this.SetValue(num1, this.ReadType(sr));

					} else {
						throw new SyntaxErrorException();
					}
				}
			}
		}

		private int ReadInt(StreamReader sr)
		{
			_sb.Clear();
			while (!sr.EndOfStream && ((char)(sr.Peek())).IsHexadecimal()) {
				_sb.Append((char)(sr.Read()));
			}
			if (int.TryParse(_sb.ToString(), NumberStyles.HexNumber, null, out int result)) {
				return result;
			} else {
				return -1;
			}
		}

		private EAWType ReadType(StreamReader sr)
		{
			switch ((char)(sr.Read())) {
				case 'F':
					return EAWType.Fullwidth;
				case 'H':
					return EAWType.Halfwidth;
				case 'W':
					return EAWType.Wide;
				case 'A':
					return EAWType.Ambiguous;
				case 'N':
					if (((char)(sr.Peek())) == 'a') {
						sr.Read();
						return EAWType.Narrow;
					} else {
						return EAWType.Neutral;
					}
				default:
					throw new SyntaxErrorException();
			}
		}

		private void SetValue(int i, EAWType t)
		{
			_fs.Position = i;
			_fs.WriteByte((byte)(t));
			if (_offset <= i && i < _eaws.Length) {
				_eaws[i - _offset] = t;
			}
		}

		/// <summary>
		///  指定された文字のEAW値を取得します。
		/// </summary>
		/// <param name="i">取得する字の<see langword="Unicode"/>値です。</param>
		/// <returns>型'<see cref="OSDeveloper.Assets.EAWType"/>'の値です。</returns>
		public EAWType GetValue(uint i)
		{
			EAWType result;
			if (_offset <= i && i < _eaws.Length) {
				result = _eaws[i - _offset];
			} else {
				_fs.Position = i;
				result = ((EAWType)(_fs.ReadByte()));
			}
			_logger.Debug($"EAW of {i:X04} = {result}");
			return result;
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
					_fs.Close();
				}
				_eaws = null;
				_sb.Clear();
				_sb = null;
				_is_disposed = true;
				_logger.Trace($"{nameof(EastAsianWidth)} was disposed");
			}
		}

		#endregion

		#region 静的

		/// <summary>
		///  <see langword="Unicode"/>に収録されている文字の総数を表します。
		/// </summary>
		public const int TotalUnicodeCharacters = 0x110000;

		/// <summary>
		///  メモリ上に読み込まれる文字の総数を表します。
		/// </summary>
		public const int MemoryUnicodeCharacters = 0x10000;

		private static Logger _logger;
		private static bool _initialized;
		private static EastAsianWidth _inst;

		static EastAsianWidth()
		{
			Init();
			_logger.Trace($"Initialized {nameof(EastAsianWidth)}");
		}

		/// <summary>
		///  静的コンストラクタが一度も呼び出されていない場合は、
		///  初期化を開始し、既に呼び出されている場合は何もしません。
		/// </summary>
		public static void Init()
		{
			if (!_initialized) {
				_logger = Logger.GetSystemLogger(nameof(EastAsianWidth));
				_logger.Trace("constructing the new static instance...");
				_inst = new EastAsianWidth();
				_logger.Trace("constructed successfully");
				_initialized = true;
			}
		}

		/// <summary>
		///  この静的クラスで利用されている全てのリソースを破棄します。
		///  この関数の実行後、再度この静的クラスを利用する場合は、
		///  <see cref="OSDeveloper.Assets.FontResources.Init"/>を呼び出してください。
		/// </summary>
		public static void Final()
		{
			if (_initialized) {
				_logger.Trace("destructing the current static instance...");
				_inst.Dispose();
				_initialized = false;
			}
		}

		/// <summary>
		///  現在のインスタンスを取得します。
		/// </summary>
		public static EastAsianWidth Current
		{
			get
			{
				return _inst;
			}
		}

		#endregion
	}
}
