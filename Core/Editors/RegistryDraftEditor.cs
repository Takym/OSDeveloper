using System;
using System.Windows.Forms;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.Logging;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  レジストリの下書きを変種します。
	/// </summary>
	public partial class RegistryDraftEditor : EditorWindow
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

			if (this.TargetFile != null) {
				// ファイル読み込み処理
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
	}
}
