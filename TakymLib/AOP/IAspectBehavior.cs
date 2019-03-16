using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace DotnetExlib.AOP
{
	public interface IAspectBehavior
	{
		void PreInitializer(IConstructionCallMessage constructionCallMessage);
		void PostInitializer(IConstructionCallMessage constructionCallMessage);
		void PreCallMethod(IMethodCallMessage methodCallMessage);
		void PostCallMethod(IMethodCallMessage methodCallMessage);
	}
}
