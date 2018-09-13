using System.Collections.Generic;
using System.Text;

namespace OSDeveloper.Core.Settings
{
	/// <summary>
	///  ヱンコン環境設定ファイル(<see langword="Yencon Environment Configuration"/>)の
	///  セクションを表します。このクラスは継承できません。
	/// </summary>
	public sealed class YenconSection : YenconValue
	{
		private Dictionary<string, YenconNode> _dic;

		/// <summary>
		///  このセクションまたはキーの種類を取得します。
		/// </summary>
		public override YenconType Kind
		{
			get
			{
				return YenconType.Section;
			}
		}

		/// <summary>
		///  このセクションが保持しているセクションまたはキーの辞書を取得します。
		/// </summary>
		public IDictionary<string, YenconNode> Children
		{
			get
			{
				return _dic;
			}
		}

		/// <summary>
		///  型'<see cref="OSDeveloper.Core.Settings.YenconSection"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public YenconSection()
		{
			_dic = new Dictionary<string, YenconNode>();
		}

		/// <summary>
		///  このセクションが保持する複数のキーを指定して、
		///  型'<see cref="OSDeveloper.Core.Settings.YenconSection"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="dic">このセクションが保持する複数のキーです。</param>
		public YenconSection(IEnumerable<YenconNode> dic)
		{
			_dic = new Dictionary<string, YenconNode>();
			foreach (var item in dic) {
				_dic.Add(item.Name, item);
			}
		}

		/// <summary>
		///  このセクションの値を取得します。
		/// </summary>
		/// <returns>このセクションまたはキーの値です。</returns>
		public override object GetValue()
		{
			return _dic;
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("{");
			foreach (var item in _dic) {
				sb.Append('\t').AppendLine(item.Value.ToString());
			}
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		///  このセクションまたはキーの値を設定ファイルの文字列として取得します。
		///  波括弧は付きません。
		/// </summary>
		/// <returns>設定ファイルにそのまま埋め込む事ができる文字列です。</returns>
		public string ToStringWithoutBrace()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in _dic) {
				sb.AppendLine(item.Value.ToString());
			}
			return sb.ToString();
		}
	}
}
