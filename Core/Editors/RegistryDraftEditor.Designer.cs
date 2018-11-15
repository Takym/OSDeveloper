namespace OSDeveloper.Core.Editors
{
	partial class RegistryDraftEditor
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.renameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.addNewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSelectMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExpand = new System.Windows.Forms.Button();
			this.btnCollapse = new System.Windows.Forms.Button();
			this.gridView = new System.Windows.Forms.DataGridView();
			this.identifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.grid_removeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.printDoc = new System.Drawing.Printing.PrintDocument();
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
			this.mainContainer.Panel1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.popupMenu.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			this.gridMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainContainer
			// 
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 0);
			this.mainContainer.Name = "mainContainer";
			// 
			// mainContainer.Panel1
			// 
			this.mainContainer.Panel1.Controls.Add(this.treeView);
			this.mainContainer.Panel1.Controls.Add(this.flowLayoutPanel1);
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.gridView);
			this.mainContainer.Size = new System.Drawing.Size(484, 461);
			this.mainContainer.SplitterDistance = 200;
			this.mainContainer.TabIndex = 0;
			// 
			// treeView
			// 
			this.treeView.ContextMenuStrip = this.popupMenu;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.LabelEdit = true;
			this.treeView.Location = new System.Drawing.Point(0, 32);
			this.treeView.Name = "treeView";
			this.treeView.Size = new System.Drawing.Size(200, 429);
			this.treeView.TabIndex = 1;
			this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
			this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
			this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
			// 
			// popupMenu
			// 
			this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.renameMenu,
            this.deleteMenu,
            this.addNewMenu,
            this.clearSelectMenu});
			this.popupMenu.Name = "popupMenu";
			this.popupMenu.Size = new System.Drawing.Size(132, 114);
			// 
			// openMenu
			// 
			this.openMenu.Name = "openMenu";
			this.openMenu.Size = new System.Drawing.Size(131, 22);
			this.openMenu.Text = "Open";
			this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
			// 
			// renameMenu
			// 
			this.renameMenu.Name = "renameMenu";
			this.renameMenu.Size = new System.Drawing.Size(131, 22);
			this.renameMenu.Text = "Rename";
			this.renameMenu.Click += new System.EventHandler(this.renameMenu_Click);
			// 
			// deleteMenu
			// 
			this.deleteMenu.Name = "deleteMenu";
			this.deleteMenu.Size = new System.Drawing.Size(131, 22);
			this.deleteMenu.Text = "Delete";
			this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// addNewMenu
			// 
			this.addNewMenu.Name = "addNewMenu";
			this.addNewMenu.Size = new System.Drawing.Size(131, 22);
			this.addNewMenu.Text = "AddNew";
			this.addNewMenu.Click += new System.EventHandler(this.addNewMenu_Click);
			// 
			// clearSelectMenu
			// 
			this.clearSelectMenu.Name = "clearSelectMenu";
			this.clearSelectMenu.Size = new System.Drawing.Size(131, 22);
			this.clearSelectMenu.Text = "ClearSelect";
			this.clearSelectMenu.Click += new System.EventHandler(this.clearSelectMenu_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnRefresh);
			this.flowLayoutPanel1.Controls.Add(this.btnExpand);
			this.flowLayoutPanel1.Controls.Add(this.btnCollapse);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 32);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(3, 3);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(23, 23);
			this.btnRefresh.TabIndex = 0;
			this.btnRefresh.Text = "R";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnExpand
			// 
			this.btnExpand.Location = new System.Drawing.Point(32, 3);
			this.btnExpand.Name = "btnExpand";
			this.btnExpand.Size = new System.Drawing.Size(23, 23);
			this.btnExpand.TabIndex = 1;
			this.btnExpand.Text = "X";
			this.btnExpand.UseVisualStyleBackColor = true;
			this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
			// 
			// btnCollapse
			// 
			this.btnCollapse.Location = new System.Drawing.Point(61, 3);
			this.btnCollapse.Name = "btnCollapse";
			this.btnCollapse.Size = new System.Drawing.Size(23, 23);
			this.btnCollapse.TabIndex = 2;
			this.btnCollapse.Text = "C";
			this.btnCollapse.UseVisualStyleBackColor = true;
			this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
			// 
			// gridView
			// 
			this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.identifier,
            this.type,
            this.value});
			this.gridView.ContextMenuStrip = this.gridMenu;
			this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridView.Location = new System.Drawing.Point(0, 0);
			this.gridView.Name = "gridView";
			this.gridView.RowTemplate.Height = 21;
			this.gridView.Size = new System.Drawing.Size(280, 461);
			this.gridView.TabIndex = 0;
			this.gridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellEndEdit);
			this.gridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridView_RowsAdded);
			this.gridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.gridView_RowsRemoved);
			// 
			// identifier
			// 
			this.identifier.Frozen = true;
			this.identifier.HeaderText = "Identifier";
			this.identifier.Name = "identifier";
			// 
			// type
			// 
			this.type.Frozen = true;
			this.type.HeaderText = "Type";
			this.type.Items.AddRange(new object[] {
            "REG_SZ",
            "REG_BINARY",
            "REG_DWORD",
            "REG_QWORD",
            "REG_MULTI_SZ",
            "REG_EXPAND_SZ"});
			this.type.Name = "type";
			// 
			// value
			// 
			this.value.Frozen = true;
			this.value.HeaderText = "Value";
			this.value.Name = "value";
			// 
			// gridMenu
			// 
			this.gridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grid_removeMenu});
			this.gridMenu.Name = "gridMenu";
			this.gridMenu.Size = new System.Drawing.Size(117, 26);
			// 
			// grid_removeMenu
			// 
			this.grid_removeMenu.Name = "grid_removeMenu";
			this.grid_removeMenu.Size = new System.Drawing.Size(116, 22);
			this.grid_removeMenu.Text = "Remove";
			this.grid_removeMenu.Click += new System.EventHandler(this.grid_removeMenu_Click);
			// 
			// RegistryDraftEditor
			// 
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.mainContainer);
			this.Name = "RegistryDraftEditor";
			this.Load += new System.EventHandler(this.RegistryDraftEditor_Load);
			this.mainContainer.Panel1.ResumeLayout(false);
			this.mainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
			this.mainContainer.ResumeLayout(false);
			this.popupMenu.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.gridMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.DataGridView gridView;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExpand;
		private System.Windows.Forms.Button btnCollapse;
		private System.Windows.Forms.ContextMenuStrip popupMenu;
		private System.Windows.Forms.ToolStripMenuItem openMenu;
		private System.Windows.Forms.ToolStripMenuItem renameMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteMenu;
		private System.Windows.Forms.ToolStripMenuItem addNewMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn identifier;
		private System.Windows.Forms.DataGridViewComboBoxColumn type;
		private System.Windows.Forms.DataGridViewTextBoxColumn value;
		private System.Windows.Forms.ContextMenuStrip gridMenu;
		private System.Windows.Forms.ToolStripMenuItem grid_removeMenu;
		private System.Windows.Forms.ToolStripMenuItem clearSelectMenu;
		private System.Drawing.Printing.PrintDocument printDoc;
	}
}
