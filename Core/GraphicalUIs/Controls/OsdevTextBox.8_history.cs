using System.Windows.Forms;
using OSDeveloper.Core.Editors;

namespace OSDeveloper.Core.GraphicalUIs.Controls
{
	partial class __ { } // デザイナ避け
	partial class OsdevTextBox : IUndoRedoFeature
	{
		public bool CanUndo
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public bool CanRedo
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		public void Undo()
		{
			throw new System.NotImplementedException();
		}

		public void Redo()
		{
			throw new System.NotImplementedException();
		}
	}
}
