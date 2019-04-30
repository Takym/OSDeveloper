using System;
using Yencon;
using SysVer = System.Version;

namespace OSDeveloper.Projects
{
	public class IDEVersion : IEquatable<IDEVersion>, IComparable<IDEVersion>
	{
		#region 現在のバージョン
		public static IDEVersion GetCurrentVersion()
		{
			if (_curver == null) {
				_curver = new IDEVersion(ASMINFO.Caption, ASMINFO.Version, ASMINFO.Edition);
			}
			return _curver;
		}
		private static IDEVersion _curver;
		#endregion

		private readonly YSection _ysection;

		public string Caption { get => (_ysection.GetNode(nameof(this.Caption)) as YString)?.Text; }
		public string Version { get => (_ysection.GetNode(nameof(this.Version)) as YString)?.Text; }
		public string Edition { get => (_ysection.GetNode(nameof(this.Edition)) as YString)?.Text; }

		public IDEVersion(string caption, string version, string edition)
		{
			_ysection = new YSection();
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
	}
}
