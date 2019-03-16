using System;
using System.Runtime.Serialization;

namespace Yencon.Exceptions
{
	/// <summary>
	///  システムで定義されていないヱンコン独自の例外を表す基本クラスです。
	/// </summary>
	public class YenconException : Exception
	{
		/// <summary>
		///  型'<see cref="Yencon.Exceptions.YenconException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconException() : base() { }

		/// <summary>
		///  指定されたメッセージを利用して、
		///  型'<see cref="Yencon.Exceptions.YenconException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">新しい例外を説明するメッセージです。</param>
		public YenconException(string msg) : base(msg) { }

		/// <summary>
		///  指定されたメッセージと内部例外を利用して、
		///  型'<see cref="Yencon.Exceptions.YenconException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">新しい例外を説明するメッセージです。</param>
		/// <param name="innerEx">この例外が発生する原因となった例外です。</param>
		public YenconException(string msg, Exception innerEx) : base(msg, innerEx) { }

		/// <summary>
		///  直列化された情報を利用して、
		///  型'<see cref="Yencon.Exceptions.YenconException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="info">シリアル化された例外の情報です。</param>
		/// <param name="context">転送元または転送先のコンテキスト情報です。</param>
		protected YenconException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
