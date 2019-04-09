using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace TakymLib.AOP
{
	/// <summary>
	///  ログを取る機能を持った<see cref="TakymLib.AOP.IAspectBehavior"/>です。
	/// </summary>
	public class LoggingAspectBehavior : IAspectBehavior
	{
		/// <summary>
		///  ログの出力先を取得または設定します。
		/// </summary>
		public static ILogger Logger
		{
			get
			{
				return _loggers;
			}

			set
			{
				if (_loggers == null) {
					_loggers = new MultipleLogger();
				}
				_loggers.Add(value);
			}
		}
		private static MultipleLogger _loggers;

		/// <summary>
		///  コンストラクタが呼び出される前に実行されます。
		/// </summary>
		/// <param name="serverType">ターゲットの型です。</param>
		/// <param name="constructionCallMessage">コンストラクタの呼び出しメッセージです。</param>
		public virtual void PreInitializer(Type serverType, IConstructionCallMessage constructionCallMessage)
		{
			Logger?.Trace($"pre-initializer: {serverType.FullName}::{constructionCallMessage.MethodName}");
		}

		/// <summary>
		///  コンストラクタが呼び出された後に実行されます。
		/// </summary>
		/// <param name="serverType">ターゲットの型です。</param>
		/// <param name="constructionCallMessage">コンストラクタの呼び出しメッセージです。</param>
		public virtual void PostInitializer(Type serverType, IConstructionCallMessage constructionCallMessage)
		{
			Logger?.Trace($"post-initializer: {serverType.FullName}::{constructionCallMessage.MethodName}");
		}

		/// <summary>
		///  関数が呼び出される前に実行されます。
		/// </summary>
		/// <param name="serverType">ターゲットの型です。</param>
		/// <param name="methodCallMessage">関数の呼び出しメッセージです。</param>
		public virtual void PreCallMethod(Type serverType, IMethodCallMessage methodCallMessage)
		{
			Logger?.Trace($"pre-function: {serverType.FullName}::{methodCallMessage.MethodName}");
		}

		/// <summary>
		///  関数が呼び出された後に実行されます。
		/// </summary>
		/// <param name="serverType">ターゲットの型です。</param>
		/// <param name="methodCallMessage">関数の呼び出しメッセージです。</param>
		public virtual void PostCallMethod(Type serverType, IMethodCallMessage methodCallMessage)
		{
			Logger?.Trace($"post-function: {serverType.FullName}::{methodCallMessage.MethodName}");
		}

		/// <summary>
		///  無効な呼び出しを処理します。
		/// </summary>
		/// <param name="serverType">ターゲットの型です。</param>
		/// <param name="callMessage">呼び出しメッセージです。</param>
		/// <returns>戻り値メッセージです。</returns>
		public virtual IMessage HandleInvalidCall(Type serverType, IMessage callMessage)
		{
			Logger?.Warn($"invalid call detected in: {serverType.FullName}");
			foreach (KeyValuePair<object, object> item in callMessage.Properties) {
				Logger?.Warn($"prop[\"{item.Key}\"] = {item.Value}");
			}
			return null;
		}
	}
}
