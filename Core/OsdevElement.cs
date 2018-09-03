using System;
using System.Reflection;
using OSDeveloper.Core.Logging;

namespace OSDeveloper.Core
{
	/**
	 * このクラスいるかな？
	 * ソフトウェア全体のコンポーネント的な物を管理する為の構造のつもりだったんだけど
	 */

	/// <summary>
	///  <see langword="OSDeveloper" />の構成要素を表します。
	///  このクラスは抽象クラスです。
	/// </summary>
	public abstract class OsdevElement : IDisposable
	{
		private readonly static Logger _logger = Logger.GetSystemLogger(nameof(OsdevElement));

		/// <summary>
		///  このオブジェクトが破棄されているかどうかを表す論理値を取得します。
		/// </summary>
		protected bool IsDisposed
		{
			get
			{
				return _is_disposed;
			}
		}
		private bool _is_disposed;

		/// <summary>
		///  このクラスの完全修飾名を取得します。
		/// </summary>
		public string Identifier
		{
			get
			{
				return this.GetType().FullName;
			}
		}

		/// <summary>
		///  派生クラスでオーバーライドされた場合、
		///  このコンポーネントの表示名を取得します。
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		///  派生クラスでオーバーライドされた場合、
		///  このコンポーネントのバージョンを取得します。
		/// </summary>
		public virtual Version Version
		{
			get
			{
				return _ver;
			}
		}
		private readonly Version _ver;

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.OsdevElement"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public OsdevElement()
		{
			_logger.Trace("The constructor of OsdevElment was called");
			_is_disposed = false;
			_ver = Assembly.GetExecutingAssembly().GetName().Version;
		}

		/// <summary>
		///  現在のオブジェクトのインスタンスを破棄します。
		/// </summary>
		~OsdevElement()
		{
			_logger.Trace("The destructor of OsdevElment was called");
			this.Dispose(false);
		}

		/// <summary>
		///  現在のインスタンスで利用されているリソースを解放します。
		/// </summary>
		public void Dispose()
		{
			_logger.Trace("The Dispose() method of OsdevElment was called");
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///  現在のインスタンスで利用されているアンマネージオブジェクトと
		///  オプションでマネージドオブジェクトを解放します。
		/// </summary>
		/// <param name="disposing">
		///  マネージドオブジェクトも破棄する場合は<see langword="true"/>、
		///  アンマネージオブジェクトのみ破棄する場合は<see langword="false"/>です。
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			_logger.Trace("The Dispose(bool) method of OsdevElment was called");
			_logger.Debug($"disposing={disposing}, _is_disposed={_is_disposed}");
			if (!_is_disposed) {
				if (disposing) {
				}
				_is_disposed = true;
			}
		}


		/// <summary>
		///  指定された型名から新しい型'<see cref="OSDeveloper.Core.OsdevElement"/>'のオブジェクトを生成し返します。
		/// </summary>
		/// <param name="typename">生成するオブジェクトの完全修飾名です。</param>
		/// <returns></returns>
		public static OsdevElement CreateInstance(string typename)
		{
			_logger.Trace("Creating a new instance of OsdevElement...");
			_logger.Info($"The typename of a new instance is: {typename}");
			if (string.IsNullOrEmpty(typename)) {
				return null;
			}

			var obj = Activator.CreateInstance(Type.GetType(typename, false));
			return obj as OsdevElement;
		}
	}
}
