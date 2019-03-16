// System.Linq にこのクラスで定義されている関数と似た様な物がある。
using System;

namespace OSDeveloper.MiscUtils
{
	/// <summary>
	///  配列の拡張メソッドと便利関数を提供します。
	///  このクラスは静的です。
	/// </summary>
	public static class ArrayUtils
	{
		/// <summary>
		///  指定された複数の配列を一つの配列に結合します。
		/// </summary>
		/// <param name="arrays">結合する複数の配列です。</param>
		/// <returns>生成された一つの配列です。</returns>
		/// <typeparam name="T">結合する配列の種類です。</typeparam>
		/// <exception cref="System.ArgumentNullException" />
		public static T[] Join<T>(params T[][] arrays)
		{
			if (arrays == null) {
				throw new ArgumentNullException(nameof(arrays));
			}
			int size = 0, index = 0;
			for (int i = 0; i < arrays.Length; ++i) {
				if (arrays[i] != null) {
					size += arrays[i].Length;
				}
			}
			T[] result = new T[size];
			for (int i = 0; i < arrays.Length; ++i) {
				if (arrays[i] != null) {
					arrays[i].CopyTo(result, index);
					index += arrays[i].Length;
				}
			}
			return result;
		}

		public static bool Contains<T>(this T[] array, T obj)
		{
			for (int i = 0; i < array.Length; ++i) {
				if (array[i].Equals(obj)) {
					return true;
				}
			}
			return false;
		}
	}
}
