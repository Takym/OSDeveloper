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
		private List<string> _lines;

		#region 共通処理

		#endregion

		#region 基本的な文字列の設定処理

		#endregion

		#region 基本的な文字列の選択処理

		public Point SelectionStart
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

		public Point SelectionEnd
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
