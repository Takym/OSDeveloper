using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  処理報告記録ファイルを読み書きします。
	///  このファイルは通常、ロガーによって管理されます。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class ProcessReportRecordFile : LogFile
	{
		private static readonly XmlSerializer _xs = new XmlSerializer(typeof(List<LogEntry>));

		private XmlWriter _xw;

		/// <summary>
		///  書き込まれたログエントリを取得します。
		/// </summary>
		public List<LogEntry> Entries { get; }

		/// <summary>
		///  書き込み先のファイルのパスを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile"/>'の
		///  新しいインスタンスを生成します。
		///  既にファイルが存在する場合は上書きされます。
		/// </summary>
		/// <param name="filename">ファイル名です。</param>
		public ProcessReportRecordFile(string filename)
		{
			_xw = XmlWriter.Create(filename);
			this.Entries = new List<LogEntry>();
			this.FileName = filename;
		}

		/// <summary>
		///  書き込み先のストリームを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="stream">書き込み先のストリームです。</param>
		public ProcessReportRecordFile(Stream stream)
		{
			_xw = XmlWriter.Create(stream);
			this.Entries = new List<LogEntry>();
		}

		/// <summary>
		///  書き込み先のテキストライターを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="writer">書き込み先のテキストライターです。</param>
		public ProcessReportRecordFile(TextWriter writer)
		{
			_xw = XmlWriter.Create(writer);
			this.Entries = new List<LogEntry>();
		}

		/// <summary>
		///  書き込み先のXMLライターを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="writer">書き込み先のXMLライターです。</param>
		public ProcessReportRecordFile(XmlWriter writer)
		{
			_xw = writer;
			this.Entries = new List<LogEntry>();
		}

		/// <summary>
		///  指定されたログを書き込みます。
		/// </summary>
		/// <param name="log">書き込むログデータです。</param>
		public override void Write(LogData log)
		{
			base.Write(log);
			this.Entries.Add(new LogEntry(log));
		}

		/// <summary>
		///  ログを保存します。
		/// </summary>
		public void Save()
		{
			_xs.Serialize(_xw, this.Entries);
		}

		/// <summary>
		///  現在のインスタンスで利用されているアンマネージオブジェクトと
		///  オプションでマネージドオブジェクトを解放します。
		/// </summary>
		/// <param name="disposing">
		///  マネージドオブジェクトも破棄する場合は<see langword="true"/>、
		///  アンマネージオブジェクトのみ破棄する場合は<see langword="false"/>です。
		/// </param>
		protected override void Dispose(bool disposing)
		{
			this.Save();
			base.Dispose(disposing);
			if (disposing) {
				_xw.Close();
			}
			this.Entries.Clear();
		}

		/// <summary>
		///  ログエントリです。
		/// </summary>
		public class LogEntry
		{
			/// <summary>
			///  作成日時を取得または設定します。
			/// </summary>
			public DateTime Created { get; set; }

			/// <summary>
			///  ログレベルを取得または設定します。
			/// </summary>
			public LogLevel Level { get; set; }

			/// <summary>
			///  ロガー名を取得または設定します。
			/// </summary>
			public string LoggerName { get; set; }

			/// <summary>
			///  メッセージを取得または設定します。
			/// </summary>
			public string Message { get; set; }

			/// <summary>
			///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile.LogEntry"/>'の
			///  新しいインスタンスを生成します。
			/// </summary>
			public LogEntry() { }

			/// <summary>
			///  型'<see cref="OSDeveloper.Core.Logging.ProcessReportRecordFile.LogEntry"/>'の
			///  新しいインスタンスを生成します。
			/// </summary>
			/// <param name="log">エントリの初期値となるオブジェクトです。</param>
			public LogEntry(LogData log)
			{
				this.Created = log.CreatedDate;
				this.Level = log.Level;
				this.LoggerName = log.Logger.LongName;
				this.Message = log.Message;
			}
		}
	}
}
