using System.ComponentModel;
using System.Diagnostics;

namespace OSDeveloper.Core.MiscUtils
{
	/// <summary>
	///  Windows Forms コントロールに関する拡張メソッドと便利関数を提供します。
	///  このクラスは静的です。
	/// </summary>
	public static class WinFormUtils
	{
		/// <summary>
		///  コントロールがデザインモードであるかどうかを取得します。
		/// </summary>
		public static bool DesignMode
		{
			get
			{
				return LicenseManager.UsageMode == LicenseUsageMode.Designtime
					|| Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV");
			}
		}
	}
}
