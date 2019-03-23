using System.Collections;
using System.Collections.Generic;

namespace TakymLib.Collections
{
	/// <summary>
	///  接続リストと配列リストの両方のアルゴリズムを利用したリストです。
	/// </summary>
	/// <typeparam name="T">リストで利用する型の種類です。</typeparam>
	public partial class HybridList<T> : IList<T>, IList, IReadOnlyList<T>
	{
		/// <summary>
		///  接続リストとして初期化して、
		///  型'<see cref="TakymLib.Collections.HybridList{T}"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public HybridList()
		{
			_mode  = HybridListMode.Linked;
			_count = 0;
		}

		/// <summary>
		///  指定された容量で配列リストを初期化して、
		///  型'<see cref="TakymLib.Collections.HybridList{T}"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="capacity">新しく生成する配列リストの初期容量です。</param>
		public HybridList(int capacity)
		{
			_mode  = HybridListMode.Array;
			_count = 0;
			this.EnsureCapacity(capacity);
		}

		private void BeArrayMode()
		{
			if (_mode == HybridListMode.Linked) {
				_mode = HybridListMode.Array;
				var item = _item_first;
				this.EnsureCapacity(_count);
				for (int i = 0; i < _count; ++i) {
					_items[i] = item.value;
					item = item.next;
				}
			}
		}

		private void BeLinkedMode()
		{
			if (_mode == HybridListMode.Array) {
				_mode = HybridListMode.Linked;
				HybridListItem item = null;
				for (int i = 0; i < _count; ++i) {
					if (item == null) {
						item = _item_first = new HybridListItem();
					} else {
						var tmp = new HybridListItem();
						item.next = tmp; tmp.prev = item;
						item = tmp;
					}
					item.value = _items[i];
				}
				_item_last = item;
			}
		}

		private bool EnsureCapacity(int size)
		{
			if (_mode == HybridListMode.Array && _items.Length >= size) {
				this.Resize(size);
				return true;
			} else {
				return false;
			}
		}

		private void Resize(int size)
		{
			var newAry = new T[size];
			var oldAry = _items;
			if (oldAry != null) {
				for (int i = 0; i < newAry.Length; ++i) {
					if (i >= oldAry.Length) break;
					newAry[i] = oldAry[i];
				}
			}
			_items = newAry;
		}
	}
}
