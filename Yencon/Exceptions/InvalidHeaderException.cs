using System;
using System.Runtime.Serialization;

namespace Yencon.Exceptions
{
	/// <summary>
	///  バイナリ形式のヱンコンのヘッダー情報が無効の場合に発生します。
	/// </summary>
	public class InvalidHeaderException : YenconException
	{
		/// <summary>
		///  例外メッセージを指定して、
		///  型'<see cref="Yencon.Exceptions.InvalidHeaderException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">この例外の原因となったキー名です。</param>
		public InvalidHeaderException(string msg) : base(msg) { }

		/// <summary>
		///  例外メッセージと内部例外を指定して、
		///  型'<see cref="Yencon.Exceptions.InvalidHeaderException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">この例外の原因となったキー名です。</param>
		/// <param name="innerEx">この例外の原因となった別の例外です。</param>
		public InvalidHeaderException(string msg, Exception innerEx) : base(msg, innerEx) { }

		/// <summary>
		///  直列化された情報を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidHeaderException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="info">シリアル化された例外の情報です。</param>
		/// <param name="context">転送元または転送先のコンテキスト情報です。</param>
		protected InvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
