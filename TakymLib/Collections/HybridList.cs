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
		public HybridList()
		{

		}
	}
}
