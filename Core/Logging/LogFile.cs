using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  ログファイルを読み書きします。
	///  このファイルは通常、ロガーによって管理されます。
	/// </summary>
	public class LogFile : IDisposable
	{
		private TextWriter _tw;

		/// <summary>
		///  内部ログファイルを生成しない場合は<see langword="true"/>、生成する場合は<see langword="false"/>です。
		/// </summary>
		public static bool NoInternalLog { get; internal set; }

		/// <summary>
		///  内部ログファイルのファイル名の種類です。
		/// </summary>
		public static ulong InternalNameKind { get; internal set; }

		/// <summary>
		///  このオブジェクトが破棄されているかどうかを表す論理値を取得します。
		/// </summary>
		protected bool IsDisposed
		{
			get
			{
				return _is_disposed;
			}
		}
		private bool _is_disposed;

		/// <summary>
		///  書き込まれた全てのログを取得します。
		/// </summary>
		public virtual LogData[] LogDatas
		{
			get
			{
				return _log_datas.ToArray();
			}
		}
		private List<LogData> _log_datas;

		/// <summary>
		///  ファイルからインスタンスを生成した場合、
		///  そのファイルのファイル名を取得します。
		/// </summary>
		public string FileName
		{
			get
			{
				return _fname;
			}

			protected set
			{
				_fname = value;
			}
		}
		private string _fname;

		/// <summary>
		///  書き込み先のファイルのパスを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.LogFile"/>'の
		///  新しいインスタンスを生成します。
		///  既にファイルが存在する場合はファイルの末尾に追加します。
		/// </summary>
		/// <param name="filename">ファイル名です。</param>
		public LogFile(string filename)
		{
			_is_disposed = false;
			_tw = new StreamWriter(filename, true, Encoding.UTF8);
			_log_datas = new List<LogData>();
			_fname = filename;
		}

		/// <summary>
		///  書き込み先のストリームを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.LogFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="stream">書き込み先のストリームです。</param>
		public LogFile(Stream stream)
		{
			_is_disposed = false;
			_tw = new StreamWriter(stream, Encoding.UTF8);
			_log_datas = new List<LogData>();
		}

		/// <summary>
		///  書き込み先のテキストライターを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.LogFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="writer">書き込み先のテキストライターです。</param>
		public LogFile(TextWriter writer)
		{
			_is_disposed = false;
			_tw = writer;
			_log_datas = new List<LogData>();
		}

		/// <summary>
		///  このクラスが継承された場合に呼び出されるコンストラクタです。
		/// </summary>
		protected LogFile()
		{
			_is_disposed = false;
			if (NoInternalLog) {
				_tw = new StringWriter();
			} else {
				var dt = DateTime.Now;
				var pid = Process.GetCurrentProcess().Id;
				var guid = Guid.NewGuid();
				var rfn = Path.GetRandomFileName();
				switch (InternalNameKind) {
					case 1:
						_fname = SystemPaths.Logs.Bond($"{dt:yyyy-MM-dd_HH}.[{pid}].{guid}.log");
						break;
					case 2: {
						var dir = SystemPaths.Logs.Bond($"{dt:yyyy-MMdd}");
						Directory.CreateDirectory(dir);
						_fname = dir.Bond($"{dt:MMdd-HH}_[{pid}].{{{guid}}}.{rfn}.log");
						break;
					}
					case 3: {
						var dir = SystemPaths.Logs.Bond($"{dt:yyyy-MMdd}");
						Directory.CreateDirectory(dir);
						_fname = dir.Bond($"PID:{pid}__{rfn}.log");
						break;
					}
					default:
						_fname = SystemPaths.Logs.Bond($"z_internal.{rfn}.log");
						break;

				}
				_tw = new StreamWriter(_fname);
			}
			_log_datas = new List<LogData>();
		}

		/// <summary>
		///  指定されたログを書き込みます。
		/// </summary>
		/// <param name="log">書き込むログデータです。</param>
		public virtual void Write(LogData log)
		{
			_log_datas.Add(log);
			_tw.Write/*Line*/(log.ToString());
		}

		#region IDisposable Support
		/// <summary>
		///  現在のオブジェクトのインスタンスを破棄します。
		/// </summary>
		~LogFile()
		{
			this.Dispose(false);
		}

		/// <summary>
		///  現在のインスタンスで利用されているリソースを解放します。
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///  現在のインスタンスで利用されているアンマネージオブジェクトと
		///  オプションでマネージドオブジェクトを解放します。
		/// </summary>
		/// <param name="disposing">
		///  マネージドオブジェクトも破棄する場合は<see langword="true"/>、
		///  アンマネージオブジェクトのみ破棄する場合は<see langword="false"/>です。
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_is_disposed) {
				if (disposing) {
					_tw.Flush();
					_tw.Close();
				}
				_log_datas.Clear();
			   _is_disposed = true;
			}
		}
		#endregion
	}
}
