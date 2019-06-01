#pragma warning disable CS1591
using System;
using System.Runtime.InteropServices;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see langword="Windows"/>のAPIを呼び出すためのクラス群を提供します。
	/// </summary>
	public static class WinapiWrapper
	{
		/// <summary>
		///  <see langword="KERNEL32.DLL"/>内のAPIを呼び出します。
		/// </summary>
		public static class Kernel32
		{
			/// <summary>
			///  このクラスで読み込むライブラリのファイルパスを取得します。
			/// </summary>
			public const string Path = "C:\\WINDOWS\\System32\\kernel32.dll";

			public const ushort LANG_NEUTRAL    = 0x00;
			public const ushort SUBLANG_DEFAULT = 0x01;
			public static uint MAKELANGID(ushort p, ushort s)
			{
				return ((uint)((s << 10) | p));
			}

			public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
			public const uint FORMAT_MESSAGE_IGNORE_INSERTS  = 0x00000200;
			public const uint FORMAT_MESSAGE_FROM_SYSTEM     = 0x00001000;

			[DllImport(Path)]
			public static extern uint FormatMessageW(
				uint dwFlags,
				IntPtr lpSource,
				uint dwMessageId,
				uint dwLanguageId,
				IntPtr lpBuffer,
				uint nSize,
				IntPtr Arguments);
		}

		/// <summary>
		///  <see langword="USER32.DLL"/>内のAPIを呼び出します。
		/// </summary>
		public static class User32
		{
			/// <summary>
			///  このクラスで読み込むライブラリのファイルパスを取得します。
			/// </summary>
			public const string Path = "C:\\WINDOWS\\System32\\user32.dll";

			public const uint PW_CLIENTONLY        = 0x00000001;
			public const uint PW_RENDERFULLCONTENT = 0x00000002;

			[DllImport(Path)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

			[DllImport(Path)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool DestroyIcon(IntPtr hIcon);
		}

		/// <summary>
		///  <see langword="SHELL32.DLL"/>内のAPIを呼び出します。
		/// </summary>
		public static class Shell32
		{
			/// <summary>
			///  このクラスで読み込むライブラリのファイルパスを取得します。
			/// </summary>
			public const string Path = "C:\\WINDOWS\\System32\\shell32.dll";

			[DllImport(Path)]
			public static extern uint ExtractIconExW(
				[MarshalAs(UnmanagedType.LPWStr)]
				string lpszFile,
				int nIconIndex,
				out IntPtr phiconLarge,
				out IntPtr phiconSmall,
				uint nIcons);
		}

		/// <summary>
		///  <see langword="LIBOSDEV.DLL"/>内のAPIを呼び出します。
		/// </summary>
		public static class Libosdev
		{
			/// <summary>
			///  このクラスで読み込むライブラリのファイルパスを取得します。
			/// </summary>
			public const string Path = "libosdev.dll";

			public const int LIB_STATE_NOT_INIT  = 0;
			public const int LIB_STATE_NOT_FOUND = 1;
			public const int LIB_STATE_LOADED    = 2;
			public const int LIB_STATE_DISPOSED  = 3;

			public const int ICON1000_LOGO                   = 1000;
			public const int ICON1001_LOGO_FORMMAIN          = 1001;
			public const int ICON1002_LOGO_JAPANESE          = 1002;
			public const int ICON1003_LOGO_ENGLISH           = 1003;
			public const int ICON1048_LOGO_ZEROLIB           = 1048;
			public const int ICON1049_LOGO_ZEROLIBEX         = 1049;
			public const int ICON1050_FORM_EDITORWINDOW      = 1050;
			public const int ICON1051_FORM_SETTINGS          = 1051;
			public const int ICON1100_FILE                   = 1100;
			public const int ICON1101_FILE_BINARY            = 1101;
			public const int ICON1102_FILE_TEXT              = 1102;
			public const int ICON1103_FILE_PROGRAM           = 1103;
			public const int ICON1104_FILE_SOURCECODE        = 1104;
			public const int ICON1105_FILE_RESOURCE          = 1105;
			public const int ICON1106_FILE_DOCUMENT          = 1106;
			public const int ICON1107_FILE_SOLUTION          = 1107;
			public const int ICON1150_FOLDER                 = 1150;
			public const int ICON1151_FOLDER_CLOSED          = 1151;
			public const int ICON1152_FOLDER_OPENED          = 1152;
			public const int ICON1153_FOLDER_DIRECTORY       = 1153;
			public const int ICON1154_FOLDER_DIRCLOSED       = 1154;
			public const int ICON1155_FOLDER_DIROPENED       = 1155;
			public const int ICON1156_FOLDER_FDD             = 1156;
			public const int ICON1157_FOLDER_HDD             = 1157;
			public const int ICON1158_FOLDER_ODD             = 1158;
			public const int ICON1159_FOLDER_JUNCTION        = 1159;
			public const int ICON1160_FOLDER_JUNCLOSED       = 1160;
			public const int ICON1161_FOLDER_JUNOPENED       = 1161;
			public const int ICON1162_FOLDER_SOLUTION        = 1162;
			public const int ICON1199_ITEM_CANNOT_ACCESS     = 1199;
			public const int ICON1200_MENU_FILE_SAVE         = 1200;
			public const int ICON1201_MENU_FILE_SAVEAS       = 1201;
			public const int ICON1202_MENU_FILE_SAVEALL      = 1202;
			public const int ICON1203_MENU_FILE_SAVEALLAS    = 1203;
			public const int ICON1204_MENU_FILE_PRINT        = 1204;
			public const int ICON1205_MENU_FILE_PRINTPREVIEW = 1205;
			public const int ICON1206_MENU_FILE_PAGESETUP    = 1206;
			public const int ICON1900_MISC_UNKNOWN           = 1900;
			public const int ICON1901_MISC_REFRESH           = 1901;
			public const int ICON1902_MISC_EXPAND            = 1902;
			public const int ICON1903_MISC_COLLAPSE          = 1903;

			public const int SENDKEY_MODE_MSG   = 0;
			public const int SENDKEY_MODE_EVENT = 1;
			public const int SENDKEY_MODE_INPUT = 2;

			[DllImport(Path)]
			public static extern int osdev_checkStatus();

			[DllImport(Path)]
			public static extern IntPtr osdev_loadIcon(uint dwIconID, int nSize, IntPtr hWnd, out uint pdwHResult);

			[DllImport(Path)]
			public static extern void osdev_sendKey(IntPtr hCtrl, uint nKeyCode, uint nModifier);

			[DllImport(Path)]
			public static extern int osdev_sendKey_getMode();

			[DllImport(Path)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool osdev_sendKey_setMode(int nMode);
		}
	}
}
 