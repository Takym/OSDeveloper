﻿using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace TakymLib
{
	/// <summary>
	///  Windows Forms コントロールに関する拡張メソッドと便利関数を提供します。
	///  このクラスは静的です。
	/// </summary>
	public static class WinFormUtils
	{
		/// <summary>
		///  コントロールがデザインモードであるかどうかを取得します。
		///  <see cref="System.Windows.Forms.Control"/>を利用しない場所でも利用できます。
		/// </summary>
		public static bool DesignMode
		{
			get
			{
				return LicenseManager.UsageMode == LicenseUsageMode.Designtime
					|| Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV");
			}
		}

		/// <summary>
		///  コントロールがデザインモードであるかどうか判定します。
		///  <see cref="TakymLib.WinFormUtils.DesignMode"/>よりも強力です。
		/// </summary>
		/// <param name="control">判定対象のコントロールです。</param>
		/// <returns>
		///  デザインモードである場合は<see langword="true"/>、実行モードである場合は<see langword="false"/>です。
		/// </returns>
		public static bool IsDesignMode(this Control control)
		{
			bool result = false;
			var c = control;
			while (control != null) {
				// 全ての親コントロールに対して判定する。
				if (control.Site != null) {
					result |= control.Site.DesignMode;
				}
				control = control.Parent;
			}
			return result || DesignMode;
		}
	}
}
