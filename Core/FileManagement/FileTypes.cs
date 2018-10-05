using System;
using System.Text;
using OSDeveloper.Core.FileManagement.Structures;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.FileManagement
{
	/// <summary>
	///  <see langword="OSDeveloper"/>で利用される全てのファイルの種類と拡張子を管理します。
	///  このクラスは静的です。
	/// </summary>
	public static class FileTypes
	{
		/// <summary>
		///  「バイナリファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType BinaryFile
			= new FileType(
				FileFormat.BinaryFile,
				nameof(FileTypeNames.BinaryFile),
				"bin", "dat", "hex");

		/// <summary>
		///  「ログファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType LogFile
			= new FileType(
				FileFormat.TextFile,
				nameof(FileTypeNames.LogFile),
				"log");

		/// <summary>
		///  「処理報告記録ファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType ProcessReportRecordFile
			= new FileType(
				FileFormat.BinaryFile,
				nameof(FileTypeNames.ProcessReportRecordFile),
				"pr2f");

		/// <summary>
		///  「Windowsレジストリ 下書きファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType RegistryDraftFile
			= new FileType(
				FileFormat.Document,
				typeof(RegistryDraftFile),
				nameof(FileTypeNames),
				"rd", "regd");

		/// <summary>
		///  「テキストファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType TextFile
			= new FileType(
				FileFormat.TextFile,
				nameof(FileTypeNames.TextFile),
				"txt", "text");

		/// <summary>
		///  「ヱンコン環境設定ファイル (バイナリ)」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType YenconBinaryFile
			= new FileType(
				FileFormat.BinaryFile,
				nameof(FileTypeNames.YenconBinaryFile),
				"bycn", "cfg");

		/// <summary>
		///  「ヱンコン環境設定ファイル (テキスト)」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType YenconTextFile
			= new FileType(
				FileFormat.TextFile,
				nameof(FileTypeNames.YenconTextFile),
				"ycn", "inix");

		/// <summary>
		///  「ヱンコン環境設定ファイル (リソース)」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType YenconFile
			= new FileType(
				FileFormat.Resource,
				nameof(FileTypeNames.YenconFile),
				ArrayUtils.Join(
					YenconTextFile.Extensions,
					YenconBinaryFile.Extensions));

		/// <summary>
		///  「サポートされている全てのファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly FileType AllSupportedFiles
			= new FileType(
				FileFormat.BinaryFile,
				nameof(FileTypeNames.AllSupportedFiles),
				ArrayUtils.Join(
					BinaryFile.Extensions,
					LogFile.Extensions,
					ProcessReportRecordFile.Extensions,
					RegistryDraftFile.Extensions,
					TextFile.Extensions,
					YenconFile.Extensions));

		/// <summary>
		///  指定されたファイルの種類名と拡張子の配列のタプルからファイルを検索する
		///  ために利用する<see langword="Search Pattern Filter"/>を作成します。
		/// </summary>
		/// <param name="obj">
		///  ファイルの種類の名前とピリオドの付かない拡張子の配列の二つの要素を持ったタプルです。
		/// </param>
		/// <returns>生成された<see langword="Search Pattern Filter"/>です。</returns>
		[Obsolete("use a FileType class")]
		public static string CreateSPF(this (string type, string[] exts) obj)
		{
			StringBuilder filter = new StringBuilder();
			for (int i = 0; i < obj.exts.Length; ++i) {
				if (i != 0) filter.Append(';');
				filter.Append($"*.{obj.exts[i]}");
			}
			return $"{obj.type} ({filter.ToString()})|{filter.ToString()}";
		}

		/// <summary>
		///  指定されたファイルの種類からファイルを検索する
		///  ために利用する<see langword="Search Pattern Filter"/>を作成します。
		/// </summary>
		/// <param name="obj">ファイルの種類を表す<see cref="OSDeveloper.Core.FileManagement.FileType"/>です。</param>
		/// <returns>生成された<see langword="Search Pattern Filter"/>です。</returns>
		public static string CreateSPF(this FileType obj)
		{
			StringBuilder filter = new StringBuilder();
			for (int i = 0; i < obj.Extensions.Length; ++i) {
				if (i != 0) filter.Append(';');
				filter.Append($"*.{obj.Extensions[i]}");
			}
			return $"{obj.GetLocalizedDisplayName()} ({filter.ToString()})|{filter.ToString()}";
		}

		/// <summary>
		///  対応している全てのファイルの<see langword="Search Pattern Filter"/>を作成します。
		/// </summary>
		/// <returns>生成された<see langword="Search Pattern Filter"/>です。</returns>
		public static string CreateFullSPFs()
		{
			return $"{FileTypeNames.AllFiles}|*"
				+ "|" + AllSupportedFiles.CreateSPF()
				+ "|" + BinaryFile.CreateSPF()
				+ "|" + LogFile.CreateSPF()
				+ "|" + ProcessReportRecordFile.CreateSPF()
				+ "|" + RegistryDraftFile.CreateSPF()
				+ "|" + TextFile.CreateSPF()
				+ "|" + YenconFile.CreateSPF()
				+ "|" + YenconBinaryFile.CreateSPF()
				+ "|" + YenconTextFile.CreateSPF();
		}

		/// <summary>
		///  指定された拡張子からファイルの種類を調べます。
		/// </summary>
		/// <param name="ext">ピリオドの付かない拡張子です。</param>
		/// <returns>
		///  ファイルの種類を表す<see cref="OSDeveloper.Core.FileManagement.FileType"/>です。
		///  不明なファイルの場合は<see langword="null"/>になります。
		/// </returns>
		public static FileType CheckFileType(string ext)
		{
			if (BinaryFile.Contains(ext)) {
				return BinaryFile;
			} else if (LogFile.Contains(ext)) {
				return LogFile;
			} else if (ProcessReportRecordFile.Contains(ext)) {
				return ProcessReportRecordFile;
			} else if (RegistryDraftFile.Contains(ext)) {
				return RegistryDraftFile;
			} else if (TextFile.Contains(ext)) {
				return TextFile;
			} else if (YenconBinaryFile.Contains(ext)) {
				return YenconBinaryFile;
			} else if (YenconTextFile.Contains(ext)) {
				return YenconTextFile;
			} else {
				return null;
			}
		}
	}
}
