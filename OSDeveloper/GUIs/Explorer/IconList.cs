using System.Collections.Generic;
using System.Drawing;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.GUIs.Explorer
{
	public static class IconList
	{
		private readonly static Logger     _logger;
		private readonly static List<Icon> _icons_file;
		private readonly static List<Icon> _icons_folder;
		private readonly static Icon       _cannot_access;

		private readonly static int _file, _bin, _txt, _prg, _src, _res, _doc;
		private readonly static int _folder, _closed, _opened, _dir, _dirc, _diro, _fdd, _hdd, _odd, _jun, _junc, _juno;
		public readonly static int _cntacs;

		static IconList()
		{
			_logger       = Logger.Get(nameof(IconList));
			_icons_file   = new List<Icon>();
			_icons_folder = new List<Icon>();

			{
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.File,           out uint v0)); _file = 0;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileBinary,     out uint v1)); _bin  = 1;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileText,       out uint v2)); _txt  = 2;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileProgram,    out uint v3)); _prg  = 3;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileSourceCode, out uint v4)); _src  = 4;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileResource,   out uint v5)); _res  = 5;
				_icons_file.Add(Libosdev.GetIcon(Libosdev.Icons.FileDocument,   out uint v6)); _doc  = 6;
			}

			{
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.Folder,          out uint v00)); _folder = 00 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderClosed,    out uint v01)); _closed = 01 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderOpened,    out uint v02)); _opened = 02 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderDirectory, out uint v03)); _dir    = 03 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderDirClosed, out uint v04)); _dirc   = 04 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderDirOpened, out uint v05)); _diro   = 05 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderFDD,       out uint v06)); _fdd    = 06 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderHDD,       out uint v07)); _hdd    = 07 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderODD,       out uint v08)); _odd    = 08 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderJunction,  out uint v09)); _jun    = 09 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderJunClosed, out uint v10)); _junc   = 10 + _icons_file.Count;
				_icons_folder.Add(Libosdev.GetIcon(Libosdev.Icons.FolderJunOpened, out uint v11)); _juno   = 11 + _icons_file.Count;
			}

			{
				_cannot_access = Libosdev.GetIcon(Libosdev.Icons.ItemCannotAccess, out uint v0);
				_cntacs = _icons_file.Count + _icons_folder.Count;
			}

			_logger.Trace($"constructed {nameof(IconList)}");
		}

		public static Image[] CreateImageArray()
		{
			_logger.Info("creating an image array from the icon list...");
			Image[] result = new Image[_icons_file.Count + _icons_folder.Count + 1];
			for (int i = 0; i < _icons_file.Count; ++i) {
				result[i] = _icons_file[i].ToBitmap();
			}
			for (int j = 0; j < _icons_folder.Count; ++j) {
				result[j + _icons_file.Count] = _icons_folder[j].ToBitmap();
			}
			result[_icons_file.Count + _icons_folder.Count] = _cannot_access.ToBitmap();
			return result;
		}

		public static int File         { get => _file; }
		public static int BinaryFile   { get => _bin; }
		public static int TextFile     { get => _txt; }
		public static int ProgramFile  { get => _prg; }
		public static int SourceFile   { get => _src; }
		public static int ResourceFile { get => _res; }
		public static int DocumentFile { get => _doc; }

		public static int Folder       { get => _folder; }
		public static int FolderClosed { get => _closed; }
		public static int FolderOpened { get => _opened; }
		public static int Directory    { get => _dir; }
		public static int DirClosed    { get => _dirc; }
		public static int DirOpened    { get => _diro; }
		public static int FloppyDisk   { get => _fdd; }
		public static int HardDisk     { get => _hdd; }
		public static int OpticalDisc  { get => _odd; }
		public static int Junction     { get => _jun; }
		public static int JunClosed    { get => _junc; }
		public static int JunOpened    { get => _juno; }

		public static int CannotAccess { get => _cntacs; }
	}
}
