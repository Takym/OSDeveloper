using System;
using System.Runtime.InteropServices;
using static OSDeveloper.Native.WinapiWrapper.Kernel32;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see cref="OSDeveloper.Native.WinapiWrapper.Kernel32"/>内の関数を利用し易い形にラップします。
	/// </summary>
	public static class Kernel32
	{
		/// <summary>
		///  指定されたエラーコードからエラーメッセージを生成します。
		/// </summary>
		/// <param name="hResult">HResult形式のエラーコードです。</param>
		/// <returns>生成されたローカライズ済みのエラーメッセージです。</returns>
		public static unsafe string GetErrorMessage(int hResult)
		{
			var lpBuf = IntPtr.Zero;
			FormatMessageW(
				FORMAT_MESSAGE_ALLOCATE_BUFFER |
				FORMAT_MESSAGE_FROM_SYSTEM |
				FORMAT_MESSAGE_IGNORE_INSERTS,
				IntPtr.Zero,
				((uint)(hResult)),
				MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
				new IntPtr(&lpBuf),
				0, IntPtr.Zero);
			string str = Marshal.PtrToStringUni(lpBuf);
			return str?.Trim();
		}
	}
}
