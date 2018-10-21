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
			_logger.Trace($"executing {nameof(RegistryDraftEditor_Load)}...");
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
		}

		#region コントロールボタン
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnRefresh_Click)}...");

			treeView.SelectedNode = null;
			treeView.Sort();

			_logger.Trace($"completed {nameof(btnRefresh_Click)}");
		}

		private void btnExpand_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnExpand_Click)}...");

			treeView.ExpandAll();

			_logger.Trace($"completed {nameof(btnExpand_Click)}");
		}

		private void btnCollapse_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(btnCollapse_Click)}...");

			treeView.CollapseAll();

			_logger.Trace($"completed {nameof(btnCollapse_Click)}");
		}
		#endregion

		#region ツリービュー
		private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_AfterLabelEdit)}...");

			e.Node.Name = e.Label;

			_logger.Trace($"completed {nameof(treeView_AfterLabelEdit)}");
		}
		#endregion

		#region ポップアップメニュー
		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(openMenu_Click)}...");

			// 危険だけどリフレクションを使って Rows プロパティ書き換え
			// .NET Framework v4.7.2 バージョンでは DataGridView プロパティは dataGridViewRows を利用している。
			// https://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/DataGridView.cs,2cc4e2a42be6fd3c

			var t = gridView.GetType();
			var f = t.GetField("dataGridViewRows");


			_logger.Trace($"completed {nameof(openMenu_Click)}");
		}

		private void renameMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(renameMenu_Click)}...");

			treeView.SelectedNode?.BeginEdit();

			_logger.Trace($"completed {nameof(renameMenu_Click)}");
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(deleteMenu_Click)}...");

			treeView.SelectedNode?.Remove();

			_logger.Trace($"completed {nameof(deleteMenu_Click)}");
		}

		private void addNewMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(addNewMenu_Click)}...");

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
			nodes.Add(new RegistryDraftTreeNode(name, gridView));

			_logger.Trace($"completed {nameof(addNewMenu_Click)}");
		}
		#endregion

		#region データグリッドビュー
		private void gridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			_logger.Trace($"executing {nameof(gridView_CellEndEdit)}...");

			_logger.Trace($"completed {nameof(gridView_CellEndEdit)}");
		}

		private void gridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			_logger.Trace($"executing {nameof(gridView_RowsAdded)}...");

			_logger.Trace($"completed {nameof(gridView_RowsAdded)}");
		}

		private void gridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			_logger.Trace($"executing {nameof(gridView_RowsRemoved)}...");

			_logger.Trace($"completed {nameof(gridView_RowsRemoved)}");
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

		#region TreeNodeCollection
		private class RegistryDraftTreeNode : TreeNode
		{
			private DataGridViewRowCollection _rows;

			public RegistryDraftTreeNode(string name, DataGridView gridView)
			{
				this.Name = name;
				this.Text = name;
				_rows = new DataGridViewRowCollection(gridView);
			}

			public DataGridViewRowCollection GetRows() => _rows;
		}
		#endregion
	}
}
