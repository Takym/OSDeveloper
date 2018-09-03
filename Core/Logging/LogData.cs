using System;

namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  一つのログメッセージを表します。
	///  この型は不変です。
	/// </summary>
	public class LogData : IComparable<LogData>
	{
		/// <summary>
		///  ログの作成日時を取得します。
		/// </summary>
		public DateTime CreatedDate { get; }

		/// <summary>
		///  ログレベルを取得します。
		/// </summary>
		public LogLevel Level { get; }

		/// <summary>
		///  このログを作成したロガーを取得します。
		/// </summary>
		public Logger Logger { get; }

		/// <summary>
		///  このログのメッセージを取得します。
		/// </summary>
		public string Message { get; }

		/// <summary>
		///  ログ情報を指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.LogData"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="level">ログレベルです。</param>
		/// <param name="logger">このログデータを作成したロガーです。</param>
		/// <param name="msg">メッセージです。</param>
		public LogData(LogLevel level, Logger logger, string msg)
		{
			this.CreatedDate = DateTime.Now;
			this.Level = level;
			this.Logger = logger;
			this.Message = msg;
		}

		/// <summary>
		///  指定されたログデータのログレベルとロガーをコピーして、
		///  型'<see cref="OSDeveloper.Core.Logging.LogData"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="obj">コピー元のログデータです。</param>
		/// <param name="msg">新しいメッセージです。</param>
		public LogData(LogData obj, string msg)
		{
			this.CreatedDate = DateTime.Now;
			this.Level = obj.Level;
			this.Logger = obj.Logger;
			this.Message = msg;
		}

		/// <summary>
		///  このログデータと別のログデータを日付で比較します。
		/// </summary>
		/// <param name="other">比較対象のログデータです。</param>
		/// <returns>
		///  このオブジェクトより<paramref name="other"/>の方が大きい場合はマイナス、
		///  同じ場合は<c>0</c>、
		///  このオブジェクトより<paramref name="other"/>の方が小さい場合はプラスです。
		/// </returns>
		public int CompareTo(LogData other)
		{
			return this.CreatedDate.CompareTo(other);
		}

		/// <summary>
		///  このログデータをユーザーが判読できる形式に変換します。
		/// </summary>
		/// <returns>
		/// 「<code>yyyy/MM/dd HH:mm:ss.fff [LOGLEVEL] @LoggerName      : <c>メッセージ</c></code>」
		/// 形式の文字列です。末尾に改行コードも含まれます。
		/// </returns>
		public override string ToString()
		{
			return $"{this.CreatedDate.ToString("yyyy/MM/dd HH:mm:ss.fff")}"
				+ $" [{this.Level.ToString(),-8}]"
				+ $" @{this.Logger.ShortName,-16}: {this.Message}\r\n";
		}
	}
}
