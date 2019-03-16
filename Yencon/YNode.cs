using Yencon.Exceptions;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定の一つのキーを表す抽象クラスです。
	/// </summary>
	public abstract class YNode
	{
		/// <summary>
		///  キーの名前を取得または設定します。
		/// </summary>
		/// <exception cref="Yencon.Exceptions.InvalidKeyNameException">
		///  指定されたキー名に不正な文字が使われている場合に発生します。
		/// </exception>
		public string Name
		{
			get
			{
				return _name;
			}

			set
			{
				if (value.Length <= 0) throw new InvalidKeyNameException(value);
				for (int i = 0; i < value.Length; ++i) {
					if (YenconStringTokenizer.IsInvalidLetter(value[i])) {
						throw new InvalidKeyNameException(value);
					}
				}
				_name = value;
			}
		}
		private string _name;

		/// <summary>
		///  このキーを格納している親セクションを取得します。
		/// </summary>
		public YSection Parent { get; internal set; }

		/// <summary>
		///  このキーが保持している値を取得します。
		/// </summary>
		/// <returns>
		///  このキーが保持している値の<see cref="object"/>形式です。
		/// </returns>
		public abstract object GetValue();

		/// <summary>
		///  このキーの値を設定します。
		/// </summary>
		/// <param name="value">設定する値です。</param>
		public abstract void SetValue(object value);

		/// <summary>
		///  このキーをテキスト形式のヱンコンに変換します。
		/// </summary>
		/// <returns>変換結果のテキスト形式のキーです。</returns>
		public override abstract string ToString();

		/// <summary>
		///  このキーをバイナリ形式のヱンコンに変換します。
		/// </summary>
		/// <returns>変換結果のバイナリ形式のキーです。</returns>
		public abstract byte[] ToBinary();
	}
}
