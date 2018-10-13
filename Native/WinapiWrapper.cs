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
			public const string Path = "C:\\Windows\\System32\\kernel32.dll";

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
		///  <see langword="LIBOSDEV.DLL"/>内のAPIを呼び出します。
		/// </summary>
		public static class Libosdev
		{
			/// <summary>
			///  このクラスで読み込むライブラリのファイルパスを取得します。
			/// </summary>
			public const string Path = "libosdev.dll";

			/* libosdev.h */

			/// <summary>
			///  ライブラリが正常に実行している事を表します。
			/// </summary>
			public const uint LIB_STATE_NORMAL			= 0x00000000;

			/// <summary>
			///  ライブラリがまだ初期化されていない事を表します。
			/// </summary>
			public const uint LIB_STATE_NOT_INIT		= 0x00000001;

			/// <summary>
			///  ライブラリがファイルシステムに存在しない事を表します。
			/// </summary>
			public const uint LIB_STATE_NOT_FOUND		= 0x00000002;

			/// <summary>
			///  不明な状態を表します。
			/// </summary>
			public const uint LIB_STATE_UNKNOWN			= 0xFFFFFFFF;

			/// <summary>
			///  ライブラリの状態を確認します。
			/// </summary>
			/// <returns>ライブラリの状態を表す番号です。</returns>
			[DllImport(Path)]
			public extern static uint osdev_checkStatus();

			/// <summary>
			///  アプリケーションのアイコンです。
			/// </summary>
			public const uint ICON_Main = 0;

			/// <summary>
			///  メインウィンドウのアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_FormMain_v0_0_0_0 = 1;

			/// <summary>
			///  不明である事を表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_Unknown_v0_0_0_0 = 2;

			/// <summary>
			///  ファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_File_v0_0_0_0 = 3;

			/// <summary>
			///  バイナリファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_BinaryFile_v0_0_0_0 = 4;

			/// <summary>
			///  テキストファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_TextFile_v0_0_0_0 = 5;

			/// <summary>
			///  フォルダを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_Folder_v0_0_0_0 = 6;

			/// <summary>
			///  閉じているフォルダを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_FolderClose_v0_0_0_0 = 7;

			/// <summary>
			///  開いているフォルダを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_FolderOpen_v0_0_0_0 = 8;

			/// <summary>
			///  「更新」を表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_Refresh_v0_0_0_0 = 9;

			/// <summary>
			///  「全て表示」を表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_Expand_v0_0_0_0 = 10;

			/// <summary>
			///  「全て隠す」を表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_Collapse_v0_0_0_0 = 11;

			/// <summary>
			///  エディタウィンドウ用のアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_EditorWindow_v0_0_0_0 = 12;

			/// <summary>
			///  プログラムファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_ProgramFile_v0_0_0_0 = 13;

			/// <summary>
			///  ソースコードファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_SourceCodeFile_v0_0_0_0 = 14;

			/// <summary>
			///  リソースファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_ResourceFile_v0_0_0_0 = 15;

			/// <summary>
			///  ドキュメントファイルを表すアイコンです。
			///  このアイコンは<c>0.0.0.0</c>で追加された物です。
			/// </summary>
			public const uint ICON_DocumentFile_v0_0_0_0 = 16;

			/// <summary>
			///  指定されたIDのアイコンを取得します。
			/// </summary>
			/// <param name="dwIconID">取得するアイコンのリソースIDから1000を引いた値です。</param>
			/// <param name="hWnd">
			///  エラーダイアログを表示する場合は、親ウィンドウのハンドル、
			///  表示しない場合、<see cref="System.IntPtr.Zero"/>を指定します。
			/// </param>
			/// <param name="pdwHResult">エラーが発生した場合、そのエラーの番号が代入されます。</param>
			/// <returns>アイコンハンドルを表す型'<see cref="System.IntPtr"/>'の値です。</returns>
			[DllImport(Path)]
			public extern static IntPtr osdev_getIcon(uint dwIconID, IntPtr hWnd, out uint pdwHResult);
		}
	}
}
 