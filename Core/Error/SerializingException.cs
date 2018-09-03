using System;

namespace OSDeveloper.Core.Error
{
	/// <summary>
	///  オブジェクトのシリアル化と逆シリアル化に失敗した場合に発生する例外です。
	/// </summary>
	public class SerializingException : Exception
	{
		/// <summary>
		///  エラーの状態を取得します。
		/// </summary>
		public SerializingFailureState Status { get; }

		/// <summary>
		///  シリアル化または逆シリアル化しようとしたフォーマットの名前を取得します。
		/// </summary>
		public string FormatName { get; }

		/// <summary>
		///  この例外を説明するメッセージを取得します。
		/// </summary>
		public override string Message
		{
			get
			{
				string msg;
				switch (this.Status) {
					case SerializingFailureState.IgnoredRootElement:
						msg = ErrorMessages.SerializingException_IgnoredRootElement;
						break;
					default:
						msg = ErrorMessages.SerializingFailureState_Unknown;
						break;
				}
				return string.Format(msg, this.FormatName);
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Error.SerializingException"/>'の新しいインスタンスを生成します。
		/// </summary>
		/// <param name="status">エラーの状態を表します。</param>
		/// <param name="formatName">シリアル化または逆シリアル化しようとしたフォーマットの名前です。</param>
		public SerializingException(SerializingFailureState status, string formatName)
		{
			this.Status = status;
			this.FormatName = formatName;
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Error.SerializingException"/>'の新しいインスタンスを生成します。
		/// </summary>
		/// <param name="status">エラーの状態を表します。</param>
		/// <param name="formatName">シリアル化または逆シリアル化しようとしたフォーマットの名前です。</param>
		/// <param name="inner">このエラーの原因となった別の例外です。</param>
		public SerializingException(SerializingFailureState status, string formatName, Exception inner) : base(null, inner)
		{
			this.Status = status;
		}
	}
}
