using System;
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

		public void AddRange(IEnumerable<T> items)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
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

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
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
