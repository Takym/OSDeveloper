using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace TakymLib.AOP
{
	/// <summary>
	///  分断されたオブジェクトで実行される処理を提供します。
	/// </summary>
	public interface IAspectBehavior
	{
		/// <summary>
		///  コンストラクタが呼び出される前に実行されます。
		/// </summary>
		/// <param name="_serverType">ターゲットの型です。</param>
		/// <param name="constructionCallMessage">コンストラクタの呼び出しメッセージです。</param>
		void PreInitializer(Type _serverType, IConstructionCallMessage constructionCallMessage);

		/// <summary>
		///  コンストラクタが呼び出された後に実行されます。
		/// </summary>
		/// <param name="_serverType">ターゲットの型です。</param>
		/// <param name="constructionCallMessage">コンストラクタの呼び出しメッセージです。</param>
		void PostInitializer(Type _serverType, IConstructionCallMessage constructionCallMessage);

		/// <summary>
		///  関数が呼び出される前に実行されます。
		/// </summary>
		/// <param name="_serverType">ターゲットの型です。</param>
		/// <param name="methodCallMessage">関数の呼び出しメッセージです。</param>
		void PreCallMethod(Type _serverType, IMethodCallMessage methodCallMessage);

		/// <summary>
		///  関数が呼び出された後に実行されます。
		/// </summary>
		/// <param name="_serverType">ターゲットの型です。</param>
		/// <param name="methodCallMessage">関数の呼び出しメッセージです。</param>
		void PostCallMethod(Type _serverType, IMethodCallMessage methodCallMessage);

		/// <summary>
		///  無効な呼び出しを処理します。
		/// </summary>
		/// <param name="_serverType">ターゲットの型です。</param>
		/// <param name="callMessage">呼び出しメッセージです。</param>
		/// <returns>戻り値メッセージです。</returns>
		IMessage HandleInvalidCall(Type _serverType, IMessage callMessage);
	}
}
