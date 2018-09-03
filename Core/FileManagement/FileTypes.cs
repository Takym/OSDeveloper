using System.Text;
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
		///  「サポートされている全てのファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly (string type, string[] exts) AllSupportedFiles
			= (FileTypeNames.AllSupportedFiles, ArrayUtils.Join(
				LogFile.exts,
				ProcessReportRecordFile.exts,
				YenconFile.exts));

		/// <summary>
		///  「ログファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly (string type, string[] exts) LogFile
			= (FileTypeNames.LogFile, new string[] { "log" });

		/// <summary>
		///  「処理報告記録ファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly (string type, string[] exts) ProcessReportRecordFile
			= (FileTypeNames.ProcessReportRecordFile, new string[] { "pr2f" });

		/// <summary>
		///  「ヱンコン環境設定ファイル」を表す全ての拡張子を取得します。
		/// </summary>
		public static readonly (string type, string[] exts) YenconFile
			= (FileTypeNames.YenconFile, new string[] { "ycn", "inix" });

		/// <summary>
		///  指定されたファイルの種類名と拡張子の配列のタプルからファイルを検索する
		///  ために利用する<see langword="Search Pattern Filter"/>を作成します。
		/// </summary>
		/// <param name="obj">
		///  ファイルの種類の名前とピリオドの付かない拡張子の配列の二つの要素を持ったタプルです。
		/// </param>
		/// <returns>生成された<see langword="Search Pattern Filter"/>です。</returns>
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
		///  対応している全てのファイルの<see langword="Search Pattern Filter"/>を作成します。
		/// </summary>
		/// <returns>生成された<see langword="Search Pattern Filter"/>です。</returns>
		public static string CreateFullSPFs()
		{
			return $"{FileTypeNames.AllFiles}|*"
				+ "|" + AllSupportedFiles.CreateSPF()
				+ "|" + LogFile.CreateSPF()
				+ "|" + ProcessReportRecordFile.CreateSPF()
				+ "|" + YenconFile.CreateSPF();
		}
	}
}
