using System;
using System.Runtime.Serialization;
using Yencon.Resources;
using Yencon.Text;

namespace Yencon.Exceptions
{
	/// <summary>
	///  テキスト形式のヱンコンで構文の誤りが発生した事を表します。
	/// </summary>
	[Serializable()]
	public class InvalidSyntaxException : YenconException
	{
		/// <summary>
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public InvalidSyntaxException() : base() { }

		/// <summary>
		///  指定されたメッセージを利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">新しい例外を説明するメッセージです。</param>
		public InvalidSyntaxException(string msg) : base(msg) { }

		/// <summary>
		///  指定されたメッセージと内部例外を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="msg">新しい例外を説明するメッセージです。</param>
		/// <param name="innerEx">この例外が発生する原因となった例外です。</param>
		public InvalidSyntaxException(string msg, Exception innerEx) : base(msg, innerEx) { }

		/// <summary>
		///  指定された予期せぬ字を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="c">予期せぬ字です。</param>
		public InvalidSyntaxException(char c)
			: base(string.Format(ErrorMessages.InvalidSyntaxException_UnexpectedChar, c))
		{
			this.Data.Add("UnexpectedChar", c);
		}

		/// <summary>
		///  指定された予期せぬ字句を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="token">予期せぬ字句です。</param>
		public InvalidSyntaxException(Token token)
			: base(string.Format(ErrorMessages.InvalidSyntaxException_UnexpectedToken, token.SourceText))
		{
			this.Data.Add("UnexpectedToken_Type", token.Type);
			this.Data.Add("UnexpectedToken_Src",  token.SourceText);
		}

		/// <summary>
		///  直列化された情報を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidSyntaxException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="info">シリアル化された例外の情報です。</param>
		/// <param name="context">転送元または転送先のコンテキスト情報です。</param>
		protected InvalidSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
