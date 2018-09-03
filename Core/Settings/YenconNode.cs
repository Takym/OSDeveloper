using OSDeveloper.Core.FileManagement;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  セクションまたはキーを表します。
	/// </summary>
	public class YenconNode : IKeyNode<YenconValue>
	{
		/// <summary>
		///  このセクションまたはキーの識別子を取得または設定します。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public YenconType Kind
		{
			get
			{
				return this.Value.Kind;
			}
		}

		/// <summary>
		///  セクションの場合はキーリスト、
		///  キーの場合は値を取得または設定します。
		/// </summary>
		public virtual YenconValue Value { get; set; }

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Settings.YenconNode"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconNode()
		{
			this.Name = string.Empty;
			this.Value = new YenconNullValue();
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			return $"{this.Name}={this.Value}";
		}
	}
}
