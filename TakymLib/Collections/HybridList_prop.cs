using System;

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
		///  現在のリストの状態を取得します。
		/// </summary>
		public HybridListMode Mode
		{
			get
			{
				return _mode;
			}
		}
	}
}
