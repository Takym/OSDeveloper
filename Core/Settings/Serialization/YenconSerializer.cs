using System;
using System.Collections.Generic;
using System.Reflection;
using OSDeveloper.Core.Error;

namespace OSDeveloper.Core.Settings.Serialization
{
	/// <summary>
	///  オブジェクトを<see langword="Yencon"/>に変換します。
	/// </summary>
	public class YenconSerializer
	{
		private Type _type;
		private Dictionary<string, MemberInfo> _map;

		/// <summary>
		///  指定した型のシリアライザを作成します。
		/// </summary>
		/// <param name="t">シリアル化と逆シリアル化する型です。</param>
		public YenconSerializer(Type t)
		{
			_type = t;
			if (_type.GetCustomAttribute<YenconIgnoreAttribute>() != null &&
				_type.GetCustomAttribute<YenconConsiderAttribute>() == null) {
				throw new SerializingException(SerializingFailureState.IgnoredRootElement, "Yencon");
			}
			foreach (var item in t.GetMembers()) {
				if (typeof(PropertyInfo).IsAssignableFrom(item.GetType()) ||
					typeof(FieldInfo).IsAssignableFrom(item.GetType())) {


					item.GetCustomAttributes<YenconAttribute>();// TODO: シリアライザを作る


				}
			}
		}
	}
}
