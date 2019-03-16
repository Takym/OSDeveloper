using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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

			// TODO: 設定で変更可能にする項目 Application.VisualStyleState
			Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled;

			// TODO: 設定で変更可能にする項目 CultureInfo
			CultureInfo.DefaultThreadCurrentCulture   = CultureInfo.InstalledUICulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InstalledUICulture;

			// システム設定をログに書き込み
			string culture = CultureInfo.DefaultThreadCurrentCulture.Name
				+ "; EN: " + CultureInfo.DefaultThreadCurrentCulture.EnglishName
				+ "; ::: " + CultureInfo.DefaultThreadCurrentCulture.NativeName;
			Logger.Debug($"{nameof(Application)}.{nameof(Application.VisualStyleState)} = {Application.VisualStyleState}");
			Logger.Debug($"{nameof(CultureInfo)}.{nameof(CultureInfo.DefaultThreadCurrentCulture)} = {culture}");

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

#if DEBUG
			// ちょっと実験 (AOP)
			using (Class1 class1 = new Class1("xyz")) {
				MessageBox.Show(class1.ABC(123).ToString());
				class1.MojiretsuField = "qwerty";
				MessageBox.Show(class1.MojiretsuProperty);
				class1.MojiretsuProperty = "kezboard";
				MessageBox.Show(class1.MojiretsuProperty);
			}
#endif

			// メインウィンドウ表示
			Application.Run(new FormMain(args));

end:
			// ログファイル破棄
			Logger.Info("The application is terminating...");
			Logger.Final();
			LogFile.Final();
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Logger.Trace($"executing {nameof(Application_ThreadException)}...");
			Logger.Exception(e.Exception);

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
				throw e.Exception;
			}

			Logger.Trace($"completed {nameof(Application_ThreadException)}...");
		}
	}
}
