using System;
using System.ComponentModel;
using System.IO;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Logging
{
	partial class Logger
	{
		/// <summary>
		///  指定された例外からログデータを作成して書き込みます。
		/// </summary>
		/// <param name="e">書き込む例外です。</param>
		/// <param name="isFatal">指定された例外が致命的かどうかを表します。</param>
		public void Exception(Exception e, bool isFatal = false)
		{
			if (e == null) {
				// ログの書き込み時点で例外を発生させると、
				// ハンドルされなくなる恐れがあるので、"ぬるぽ"は全て無視。
				this.Warn("The exception was null.");
				return;
			}

			// エラーメッセージを書き込み
			if (isFatal) {
				this.Fatal("The following unresolvable exception occurred.");
				this.Fatal(e.Message);
			} else {
				this.Error("The following handled exception occurred.");
				this.Error(e.Message);
			}

			// 共通エラー情報を書き込み
			this.Info("TypeOfError: " + e.GetType().FullName);
			this.Info("Source     : " + (e.Source ?? "<null>"));
			this.Info("TargetSite : Method Name : " + e.TargetSite?.Name ?? "<null>");
			this.Info("TargetSite : Class  Name : " + e.TargetSite?.DeclaringType?.FullName ?? "<null>");
			this.Info("HelpLink   : " + (e.HelpLink ?? "<null>"));

			// HResult情報を書き込み
			this.Info("HResult    : " + $"0x{e.HResult:X8} ({e.HResult})");
			this.Info("HResult Msg: " + Kernel32.GetErrorMessage(e.HResult));

			// スタックトレースを書き込み
			if (e.StackTrace != null) {
				string[] st = e.StackTrace.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (var item in st) {
					this.Info("StackTrace : " + item.Trim());
				}
			} else {
				this.Info("StackTrace : <null>");
			}

			// 追加情報を書き込み
			if (e.Data == null) {
				this.Info("Data       : <null>");
			} else if (e.Data.Count == 0) {
				this.Info("Data       : <empty>");
			} else {
				var ks = e.Data.Keys.GetEnumerator();
				var vs = e.Data.Values.GetEnumerator();
				ks.Reset();
				vs.Reset();
				for (int i = 0; i < e.Data.Count; ++i) {
					this.Info($"Data       : {ks.Current ?? "<null>"}=={vs.Current ?? "<null>"}");
					ks.MoveNext();
					vs.MoveNext();
				}
			}

			// 例外ごとに特別な情報を書き込み
			switch (e) {
				case NotImplementedException nie:
					this.Notice("The process was not implemented. So, this exception is not serious.");
					this.Notice("If the application terminated because of this error, then it is a bug.");
					break;
				case ArgumentException ae:
					this.Notice($"The argument name is: \'{ae.ParamName}\'");
					break;
				case FileNotFoundException fnfe:
					this.Notice($"The file name is: \"{fnfe.FileName}\"");
					break;
				case FileLoadException fle:
					this.Notice($"The file name is: \"{fle.FileName}\"");
					break;
				case Win32Exception w32e:
					this.Notice($"The error code is: " + w32e.ErrorCode);
					this.Notice($"The native error code is: " + w32e.NativeErrorCode);
					break;
			}

			// 内部例外を書き込み
			if (e.InnerException != null) {
				this.Notice("This exception has inner exceptions.");
				this.Exception(e.InnerException, isFatal);
			} else {
				this.Trace("-------- End of Error Report --------");
			}
		}
	}
}
