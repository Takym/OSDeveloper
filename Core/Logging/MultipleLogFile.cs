namespace OSDeveloper.Core.Logging
{
	/// <summary>
	///  複数のログファイルを同時に書き込めるようにします。
	/// </summary>
	public class MultipleLogFile : LogFile
	{
		/// <summary>
		///  書き込み先のログファイルを取得します。
		/// </summary>
		public LogFile[] LogFiles { get; }

		/// <summary>
		///  書き込み先の複数のログファイルを指定して、
		///  型'<see cref="OSDeveloper.Core.Logging.MultipleLogFile"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="logFiles">書き込み先のログファイルです。</param>
		public MultipleLogFile(params LogFile[] logFiles)
		{
			this.LogFiles = logFiles;
		}

		/// <summary>
		///  複数のログファイルに指定されたログデータを書き込みます。
		/// </summary>
		/// <param name="log">書き込むログデータです。</param>
		public override void Write(LogData log)
		{
			base.Write(log);
			foreach (var item in this.LogFiles) {
				item.Write(log);
			}
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
			base.Dispose(disposing);
			if (disposing) {
				foreach (var item in this.LogFiles) {
					item.Dispose();
				}
			}
		}
	}
}
