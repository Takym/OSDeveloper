using System;
using System.Collections.Generic;
using System.Text;
using Yencon.Binary;

namespace Yencon
{
	/// <summary>
	///  ヱンコン環境設定のセクション(キーの集まり)を表します。
	/// </summary>
	public class YSection : YNode
	{
		private List<YNode> _keys;

		/// <summary>
		///  このセクションの子キーを取得します。
		/// </summary>
		public YNode[] SubKeys
		{
			get
			{
				return _keys.ToArray();
			}
		}

		/// <summary>
		///  このキーがルートセクションかどうかを表す論理値を取得します。
		///  ルートセクションの場合は<see langword="true"/>、
		///  それ以外は<see langword="false"/>です。
		/// </summary>
		public bool IsRoot { get => this.Parent is null; } // == null だと、正しく動作しない可能性がある

		/// <summary>
		///  型'<see cref="Yencon.YSection"/>'の新しいインスタンスを生成します。
		/// </summary>
		public YSection()
		{
			_keys = new List<YNode>();
		}

		/// <summary>
		///  このセクションの子キーを表す配列のコピーを取得します。
		/// </summary>
		/// <returns>取得した型'<see cref="Yencon.YNode"/>'の配列です。</returns>
		public override object GetValue()
		{
			return _keys.ToArray();
		}

		/// <summary>
		///  指定されたキーの配列をこのセクションの子キーとして設定します。
		/// </summary>
		/// <param name="value">設定する型'<see cref="Yencon.YNode"/>'の配列です。</param>
		/// <exception cref="System.InvalidCastException">
		///  型'<see cref="Yencon.YNode"/>'の配列に変換できない型が渡された場合に発生します。
		/// </exception>
		public override void SetValue(object value)
		{
			var newKeys = ((YNode[])(value));
			while (_keys.Count > 0) {
				_keys[0].Parent = null;
				_keys.RemoveAt(0);
			}
			_keys.AddRange(newKeys);
		}

		/// <summary>
		///  指定されたキーをこのセクションの末尾に追加します。
		///  既に同じキー名のキーを保持している場合は置き換えます。
		/// </summary>
		/// <param name="key">追加するキーです。</param>
		/// <returns>キーを正常に追加した場合は<see langword="true"/>、失敗した場合は<see langword="false"/>です。</returns>
		public bool Add(YNode key)
		{
			var n = this.GetNode(key.Name);
			if (n != null) {
				_keys[_keys.IndexOf(n)] = key;
				n  .Parent = null;
				key.Parent = this;
				return true;
			} else {
				_keys.Add(key);
				key.Parent = this;
				return true;
			}
		}

		/// <summary>
		///  指定されたキー名からキーを検索して返します。
		/// </summary>
		/// <param name="name">取得するキーの名前です。</param>
		/// <returns>
		///  キーが存在する場合はそのキーを表すオブジェクト、
		///  存在しない場合は<see langword="null"/>を返します。
		/// </returns>
		public YNode GetNode(string name)
		{
			for (int i = 0; i < _keys.Count; ++i) {
				if (_keys[i].Name == name) {
					return _keys[i];
				}
			}
			return null;
		}

		/// <summary>
		///  指定されたキーをこのセクションから削除します。
		/// </summary>
		/// <param name="key">削除するキーです。</param>
		/// <returns>キーの削除に成功した場合は<see langword="true"/>、失敗した場合は<see langword="false"/>です。</returns>
		public bool Remove(YNode key)
		{
			if (_keys.Contains(key)) {
				_keys.Remove(key);
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		///  指定されたキー名のキーをこのセクションから削除します。
		/// </summary>
		/// <param name="name">削除するキーの名前です。</param>
		/// <returns>キーの削除に成功した場合は<see langword="true"/>、失敗した場合は<see langword="false"/>です。</returns>
		public bool Remove(string name)
		{
			var key = this.GetNode(name);
			// == null だと、正しく動作しない可能性がある
			if (key is null) return false;
			return this.Remove(key);
		}

		/// <summary>
		///  指定されたキーがこのセクション内に存在するか評価します。
		/// </summary>
		/// <param name="key">存在を確かめるキーオブジェクトです。</param>
		/// <returns>存在している場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public bool Contains(YNode key)
		{
			return _keys.Contains(key);
		}

		/// <summary>
		///  指定されたキーがこのセクション内に存在するか評価します。
		/// </summary>
		/// <param name="name">存在を確かめるキーの名前です。</param>
		/// <returns>存在している場合は<see langword="true"/>、存在しない場合は<see langword="false"/>です。</returns>
		public bool Contains(string name)
		{
			// != null だと、正しく動作しない可能性がある
			return !(this.GetNode(name) is null);
		}

		/// <summary>
		///  このセクションをテキスト形式のヱンコンに変換します。
		/// </summary>
		/// <returns>変換結果のテキスト形式のキーです。</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			for (int i = 0; i < _keys.Count; ++i) {
				if (_keys[i] is YComment c) { // コメントの場合
					if (c.HasName) { // コメントに名前がある場合
						sb.Append(c.Name);
						sb.Append('=');
					}
					sb.Append(c.ToString());
				} else { // コメントではない場合
					sb.Append(_keys[i].Name);
					sb.Append('=');
					if (_keys[i] is YSection s) { // セクションの場合
						sb.Append("{\n");
						// 行ごとに分割する
						var vs = s.ToString().Split('\n');
						// 先頭にタブを追加して結合する
						for (int j = 0; j < vs.Length; ++j) {
							sb.Append('\t');
							sb.Append(vs[j]);
							sb.Append('\n');
						}
						sb.Append('}');
					} else { // その他のキー
						sb.Append(_keys[i].ToString());
					}
				}
				sb.Append('\n'); // sb.AppendLine(); // 必ず LF にしたいから
			}
			return sb.ToString();
		}

		/// <summary>
		///  限定の設定で、
		///  このセクションをバイナリ形式のヱンコンに変換します。
		/// </summary>
		/// <returns>変換結果のバイナリ形式のキーです。</returns>
		public override byte[] ToBinary()
		{
			return this.ToBinary(YenconBinaryHeader.DEFAULT);
		}

		/// <summary>
		///  ヘッダー情報を指定して、
		///  このセクションをバイナリ形式のヱンコンに変換します。
		/// </summary>
		/// <param name="header">ヱンコンファイルのヘッダー情報です。キー名の状態を決定する為に利用されます。</param>
		/// <returns>変換結果のバイナリ形式のキーです。</returns>
		public byte[] ToBinary(YenconBinaryHeader header)
		{
			var enc = header.GetEncoding();
			var result = new List<byte>();
			result.AddRange(BitConverter.GetBytes((uint)(_keys.Count)));
			for (int i = 0; i < _keys.Count; ++i) {
				if (_keys[i] is YComment c && !c.HasName) { // コメントでキー名が無い場合
					switch (header.KeyNameSize) {
						case KeyNameSize.HWord:
							result.Add(0);
							break;
						case KeyNameSize.Word:
							result.Add(0); result.Add(0);
							break;
						case KeyNameSize.DWord:
							result.Add(0); result.Add(0); result.Add(0); result.Add(0);
							break;
#if false
						case KeyNameSize.QWord:
							result.Add(0); result.Add(0); result.Add(0); result.Add(0);
							result.Add(0); result.Add(0); result.Add(0); result.Add(0);
							break;
#endif
					}
				} else { // コメントではない場合
					byte[] name = enc.GetBytes(_keys[i].Name);
					long size = 0;
					switch (header.KeyNameSize) {
						case KeyNameSize.HWord:
							byte v8 = unchecked((byte)(name.Length));
							result.Add(v8);
							size = v8;
							break;
						case KeyNameSize.Word:
							ushort v16 = unchecked((ushort)(name.Length));
							result.AddRange(BitConverter.GetBytes(v16));
							size = v16;
							break;
						case KeyNameSize.DWord:
							uint v32 = unchecked((uint)(name.Length));
							result.AddRange(BitConverter.GetBytes(v32));
							size = v32;
							break;
#if false
						case KeyNameSize.QWord:
							ulong v64 = unchecked((ulong)(name.LongLength));
							result.AddRange(BitConverter.GetBytes(v64));
							size = ((long)(v64));
							break;
#endif
					}

					byte[] name2 = new byte[size];
					Array.Copy(name, name2, size);
					result.AddRange(name2);
				}

				if (_keys[i] is YSection) { // セクションの場合
					result.Add(0x01);
				}

				// 値を書き込み
				result.AddRange(_keys[i].ToBinary());
			}
			return result.ToArray();
		}
	}
}
