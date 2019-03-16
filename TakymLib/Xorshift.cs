using System;

namespace DotnetExlib
{
	/// <summary>
	///  Xorshiftを使用した疑似乱数生成器です。
	///  このクラスは継承できません。
	/// </summary>
	public sealed class Xorshift : Random, IRandom
	{
		/// <summary>
		///  最初に与えられたシード値です。
		/// </summary>
		public ulong Seed { get; private set; }

		private ulong x, y, z, w;

		/// <summary>
		///  シード値を以下の様に設定して、新しいインスタンスを生成します。
		///  <see cref="DotnetExlib.Xorshift.Seed"/> =
		///  <see cref="System.Environment.TickCount"/> XOR <see cref="System.Random.Sample"/>
		/// </summary>
		public Xorshift()
		{
			ulong a = ((ulong)(Sample() * long.MaxValue));
			this.Seed = ((ulong)(Environment.TickCount)) ^ a;
			this.ResetSeed();
		}

		/// <summary>
		///  シード値を設定して、新しいインスタンスを生成します。
		/// </summary>
		/// <param name="seed">シード値です。</param>
		public Xorshift(long seed)
		{
			this.Seed = ((ulong)(seed));
			this.ResetSeed();
		}

		/// <summary>
		///  シード値を設定して、新しいインスタンスを生成します。
		/// </summary>
		/// <param name="seed">シード値です。</param>
		public Xorshift(ulong seed)
		{
			this.Seed = seed;
			this.ResetSeed();
		}

		/// <summary>
		///  新しいシード値を設定します。
		/// </summary>
		/// <param name="seed">設定するシード値です。</param>
		public void SetSeed(ulong seed)
		{
			this.Seed = seed;
			this.ResetSeed();
		}

		/// <summary>
		///  シード値を初期値に戻します。
		/// </summary>
		public void ResetSeed()
		{
			x = Seed ^ 0x83F38C937DE3A4B3;
			y = Seed ^ 0xCF2628AE4CD41B08;
			z = ((Seed >> 32) | (Seed << 32));
			w = x ^ y;
		}

		/// <summary>
		///  0～1の間の乱数を生成します。
		/// </summary>
		/// <returns>生成された型'<see cref="System.Double"/>'の値です。</returns>
		public override double NextDouble()
		{
			return 1D / NextUInt64();
		}

		/// <summary>
		///  符号無し64bitの乱数を生成します。
		/// </summary>
		/// <returns>乱数です。</returns>
		public ulong NextUInt64()
		{
			ulong i = x ^ (x << 11);
			x = y; y = z; z = w;
			return w = (w ^ (w >> 19)) ^ (i ^ (i >> 8));
		}

		/// <summary>
		///  符号有り64bitの乱数を生成します。
		/// </summary>
		/// <returns>乱数です。</returns>
		public long NextSInt64()
		{
			ulong i = x ^ (x << 11);
			x = y; y = z; z = w;
			return ((long)(w = (w ^ (w >> 19)) ^ (i ^ (i >> 8))));
		}

		/// <summary>
		///  64ビット符号無し整数の設定された最大値未満の乱数を生成します。
		/// </summary>
		/// <param name="maxValue">乱数の最大値です。</param>
		/// <returns>生成された型'<see cref="System.UInt64"/>'の値です。</returns>
		public ulong NextUInt64(ulong maxValue)
		{
			return NextUInt64() % maxValue;
		}

		/// <summary>
		///  64ビット符号無し整数の設定された最小値以上で最大値未満の乱数を生成します。
		/// </summary>
		/// <param name="minValue">乱数の最小値です。</param>
		/// <param name="maxValue">乱数の最大値です。</param>
		/// <returns>生成された型'<see cref="System.UInt64"/>'の値です。</returns>
		public ulong NextUInt64(ulong minValue, ulong maxValue)
		{
			return NextUInt64() % (maxValue - minValue) + minValue;
		}

		/// <summary>
		///  このオブジェクトのハッシュコードです。
		/// </summary>
		/// <returns>int型の値です。</returns>
		public override int GetHashCode()
		{
			return Seed.GetHashCode();
		}
	}
}
