namespace OSDeveloper.GUIs.Features
{
	public interface IUndoRedoFeature
	{
		bool CanUndo { get; }
		bool CanRedo { get; }
		void Undo(int steps = 1);
		void Redo(int steps = 1);
	}
}
