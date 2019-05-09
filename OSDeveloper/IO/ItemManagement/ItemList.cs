using System.Collections.Generic;
using OSDeveloper.IO.Logging;
using TakymLib;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public static class ItemList
	{
		private static bool                                         _is_initialized;
		private static Logger                                       _logger;
		private static SortedDictionary<PathString, FolderMetadata> _dirs;
		private static SortedDictionary<PathString, FileMetadata>   _files;

		#region コンストラクタ/デストラクタ
		static ItemList()
		{
			Init();
		}

		internal static void Init()
		{
			if (!_is_initialized) {
				_logger = Logger.Get(nameof(ItemList));
				_dirs   = new SortedDictionary<PathString, FolderMetadata>();
				_files  = new SortedDictionary<PathString, FileMetadata>();
				_is_initialized = true;

				_logger.Trace("constructed the item list of files/directories");
			}
		}

		internal static void Final()
		{
			if (_is_initialized) {
				_logger.Trace("destructing the item list of files/directories...");

				_dirs   .Clear();
				_dirs   = null;
				_files  .Clear();
				_files  = null;
				_logger = null;
				_is_initialized = false;
			}
		}
		#endregion

		public static FolderMetadata GetDir(PathString path)
		{
			if (path == null) return null;
			if (_dirs.ContainsKey(path)) {
				_logger.Trace($"getting the folder metadata object for:\"{path}\"...");
				return _dirs[path];
			} else {
				_logger.Trace($"creating the folder metadata object for:\"{path}\"...");
				var result = new FolderMetadata(path);
				_dirs.Add(path, result);
				return result;
			}
		}

		public static FileMetadata GetFile(PathString path, FileFormat format)
		{
			if (path == null) return null;
			if (_files.ContainsKey(path)) {
				_logger.Trace($"getting the file metadata object for:\"{path}\"...");
				_files[path].Format = format;
				return _files[path];
			} else {
				_logger.Trace($"creating the file metadata object for:\"{path}\"...");
				var result = new FileMetadata(path, format);
				_files.Add(path, result);
				return result;
			}
		}

		internal static bool RenameItem(PathString oldpath, PathString newpath)
		{
			if (oldpath == null || newpath == null) return false;
			if (_dirs.ContainsKey(oldpath)) {
				_logger.Trace($"renaming the folder metadata object for:\"{oldpath}\"...");
				var meta = _dirs[oldpath];
				if (_dirs.Remove(oldpath)) {
					_dirs.Add(newpath, meta);
					_logger.Notice("renamed the folder.");
					return true;
				} else {
					_logger.Notice("could not rename the folder.");
					return false;
				}
			} else if (_files.ContainsKey(oldpath)) {
				_logger.Trace($"renaming the file metadata object for:\"{oldpath}\"...");
				var meta = _files[oldpath];
				if (_files.Remove(oldpath)) {
					_files.Add(newpath, meta);
					_logger.Notice("renamed the file.");
					return true;
				} else {
					_logger.Notice("could not rename the file.");
					return false;
				}
			} else {
				_logger.Warn($"\"{oldpath}\" does not exist in the file/folder list.");
				_logger.Notice("could not rename the file/folder.");
				return false;
			}
		}

		internal static bool RemoveItem(PathString path)
		{
			if (path == null) return false;
			if (_dirs.ContainsKey(path)) {
				_logger.Trace($"removing the folder metadata object for:\"{path}\"...");
				return _dirs.Remove(path);
			} else if (_files.ContainsKey(path)) {
				_logger.Trace($"removing the file metadata object for:\"{path}\"...");
				return _files.Remove(path);
			} else {
				_logger.Warn($"\"{path}\" does not exist in the file/folder list.");
				_logger.Notice("could not remove the file/folder.");
				return false;
			}
		}

		internal static void ClearRemovedItems()
		{
			_logger.Trace($"clearing removed items...");
			_dirs.RemoveAll((KeyValuePair<PathString, FolderMetadata> k) => k.Value.IsRemoved);
			_files.RemoveAll((KeyValuePair<PathString, FileMetadata> k) => k.Value.IsRemoved);
			_logger.Trace($"cleared removed items");
		}

		internal static PathString[] GetLoadedDirs()
		{
			int i = 0;
			var result = new PathString[_dirs.Count];
			foreach (var item in _dirs) {
				result[i] = item.Key;
				++i;
			}
			return result;
		}

		internal static PathString[] GetLoadedFiles()
		{
			int i = 0;
			var result = new PathString[_files.Count];
			foreach (var item in _files) {
				result[i] = item.Key;
				++i;
			}
			return result;
		}
	}
}
