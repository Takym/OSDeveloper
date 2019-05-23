using System.Collections.Generic;
using System.IO;
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

		public static ItemMetadata GetItem(PathString path)
		{
			if (Directory.Exists(path)) {
				return GetDir(path);
			} else if (File.Exists(path)) {
				return GetFile(path);
			}
			return null;
		}

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

		public static FileMetadata GetFile(PathString path)
		{
			if (path == null) return null;
			if (_files.ContainsKey(path)) {
				_logger.Trace($"getting the file metadata object for:\"{path}\"...");
				return _files[path];
			} else {
				_logger.Trace($"creating the file metadata object for:\"{path}\"...");
				var result = new FileMetadata(path, FileFormat.Unknown);
				_files.Add(path, result);
				return result;
			}
		}

		public static FolderMetadata CreateDir(PathString path)
		{
			if (path == null) return null;
			if (_dirs.ContainsKey(path)) {
				_logger.Trace($"getting the folder metadata object for:\"{path}\"...");
				return _dirs[path];
			} else {
				_logger.Trace($"creating a new folder and its metadata object for:\"{path}\"...");
				Directory.CreateDirectory(path);
				var result = new FolderMetadata(path);
				_dirs.Add(path, result);
				return result;
			}
		}

		public static FileMetadata CreateNewFile(PathString path, FileFormat format)
		{
			if (path == null) return null;
			if (_files.ContainsKey(path)) {
				_logger.Trace($"recreating the file and its file metadata object for:\"{path}\"...");
				if (RemoveItem(path)) {
					return CreateNewFile(path, format);
				} else {
					return null; // 絶対に来ない筈
				}
			} else {
				_logger.Trace($"creating a new file and its file metadata object for:\"{path}\"...");
				Directory.CreateDirectory(path.GetDirectory());
				File.Create(path).Close();
				var result = new FileMetadata(path, format);
				_files.Add(path, result);
				return result;
			}
		}

		internal static FileMetadata GetFile(PathString path, FileFormat format)
		{
			if (path == null) return null;
			var meta = GetFile(path);
			if (meta == null) return null;
			meta.Format = format;
			return meta;
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
