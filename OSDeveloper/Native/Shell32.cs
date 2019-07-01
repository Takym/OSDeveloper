using System;
using System.Drawing;
using static OSDeveloper.Native.WinapiWrapper.Shell32;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see cref="OSDeveloper.Native.WinapiWrapper.Shell32"/>内の関数を利用し易い形にラップします。
	/// </summary>
	public static class Shell32
	{
		public const string Imageres_Path = "C:\\WINDOWS\\System32\\imageres.dll";

		public static Icon GetIconFrom(string filename, int index, bool isLarge)
		{
			var large = IntPtr.Zero;
			var small = IntPtr.Zero;
			ExtractIconExW(filename, index, out large, out small, 1);
			if (large == IntPtr.Zero || small == IntPtr.Zero) {
				if (large != IntPtr.Zero) WinapiWrapper.User32.DestroyIcon(large);
				if (small != IntPtr.Zero) WinapiWrapper.User32.DestroyIcon(small);
				return Libosdev.GetIcon(Libosdev.Icons.MiscUnknown, out uint w);
			}
			var result = ((Icon)(Icon.FromHandle(isLarge ? large : small).Clone()));
			WinapiWrapper.User32.DestroyIcon(large);
			WinapiWrapper.User32.DestroyIcon(small);
			return result;
		}

		public static Bitmap GetSmallImageAt(int index, bool useImageres = false)
		{
			if (useImageres) {
				return GetIconFrom(Imageres_Path, index, false).ToBitmap();
			} else {
				return GetIconFrom(         Path, index, false).ToBitmap();
			}
		}
	}
}
