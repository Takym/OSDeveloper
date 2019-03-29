using System.Collections;

namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		/// <summary>
		///  指定されたインデックス番号の値を取得または設定します。
		/// </summary>
		/// <param name="index">取得または設定する値のインデックス番号です。</param>
		/// <returns>指定されたインデックス番号の位置に保存されている値です。</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///  <paramref name="index"/>が現在のリストの範囲外である場合に発生します。
		/// </exception>
		public T this[int index]
		{
			get
			{
				this.CheckIndex(index);
				if (_mode == HybridListMode.Array) {
					return _items[index];
				} else { // if (_mode == HybridListMode.Linked)
					if (index < _count / 2) {
						var item = _item_first;
						for (int i = 0; i < index; ++i) {
							item = item.next;
						}
						return item.value;
					} else {
						var item = _item_last;
						for (int i = _count - 1; i > index; --i) {
							item = item.prev;
						}
						return item.value;
					}
				}
			}

			set
			{
				this.CheckIndex(index);
				if (_mode == HybridListMode.Array) {
					_items[index] = value;
				} else {
					if (index < _count / 2) {
						var item = _item_first;
						for (int i = 0; i < index; ++i) {
							item = item.next;
						}
						item.value = value;
					} else {
						var item = _item_last;
						for (int i = _count - 1; i > index; --i) {
							item = item.prev;
						}
						item.value = value;
					}
				}
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
