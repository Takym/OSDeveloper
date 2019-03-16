using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace TakymLib.AOP
{
	internal class EmptyAspectBehavior : IAspectBehavior
	{
		public void PreInitializer(Type _serverType, IConstructionCallMessage constructionCallMessage)
		{
			return;
		}

		public void PostInitializer(Type _serverType, IConstructionCallMessage constructionCallMessage)
		{
			return;
		}

		public void PreCallMethod(Type _serverType, IMethodCallMessage methodCallMessage)
		{
			return;
		}

		public void PostCallMethod(Type _serverType, IMethodCallMessage methodCallMessage)
		{
			return;
		}

		public IMessage HandleInvalidCall(Type _serverType, IMessage callMessage)
		{
			return null;
		}
	}
}
