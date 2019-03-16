using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace DotnetExlib.AOP
{
	public class AspectProxy : RealProxy
	{
		private MarshalByRefObject _target;
		private IAspectBehavior _behavior;

		public AspectProxy(MarshalByRefObject target, Type t, IAspectBehavior behavior) : base(t)
		{
			_target = target;
			_behavior = behavior;
		}

		public override IMessage Invoke(IMessage msg)
		{
			IMethodCallMessage call = msg as IMethodCallMessage;
			IConstructionCallMessage ctor = msg as IConstructionCallMessage;
			IMethodReturnMessage result = null;

			if (ctor != null) { // 呼び出し関数がコンストラクタの場合

				_behavior.PreInitializer(ctor);

				RealProxy rp = RemotingServices.GetRealProxy(_target);
				rp.InitializeServerObject(ctor);
				MarshalByRefObject tp = this.GetTransparentProxy() as MarshalByRefObject;
				result = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);

				_behavior.PostInitializer(ctor);

			} else if (call != null) { // 通常関数の場合

				_behavior.PreCallMethod(call);

				result = RemotingServices.ExecuteMessage(this._target, call);

				_behavior.PostCallMethod(call);

			} else {
				Console.WriteLine($"メソッドの実行に失敗しました：{msg}");
				//throw new Exception($"メソッドの実行に失敗しました：{msg}");
			}

			return result;
		}
	}
}
