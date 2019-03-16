using System.Collections.Generic;
using OSDeveloper.GraphicalUIs.Terminal;

namespace OSDeveloper.IO.Logging
{
	partial class Logger
	{
		private static bool _is_initialized;
		private static Dictionary<string, Logger> _loggers;

		static Logger()
		{
			Init();
		}

		internal static void Init()
		{
			if (!_is_initialized) {
				_loggers = new Dictionary<string, Logger>();
				_is_initialized = true;
			}
		}

		internal static void Final()
		{
			if (_is_initialized) {
				_loggers.Clear();
				_loggers = null;
				_is_initialized = false;
			}
		}

		public static Logger Get(string name)
		{
			if (_loggers.ContainsKey(name)) {
				return _loggers[name];
			} else {
				var result = new Logger(name);
				_loggers.Add(name, result);
				return result;
			}
		}

		public static string[] GetLoggerNames()
		{
			string[] result = new string[_loggers.Keys.Count];
			_loggers.Keys.CopyTo(result, 0);
			return result;
		}
	}
}
