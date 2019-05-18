using System;
using Yencon;
using SysVer = System.Version;

namespace OSDeveloper.Projects
{
	public struct IDEVersion : IEquatable<IDEVersion>, IComparable<IDEVersion>, IComparable
	{
		#region 現在のバージョン

		public static IDEVersion GetCurrentVersion()
		{
			return new IDEVersion(ASMINFO.Caption, ASMINFO.Version, ASMINFO.Edition);
		}

		public static IDEVersion GetCurrentVersion(string keyname)
		{
			return new IDEVersion(ASMINFO.Caption, ASMINFO.Version, ASMINFO.Edition, keyname);
		}

		#endregion

		public const string DefaultKeyName = nameof(IDEVersion);

		private readonly YSection _ysection;

		public string Caption { get => (_ysection?.GetNode(nameof(this.Caption)) as YString)?.Text ?? string.Empty; }
		public string Version { get => (_ysection?.GetNode(nameof(this.Version)) as YString)?.Text ?? string.Empty; }
		public string Edition { get => (_ysection?.GetNode(nameof(this.Edition)) as YString)?.Text ?? string.Empty; }

		public IDEVersion(string caption, string version, string edition, string keyname = DefaultKeyName)
		{
			_ysection = new YSection();
			_ysection.Name = keyname;
			_ysection.Add(new YString() { Name = nameof(this.Caption), Text = caption });
			_ysection.Add(new YString() { Name = nameof(this.Version), Text = version });
			_ysection.Add(new YString() { Name = nameof(this.Edition), Text = edition });
		}

		public IDEVersion(YSection section)
		{
			_ysection = section;
		}

		public YSection GetYSection()
		{
			return _ysection;
		}

		public SysVer GetVersion()
		{
			if (SysVer.TryParse(this.Version, out var result)) {
				return result;
			} else {
				return null;
			}
		}

		/// <summary>
		///  このバージョンと現在のバージョンに互換性があるか確認する。
		/// </summary>
		public bool HasCompatible()
		{
			// TODO: バージョン更新の度に必要ならば書き換える。
			var current = GetCurrentVersion();
			if (this.Caption != current.Caption ||
				this.Edition != current.Edition) return false;
			var thisver = this.GetVersion();
			var curver = current.GetVersion();
			return thisver.Major == curver.Major
				&& thisver.Minor >= curver.Minor;
		}

#if false
		public bool HasCompatibleWith(IDEVersion baseVer)
		{
			// 上位互換性、前方互換性
			return baseVer <= this;
		}

		public bool HasCompatibleTo(IDEVersion targetVer)
		{
			// 下位互換性、後方互換性
			return targetVer >= this;
		}
#endif

		#region 比較系

		public override bool Equals(object obj)
		{
			if (obj is IDEVersion ver) {
				return this == ver;
			} else {
				return false;
			}
		}

		public bool Equals(IDEVersion other)
		{
			return this == other;
		}

		public int CompareTo(object obj)
		{
			if (obj is IDEVersion ver) {
				return this.GetVersion().CompareTo(ver.GetVersion());
			} else {
				return this.GetVersion().CompareTo(obj);
			}
		}

		public int CompareTo(IDEVersion other)
		{
			return this.GetVersion().CompareTo(other.GetVersion());
		}

		public override int GetHashCode()
		{
			return this.Caption.GetHashCode() ^ this.Version.GetHashCode() ^ this.Edition.GetHashCode();
		}

		public override string ToString()
		{
			return $"{this.Caption} {this.Edition} [v{this.Version}]";
		}

		#endregion

		#region 演算子

		public static bool operator ==(IDEVersion left, IDEVersion right)
		{
			return left.Caption == right.Caption
				&& left.Version == right.Version
				&& left.Edition == right.Edition;
		}

		public static bool operator !=(IDEVersion left, IDEVersion right)
		{
			return left.Caption != right.Caption
				|| left.Version != right.Version
				|| left.Edition != right.Edition;
		}

		public static bool operator <(IDEVersion left, IDEVersion right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator >(IDEVersion left, IDEVersion right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator <=(IDEVersion left, IDEVersion right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >=(IDEVersion left, IDEVersion right)
		{
			return left.CompareTo(right) >= 0;
		}

		#endregion
	}
}
