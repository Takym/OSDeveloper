using System;
using System.Drawing;
using System.Threading;
using OSDeveloper.IO.Logging;
using static OSDeveloper.Native.WinapiWrapper.Libosdev;

namespace OSDeveloper.Native
{
	/// <summary>
	///  <see cref="OSDeveloper.Native.WinapiWrapper.Libosdev"/>内の関数を利用し易い形にラップします。
	/// </summary>
	public static class Libosdev
	{
		private readonly static Logger _logger;

		static Libosdev()
		{
			_logger = Logger.Get(nameof(Libosdev));
		}

		public enum Status
		{
			NotInit  = LIB_STATE_NOT_INIT,
			NotFound = LIB_STATE_NOT_FOUND,
			Loaded   = LIB_STATE_LOADED,
			Disposed = LIB_STATE_DISPOSED
		}

		public static Status CheckStatus()
		{
			try {
				_logger.Trace("loading \"libosdev.dll\"...");
				var x = ((Status)(osdev_checkStatus()));
				if (x == Status.NotInit) {
					Random rnd = new Random();
					_logger.Warn("failed to load \"libosdev.dll\"...");
					for (int i = 0; i < 10; ++i) {
						int t = rnd.Next(5, 20);
						_logger.Info($"[{i}], will try again in {t} milliseconds...");
						Thread.Sleep(t);
						x = ((Status)(osdev_checkStatus()));
						if (x == Status.Loaded) return x;
					}
					_logger.Warn("failed to load \"libosdev.dll\" 10 times");
				}
				return x;
			} catch (Exception e) {
				_logger.Exception(e);
				return Status.NotFound;
			}
		}

		public enum Icons
		{
			Logo                 = ICON1000_LOGO,
			LogoFormMain         = ICON1001_LOGO_FORMMAIN,
			LogoJapanese         = ICON1002_LOGO_JAPANESE,
			LogoEnglish          = ICON1003_LOGO_ENGLISH,
			LogoZerolib          = ICON1048_LOGO_ZEROLIB,
			LogoZerolibEx        = ICON1049_LOGO_ZEROLIBEX,
			FormEditorWindow     = ICON1050_FORM_EDITORWINDOW,
			FormSettings         = ICON1051_FORM_SETTINGS,
			File                 = ICON1100_FILE,
			FileBinary           = ICON1101_FILE_BINARY,
			FileText             = ICON1102_FILE_TEXT,
			FileProgram          = ICON1103_FILE_PROGRAM,
			FileSourceCode       = ICON1104_FILE_SOURCECODE,
			FileResource         = ICON1105_FILE_RESOURCE,
			FileDocument         = ICON1106_FILE_DOCUMENT,
			FileSolution         = ICON1107_FILE_SOLUTION,
			Folder               = ICON1150_FOLDER,
			FolderClosed         = ICON1151_FOLDER_CLOSED,
			FolderOpened         = ICON1152_FOLDER_OPENED,
			FolderDirectory      = ICON1153_FOLDER_DIRECTORY,
			FolderDirClosed      = ICON1154_FOLDER_DIRCLOSED,
			FolderDirOpened      = ICON1155_FOLDER_DIROPENED,
			FolderFDD            = ICON1156_FOLDER_FDD,
			FolderHDD            = ICON1157_FOLDER_HDD,
			FolderODD            = ICON1158_FOLDER_ODD,
			FolderJunction       = ICON1159_FOLDER_JUNCTION,
			FolderJunClosed      = ICON1160_FOLDER_JUNCLOSED,
			FolderJunOpened      = ICON1161_FOLDER_JUNOPENED,
			FolderSolution       = ICON1162_FOLDER_SOLUTION,
			ItemCannotAccess     = ICON1199_ITEM_CANNOT_ACCESS,
			MenuFileSave         = ICON1200_MENU_FILE_SAVE,
			MenuFileSaveAs       = ICON1201_MENU_FILE_SAVEAS,
			MenuFileSaveAll      = ICON1202_MENU_FILE_SAVEALL,
			MenuFileSaveAllAs    = ICON1203_MENU_FILE_SAVEALLAS,
			MenuFilePrint        = ICON1204_MENU_FILE_PRINT,
			MenuFilePrintPreview = ICON1205_MENU_FILE_PRINTPREVIEW,
			MenuFilePageSetup    = ICON1206_MENU_FILE_PAGESETUP,
			MiscUnknown          = ICON1900_MISC_UNKNOWN,
			MiscRefresh          = ICON1901_MISC_REFRESH,
			MiscExpand           = ICON1902_MISC_EXPAND,
			MiscCollapse         = ICON1903_MISC_COLLAPSE
		}

		public static Icon GetIcon(Icons name, out uint hResult)
		{
			_logger.Trace($"getting an icon named {name}...");
			var hIcon = osdev_loadIcon(
				((uint)(name)),
				16,
				IntPtr.Zero,
				out hResult);
			_logger.Info("HResult    : " + $"0x{hResult:X8} ({hResult})");
			_logger.Info("HResult Msg: " + Kernel32.GetErrorMessage(unchecked((int)(hResult))));
			return Icon.FromHandle(hIcon);
		}

		public static Icon GetIcon(string name)
		{
			Icon result = null;
			if (Enum.TryParse(name, out Icons i)) {
				result = GetIcon(i, out uint v);
			}
			if (result == null) {
				_logger.Warn($"the specified icon (\"{name}\") is not found.");
			}
			return result ?? GetIcon(Icons.MiscUnknown, out uint w);
		}
	}
}
