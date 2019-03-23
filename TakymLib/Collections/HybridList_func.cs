using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		#region　追加系
		/// <summary>
		///  指定されたオブジェクトをこのリストに追加します。
		/// </summary>
		/// <param name="item">追加するオブジェクトです。</param>
		public void Add(T item)
		{
			if (_mode == HybridListMode.Linked) {
				if (_item_first == null) {
					_item_first = new HybridListItem();
					_item_first.value = item;
					_item_last = _item_first;
					_count = 1;
				} else {
					var tmp = new HybridListItem();
					tmp.value = item;
					_item_last.next = tmp; tmp.prev = _item_last;
					_item_last = tmp;
					++_count;
				}
			} else { // if (_mode == HybridListMode.Array)
				this.EnsureCapacity(_count + 2);
				_items[_count] = item;
				++_count;
			}
		}

		int IList.Add(object value)
		{
			int c = _count;
			this.Add((T)(value));
			return c;
		}

		/// <summary>
		///  指定された複数のオブジェクトをこのリストに追加します。
		/// </summary>
		/// <param name="items">追加する複数のオブジェクトです。</param>
		public void AddRange(IEnumerable<T> items)
		{
			foreach (var item in items) {
				this.Add(item);
			}
		}

		/// <summary>
		///  指定されたオブジェクト配列をこのリストに追加します。
		/// </summary>
		/// <param name="items">追加するオブジェクト配列です。</param>
		public void AddRange(params T[] items)
		{
			for (int i = 0; i < items.Length; ++i) {
				this.Add(items[i]);
			}
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		void IList.Insert(int index, object value)
		{
			this.Insert(index, ((T)(value)));
		}

		public void InsertRange(int index, IEnumerable<T> items)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 削除系
		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		void IList.Remove(object value)
		{
			this.Remove((T)(value));
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 判定系
		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		bool IList.Contains(object value)
		{
			return this.Contains((T)(value));
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)(value));
		}

		public int LastIndexOf(T item)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 変換系
		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo(((T[])(array)), index);
		}

		public T[] ToArray()
		{
			throw new NotImplementedException();
		}

		public ReadOnlyCollection<T> AsReadOnly()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region 操作系
		public void Sort()
		{
			throw new NotImplementedException();
		}

		public void Compact()
		{
			throw new NotImplementedException();
		}

		public void Reverse()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
