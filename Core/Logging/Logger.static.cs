using System;
using System.Collections.Generic;
using System.Diagnostics;
using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Logging
{
	partial class Logger
	{
		private static bool _is_initialized;
		private static Dictionary<string, Logger> _loggers;
		private static List<LogFile> _log_files;
		private static LogFile _system_log_file;

		static Logger()
		{
			Init();
			_system_log_file.Write(new LogData(LogLevel.Trace, GetSystemLogger("system"),
				"The logging system was initialized"));
		}

		/// <summary>
		///  静的コンストラクタが一度も呼び出されていない場合は、
		///  初期化を開始し、既に呼び出されている場合は何もしません。
		/// </summary>
		public static void Init()
		{
			if (!_is_initialized) {
				_loggers = new Dictionary<string, Logger>();
				_log_files = new List<LogFile>();
				_system_log_file = new ProcessReportRecordFile(
					SystemPaths.Logs.Bond($"{DateTime.Now:yyyy-MM-dd_HH}.[{Process.GetCurrentProcess().Id}].pr2f"));
				_is_initialized = true;
			}
		}

		/// <summary>
		///  この静的クラスで利用されている全てのリソースを破棄します。
		///  この関数の実行後、再度この静的クラスを利用する場合は、
		///  <see cref="OSDeveloper.Core.Logging.Logger.Init"/>を呼び出してください。
		/// </summary>
		public static void Final()
		{
			if (_is_initialized) {
				// _log_files 破棄
				foreach (var item in _log_files) {
					if (item != _system_log_file) {
						item.Dispose();
					}
				}
				_log_files.Clear();
				// _system_log_file 破棄
				_system_log_file.Write(new LogData(LogLevel.Trace, GetSystemLogger("system"),
					"The all loggers are closed"));
				_system_log_file.Dispose();
				// _loggers 破棄
				_loggers.Clear();
				// _is_initialized フラグ更新
				_is_initialized = false;
			}
		}

		/// <summary>
		///  ロガーの名前とログの保存先を指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.Logger"/>'の
		///  新しいインスタンスを生成します。
		///  既に同じ名前のロガーが存在する場合はそのオブジェクトのインスタンスを返し、ログの保存先は無視されます。
		/// </summary>
		/// <param name="name">ロガーの名前です。</param>
		/// <param name="logFile">ログの保存先です。</param>
		/// <returns>生成されたロガーです。</returns>
		public static Logger GetLogger(string name, LogFile logFile)
		{
			if (_is_initialized) {
				// ログファイルの保持
				if (!_log_files.Contains(logFile)) {
					_log_files.Add(logFile);
				}

				if (_loggers.ContainsKey(name)) { // 既に同名のロガーがある場合はそれを返す
					return _loggers[name];
				} else {                          // 無い場合は新たに生成してそれを返す
					var result = new Logger(name, logFile);
					_loggers.Add(name, result);
					return result;
				}
			} else { // 初期化が行われていない場合はダミーのログファイルを返す
				return new Logger(name, logFile);
			}
		}

		/// <summary>
		///  ロガーの名前を指定して、
		///  システムで利用されるロガーを取得または生成します。
		/// </summary>
		/// <param name="name">ロガーの名前です。</param>
		/// <returns>取得または生成されたロガーです。</returns>
		public static Logger GetSystemLogger(string name)
		{
			return GetLogger(name, _system_log_file);
		}

		/// <summary>
		///  ロガーの名前と追加のログの出力先を指定して、
		///  システムで利用されるロガーを取得または生成します。
		/// </summary>
		/// <param name="name">ロガーの名前です。</param>
		/// <param name="logFile">ログの出力先です。</param>
		/// <returns>取得または生成されたロガーです。</returns>
		public static Logger GetSystemLogger(string name, LogFile logFile)
		{
			return GetLogger(name, new MultipleLogFile(_system_log_file, logFile));
		}
	}
}
