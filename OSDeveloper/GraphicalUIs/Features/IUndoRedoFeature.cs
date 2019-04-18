namespace OSDeveloper.GraphicalUIs.Features
{
	public interface IUndoRedoFeature
	{
		bool CanUndo { get; }
		bool CanRedo { get; }
		void Undo(int count = 1);
		void Redo(int count = 1);
	}
}
