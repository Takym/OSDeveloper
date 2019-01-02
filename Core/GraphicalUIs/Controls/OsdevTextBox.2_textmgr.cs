using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox : ITextSelectionFeature
	{
		private const uint UpperSurrogate = 0b110110_0000_000000;
		private const uint LowerSurrogate = 0b110111_0000_000000;
		private const uint BitMaskA       = 0b000000_1111_000000;
		private const uint BitMaskB       = 0b000000_0000_111111;
		private const uint BitMaskC       = 0b000000_1111_111111;
		private const uint BitMaskD = 0b000_11111_000000_0000000000;
		private const uint BitMaskE = 0b000_00000_111111_0000000000;
		private const uint BitMaskF = 0b000_00000_000000_1111111111;
		private List<uint> _text;

		#region 共通処理

		#endregion

		#region 基本的な文字列の設定処理

		/// <summary>
		///  文字列をこのテキストボックスに設定します。
		/// </summary>
		/// <param name="s">設定する文字列です。</param>
		public void SetText(string s)
		{
			_text.Clear();
			for (int i = 0; i < s.Length; ++i) {
				uint x = s[i];
				if ((x & UpperSurrogate) == UpperSurrogate &&
					(i + 1) < s.Length && (s[i + 1] & LowerSurrogate) == LowerSurrogate) {
					uint y = s[i + 1]; ++i;
					uint z = ((x & BitMaskA) + 1) << 16;
					z |= ((x & BitMaskB) << 10) | (y & BitMaskC);
					_text.Add(z);
				} else {
					_text.Add(x);
				}
			}
			this.OnTextChanged(new EventArgs());
		}

		/// <summary>
		///  このテキストボックスが格納している文字列を取得します。
		/// </summary>
		/// <returns>取得した文字列です。</returns>
		public string GetText()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in _text) {
				if (item < 0x10000) {
					sb.Append((char)(item));
				} else {
					uint a = UpperSurrogate;
					uint b = LowerSurrogate;
					a |= (((item & BitMaskD) - 1) >> 10)
					  |  ( (item & BitMaskE)      >> 10);
					b |=   item & BitMaskF;
					sb.Append((char)(a)).Append((char)(b));
				}
			}
			return sb.ToString();
		}

		#endregion

		#region 基本的な文字列の選択処理

		public bool IsSelected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void SelectAll()
		{
			throw new NotImplementedException();
		}

		public void ClearSelection()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region 応用的な文字列の選択処理

		public string SelectedText
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public int SelectionIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int SelectionLastIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int SelectionLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
