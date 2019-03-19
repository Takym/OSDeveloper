using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		#region　追加系
		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		int IList.Add(object value)
		{
			int c = this.Count;
			this.Add((T)(value));
			return c;
		}

		public void AddRange(IEnumerable<T> items)
		{
			throw new NotImplementedException();
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
