using System;
using System.Collections.Generic;
using Yencon.Exceptions;
using Yencon.Resources;
using Yencon.Text;

namespace Yencon
{
	/// <summary>
	///  テキスト形式のヱンコンの文字列を構文解析します。
	/// </summary>
	public class YenconStringParser
	{
		/// <summary>
		///  指定された字句の集まりを解析してセクションを生成します。
		/// </summary>
		/// <param name="tokens">解析する字句のリストです。</param>
		/// <returns>解析して生成されたヱンコンのセクションです。</returns>
		/// <exception cref="Yencon.Exceptions.InvalidSyntaxException">
		///  不正なテキスト形式のヱンコンである場合に発生します。
		/// </exception>
		public YSection Parse(IReadOnlyList<Token> tokens)
		{
			try {
				return this.ParseInternal(tokens);
			} catch (Exception e) when (e.GetType() != typeof(InvalidSyntaxException)) {
				throw new InvalidSyntaxException(ErrorMessages.InvalidSyntaxException_InnerException, e);
			}
		}

		private YSection ParseInternal(IReadOnlyList<Token> tokens)
		{
			// 戻り値用変数の準備
			var result = new YSection();

			for (int i = 0; i < tokens.Count;) {
				switch (tokens[i].Type) {
					case TokenType.Name: // 名前なら
						// 名前を保持
						string name = tokens[i].SourceText;
						// カウンタを進める
						++i;
						// 等号じゃなかったらエラー
						if (tokens[i].Type != TokenType.Equal) {
							throw new InvalidSyntaxException(tokens[i]);
						}
						// カウンタを進める
						++i;
						// 型指定子を使って型を特定する
						YNode node;
						switch (tokens[i].Type) {
							case TokenType.NullOrEmpty: // 空値なら
								// カウンタを進める
								++i;
								// インスタンスの生成
								node = new YNullOrEmpty();
								break;
							case TokenType.CommentChar: // コメントなら
								// カウンタを進める
								++i;
								// インスタンスの生成
								var com = new YComment();
								com.HasName = true; // 名前あり
								// コメント文字列があるなら
								if (tokens[i].Type == TokenType.CommentText) {
									com.SetValue(tokens[i].SourceText);
									++i;
								}
								node = com;
								break;
							case TokenType.SectionStart: // セクションなら
								// カウンタを進める
								++i;
								// 新しいリストにセクションの終わりまでの字句を追加する
								var list = new List<Token>();
								int j = 1;
								while (j > 0) {
									if (tokens[i].Type == TokenType.SectionStart) ++j;
									if (tokens[i].Type == TokenType.SectionEnd) --j;
									// リストに追加する
									list.Add(tokens[i]);
									// カウンタを進める
									++i;
								}
								// 終端の SectionEnd を削除
								list.RemoveAt(list.Count - 1);
								// 右の処理は必要ない？ //// カウンタを進める //++i;
								// インスタンスの生成
								node = this.ParseInternal(list);
								break;
							case TokenType.String: // 文字列なら
								// インスタンスの生成
								var str = new YString();
								str.SetEscapedText(tokens[i].SourceText);
								// カウンタを進める
								++i;
								node = str;
								break;
							case TokenType.Number: // 数値なら
								// インスタンスの生成
								var num = new YNumber();
								num.UInt64Value = ulong.Parse(tokens[i].SourceText);
								// カウンタを進める
								++i;
								node = num;
								break;
							case TokenType.Boolean: // 論理値なら
								// インスタンスの生成
								var flg = new YBoolean();
								flg.Flag = tokens[i].SourceText == "T";
								// カウンタを進める
								++i;
								node = flg;
								break;
							default: // それ以外、例外を発生させる
								throw new InvalidSyntaxException(tokens[i]);
						}
						node.Name = name;
						result.Add(node);
						break;
					case TokenType.CommentChar: // コメントなら
						// カウンタを進める
						++i;
						// インスタンスの生成
						var obj = new YComment();
						// プロパティの設定
						obj.Name    = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "_");
						obj.HasName = false;
						// もしコメント文字列があるなら設定
						if (tokens[i].Type == TokenType.CommentText) {
							obj.SetValue(tokens[i].SourceText);
							++i;
						}
						// 結果に追加
						result.Add(obj);
						break;
					default: // それ以外、例外を発生させる
						throw new InvalidSyntaxException(tokens[i]);
				}
			}

			// 結果を返す
			return result;
		}
	}
}
