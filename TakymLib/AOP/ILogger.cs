using System;

namespace TakymLib.AOP
{
	/// <summary>
	///  ログを書き込む機能を提供します。
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		///  <see langword="Notice"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Notice(string msg);

		/// <summary>
		///  <see langword="Trace"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Trace(string msg);

		/// <summary>
		///  <see langword="Debug"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Debug(string msg);

		/// <summary>
		///  <see langword="Info"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Info(string msg);

		/// <summary>
		///  <see langword="Warn"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Warn(string msg);

		/// <summary>
		///  <see langword="Error"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Error(string msg);

		/// <summary>
		///  <see langword="Fatal"/>レベルで、指定されたメッセージでログを書き込みます。
		/// </summary>
		/// <param name="msg">ログに書き込むメッセージです。</param>
		void Fatal(string msg);

		/// <summary>
		///  指定された例外からログデータを作成して書き込みます。
		/// </summary>
		/// <param name="e">書き込む例外です。</param>
		/// <param name="isFatal">指定された例外が致命的かどうかを表します。</param>
		void Exception(Exception e, bool isFatal = false);
	}
}
