using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Assets
{
	/// <summary>
	///  <see langword="OSDeveloper"/>用の追加フォントを利用します。
	/// </summary>
	public static class FontResources
	{
		private static Logger _logger;
		private static PrivateFontCollection _pfc;
		private static bool _initialized;

		/// <summary>
		///  読み込んだ複数のフォントを配列形式で取得します。
		/// </summary>
		internal static FontFamily[] Families
		{
			get
			{
				return _pfc.Families;
			}
		}

		static FontResources()
		{
			Init();
			_logger.Trace($"Initialized {nameof(FontResources)}");
		}

		/// <summary>
		///  静的コンストラクタが一度も呼び出されていない場合は、
		///  初期化を開始し、既に呼び出されている場合は何もしません。
		/// </summary>
		public static void Init()
		{
			if (!_initialized) {
				_logger = Logger.GetSystemLogger(nameof(FontResources));
				_logger.Trace("constructing the new static instance...");

				_pfc = new PrivateFontCollection();
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexg.ttf"));
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAexfont00301\\ipaexm.ttf"));
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAfont00303\\ipag.ttf"));
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAfont00303\\ipagp.ttf"));
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAfont00303\\ipam.ttf"));
				_pfc.AddFontFile(SystemPaths.Resources.Bond("IPAfont00303\\ipamp.ttf"));

				for (int i = 0; i < _pfc.Families.Length; ++i) {
					var item = _pfc.Families[i];
					string enus = item.GetName(CultureInfo.GetCultureInfo("en-US").LCID);
					string jajp = item.GetName(CultureInfo.GetCultureInfo("ja-JP").LCID);
					string cc = item.GetName(CultureInfo.CurrentCulture.LCID);
					_logger.Info($"Loaded a font: {i}, {item.Name}; EN:{enus}; 日:{jajp}; ::: {cc}");
				}

				_logger.Trace("constructed successfully");
				_initialized = true;
			}
		}

		/// <summary>
		///  この静的クラスで利用されている全てのリソースを破棄します。
		///  この関数の実行後、再度この静的クラスを利用する場合は、
		///  <see cref="OSDeveloper.Assets.FontResources.Init"/>を呼び出してください。
		/// </summary>
		public static void Final()
		{
			if (_initialized) {
				_logger.Trace("destructing the current static instance...");
				_pfc.Dispose();
				_initialized = false;
			}
		}

		/// <summary>
		///  ゴシック体のフォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateGothic()
		{
			return new Font(_pfc.Families[4], 12, FontStyle.Regular, GraphicsUnit.Point);
		}

		/// <summary>
		///  明朝体のフォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateSerif()
		{
			return new Font(_pfc.Families[5], 12, FontStyle.Regular, GraphicsUnit.Point);
		}

		/// <summary>
		///  本文用フォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateTextFont()
		{
			return new Font(_pfc.Families[2], 12, FontStyle.Regular, GraphicsUnit.Point);
		}

		/// <summary>
		///  上部見出し用フォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateHeaderFont()
		{
			return new Font(_pfc.Families[2], 24, FontStyle.Bold, GraphicsUnit.Point);
		}

		/// <summary>
		///  下部見出し用フォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateFooterFont()
		{
			return new Font(_pfc.Families[3], 10.5F, FontStyle.Italic, GraphicsUnit.Point);
		}

		/// <summary>
		///  小見出し用フォントを新しく生成します。
		/// </summary>
		/// <returns>生成されたフォントオブジェクトです。</returns>
		public static Font CreateCaptionFont()
		{
			return new Font(_pfc.Families[0], 14, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
		}
	}
}
