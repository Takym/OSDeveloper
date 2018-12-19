using System;
using System.Collections.Generic;
using System.Drawing;
using OSDeveloper.Core.Editors;
using OSDeveloper.Core.Error;
using OSDeveloper.Core.MiscUtils;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox : ITextSelectionFeature
	{
		/// <summary>
		///  <see cref="Lines"/>プロパティの実態です。
		/// </summary>
		private List<string> _lines;

		/// <summary>
		///  選択範囲を表します。
		///  <see cref="_row_ss"/>は最初の場所の行数です。
		///  <see cref="_col_ss"/>は最初の場所の列数です。
		///  <see cref="_row_se"/>は最後の場所の行数です。
		///  <see cref="_col_se"/>は最後の場所の列数です。
		/// </summary>
		private int _row_ss, _col_ss, _row_se, _col_se;

		#region 共通処理

		private string CheckPoint(int row, int col)
		{
			if (row >= _lines.Count) {
				throw ErrorGen.ArgOutOfRange(row, 0, _lines.Count - 1, nameof(row));
			}
			string l = _lines[row];
			if (col >= l.Length) {
				throw ErrorGen.ArgOutOfRange(col, 0, l.Length - 1, nameof(col));
			}
			return l;
		}

		private void InsertLines(string[] vs, int row)
		{
			for (int i = vs.Length - 1; i >= 0; --i) {
				_lines.Insert(row, vs[i]);
			}
			this.UpdateText();
		}

		private void RemoveLines(int row, int count)
		{
			for (int i = row; i < count; ++i) {
				_lines.RemoveAt(i);
			}
			this.UpdateText();
		}

		private void UpdateText()
		{
			vScrollBar.Maximum = _lines.Count;
			this.Invalidate();
			base.Text = string.Join("\n", _lines.ToArray());
		}

		private (int row1, int col1, int row2, int col2) ReplaceTextMultiple(int row1, int col1, int row2, int col2, string s)
		{
			return (0, 0, 0, 0);
		}

		private (int row1, int col1, int row2, int col2) ReplaceTextSingle(int row1, int col1, int row2, int col2, string s)
		{
			return (0, 0, 0, 0);
		}

		#endregion

		#region 基本的な文字列の設定処理

		/// <summary>
		///  このテキストボックスに表示される文字列を設定します。
		/// </summary>
		/// <param name="text">表示する文字列です。</param>
		public void SetText(string text)
		{
			text = text.CRtoLF();
			_lines.Clear();
			_lines.AddRange(text.Split('\n'));
			vScrollBar.Maximum = _lines.Count;
			this.Invalidate();
			base.Text = text;
		}

		/// <summary>
		///  このテキストボックスに表示される文字列を取得します。
		/// </summary>
		/// <returns>表示されている文字列です。</returns>
		public string GetText()
		{
			return base.Text = string.Join("\n", _lines.ToArray());
		}

		/// <summary>
		///  指定された場所に指定された文字列を挿入します。
		/// </summary>
		/// <param name="row">追加先の行です。</param>
		/// <param name="col">追加先の列です。</param>
		/// <param name="s">追加する文字列です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException"/>
		public void InsertString(int row, int col, string s)
		{
			this.CheckPoint(row, col);
			s = s ?? string.Empty;
			string[] vs = _lines[row].Insert(col, s).CRtoLF().Split('\n');
			_lines.RemoveAt(row);
			this.InsertLines(vs, row);
		}

		/// <summary>
		///  指定された行に文字列を追加します。
		/// </summary>
		/// <param name="row">追加先の行です。</param>
		/// <param name="s">追加する文字列です。</param>
		public void InsertTextLine(int row, string s)
		{
			this.CheckPoint(row, 0);
			this.InsertLines(s.CRtoLF().Split('\n'), row);
		}

		/// <summary>
		///  指定された場所の文字列を指定数回分だけ削除します。
		/// </summary>
		/// <param name="row">削除する文字列の行番号です。</param>
		/// <param name="col">削除する文字列の列番号です。</param>
		/// <param name="count">削除する文字数です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveStringAt(int row, int col, int count)
		{
			this.CheckPoint(row, col);
			_lines[row] = _lines[row].Remove(col, count);
			this.UpdateText();
		}

		/// <summary>
		///  指定されたテキスト行を削除します。
		/// </summary>
		/// <param name="row">削除する行の行番号です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveTextLine(int row)
		{
			this.CheckPoint(row, 0);
			_lines.RemoveAt(row);
			this.UpdateText();
		}

		/// <summary>
		///  指定された行の改行を削除します。
		/// </summary>
		/// <param name="row">改行を削除する行の行番号です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveLineBreakOf(int row)
		{
			this.CheckPoint(row, 0);
			if (row < _lines.Count - 1) {
				_lines[row] += _lines[row + 1];
				_lines.RemoveAt(row + 1);
			}
			this.UpdateText();
		}

		/// <summary>
		///  指定された範囲の文字列を別の文字列に置換します。
		/// </summary>
		/// <param name="row1">置換する文字列の最初の行です。</param>
		/// <param name="col1">置換する文字列の最初の列です。</param>
		/// <param name="row2">置換する文字列の最後の行です。</param>
		/// <param name="col2">置換する文字列の最後の列です。</param>
		/// <param name="s">置換後の文字列です。</param>
		/// <returns>置換後の範囲です。</returns>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public (int row1, int col1, int row2, int col2) ReplaceText(int row1, int col1, int row2, int col2, string s)
		{
			this.CheckPoint(row1, col1);
			this.CheckPoint(row2, col2);
			s = s ?? string.Empty;
			(int row1, int col1, int row2, int col2) result;

			if (row1 < row2) {             // 行1が行2より前にある場合
				result = this.ReplaceTextMultiple(row1, col1, row2, col2, s);
			} else if (row1 == row2) {     // 行1と行2が同じ場合
				if (col1 < col2) {         // 列1が列2より前にある場合
					result = this.ReplaceTextSingle(row1, col1, row2, col2, s);
				} else if (col1 == col2) { // 列1と列2が同じ場合
					return (row1, col1, row2, col2);
				} else {                   // 列1が列2より後にある場合
					var r = this.ReplaceTextSingle(row2, col2, row1, col1, s);
					result.row1 = r.row2; result.col1 = r.col2;
					result.row2 = r.row1; result.col2 = r.col1;
				}
			} else {                       // 行1が行2より後にある場合
				var r = this.ReplaceTextMultiple(row2, col2, row1, col1, s);
				result.row1 = r.row2; result.col1 = r.col2;
				result.row2 = r.row1; result.col2 = r.col1;
			}

			return result;
		}
		#endregion

		#region 基本的な文字列の選択処理

		/// <summary>
		///  選択範囲の最初の場所を座標形式で取得または設定します。
		///  <see cref="System.Drawing.Point.X"/>が行数で、
		///  <see cref="System.Drawing.Point.Y"/>が列数です。
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public Point SelectionStart
		{
			get
			{
				return new Point(_row_ss, _col_ss);
			}

			set
			{
				this.CheckPoint(value.X, value.Y);
				_row_ss = value.X;
				_col_ss = value.Y;
				this.Invalidate();
			}
		}

		/// <summary>
		///  選択範囲の最後の場所を座標形式で取得または設定します。
		///  <see cref="System.Drawing.Point.X"/>が行数で、
		///  <see cref="System.Drawing.Point.Y"/>が列数です。
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public Point SelectionEnd
		{
			get
			{
				return new Point(_row_se, _col_se);
			}

			set
			{
				this.CheckPoint(value.X, value.Y);
				_row_se = value.X;
				_col_se = value.Y;
				this.Invalidate();
			}
		}

		/// <summary>
		///  文字列が選択されているかどうかを表す論理値を取得します。
		/// </summary>
		public bool IsSelected
		{
			get
			{
				return !(_row_ss == _row_se && _col_ss == _col_se);
			}
		}

		/// <summary>
		///  このテキストボックス内の全ての文字列を選択します。
		/// </summary>
		public void SelectAll()
		{
			_row_ss = 0;
			_col_ss = 0;
			_row_se = _lines.Count - 1;
			_col_se = _lines[_row_se].Length - 1;
			this.Invalidate();
		}

		/// <summary>
		///  選択を解除します。
		/// </summary>
		public void ClearSelection()
		{
			_row_se = _row_ss;
			_col_se = _col_ss;
			this.Invalidate();
		}

		#endregion

		#region 応用的な文字列の選択処理

		/// <summary>
		///  このテキストエディタで選択されている文字列を取得または設定します。
		/// </summary>
		public string SelectedText
		{
			get
			{
				return this.Text.Substring(this.SelectionIndex, this.SelectionIndex);
			}

			set
			{
				// TODO: 選択範囲の書き換え、中々いいアルゴリズムが思い付かない。
				throw new NotImplementedException();
			}
		}

		/// <summary>
		///  選択範囲の開始位置を取得または設定します。
		/// </summary>
		public int SelectionIndex
		{
			get
			{
				int x = 0;
				for (int i = 0; i < _row_ss; ++i) {
					x += _lines[i].Length;
				}
				return x + _col_ss;
			}
		}

		/// <summary>
		///  選択範囲の終了位置を取得または設定します。
		/// </summary>
		public int SelectionLastIndex
		{
			get
			{
				int x = 0;
				for (int i = 0; i < _row_se; ++i) {
					x += _lines[i].Length;
				}
				return x + _col_se;
			}
		}

		/// <summary>
		///  選択されている文字数を取得または設定します。
		/// </summary>
		public int SelectionLength
		{
			get
			{
				return this.SelectionLastIndex - this.SelectionIndex;
			}
		}

		#endregion
	}
}
