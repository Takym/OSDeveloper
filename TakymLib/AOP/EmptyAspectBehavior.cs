using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace TakymLib.AOP
{
	internal class EmptyAspectBehavior : IAspectBehavior
	{
		public void PreInitializer(Type serverType, IConstructionCallMessage constructionCallMessage)
		{
			return;
		}

		public void PostInitializer(Type serverType, IConstructionCallMessage constructionCallMessage)
		{
			return;
		}

		public void PreCallMethod(Type serverType, IMethodCallMessage methodCallMessage)
		{
			return;
		}

		public void PostCallMethod(Type serverType, IMethodCallMessage methodCallMessage)
		{
			return;
		}

		public IMessage HandleInvalidCall(Type serverType, IMessage callMessage)
		{
			return null;
		}
	}
}
