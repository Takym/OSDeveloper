// System.Linq にこのクラスで定義されている関数と似た様な物がある。
using System;
using System.Collections.Generic;

namespace TakymLib
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
		/// <exception cref="System.ArgumentNullException">
		///  <paramref name="arrays"/>が<see langword="null"/>の場合に発生します。
		/// </exception>
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

		/// <summary>
		///  指定された配列に指定されたオブジェクトと同値のオブジェクトインスタンスが存在しているかどうか判定します。
		/// </summary>
		/// <typeparam name="T">配列の型の種類です。</typeparam>
		/// <param name="array">判定する配列です。</param>
		/// <param name="obj">配列に存在するか判定するオブジェクトです。</param>
		/// <returns>存在する場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public static bool ContainsValue<T>(this T[] array, T obj)
		{
			for (int i = 0; i < array.Length; ++i) {
				if (array[i].Equals(obj)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		///  指定された配列に指定されたオブジェクトの参照が存在しているかどうか判定します。
		/// </summary>
		/// <typeparam name="T">配列の型の種類です。</typeparam>
		/// <param name="array">判定する配列です。</param>
		/// <param name="obj">配列に存在するか判定するオブジェクトです。</param>
		/// <returns>存在する場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public static bool ContainsReference<T>(this T[] array, T obj)
		{
			for (int i = 0; i < array.Length; ++i) {
				if (ReferenceEquals(array[i], obj)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		///  <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>クラスをキーと値に分解します。
		/// </summary>
		/// <typeparam name="TKey">キーの型です。</typeparam>
		/// <typeparam name="TValue">値の型です。</typeparam>
		/// <param name="kvp">分解前のオブジェクトです。</param>
		/// <param name="key">分解後のキーです。</param>
		/// <param name="value">分解後の値です。</param>
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
		{
			key   = kvp.Key;
			value = kvp.Value;
		}
	}
}
