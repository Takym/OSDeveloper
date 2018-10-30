using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ソフトウェア全体の設定を管理します。
	/// </summary>
	public static class ConfigManager
	{
		/// <summary>
		///  システムの設定を取得します。
		///  この設定を変更した場合はアプリケーションの再起動が必要になる可能性があります。
		/// </summary>
		public static YenconSection System
		{
			get
			{
				if (_system == null) {
					_system = LoadYencon(PathOfSystem, out _system_header);
				}
				return _system;
			}
		}
		private static YenconSection _system;
		private static YenconHeader _system_header;

		/// <summary>
		///  <see cref="OSDeveloper.Core.Settings.ConfigManager.System"/>
		///  の設定データが格納されているファイルのファイルパスを取得します。
		/// </summary>
		public static PathString PathOfSystem { get; }

		static ConfigManager()
		{
			PathOfSystem = SystemPaths.Settings.Bond("system.inix");
		}

		/// <summary>
		///  システム設定を読み込み適用させます。
		/// </summary>
		public static void ApplySystemSettings()
		{
			{
				// ビジュアルスタイルの設定を読み込み
				var vs_node = System.GetNode("visualstyle", new YenconStringKey() {
					Text = Application.VisualStyleState.ToString()
				});
				if (vs_node.Kind == YenconType.StringKey) {
					if (Enum.TryParse(vs_node.Value.GetValue().ToString(), out VisualStyleState vs)) {
						Application.VisualStyleState = vs;
					}
				}
			}

			{
				// 言語設定を読み込み
				var lang_node = System.GetNode("lang", new YenconStringKey() {
					Text = CultureInfo.CurrentCulture.Name
				});
				if (lang_node.Kind == YenconType.StringKey) {
					CultureInfo ci;
					try {
						ci = CultureInfo.GetCultureInfo(lang_node.Value.GetValue().ToString());
					} catch (CultureNotFoundException) {
						ci = CultureInfo.CurrentCulture;
					}
					CultureInfo.CurrentCulture = ci;
					CultureInfo.CurrentUICulture = ci;
				}
			}

			{
				// 内ログの設定を読み込み
				var log_node = System.GetNode("debug_and_logs", new YenconSection(new YenconNode[] {
					new YenconNode() { Name = "no_internal_log", Value = new YenconBooleanKey() { Flag = false } },
					new YenconNode() { Name = "fname_kind", Value = new YenconNumberKey() { Count = 2 } }
				}));
				if (log_node.Kind == YenconType.Section) {
					YenconSection section = ((YenconSection)(log_node.Value));
					// 内部ログを生成するかどうか
					var no_internal_log = section["no_internal_log"];
					if (no_internal_log.Kind == YenconType.BooleanKey) {
						LogFile.NoInternalLog = ((YenconBooleanKey)(no_internal_log)).Flag;
					}
					// 内部ログの名前の種類
					var fname_kind = section["fname_kind"];
					if (fname_kind.Kind == YenconType.NumberKey) {
						LogFile.InternalNameKind = ((YenconNumberKey)(fname_kind)).Count;
					}
				}
			}

			// 現在の設定データを保存
			Save();
		}

		/// <summary>
		///  現在の設定データをファイルに保存します。
		/// </summary>
		public static void Save()
		{
			SaveYencon(PathOfSystem, _system_header, _system);
		}

		/// <summary>
		///  指定されたファイルをヱンコンとして読み込みます。
		/// </summary>
		/// <param name="path">読み込むファイルのパスです。</param>
		/// <param name="binhdr">バイナリ形式の場合はヘッダー情報、テキスト形式の場合は<see langword="null"/>を返します。</param>
		/// <returns>読み込んだヱンコンオブジェクトです。</returns>
		public static YenconSection LoadYencon(string path, out YenconHeader binhdr)
		{
			var header = new YenconHeader();
			var result = YenconBinaryFile.Scan(path, header);
			if (result == null) {
				// テキスト形式
				binhdr = null;
				return YenconParser.Load(path);
			} else {
				// バイナリ形式
				binhdr = header;
				return new YenconSection(result);
			}
		}

		/// <summary>
		///  指定されたヱンコンオブジェクトを保存します。
		/// </summary>
		/// <param name="path">保存先のファイルのパスです。</param>
		/// <param name="binhdr">バイナリ形式の場合はヘッダー情報、テキスト形式の場合は<see langword="null"/>を指定してください。</param>
		/// <param name="yencon">保存するヱンコンオブジェクトです。</param>
		public static void SaveYencon(string path, YenconHeader binhdr, YenconSection yencon)
		{
			if (binhdr == null) {
				// テキスト形式
				YenconParser.Save(path, yencon);
			} else {
				// バイナリ形式
				byte[] buf = YenconBinaryFile.ConvertToBinary(new List<YenconNode>(yencon.Children.Values), binhdr);
				using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
				using (BinaryWriter bw = new BinaryWriter(fs)) {
					bw.Write(buf);
				}
			}
		}
	}
}
