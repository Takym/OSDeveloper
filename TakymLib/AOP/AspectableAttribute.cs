using System;
using System.Runtime.Remoting.Proxies;

namespace DotnetExlib.AOP
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AspectableAttribute : ProxyAttribute
	{
		public Type AspectProxyType
		{
			get
			{
				return _proxy_type;
			}

			set
			{
				if (typeof(IAspectBehavior).IsAssignableFrom(_proxy_type)) {
					_proxy_type = value;
				} else {
					throw new InvalidCastException(
						$"\'{nameof(AspectProxyType)}\'は、型\'{nameof(IAspectBehavior)}\'を継承した型でなければなりません。");
				}
			}
		}
		private Type _proxy_type = typeof(Logger);
		private IAspectBehavior _obj_catch = null;

		public override MarshalByRefObject CreateInstance(Type serverType)
		{
			if (_obj_catch == null) _obj_catch = ((IAspectBehavior)(Activator.CreateInstance(_proxy_type, serverType)));
			MarshalByRefObject target = base.CreateInstance(serverType);
			RealProxy rp = new AspectProxy(target, serverType, _obj_catch);
			return rp.GetTransparentProxy() as MarshalByRefObject;
		}
	}
}
