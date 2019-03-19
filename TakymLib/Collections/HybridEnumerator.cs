using System.Collections;
using System.Collections.Generic;

namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		/// <summary>
		///  反復処理で利用する為の列挙子を取得します。
		/// </summary>
		/// <returns>反復処理で利用する為の列挙子です。</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return new HybridEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HybridEnumerator(this);
		}

		/// <summary>
		///  <see cref="TakymLib.Collections.HybridList{T}"/>で利用される<see cref="System.Collections.Generic.IEnumerator{T}"/>です。
		/// </summary>
		internal struct HybridEnumerator : IEnumerator<T>
		{
			private readonly HybridList<T> _list;
			private int _index;

			public T           Current => _index < 0 ? default : _list[_index];
			object IEnumerator.Current => _index < 0 ? default : _list[_index];

			public HybridEnumerator(HybridList<T> list)
			{
				_list  = list;
				_index = -1;
			}

			public bool MoveNext()
			{
				++_index;
				if (_list.Count <= _index) {
					_index = -1;
					return false;
				} else {
					return true;
				}
			}

			public void Reset()
			{
				_index = 0;
			}

			public void Dispose()
			{
				// do nothing
			}
		}
	}
}
