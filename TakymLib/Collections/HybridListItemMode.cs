namespace TakymLib.Collections
{
	partial class HybridList<T>
	{
		private HybridListMode _mode;
		private HybridListItem _item_first;
		private HybridListItem _item_last;
		private T[]            _items;
		private int            _count;

		internal class HybridListItem
		{
			internal T value;
			internal HybridListItem prev, next;
		}

		/// <summary>
		///  <see cref="TakymLib.Collections.HybridList{T}"/>の状態を表します。
		/// </summary>
		public enum HybridListMode
		{
			/// <summary>
			///  配列リストを表します。
			///  特定の位置の値を取得または設定する時等の項目数が変更されない処理を実行する場合に高速に動作します。
			/// </summary>
			Array,

			/// <summary>
			///  接続リストを表します。ただし、通常の線形リストではなく二重線形リストです。
			///  値を途中に挿入したり、削除する時等の項目数が変更される処理を実行する場合に高速に動作します。
			/// </summary>
			Linked,

			/*
			Sorted,

			Compact,

			CompactSorted,

			SortedCompact = CompactSorted,
			//*/
		}
	}
}
