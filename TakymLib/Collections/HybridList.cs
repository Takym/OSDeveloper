using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakymLib.Collections
{
	public class HybridList<T> : IList<T>
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

		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public class HybridEnumerator<T> : IEnumerator<T>
		{
			public T Current
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			object IEnumerator.Current
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			public bool MoveNext()
			{
				throw new NotImplementedException();
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}

			#region IDisposable Support
			private bool disposedValue = false; // 重複する呼び出しを検出するには

			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue) {
					if (disposing) {
						// TODO: マネージド状態を破棄します (マネージド オブジェクト)。
					}

					// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
					// TODO: 大きなフィールドを null に設定します。

					disposedValue = true;
				}
			}

			// TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
			// ~HybridEnumerator() {
			//   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
			//   Dispose(false);
			// }

			// このコードは、破棄可能なパターンを正しく実装できるように追加されました。
			public void Dispose()
			{
				// このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
				Dispose(true);
				// TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
				// GC.SuppressFinalize(this);
			}
			#endregion
		}
	}
}
