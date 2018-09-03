using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  ログを書き込みます。
	/// </summary>
	public partial class Logger
	{
		private string _name;

		/// <summary>
		///  このロガーの名前を取得します。
		/// </summary>
		public string LongName
		{
			get
			{
				return _name;
			}
		}

		/// <summary>
		///  このロガーの名前を16文字以内に収めて取得します。
		/// </summary>
		public string ShortName
		{
			get
			{
				if (_name.Length > 16) {
					return _name.Remove(13) + "...";
				} else {
					return _name;
				}
			}
		}

		/// <summary>
		///  このロガーの保存先の動作報告ファイルを取得します。
		/// </summary>
		public LogFile LogFile { get; }

		private Logger(string name, LogFile logFile)
		{
			_name = name;
			this.LogFile = logFile;
			this.Log(LogLevel.Trace, $"The new logger \'{name}\' was started.");
		}

		/// <summary>
		///  指定されたレベルでメッセージでログを書き込みます。
		/// </summary>
		/// <param name="lvl">ログレベルです。</param>
		/// <param name="msg">メッセージです。</param>
		public void Log(LogLevel lvl, string msg)
		{
			msg = msg.CRtoLF();
			if (msg.Contains("\n")) {
				string[] vs = msg.Split('\n');
				for (int i = 0; i < vs.Length; ++i) {
					LogData log = new LogData(lvl, this, vs[i]);
					this.LogFile.Write(log);
				}
			} else {
				LogData log = new LogData(lvl, this, msg);
				this.LogFile.Write(log);
			}
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Notice"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Notice(string msg)
		{
			this.Log(LogLevel.Notice, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Trace"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Trace(string msg)
		{
			this.Log(LogLevel.Trace, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Debug"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Debug(string msg)
		{
			this.Log(LogLevel.Debug, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Info"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Info(string msg)
		{
			this.Log(LogLevel.Info, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Warn"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Warn(string msg)
		{
			this.Log(LogLevel.Warn, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Error"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Error(string msg)
		{
			this.Log(LogLevel.Error, msg);
		}

		/// <summary>
		///  <see cref="OSDeveloper.Core.Logging.LogLevel.Fatal"/>レベルで
		///  指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">メッセージです。</param>
		public void Fatal(string msg)
		{
			this.Log(LogLevel.Fatal, msg);
		}
	}
}
