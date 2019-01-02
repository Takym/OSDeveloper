using System;
using System.Collections.Generic;
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
		private int _i, _li;

		#region 共通処理

		private List<uint> SetTextPrivate(string s)
		{
			List<uint> result = new List<uint>();
			s = s.CRtoLF();
			for (int i = 0; i < s.Length; ++i) {
				uint x = s[i];
				if ((x & UpperSurrogate) == UpperSurrogate &&
					(i + 1) < s.Length && (s[i + 1] & LowerSurrogate) == LowerSurrogate) {
					uint y = s[i + 1]; ++i;
					uint z = ((x & BitMaskA) + 1) << 16;
					z |= ((x & BitMaskB) << 10) | (y & BitMaskC);
					result.Add(z);
				} else {
					result.Add(x);
				}
			}
			return result;
		}

		private string GetTextPrivate(uint n)
		{
			if (n < 0x10000) {
				return new string((char)(n), 1);
			} else {
				uint a = UpperSurrogate;
				uint b = LowerSurrogate;
				a |= (((n & BitMaskD) - 1) >> 10)
				  |  ( (n & BitMaskE)      >> 10);
				b |= n & BitMaskF;
				return ((char)(a)).ToString() + ((char)(b));
			}
		}

		private string GetTextPrivate(List<uint> vs)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in vs) {
				sb.Append(this.GetTextPrivate(item));
			}
			return sb.ToString();
		}

		#endregion

		#region 文字列の設定処理

		/// <summary>
		///  文字列をこのテキストボックスに設定します。
		/// </summary>
		/// <param name="s">設定する文字列です。</param>
		public void SetText(string s)
		{
			_text.Clear();
			_text.AddRange(this.SetTextPrivate(s));
			this.OnTextChanged(new EventArgs());
		}

		/// <summary>
		///  このテキストボックスが格納している文字列を取得します。
		/// </summary>
		/// <returns>取得した文字列です。</returns>
		public string GetText()
		{
			return this.GetTextPrivate(_text);
		}

		#endregion

		#region 文字列の選択処理

		/// <summary>
		///  このテキストボックスで選択されている文字列を取得または設定します。
		/// </summary>
		public string SelectedText
		{
			get
			{
				return this.GetTextPrivate(_text.GetRange(_i, _li));
			}

			set
			{
				var r = this.SetTextPrivate(value);
				_text.RemoveRange(_i, _li);
				_text.InsertRange(_i, r);
				_li = _i + r.Count;
				this.OnTextChanged(new EventArgs());
			}
		}

		/// <summary>
		///  このテキストボックスの選択文字列の開始位置を取得または設定します。
		/// </summary>
		public int SelectionIndex
		{
			get
			{
				return _i;
			}

			set
			{
				if (0 <= value && value < _text.Count) {
					_i = value;
				} else {
					throw ErrorGen.ArgOutOfRange(value, 0, _text.Count - 1, nameof(value));
				}
			}
		}

		/// <summary>
		///  このテキストボックスの選択文字列の終了位置を取得または設定します。
		/// </summary>
		public int SelectionLastIndex
		{
			get
			{
				return _li;
			}

			set
			{
				if (0 <= value && value < _text.Count) {
					_li = value;
				} else {
					throw ErrorGen.ArgOutOfRange(value, 0, _text.Count - 1, nameof(value));
				}
			}
		}

		/// <summary>
		///  このテキストボックスの選択文字列の長さを取得または設定します。
		/// </summary>
		public int SelectionLength
		{
			get
			{
				return _li - _i;
			}

			set
			{
				if (-_i <= value && value < _text.Count - _i) {
					_li = value + _i;
				} else {
					throw ErrorGen.ArgOutOfRange(value, -_i, _text.Count - (_i + 1), nameof(value));
				}
			}
		}

		/// <summary>
		///  このテキストボックスで文字列が選択されているかどうかを表す論理値を取得します。
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return _li != _i;
			}
		}

		/// <summary>
		///  このテキストボックスに格納されている文字列を全て選択します。
		/// </summary>
		public void SelectAll()
		{
			_i = 0;
			_li = _text.Count;
		}

		/// <summary>
		///  文字列の選択を解除します。
		/// </summary>
		public void ClearSelection()
		{
			_li = _i;
		}

		#endregion
	}
}
