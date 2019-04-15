using System.Collections.Generic;
using System.Text;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.Resources;
using TakymLib;

namespace OSDeveloper.IO
{
	public static class FileTypeRegistry
	{
		private readonly static List<FileType> _types;
		private          static string[]       _all_exts;

		static FileTypeRegistry()
		{
			_types = new List<FileType>();

			_types.Add(new SystemFileType(
				FileFormat.Binary,
				nameof(FileTypeNames.BinaryFile),
				false,
				"bin", "dat", "hex"));

			_types.Add(new SystemFileType(
				FileFormat.Text,
				nameof(FileTypeNames.TextFile),
				typeof(TextFileExtendedDetail),
				false,
				"txt", "text"));

			//_types.Add(new SystemFileType(
			//	FileFormat.Program,
			//	nameof(FileTypeNames.Program),
			//	false,
			//	));

			//_types.Add(new SystemFileType(
			//	FileFormat.SourceCode,
			//	nameof(FileTypeNames.SourceCode),
			//	false,
			//	));

			//_types.Add(new SystemFileType(
			//	FileFormat.Resource,
			//	nameof(FileTypeNames.Resource),
			//	false,
			//	));

			_types.Add(new SystemFileType(
				FileFormat.Document,
				nameof(FileTypeNames.Document),
				false,
				"doc", "docx", "docm", "xls", "xlsx", "xlsm", "ppt", "pptx", "pptm"));

			_types.Add(new SystemFileType(
				FileFormat.Document,
				nameof(FileTypeNames.Document2),
				false,
				"pdf", "rtf", "csv", "tsv"));

			_types.Add(new SystemFileType(
				FileFormat.Text,
				nameof(FileTypeNames.LogFile),
				typeof(TextFileExtendedDetail),
				false,
				"log"));

			_types.Add(new SystemFileType(
				FileFormat.Resource,
				nameof(FileTypeNames.YenconResource),
				false,
				"ycn", "rycn"));

			_types.Add(new SystemFileType(
				FileFormat.Text,
				nameof(FileTypeNames.YenconText),
				false,
				"tycn", "inix"));

			_types.Add(new SystemFileType(
				FileFormat.Binary,
				nameof(FileTypeNames.YenconBinary),
				false,
				"bycn", "cfg"));
		}

		public static void Add(FileType fileType)
		{
			if (!_types.Contains(fileType)) {
				_types.Add(fileType);
				_all_exts = null;
			}
		}

		public static void Remove(FileType fileType)
		{
			if (_types.Contains(fileType)) {
				// システムによって定義されたファイルの種類が削除されない様にする。
				if (fileType is SystemFileType sft) {
					if (sft.Removable) { // 削除可能フラグが付いている場合は削除する。
						_types.Remove(sft);
						_all_exts = null;
					}
				} else { // それ以外 (普通のFileType)
					_types.Remove(fileType);
					_all_exts = null;
				}
			}
		}

		public static string[] GetAllExtensions()
		{
			if (_all_exts == null) {
				List<string> result = new List<string>();
				for (int i = 0; i < _types.Count; ++i) {
					for (int j = 0; j < _types[i].Extensions.Length; ++j) {
						if (!result.Contains(_types[i].Extensions[j])) {
							result.Add(_types[i].Extensions[j]);
						}
					}
				}
				_all_exts = result.ToArray();
			}
			return ((string[])(_all_exts.Clone()));
		}

		public static string CreateFullSPFs()
		{
			// SPF = Search Pattern Filter
			StringBuilder sb = new StringBuilder();
			sb.Append(new AllSupportedFileTypes().CreateSPF()).Append('|');
			for (int i = 0; i < _types.Count; ++i) {
				sb.Append(_types[i].CreateSPF()).Append('|');
			}
			sb.Append($"{FileTypeNames.AllFiles}|*");
			return sb.ToString();
		}

		public static string CreateSPFs(this FileType[] fileTypes)
		{
			// SPF = Search Pattern Filter
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < fileTypes.Length; ++i) {
				sb.Append(fileTypes[i].CreateSPF()).Append('|');
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public static FileType[] GetAll()
		{
			return _types.ToArray();
		}

		public static FileType[] GetByName(string name)
		{
			List<FileType> result = new List<FileType>();
			for (int i = 0; i < _types.Count; ++i) {
				if (_types[i].Name == name) {
					// 名前が name である FileType を全て返還する。
					result.Add(_types[i]);
				}
			}
			return result.ToArray();
		}

		public static FileType[] GetByExtension(string ext)
		{
			List<FileType> result = new List<FileType>();
			for (int i = 0; i < _types.Count; ++i) {
				if (_types[i].Extensions.ContainsValue(ext)) {
					// ext を含む全ての FileType を返還する。
					result.Add(_types[i]);
				}
			}
			return result.ToArray();
		}
	}
}
