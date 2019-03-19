using System;
using System.Collections;

namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		public T this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}

			set
			{
				this[index] = ((T)(value));
			}
		}

		/// <summary>
		///  このリストに追加されている項目数を取得します。
		/// </summary>
		public int Count
		{
			get
			{
				return _count;
			}
		}

		/// <summary>
		///  現在のリストの状態を取得します。
		/// </summary>
		public HybridListMode Mode
		{
			get
			{
				return _mode;
			}
		}

		/// <summary>
		///  このリストが読み取り専用かどうかを表す論理値を取得します。
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		///  このリストが書き換え可能かどうかを表す論理値を取得します。
		/// </summary>
		public bool IsRewritable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		///  このリストが固定長かどうかを表す論理値を取得します。
		/// </summary>
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		///  スレッドセーフかどうかを表す論理値を取得します。
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		///  このリストはスレッドセーフではない為、常に<see langword="null"/>を返します。
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}
	}
}
