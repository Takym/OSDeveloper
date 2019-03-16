using System;
using System.Runtime.Remoting.Proxies;

namespace TakymLib.AOP
{
	/// <summary>
	///  クラスが分断可能である事を表す属性です。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AspectableAttribute : ProxyAttribute
	{
		/// <summary>
		///  実行するアスペクト処理を表す型の種類を取得します。
		/// </summary>
		public Type AspectProxyType
		{
			get
			{
				return _proxy_type;
			}
		}
		private readonly Type            _proxy_type;
		private          IAspectBehavior _obj_catch;

		/// <summary>
		///  型'<see cref="TakymLib.AOP.AspectableAttribute"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="aspectProxyType">実行するアスペクト処理を表す型の種類です。</param>
		public AspectableAttribute(Type aspectProxyType)
		{
			if (typeof(IAspectBehavior).IsAssignableFrom(aspectProxyType)) {
				_proxy_type = aspectProxyType;
			} else {
				_proxy_type = typeof(EmptyAspectBehavior);
			}
		}

		/// <summary>
		///  指定された型のインスタンスとそれに対する透過的なプロキシを生成します。
		/// </summary>
		/// <param name="serverType">生成する型です。</param>
		/// <returns>生成した型のインスタンスの透過的なプロキシを返します。</returns>
		public override MarshalByRefObject CreateInstance(Type serverType)
		{
			_obj_catch = _obj_catch ?? Activator.CreateInstance(_proxy_type) as IAspectBehavior;
			var proxy = new AspectProxy(
				base.CreateInstance(serverType),
				serverType,
				_obj_catch);
			return proxy.GetTransparentProxy() as MarshalByRefObject;
		}
	}
}
