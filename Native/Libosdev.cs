using System;
using System.Drawing;
using System.Windows.Forms;
using static OSDeveloper.Native.WinapiWrapper.Libosdev;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see cref="OSDeveloper.Native.WinapiWrapper.Libosdev"/>内の関数を利用し易い形にラップします。
	/// </summary>
	public static class Libosdev
	{
		/// <summary>
		///  ライブラリの状態を確認します。
		/// </summary>
		/// <returns>ライブラリの状態を表す型'<see cref="OSDeveloper.Native.LibState"/>'の値です。</returns>
		public static LibState CheckStatus(out Exception error)
		{
			error = null;
			try {
				LibState result = ((LibState)(osdev_checkStatus()));
				if (Enum.IsDefined(typeof(LibState), result)) {
					return result;
				} else {
					return LibState.Unknown;
				}
			} catch (DllNotFoundException e) {
				error = e;
				return LibState.LibraryNotFound;
			}
		}

		/// <summary>
		///  ネイティブリソースライブラリからアイコンを取得します。
		/// </summary>
		/// <param name="name">アイコンの名前です。</param>
		/// <param name="owner">
		///  エラーダイアログを表示する場合は、親ウィンドウを指定します。
		///  <see langword="null"/>の場合は、ダイアログは表示されません。
		/// </param>
		/// <param name="hResult">アイコンの読み込みに失敗した場合は、そのエラーのエラーコードを返します。</param>
		/// <returns>取得したアイコンを<see langword=".NET Framework"/>ランタイム上のオブジェクトに変換した値です。</returns>
		public static Icon GetIcon(string name, IWin32Window owner, out int hResult)
		{
			IntPtr hwnd;
			uint id;

			if (owner == null) {
				hwnd = IntPtr.Zero;
			} else {
				hwnd = owner.Handle;
			}

			switch (name) {
				case "FormMain":
				case "Unknown":
				case "File":
				case "BinaryFile":
				case "TextFile":
				case "Folder":
				case "FolderClose":
				case "FolderOpen":
				case "Refresh":
				case "Expand":
				case "Collapse":
					name += ".v0_0";
					break;
			}

			switch (name) {
				case "Main":
					id = ICON_Main;
					break;
				case "FormMain.v0":
				case "FormMain.v0_0":
				case "FormMain.v0_0_0":
				case "FormMain.v0_0_0_0":
					id = ICON_FormMain_v0_0_0_0;
					break;
				case "Unknown.v0":
				case "Unknown.v0_0":
				case "Unknown.v0_0_0":
				case "Unknown.v0_0_0_0":
					id = ICON_Unknown_v0_0_0_0;
					break;
				case "File.v0":
				case "File.v0_0":
				case "File.v0_0_0":
				case "File.v0_0_0_0":
					id = ICON_File_v0_0_0_0;
					break;
				case "BinaryFile.v0":
				case "BinaryFile.v0_0":
				case "BinaryFile.v0_0_0":
				case "BinaryFile.v0_0_0_0":
					id = ICON_BinaryFile_v0_0_0_0;
					break;
				case "TextFile.v0":
				case "TextFile.v0_0":
				case "TextFile.v0_0_0":
				case "TextFile.v0_0_0_0":
					id = ICON_TextFile_v0_0_0_0;
					break;
				case "Folder.v0":
				case "Folder.v0_0":
				case "Folder.v0_0_0":
				case "Folder.v0_0_0_0":
					id = ICON_Folder_v0_0_0_0;
					break;
				case "FolderClose.v0":
				case "FolderClose.v0_0":
				case "FolderClose.v0_0_0":
				case "FolderClose.v0_0_0_0":
					id = ICON_FolderClose_v0_0_0_0;
					break;
				case "FolderOpen.v0":
				case "FolderOpen.v0_0":
				case "FolderOpen.v0_0_0":
				case "FolderOpen.v0_0_0_0":
					id = ICON_FolderOpen_v0_0_0_0;
					break;
				case "Refresh.v0":
				case "Refresh.v0_0":
				case "Refresh.v0_0_0":
				case "Refresh.v0_0_0_0":
					id = ICON_Refresh_v0_0_0_0;
					break;
				case "Expand.v0":
				case "Expand.v0_0":
				case "Expand.v0_0_0":
				case "Expand.v0_0_0_0":
					id = ICON_Expand_v0_0_0_0;
					break;
				case "Collapse.v0":
				case "Collapse.v0_0":
				case "Collapse.v0_0_0":
				case "Collapse.v0_0_0_0":
					id = ICON_Collapse_v0_0_0_0;
					break;
				default:
					id = ICON_Unknown_v0_0_0_0;
					break;
			}

			var p = osdev_getIcon(id, hwnd, out uint pdwHRes);
			hResult = ((int)(pdwHRes));
			return Icon.FromHandle(p);
		}
	}

	/// <summary>
	///  <see cref="OSDeveloper.Native.Libosdev"/>の状態を表します。
	/// </summary>
	public enum LibState : uint
	{
		/// <summary>
		///  ライブラリが正常に実行している事を表します。
		/// </summary>
		Normal = LIB_STATE_NORMAL,

		/// <summary>
		///  ライブラリがまだ初期化されていない事を表します。
		/// </summary>
		NotInitialized = LIB_STATE_NOT_INIT,

		/// <summary>
		///  ライブラリがファイルシステムに存在しない事を表します。
		/// </summary>
		LibraryNotFound = LIB_STATE_NOT_FOUND,

		/// <summary>
		///  不明な状態を表します。
		/// </summary>
		Unknown = LIB_STATE_UNKNOWN,
	}
}
