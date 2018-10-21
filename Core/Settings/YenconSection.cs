using System;
using System.Collections.Generic;
using System.Text;
using OSDeveloper.Core.MiscUtils;

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
		///  <see cref="OSDeveloper.Core.Settings.YenconSection.GetNode(string, YenconValue)"/>と
		///  <see cref="OSDeveloper.Core.Settings.YenconSection.SetNode(string, YenconValue)"/>を
		///  利用して指定された名前のノードを取得または設定します。
		/// </summary>
		/// <param name="name">取得または設定するノードの名前です。</param>
		/// <value>設定するノードです。</value>
		/// <returns>取得したノードです。</returns>
		public YenconValue this[string name]
		{
			get
			{
				return this.GetNode(name, new YenconNullValue()).Value;
			}

			set
			{
				this.SetNode(name, value);
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
		///  指定された名前のノードを取得します。
		/// </summary>
		/// <param name="name">取得するノードの名前です。</param>
		/// <param name="defaultValue">値が取得できなかった場合に新たに子ノードとして追加する限定値です。</param>
		/// <returns>取得した<see cref="OSDeveloper.Core.Settings.YenconNode"/>です。</returns>
		public YenconNode GetNode(string name, YenconValue defaultValue)
		{
			if (this.Children.ContainsKey(name)) {
				return this.Children[name];
			} else {
				if (defaultValue == null) {
					defaultValue = new YenconNullValue();
				}
				var node = new YenconNode();
				node.Name = name;
				node.Value = defaultValue;
				this.Children.Add(name, node);
				return node;
			}
		}

		/// <summary>
		///  指定された名前に新しいノードを設定します。
		/// </summary>
		/// <param name="name">設定するノードの名前です。</param>
		/// <param name="value">設定する新しい値です。</param>
		public void SetNode(string name, YenconValue value)
		{
			if (this.Children.ContainsKey(name)) {
				//this.Children[name].Name = name;
				this.Children[name].Value = value;
			} else {
				YenconNode node = new YenconNode();
				node.Name = name;
				node.Value = value;
				this.Children.Add(name, node);
			}
		}

		/// <summary>
		///  指定された名前のノードを削除します。
		/// </summary>
		/// <param name="name">削除するノードの名前です。</param>
		/// <returns>
		///  ノードが存在し削除できた場合は<see langword="true"/>、
		///  ノードが存在せず削除できなかった場合は<see langword="false"/>です。
		/// </returns>
		public bool RemoveNode(string name)
		{
			if (this.Children.ContainsKey(name)) {
				this.Children.Remove(name);
				return true;
			}
			return false;
		}

		/// <summary>
		///  指定された名前のノードが存在するかどうか判定します。
		/// </summary>
		/// <param name="name">判定するノードの名前です。</param>
		/// <returns>ノードが存在した場合は<see langword="true"/>、存在しなかった場合は<see langword="false"/>です。</returns>
		public bool ContainsNode(string name)
		{
			return this.Children.ContainsKey(name);
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
				string[] lines = item.Value.ToString().CRtoLF().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < lines.Length; ++i) {
					sb.Append('\t').AppendLine(lines[i]);
				}
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
