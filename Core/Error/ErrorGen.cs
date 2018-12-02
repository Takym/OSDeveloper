using System;

namespace OSDeveloper.Core.Error
{
	/// <summary>
	///  例外オブジェクトを生成します。
	///  このクラスは静的です。
	/// </summary>
	public static class ErrorGen
	{
		/// <summary>
		///  値が範囲外である事を示す例外オブジェクトを生成します。
		/// </summary>
		/// <param name="v">指定された値です。</param>
		/// <param name="min">値の最小値です。</param>
		/// <param name="max">値の最大値です。</param>
		/// <typeparam name="T">値の種類です。</typeparam>
		/// <returns>生成された<see cref="System.ArgumentOutOfRangeException"/>オブジェクトです。</returns>
		public static ArgumentOutOfRangeException ArgOutOfRange<T>(T v, T min, T max)
		{
			return new ArgumentOutOfRangeException(string.Format(
				ErrorMessages.ArgOutOfRange, v, min, max));
		}
	}
}
