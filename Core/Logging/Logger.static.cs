using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Logging
{
	partial class Logger
	{
		private readonly static Dictionary<string, Logger> _loggers = new Dictionary<string, Logger>();
		private readonly static List<MultipleLogFile> _multiple_logs = new List<MultipleLogFile>();
		private readonly static LogFile _system_log_file =
			//new LogFile(SystemPaths.Logs.Bond($"{DateTime.Now:yyyy-MM-dd_HH}.[{Process.GetCurrentProcess().Id}].log"));
			new ProcessReportRecordFile(SystemPaths.Logs.Bond($"{DateTime.Now:yyyy-MM-dd_HH}.[{Process.GetCurrentProcess().Id}].pr2f"));

		static Logger()
		{
			Application.ApplicationExit += Application_ApplicationExit;
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			_system_log_file.Write(new LogData(LogLevel.Trace, GetSystemLogger("system"),
				"The aplication is shutdowning..."));
			_system_log_file.Dispose();
			foreach (var item in _multiple_logs) {
				item.Dispose();
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
			if (_loggers.ContainsKey(name)) {
				return _loggers[name];
			} else {
				var result = new Logger(name, logFile);
				_loggers.Add(name, result);
				return result;
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
			var f = new MultipleLogFile(_system_log_file, logFile);
			_multiple_logs.Add(f);
			return GetLogger(name, f);
		}
	}
}
