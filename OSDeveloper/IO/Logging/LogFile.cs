using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OSDeveloper.IO.Logging
{
	internal static class LogFile
	{
		private static bool          _is_initialized;
		private static FileStream    _fs;
		private static StreamWriter  _sw;
		private static List<LogData> _logs;

		static LogFile()
		{
			Init();
		}

		internal static void Init()
		{
			if (!_is_initialized) {
				var dt = DateTime.Now;
				int pid = Process.GetCurrentProcess().Id;
				_fs = new FileStream(SystemPaths.Logs.Bond($"{dt:yyyyMMddHHmmssfff}.[{pid}].log"),
					FileMode.Create, FileAccess.Write, FileShare.Read);
				_sw = new StreamWriter(_fs, Encoding.UTF8);
				_logs = new List<LogData>();
				_is_initialized = true;
			}
		}

		internal static void Final()
		{
			if (_is_initialized) {
				_sw.Close();
				_fs.Close();
				_logs.Clear();
				_logs = null;
				_is_initialized = false;
			}
		}

		internal static void Write(LogData log)
		{
			if (_is_initialized) {
				_logs.Add(log);
				_sw.Write(log.ToString());
			}
		}

		public static LogData[] GetLogs()
		{
			return _logs.ToArray();
		}
	}
}
