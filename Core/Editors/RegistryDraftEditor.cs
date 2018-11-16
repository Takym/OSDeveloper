using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using OSDeveloper.Assets;
using OSDeveloper.Core.FileManagement;
using OSDeveloper.Core.FileManagement.Structures;
using OSDeveloper.Core.GraphicalUIs;
using OSDeveloper.Core.Logging;
using OSDeveloper.Core.MiscUtils;
using OSDeveloper.Core.Settings;
using OSDeveloper.Native;

namespace OSDeveloper.Core.Editors
{
	/// <summary>
	///  レジストリの下書きを変種します。
	/// </summary>
	public partial class RegistryDraftEditor : EditorWindow, IFileSaveLoadFeature, IPrintingFeature
	{
		private Logger _logger;
		private YenconHeader _header;
		private List<DataGridViewRow> _current_rows;

		#region 初期化
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
			_header = null;
			_current_rows = null;
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
			clearSelectMenu.Text = RegistryDraftEditorTexts.Popup_ClearSelect;
			grid_removeMenu.Text = RegistryDraftEditorTexts.Popup_Grid_Remove;

			_logger.Info("Setting the data grid view of RegistryDraftEditor...");
			identifier.HeaderText = RegistryDraftEditorTexts.DataGridView_Identifier;
			type.HeaderText = RegistryDraftEditorTexts.DataGridView_Type;
			value.HeaderText = RegistryDraftEditorTexts.DataGridView_Value;

			if (this.TargetFile != null) {
				this.Reload();
			}

			_logger.Info("Explorer control was initialized");
		}
		#endregion

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

		private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_NodeMouseClick)}...");

			this.OpenNode();

			_logger.Trace($"completed {nameof(treeView_NodeMouseClick)}");
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			_logger.Trace($"executing {nameof(treeView_NodeMouseDoubleClick)}...");

			this.OpenNode();

			_logger.Trace($"completed {nameof(treeView_NodeMouseDoubleClick)}");
		}
		#endregion

		#region ポップアップメニュー
		private void openMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(openMenu_Click)}...");

			this.OpenNode();

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
			nodes.Add(new RegistryDraftTreeNode(name));

			_logger.Trace($"completed {nameof(addNewMenu_Click)}");
		}

		private void clearSelectMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(clearSelectMenu_Click)}...");

			treeView.SelectedNode = null;

			_logger.Trace($"completed {nameof(clearSelectMenu_Click)}");
		}

		private void grid_removeMenu_Click(object sender, EventArgs e)
		{
			_logger.Trace($"executing {nameof(grid_removeMenu_Click)}...");

			foreach (DataGridViewRow item in gridView.SelectedRows) {
				if (!item.IsNewRow) {
					gridView.Rows.Remove(item);
				}
			}

			_logger.Trace($"completed {nameof(grid_removeMenu_Click)}");
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

		/// <summary>
		///  ファイルを<see cref="OSDeveloper.Core.Editors.EditorWindow.TargetFile"/>に保存します。
		/// </summary>
		public void Save()
		{
			this.SaveNode();
			var data = this.SaveInternal(treeView.Nodes);
			ConfigManager.SaveYencon(this.TargetFile.FilePath, _header, data);
		}

		private YenconSection SaveInternal(TreeNodeCollection tnc)
		{
			YenconSection section = new YenconSection();
			for (int i = 0; i < tnc.Count; ++i) {
				if (tnc[i] is RegistryDraftTreeNode key) {
					YenconSection ykey = new YenconSection();
					ykey.SetNode("keyname", new YenconStringKey(key.Name));
					ykey.SetNode("subkeys", this.SaveInternal(key.Nodes));
					ykey.SetNode("values", this.SaveValues(key.GetRows()));
					section.SetNode($"Key_{i}", ykey);
				}
			}
			return section;
		}

		private YenconSection SaveValues(List<DataGridViewRow> rows)
		{
			YenconSection section = new YenconSection();
			section.SetNode("_count", new YenconNumberKey(rows.Count));
			for (int i = 0; i < rows.Count; ++i) {
				var c = rows[i].Cells;
				YenconSection value = new YenconSection();
				switch (c.Count) {
					case 0:
						break;
					case 1:
						value.SetNode("name", new YenconStringKey(c[0].Value));
						break;
					case 2:
						value.SetNode("name", new YenconStringKey(c[0].Value));
						value.SetNode("type", new YenconStringKey(c[1].Value));
						break;
					default:
						value.SetNode("name", new YenconStringKey(c[0].Value));
						value.SetNode("type", new YenconStringKey(c[1].Value));
						value.SetNode("data", new YenconStringKey(c[2].Value));
						break;
				}
				section.SetNode(i.ToString(), value);
			}
			return section;
		}

		/// <summary>
		///  ファイルを再度読み込みます。変更内容は破棄されます。
		/// </summary>
		public void Reload()
		{
			_current_rows = null;
			var data = ConfigManager.LoadYencon(this.TargetFile.FilePath, out var _header);
			treeView.Nodes.Clear();
			this.ReloadInternal(data, treeView.Nodes);
			if (treeView.Nodes.Count > 0) {
				treeView.SelectedNode = treeView.Nodes[0];
				this.OpenNode();
			}
			printDoc.DocumentName = this.TargetFile.Name;
		}

		private void ReloadInternal(YenconSection section, TreeNodeCollection tnc)
		{
			if (section == null) return;
			foreach (var item in section.Children) {
				YenconSection ykey = item.Value.Value as YenconSection;
				if (ykey != null) {
					var keyname = ykey["keyname"] as YenconStringKey;
					if (keyname != null) {
						RegistryDraftTreeNode rdtn = new RegistryDraftTreeNode(keyname.Text.Unescape());
						this.ReloadInternal(ykey["subkeys"] as YenconSection, rdtn.Nodes);
						this.ReloadValues(ykey["values"] as YenconSection, rdtn.GetRows());
						tnc.Add(rdtn);
					}
				}
			}
		}

		private void ReloadValues(YenconSection section, List<DataGridViewRow> rows)
		{
			var _count = section["_count"] as YenconNumberKey;
			if (_count == null) return;
			for (int i = 0; i < ((int)(_count.Count)); ++i) {
				var value = section[i.ToString()] as YenconSection;
				if (value != null) {
					var r = new DataGridViewRow();
					string name = (value["name"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					string type = (value["type"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					string data = (value["data"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					r.CreateCells(gridView, name, type, data);
					rows.Add(r);
				}
			}
		}
		#endregion

		#region IPrintingFeature
		/// <summary>
		///  印刷に使用するドキュメントを取得します。
		/// </summary>
		public PrintDocument PrintDocument
		{
			get
			{
				return printDoc;
			}
		}

		private SubkeyDict _print_root;

		private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			_logger.Trace($"executing {nameof(printDoc_PrintPage)}...");

			if (_print_root == null) {
				this.SaveNode();
				var data = this.SaveInternal(treeView.Nodes);
				_print_root = this.GetKeysAsDict(data);
			}

			// TODO: 失敗作
			using (Font h = FontResources.CreateHeaderFont()) {
				Graphics g = e.Graphics;
				int x = e.MarginBounds.X;
				int y = e.MarginBounds.Y;
				var names = _print_root.Keys.GetEnumerator();
				var keyvals = _print_root.Values.GetEnumerator();
				names.MoveNext();

				while (names.Current != null) {
					g.DrawString(names.Current, h, Brushes.Black, new Point(x, y));
					names.MoveNext();
					keyvals.MoveNext();
					y += h.Height;
					if (y > e.MarginBounds.Height) {
						e.HasMorePages = true;
						return;
					}
				}
			}

			_print_root = null;

			_logger.Trace($"completed {nameof(printDoc_PrintPage)}");
		}

		private sealed class ValuesList : List<(string name, string type, string data)> { }
		private sealed class SubkeyDict : Dictionary<string, (SubkeyDict keys, ValuesList vals)> { }

		private SubkeyDict GetKeysAsDict(YenconSection section)
		{
			var result = new SubkeyDict();
			foreach (var item in section.Children) {
				YenconSection ykey = item.Value.Value as YenconSection;
				if (ykey != null) {
					var keyname = ykey["keyname"] as YenconStringKey;
					if (keyname != null) {
						var keys = this.GetKeysAsDict(ykey["subkeys"] as YenconSection);
						var vals = this.GetValsAsDict(ykey["values"] as YenconSection);
						result.Add(keyname.Text.Unescape(), (keys, vals));
					}
				}
			}
			return result;
		}

		private ValuesList GetValsAsDict(YenconSection section)
		{
			var _count = section["_count"] as YenconNumberKey;
			if (_count == null) return null;
			var result = new ValuesList();
			for (int i = 0; i < ((int)(_count.Count)); ++i) {
				var value = section[i.ToString()] as YenconSection;
				if (value != null) {
					string name = (value["name"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					string type = (value["type"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					string data = (value["data"] as YenconStringKey)?.Text?.Unescape() ?? string.Empty;
					result.Add((name, type, data));
				}
			}
			return result;
		}
		#endregion

		#region RegistryDraftTreeNode
		private class RegistryDraftTreeNode : TreeNode
		{
			private List<DataGridViewRow> _rows;

			public RegistryDraftTreeNode(string name)
			{
				this.Name = name;
				this.Text = name;
				_rows = new List<DataGridViewRow>();
			}

			public List<DataGridViewRow> GetRows() => _rows;
		}
		#endregion

		#region 共通処理
		private void OpenNode()
		{
			if (treeView.SelectedNode != null && treeView.SelectedNode is RegistryDraftTreeNode rdtn) {
				// 以下の処理は時間がかかるけど、DataGridView.Rows を書き換える方法がないので我慢する。

				this.SaveNode();

				// rdtn.GetRows() から gridView.Rows にコピーする。_current_rowsも代入。
				gridView.Rows.Clear();
				_current_rows = rdtn.GetRows();
				foreach (var item in _current_rows) {
					gridView.Rows.Add(item);
				}
			}
		}

		private void SaveNode()
		{
			// _current_rows が存在する場合、現在の内容をコピーする。
			if (_current_rows != null) {
				_current_rows.Clear();
				foreach (DataGridViewRow item in gridView.Rows) {
					if (!item.IsNewRow) {
						_current_rows.Add(item);
					}
				}
			}
		}
		#endregion
	}
}
