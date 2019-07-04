using System;
using System.Runtime.Serialization;
using Yencon.Resources;

namespace Yencon.Exceptions
{
	/// <summary>
	///  キー名が無効の場合に発生します。
	/// </summary>
	[Serializable()]
	public class InvalidKeyNameException : YenconException
	{
		/// <summary>
		///  無効なキー名を指定して、
		///  型'<see cref="Yencon.Exceptions.InvalidKeyNameException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="name">この例外の原因となったキー名です。</param>
		public InvalidKeyNameException(string name) : base(string.Format(ErrorMessages.InvalidKeyNameException_Message, name))
		{
			this.Data.Add("SpecifiedName", name);
		}

		/// <summary>
		///  無効なキー名と内部例外を指定して、
		///  型'<see cref="Yencon.Exceptions.InvalidKeyNameException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="name">この例外の原因となったキー名です。</param>
		/// <param name="innerEx">この例外の原因となった別の例外です。</param>
		public InvalidKeyNameException(string name, Exception innerEx)
			: base(string.Format(ErrorMessages.InvalidKeyNameException_Message, name), innerEx)
		{
			this.Data.Add("SpecifiedName", name);
		}

		/// <summary>
		///  直列化された情報を利用して、
		///  型'<see cref="Yencon.Exceptions.InvalidKeyNameException"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="info">シリアル化された例外の情報です。</param>
		/// <param name="context">転送元または転送先のコンテキスト情報です。</param>
		protected InvalidKeyNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
