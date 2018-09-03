using System;

namespace OSDeveloper.Core.Settings.Serialization
{
	/// <summary>
	///  オブジェクトを<see langword="Yencon"/>でシリアル化と逆シリアル化を実行する為に必要な情報を、
	///  <see cref="OSDeveloper.Core.Settings.Serialization.YenconSerializer"/>に渡します。
	///  このクラスは抽象クラスです。
	/// </summary>
	public abstract class YenconAttribute : Attribute
	{
		internal YenconAttribute() { } // コンストラクタの隠蔽
	}

	/// <summary>
	///  <see langword="Yencon"/>のセクションを表します。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class YenconSectionAttribute : YenconAttribute
	{
		/// <summary>
		///  セクション名を取得します。
		/// </summary>
		public string Name { get; }

		/// <summary>
		///  <see langword="Yencon"/>のセクションを表します。
		/// </summary>
		/// <param name="name">セクション名です。</param>
		public YenconSectionAttribute(string name)
		{
			this.Name = name;
		}
	}

	/// <summary>
	///  <see langword="Yencon"/>のキーを表します。
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class YenconKeyAttribute : YenconAttribute
	{
		/// <summary>
		///  キー名を取得します。
		/// </summary>
		public string Name { get; }

		/// <summary>
		///  キーの種類を取得します。
		/// </summary>
		public YenconType Kind { get; }

		/// <summary>
		///  <see langword="Yencon"/>のキーを表します。
		/// </summary>
		/// <param name="name">キー名です。</param>
		/// <param name="kind">キーの種類です。</param>
		public YenconKeyAttribute(string name, YenconType kind)
		{
			this.Name = name;
			this.Kind = kind;
		}
	}

	/// <summary>
	///  <see langword="Yencon"/>の値として利用しない事を表します。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class YenconIgnoreAttribute : YenconAttribute { }

	/// <summary>
	///  <see cref="OSDeveloper.Core.Settings.Serialization.YenconIgnoreAttribute"/>
	///  で無視されていた項目を再び有効にします。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class YenconConsiderAttribute : YenconAttribute { }
}
