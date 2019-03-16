using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace TakymLib.AOP
{
	/// <summary>
	///  分断されたオブジェクトを管理するプロキシです。
	/// </summary>
	public class AspectProxy : RealProxy
	{
		private readonly MarshalByRefObject _target;
		private readonly Type               _serverType;
		private readonly IAspectBehavior    _behavior;

		/// <summary>
		///  型'<see cref="TakymLib.AOP.AspectProxy"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="target">対象のインスタンスです。</param>
		/// <param name="serverType">対象の型です。</param>
		/// <param name="behavior">実行する動作です。</param>
		public AspectProxy(MarshalByRefObject target, Type serverType, IAspectBehavior behavior) : base(serverType)
		{
			_target     = target;
			_serverType = serverType;
			_behavior   = behavior;
		}

		/// <summary>
		///  処理を実行します。
		/// </summary>
		/// <param name="msg">呼び出し元を表すメッセージです。</param>
		/// <returns>戻り値を表すメッセージです。</returns>
		public override IMessage Invoke(IMessage msg)
		{
			IMessage result = null;

			if (msg is IConstructionCallMessage ctor) { // コンストラクタの場合
				// 特殊な処理
				_behavior.PreInitializer(_serverType, ctor);

				// コンストラクタ呼び出し
				var rp = RemotingServices.GetRealProxy(_target);
				rp.InitializeServerObject(ctor);
				var tp = this.GetTransparentProxy() as MarshalByRefObject;
				result = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);

				// 特殊な処理
				_behavior.PostInitializer(_serverType, ctor);
			} else if (msg is IMethodCallMessage func) { // 通常関数の場合
				// 特殊な処理
				_behavior.PreCallMethod(_serverType, func);

				// 通常関数呼び出し
				result = RemotingServices.ExecuteMessage(_target, func);

				// 特殊な処理
				_behavior.PostCallMethod(_serverType, func);
			} else {
				// 呼び出しが不正
				result = _behavior.HandleInvalidCall(_serverType, msg);
			}

			return result;
		}
	}
}
