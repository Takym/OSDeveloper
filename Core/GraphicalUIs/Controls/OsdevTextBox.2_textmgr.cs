using System;
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
		private string[] _lines;

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
			if (row >= _lines.Length) {
				throw ErrorGen.ArgOutOfRange(row, 0, _lines.Length - 1);
			}
			string l = _lines[row];
			if (col >= l.Length) {
				throw ErrorGen.ArgOutOfRange(col, 0, l.Length - 1);
			}
			return l;
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
			_lines = text.Split('\n');
			base.Text = text;
			vScrollBar.Maximum = _lines.Length;
			this.Invalidate();
		}

		/// <summary>
		///  指定された場所に指定された文字列を追加します。
		/// </summary>
		/// <param name="row">追加先の行です。</param>
		/// <param name="col">追加先の列です。</param>
		/// <param name="s">追加する文字列です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void AddStringTo(int row, int col, string s)
		{
			string l = this.CheckPoint(row, col);
			_lines[row] = l.Insert(col, s);
			base.Text = string.Join("\n", _lines);
			this.Invalidate();
		}

		/// <summary>
		///  指定された場所の文字列を指定された回数分だけ削除します。
		/// </summary>
		/// <param name="row">削除する文字列の行です。</param>
		/// <param name="col">削除する文字列の列です。</param>
		/// <param name="count">削除する文字数です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveStringFrom(int row, int col, int count)
		{
			string l = this.CheckPoint(row, col);
			_lines[row] = l.Remove(col, count);
			base.Text = string.Join("\n", _lines);
			this.Invalidate();
		}

		/// <summary>
		///  指定された場所に指定された字を追加します。
		/// </summary>
		/// <param name="pos">
		///  字を追加する場所です。
		///  <see cref="System.Drawing.Point.X"/>が行数で、
		///  <see cref="System.Drawing.Point.Y"/>が列数です。
		/// </param>
		/// <param name="c">追加する字です。</param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void AddCharTo(Point pos, char c)
		{
			this.AddStringTo(pos.X, pos.Y, c.ToString());
		}

		/// <summary>
		///  指定された場所の字を削除します。
		/// </summary>
		/// <param name="pos">
		///  削除する字の場所です。
		///  <see cref="System.Drawing.Point.X"/>が行数で、
		///  <see cref="System.Drawing.Point.Y"/>が列数です。
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException" />
		public void RemoveCharFrom(Point pos)
		{
			this.RemoveStringFrom(pos.X, pos.Y, 1);
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
			_row_se = _lines.Length - 1;
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

		#region 応用的な文字列の選択処理 (後で実装)

		// TODO: SelectionIndex、SelectionLength

		/// <summary>
		///  このテキストエディタで選択されている文字列を取得または設定します。
		/// </summary>
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

		/// <summary>
		///  選択位置を取得または設定します。
		/// </summary>
		public int SelectionIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		///  選択されている文字数を取得または設定します。
		/// </summary>
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
