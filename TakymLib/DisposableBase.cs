using System;
using System.Reflection;

namespace TakymLib
{
	/// <summary>
	///  <see cref="System.IDisposable"/>クラスの基本メソッドを実装した、抽象クラスです。
	/// </summary>
	public abstract class DisposableBase : ContextBoundObject, IDisposable
	{
		/// <summary>
		///  このオブジェクトが破損されているかどうかを取得します。
		/// </summary>
		protected bool IsDisposed { get; private set; } = false;

		/// <summary>
		///  このクラスの新しいオブジェクトインスタンスを生成します。
		/// </summary>
		public DisposableBase() : base() { }

		/// <summary>
		///  このオブジェクトのデストラクタです。
		/// </summary>
		~DisposableBase()
		{
			this.Dispose(false);
		}

		/// <summary>
		///  このインスタンスのリソースを解放します。
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///  このインスタンスのリソースを解放します。
		/// </summary>
		/// <param name="disposing">
		///  マネージドオブジェクトを破棄するかどうかです。
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed) {
				if (disposing) {
					GC.Collect();
				}
				IsDisposed = true;
			}
		}

		/// <summary>
		///  指定したオブジェクトを破棄します。
		///  <see cref="System.IDisposable"/>を継承しているクラスの場合、<see cref="System.IDisposable.Dispose"/>を呼び出し、
		///  それ以外のクラスは強制的にデストラクタ(例：<see cref="object.Finalize"/>)を強制的に実行します。
		///  この関数を利用して削除したオブジェクトは利用しないでください。
		/// </summary>
		/// <param name="obj">削除対象のオブジェクトです。</param>
		[Obsolete("この関数を利用して削除したオブジェクトは利用しないでください。")]
		public static void DisposeObject(object obj)
		{
			Type t = obj.GetType();
			if (typeof(IDisposable).IsAssignableFrom(obj.GetType())) {
				((IDisposable)(obj)).Dispose();
			} else {
				var mi = obj.GetType().GetMethod(
					"Finalize",
					BindingFlags.NonPublic |
					BindingFlags.InvokeMethod |
					BindingFlags.Instance);
				mi.Invoke(obj, null);
				GC.SuppressFinalize(obj);
			}
		}
	}
}
