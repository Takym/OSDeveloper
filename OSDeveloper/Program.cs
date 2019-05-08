using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using OSDeveloper.IO.Configuration;
using OSDeveloper.IO.ItemManagement;
using OSDeveloper.IO.Logging;
using OSDeveloper.Native;
using OSDeveloper.Resources;
using TakymLib.AOP;

namespace OSDeveloper
{
	internal static class Program
	{
		public static Logger Logger;

		[STAThread()]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#if RELEASE
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += Application_ThreadException;
#endif

			// ログファイル初期化
			LogFile.Init();
			Logger.Init();
			Logger = Logger.Get("system");
			Logger.Info($"The application is started with command-line: {{{string.Join(", ", args)}}}");
			// アスペクト処理にロガーを設定
			LoggingAspectBehavior.Logger = Logger.Get("aop");

			// 設定読み込み
			SettingManager.Init();
			Application.VisualStyleState = SettingManager.System.VisualStyle;
			var lang = SettingManager.System.Language;

			// システム設定をログに書き込み
			CultureInfo.DefaultThreadCurrentCulture   = CultureInfo.GetCultureInfo("ja");
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("ja");
			string culture = lang.Name + "; EN: " + lang.EnglishName + "; 日: " + lang.DisplayName + "; ::: " + lang.NativeName;
			Logger.Debug($"{nameof(Application)}.{nameof(Application.VisualStyleState)} = {Application.VisualStyleState}");
			Logger.Debug($"{nameof(CultureInfo)}.{nameof(CultureInfo.DefaultThreadCurrentCulture)} = {culture}");
			Logger.Debug($"{nameof(FormMain)}.{nameof(FormMain.Location)} = {SettingManager.System.MainWindowPosition}");
			Logger.Debug($"{nameof(SettingManager.System.UseEXDialog)} = {SettingManager.System.UseEXDialog}");
			Logger.Debug($"{nameof(SettingManager.System.UseWSLCommand)} = {SettingManager.System.UseWSLCommand}");

			// 言語設定
			CultureInfo.DefaultThreadCurrentCulture   = lang;
			CultureInfo.DefaultThreadCurrentUICulture = lang;

			// ネイティブライブラリ読み込み
			var s = Libosdev.CheckStatus();
			if (s != Libosdev.Status.Loaded) {
				Logger.Fatal("could not load \"libosdev.dll\"");
				Logger.Info($"status = {s}");
				MessageBox.Show(
					ErrorMessages.Libosdev_CannotLoad,
					ASMINFO.Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				goto end;
			}

			// ファイル/ディレクトリ リストの初期化
			ItemList.Init();

			// メインウィンドウ表示
			Application.Run(new FormMain(args));

end:

			// ファイル/ディレクトリ リストの破棄
			ItemList.Final();

			// 設定書き込み
			SettingManager.Final();

			// ログファイル破棄
			Logger.Info("The application is terminating...");
			Logger.Final();
			LogFile.Final();
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Logger.Trace($"executing {nameof(Application_ThreadException)}...");
			Logger.Exception(e.Exception, true);

			var dr = MessageBox.Show(
				e.Exception.Message,
				ASMINFO.Caption,
				MessageBoxButtons.AbortRetryIgnore,
				MessageBoxIcon.Error);
			if (dr == DialogResult.Abort) {
				Application.Exit();
			} else if (dr == DialogResult.Retry) {
				// エラー発生源には戻る事ができないため。
				MessageBox.Show(
					FormMainRes.Error_CannotRetry,
					ASMINFO.Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				// 取り敢えず再スロー
				throw new Exception(e.Exception.Message, e.Exception);
			}

			Logger.Trace($"completed {nameof(Application_ThreadException)}...");
		}
	}
}
