using System.Collections.Generic;
using OSDeveloper.IO.Logging;
using TakymLib;
using TakymLib.IO;

namespace OSDeveloper.IO.ItemManagement
{
	public static class ItemList
	{
		private static bool                                   _is_initialized;
		private static Logger                                 _logger;
		private static Dictionary<PathString, FolderMetadata> _dirs;
		private static Dictionary<PathString, FileMetadata>   _files;

		#region コンストラクタ/デストラクタ
		static ItemList()
		{
			Init();
		}

		internal static void Init()
		{
			if (!_is_initialized) {
				_logger = Logger.Get(nameof(ItemList));
				_dirs   = new Dictionary<PathString, FolderMetadata>();
				_files  = new Dictionary<PathString, FileMetadata>();
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
				return _files[path];
			} else {
				_logger.Trace($"creating the file metadata object for:\"{path}\"...");
				var result = new FileMetadata(path, format);
				_files.Add(path, result);
				return result;
			}
		}

		public static void ClearRemovedItems()
		{
			_logger.Trace($"clearing removed items...");
			_dirs.RemoveAll((KeyValuePair<PathString, FolderMetadata> k) => k.Value.IsRemoved);
			_files.RemoveAll((KeyValuePair<PathString, FileMetadata> k) => k.Value.IsRemoved);
			_logger.Trace($"cleared removed items");
		}
	}
}
