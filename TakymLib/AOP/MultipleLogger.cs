using System;
using System.Collections.Generic;

namespace TakymLib.AOP
{
	/// <summary>
	///  複数の出力先を持つロガーです。
	/// </summary>
	public class MultipleLogger : List<ILogger>/*HybridList<ILogger>*/, ILogger
	{
		/// <summary>
		///  <see langword="Notice"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Notice(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Notice(msg);
			}
		}

		/// <summary>
		///  <see langword="Trace"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Trace(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Trace(msg);
			}
		}

		/// <summary>
		///  <see langword="Debug"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Debug(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Debug(msg);
			}
		}

		/// <summary>
		///  <see langword="Info"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Info(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Info(msg);
			}
		}

		/// <summary>
		///  <see langword="Warn"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Warn(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Warn(msg);
			}
		}

		/// <summary>
		///  <see langword="Error"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Error(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Error(msg);
			}
		}

		/// <summary>
		///  <see langword="Fatal"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		public void Fatal(string msg)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Fatal(msg);
			}
		}

		/// <summary>
		///  指定された例外からログデータを作成して書き込みます。
		/// </summary>
		/// <param name="e">書き込む例外です。</param>
		/// <param name="isFatal">指定された例外が致命的かどうかを表します。</param>
		public void Exception(Exception e, bool isFatal = false)
		{
			for (int i = 0; i < this.Count; ++i) {
				this[i].Exception(e, isFatal);
			}
		}
	}
}
