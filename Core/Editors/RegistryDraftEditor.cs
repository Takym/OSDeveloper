using System;
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

			if (this.TargetFile != null) {
				treeView.Nodes.Add(this.TargetFile.Name);
			}

			_logger.Info("Explorer control was initialized");
			_logger.Trace("Finished OnLoad event of RegistryDraftEditor");
		}

		#region コントロールボタン
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Refresh button in RegistryDraftEditor was called");
			// 再読み込みする必要は無いので何もしない
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

		#region ポップアップメニュー
		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Open popup-menu in RegistryDraftEditor was called");

			_logger.Trace("Finished OnClick event of Open popup-menu in RegistryDraftEditor");
		}

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Rename popup-menu in RegistryDraftEditor was called");
			treeView.SelectedNode.BeginEdit();
			_logger.Trace("Finished OnClick event of Rename popup-menu in RegistryDraftEditor");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of Delete popup-menu in RegistryDraftEditor was called");
			treeView.SelectedNode.Remove();
			_logger.Trace("Finished OnClick event of Delete popup-menu in RegistryDraftEditor");
		}

		private void addNewMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace("The OnClick event of AddNew popup-menu in RegistryDraftEditor was called");
			treeView.SelectedNode.Nodes.Add("New Key");
			_logger.Trace("Finished OnClick event of AddNew popup-menu in RegistryDraftEditor");
		}
		#endregion
	}
}
