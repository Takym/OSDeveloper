using System;
using System.IO;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  コメントを表します。このクラスは継承できません。
	/// </summary>
	public sealed class YenconComment : YenconNode
	{
		/// <summary>
		///  このコメントを表す文字列を取得または設定します。
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///  常に<see cref="OSDeveloper.Core.Settings.YenconNullValue"/>を返します。
		/// </summary>
		public override YenconValue Value
		{
			get
			{
				return base.Value;
			}

			set
			{
				// 値は書き換えない。
			}
		}

		/// <summary>
		///  保持するコメント文字列を設定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconComment"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconComment(string comment)
		{
			this.Name = Guid.NewGuid() + Path.GetRandomFileName() + StringUtils.GetRandomText();
			this.Text = comment.Trim();

			base.Value = new YenconNullValue();
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			return $"; {this.Text}";
		}
	}
}
