using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OSDeveloper.IO.Configuration
{
	public static class Globalization
	{
		private static CultureInfo[] _s_cultures;
		private static CultureInfo[] _i_cultures;

		public static CultureInfo[] GetSupportedCultures()
		{
			if (_s_cultures == null) {
				_s_cultures = new CultureInfo[] {
					CultureInfo.GetCultureInfo("en"),
					CultureInfo.GetCultureInfo("ja")
				};
			}
			return _s_cultures;
		}

		public static CultureInfo[] GetInstalledCultures()
		{
			if (_i_cultures == null) {
				var p    = SystemPaths.Program;
				var dirs = Directory.GetDirectories(p);
				var ls   = new List<CultureInfo>();
				var cltr = new List<CultureInfo>(CultureInfo.GetCultures(CultureTypes.AllCultures));
				cltr.Sort(comparer);
				for (int i = 0; i < dirs.Length; ++i) {
					string name = Path.GetFileName(dirs[i]);
					try {
						int j = cltr.BinarySearch(CultureInfo.GetCultureInfo(name), comparer);
						if (0 <= j && j < cltr.Count) {
							ls.Add(cltr[j]);
						}
					} catch (Exception e) {
						Program.Logger.Notice($"The exception occurred in {nameof(Globalization)}");
						Program.Logger.Exception(e);
					}
				}
				_i_cultures = ls.ToArray();
			}
			return _i_cultures;
		}

		public static CultureInfo GetDefaultCulture()
		{
			var c = CultureInfo.InstalledUICulture;
			var cultures = new List<CultureInfo>(GetInstalledCultures());
			int i = cultures.BinarySearch(CultureInfo.GetCultureInfo(c.Parent?.Name ?? c.Name), comparer);
			if (0 <= i && i < cultures.Count) {
				return c;
			} else {
				return CultureInfo.GetCultureInfo("en");
			}
		}

		private readonly static CultureInfoComparer comparer = new CultureInfoComparer();
		private class CultureInfoComparer : IComparer<CultureInfo>
		{
			public int Compare(CultureInfo x, CultureInfo y)
			{
				return x.Name.CompareTo(y.Name);
			}
		}
	}
}
