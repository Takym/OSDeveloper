using System;
using System.Windows.Forms;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.FileManagement.Structures;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  レジストリの下書きを変種します。
	/// </summary>
	public partial class RegistryDraftEditor : EditorWindow, IFileSaveLoadFeature
	{
		private Logger _logger;

		/// <summary>
		///  親ウィンドウを指定して、
		///  型'<see cref="OSDeveloper.Core.Editors.RegistryDraftEditor"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		/// <param name="parent">親ウィンドウです。</param>
		public RegistryDraftEditor(MainWindowBase parent) : base(parent)
		{
			this.InitializeComponent();
			_logger = Logger.GetSystemLogger(nameof(RegistryDraftEditor));
		}

		/// <summary>
		///  親ウィンドウを指定せずに単独のウィンドウとして、
		///  型'<see cref="OSDeveloper.Core.Editors.RegistryDraftEditor"/>'の
		///  新しいインスタンスを生成します。
		/// </summary>
		public RegistryDraftEditor()
		{
			this.InitializeComponent();
			_logger = Logger.GetSystemLogger(nameof(RegistryDraftEditor));
		}

		private void RegistryDraftEditor_Load(object sender, EventArgs e)
		{
			_logger.Trace("The OnLoad event of RegistryDraftEditor was called");
			_logger.Info("Setting the tool strip bar for RegistryDraftEditor...");

			_logger.Info("Setting the control buttons of RegistryDraftEditor...");
			btnRefresh.Image = Libosdev.GetIcon("Refresh", this, out int vREF).ToBitmap();
			btnRefresh.Text = string.Empty;
			btnExpand.Image = Libosdev.GetIcon("Expand", this, out int vEXP).ToBitmap();
			btnExpand.Text = string.Empty;
			btnCollapse.Image = Libosdev.GetIcon("Collapse", this, out int vCOL).ToBitmap();
			btnCollapse.Text = string.Empty;
			_logger.Info($"GetIcon HResults = REF:{vREF}, EXP:{vEXP}, COL:{vCOL}");

			_logger.Info("Setting the popup menus of RegistryDraftEditor...");
			openMenu.Text = RegistryDraftEditorTexts.Popup_Open;
			renameMenu.Text = RegistryDraftEditorTexts.Popup_Rename;
			deleteMenu.Text = RegistryDraftEditorTexts.Popup_Delete;
			addNewMenu.Text = RegistryDraftEditorTexts.Popup_AddNew;

			_logger.Info("Setting the data grid view of RegistryDraftEditor...");
			identifier.HeaderText = RegistryDraftEditorTexts.DataGridView_Identifier;
			type.HeaderText = RegistryDraftEditorTexts.DataGridView_Type;
			value.HeaderText = RegistryDraftEditorTexts.DataGridView_Value;

			if (this.TargetFile != null) {
				this.Reload();
			}

			_logger.Info("Explorer control was initialized");
			_logger.Trace("Finished OnLoad event of RegistryDraftEditor");
		}

		#region コントロールボタン
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Refresh button in RegistryDraftEditor was called");

			treeView.SelectedNode = null;
			treeView.Sort();

			_logger.Trace("Finished OnClick event of Refresh button in RegistryDraftEditor");
		}

		private void btnExpand_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Expand button in RegistryDraftEditor was called");

			treeView.ExpandAll();

			_logger.Trace("Finished OnClick event of Expand button in RegistryDraftEditor");
		}

		private void btnCollapse_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Collapse button in RegistryDraftEditor was called");

			treeView.CollapseAll();

			_logger.Trace("Finished OnClick event of Collapse button in RegistryDraftEditor");
		}
		#endregion

		#region ツリービュー
		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace("The OnAfterLabelEdit event of the tree view in RegistryDraftEditor was called");

			e.Node.Name = e.Label;

			_logger.Trace("Finished OnAfterLabelEdit event of the tree view in RegistryDraftEditor");
		}
		#endregion

		#region ポップアップメニュー
		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Open popup-menu in RegistryDraftEditor was called");

			_logger.Trace("Finished OnClick event of Open popup-menu in RegistryDraftEditor");
		}

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Rename popup-menu in RegistryDraftEditor was called");

			treeView.SelectedNode?.BeginEdit();

			_logger.Trace("Finished OnClick event of Rename popup-menu in RegistryDraftEditor");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Delete popup-menu in RegistryDraftEditor was called");

			treeView.SelectedNode?.Remove();

			_logger.Trace("Finished OnClick event of Delete popup-menu in RegistryDraftEditor");
		}

		private void addNewMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of AddNew popup-menu in RegistryDraftEditor was called");

			TreeNodeCollection nodes;
			if (treeView.SelectedNode == null) {
				nodes = treeView.Nodes;
			} else {
				nodes = treeView.SelectedNode.Nodes;
			}

			string name = "New Key";
			for (int i = 1; i < int.MaxValue; ++i) {
				name = $"New Key #{i}";
				if (!nodes.ContainsKey(name)) {
					break;
				}
			}
			nodes.Add(name, name);

			_logger.Trace("Finished OnClick event of AddNew popup-menu in RegistryDraftEditor");
		}
		#endregion

		#region データグリッドビュー
		private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{

		}

		private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{

		}
		#endregion

		#region IFileSaveLoadFeature
		/// <summary>
		///  このエディタがサポートしているファイルの種類を取得します。
		/// </summary>
		/// <returns>ファイルの種類を表すオブジェクトです。</returns>
		public FileType GetFileType()
		{
			return FileTypes.RegistryDraftFile;
		}

		/// <summary>
		///  ファイルを<see cref="OSDeveloper.Core.Editors.EditorWindow.TargetFile"/>に保存します。
		/// </summary>
		public void Save()
		{
			// TODO: 保存処理を作成する
		}
		
		/// <summary>
		///  ファイルを別名で保存します。
		///  <see cref="OSDeveloper.Core.Editors.EditorWindow.TargetFile"/>は新しいファイルのファイル情報へ変更されます。
		/// </summary>
		/// <param name="path">保存先のファイルのパスです。</param>
		public void SaveAs(string path)
		{
			this.TargetFile = new RegistryDraftFile(path);
			this.Save();
		}

		/// <summary>
		///  ファイルを再度読み込みます。変更内容は破棄されます。
		/// </summary>
		public void Reload()
		{
			// TODO: 読込処理を作成する
		}

		/// <summary>
		///  ファイルを別の場所から読み込み、<see cref="OSDeveloper.Core.Editors.EditorWindow.TargetFile"/>を書き換えます。
		/// </summary>
		/// <param name="path">読み込み元のファイルのパスです。</param>
		/// <param name="saveCurrent">
		///  現在の変更内容を保存してから別のファイルを開く場合は<see langword="true"/>、
		///  保存しないで開く場合は<see langword="false"/>です。
		/// </param>
		public void LoadFrom(string path, bool saveCurrent)
		{
			if (saveCurrent) this.Save();
			this.TargetFile = new RegistryDraftFile(path);
			this.Reload();
		}
		#endregion
	}
}
