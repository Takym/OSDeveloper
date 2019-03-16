namespace OSDeveloper.GraphicalUIs.Explorer
{
	partial class FileTree
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTree));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnRefresh = new System.Windows.Forms.ToolStripButton();
			this.btnExpand = new System.Windows.Forms.ToolStripButton();
			this.btnCollapse = new System.Windows.Forms.ToolStripButton();
			this.treeView = new System.Windows.Forms.TreeView();
			this.iconList = new System.Windows.Forms.ImageList(this.components);
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.renameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.propertyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip.SuspendLayout();
			this.popupMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnExpand,
            this.btnCollapse});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(196, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// btnRefresh
			// 
			this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(23, 22);
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnExpand
			// 
			this.btnExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
			this.btnExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExpand.Name = "btnExpand";
			this.btnExpand.Size = new System.Drawing.Size(23, 22);
			this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
			// 
			// btnCollapse
			// 
			this.btnCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapse.Image")));
			this.btnCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCollapse.Name = "btnCollapse";
			this.btnCollapse.Size = new System.Drawing.Size(23, 22);
			this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.HideSelection = false;
			this.treeView.HotTracking = true;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.iconList;
			this.treeView.LabelEdit = true;
			this.treeView.Location = new System.Drawing.Point(0, 25);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(196, 171);
			this.treeView.TabIndex = 1;
			this.treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
			this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
			this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeSelect);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// iconList
			// 
			this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.iconList.ImageSize = new System.Drawing.Size(16, 16);
			this.iconList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// popupMenu
			// 
			this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameMenu,
            this.deleteMenu,
            this.propertyMenu});
			this.popupMenu.Name = "fileMenu";
			this.popupMenu.Size = new System.Drawing.Size(181, 92);
			// 
			// renameMenu
			// 
			this.renameMenu.Name = "renameMenu";
			this.renameMenu.Size = new System.Drawing.Size(180, 22);
			this.renameMenu.Text = "rename";
			this.renameMenu.Click += new System.EventHandler(this.renameMenu_Click);
			// 
			// deleteMenu
			// 
			this.deleteMenu.Name = "deleteMenu";
			this.deleteMenu.Size = new System.Drawing.Size(180, 22);
			this.deleteMenu.Text = "delete";
			this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// propertyMenu
			// 
			this.propertyMenu.Name = "propertyMenu";
			this.propertyMenu.Size = new System.Drawing.Size(180, 22);
			this.propertyMenu.Text = "property";
			this.propertyMenu.Click += new System.EventHandler(this.propertyMenu_Click);
			// 
			// FileTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.toolStrip);
			this.Name = "FileTree";
			this.Size = new System.Drawing.Size(196, 196);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.popupMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton btnRefresh;
		private System.Windows.Forms.ToolStripButton btnExpand;
		private System.Windows.Forms.ToolStripButton btnCollapse;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList iconList;
		private System.Windows.Forms.ContextMenuStrip popupMenu;
		private System.Windows.Forms.ToolStripMenuItem renameMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteMenu;
		private System.Windows.Forms.ToolStripMenuItem propertyMenu;
	}
}
