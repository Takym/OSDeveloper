using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OSDeveloper.Core.FileManagement;

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
					_system = YenconParser.Load(PathOfSystem);
				}
				return _system;
			}
		}
		private static YenconSection _system;

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
			// ビジュアルスタイルの設定を読み込み
			if (System.Children.ContainsKey("visualstyle")) {
				// キーが存在する場合は、そのまま読み込み
				var vs_node = System.Children["visualstyle"];
				if (vs_node.Kind == YenconType.StringKey) {
					if (Enum.TryParse(vs_node.Value.GetValue().ToString(), out VisualStyleState vs)) {
						Application.VisualStyleState = vs;
					}
				}
			} else {
				// キーが無い場合は、限定の設定から新しく作成
				var vs_node = new YenconNode();
				vs_node.Name = "visualstyle";
				vs_node.Value = new YenconStringKey() {
					Text = Application.VisualStyleState.ToString(),
				};
				System.Children.Add(vs_node.Name, vs_node);
			}

			// 言語設定を読み込み
			if (System.Children.ContainsKey("lang")) {
				// キーが存在する場合は、そのまま読み込み
				var lang_node = System.Children["lang"];
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
			} else {
				// キーが無い場合は、限定の設定から新しく作成
				var lang_node = new YenconNode();
				lang_node.Name = "lang";
				lang_node.Value = new YenconStringKey() {
					Text = CultureInfo.CurrentCulture.Name,
				};
				System.Children.Add(lang_node.Name, lang_node);
			}

			// 現在の設定データを保存
			Save();
		}

		/// <summary>
		///  現在の設定データをファイルに保存します。
		/// </summary>
		public static void Save()
		{
			YenconParser.Save(PathOfSystem, _system);
		}
	}
}
