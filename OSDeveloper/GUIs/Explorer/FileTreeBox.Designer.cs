namespace OSDeveloper.GUIs.Explorer
{
	partial class FileTreeBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTreeBox));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnRefresh = new System.Windows.Forms.ToolStripButton();
			this.btnExpand = new System.Windows.Forms.ToolStripButton();
			this.btnCollapse = new System.Windows.Forms.ToolStripButton();
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openInMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultAppMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.explorerMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.powershellMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bashMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.createFileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.createDirMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.additemMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.generateNewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.fromSystemMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cloneMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cutMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.removeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.renameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.propertyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
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
			this.toolStrip.Text = "toolStrip";
			// 
			// btnRefresh
			// 
			this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(23, 22);
			this.btnRefresh.Text = "btnRefresh";
			this.btnRefresh.ToolTipText = "btnRefresh";
			// 
			// btnExpand
			// 
			this.btnExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
			this.btnExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExpand.Name = "btnExpand";
			this.btnExpand.Size = new System.Drawing.Size(23, 22);
			this.btnExpand.Text = "btnExpand";
			this.btnExpand.ToolTipText = "btnExpand";
			// 
			// btnCollapse
			// 
			this.btnCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapse.Image")));
			this.btnCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCollapse.Name = "btnCollapse";
			this.btnCollapse.Size = new System.Drawing.Size(23, 22);
			this.btnCollapse.Text = "btnCollapse";
			this.btnCollapse.ToolTipText = "btnCollapse";
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.Location = new System.Drawing.Point(0, 25);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(196, 171);
			this.treeView.TabIndex = 1;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// popupMenu
			// 
			this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.openInMenu,
            this.toolStripSeparator1,
            this.createFileMenu,
            this.createDirMenu,
            this.additemMenu,
            this.toolStripSeparator2,
            this.cloneMenu,
            this.copyMenu,
            this.cutMenu,
            this.pasteMenu,
            this.removeMenu,
            this.deleteMenu,
            this.renameMenu,
            this.toolStripSeparator3,
            this.propertyMenu});
			this.popupMenu.Name = "popupMenu";
			this.popupMenu.Size = new System.Drawing.Size(181, 330);
			// 
			// openMenu
			// 
			this.openMenu.Name = "openMenu";
			this.openMenu.Size = new System.Drawing.Size(124, 22);
			this.openMenu.Text = "open";
			// 
			// openInMenu
			// 
			this.openInMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultAppMenu,
            this.explorerMenu,
            this.cmdMenu,
            this.powershellMenu,
            this.bashMenu});
			this.openInMenu.Name = "openInMenu";
			this.openInMenu.Size = new System.Drawing.Size(180, 22);
			this.openInMenu.Text = "openIn";
			// 
			// defaultAppMenu
			// 
			this.defaultAppMenu.Name = "defaultAppMenu";
			this.defaultAppMenu.Size = new System.Drawing.Size(180, 22);
			this.defaultAppMenu.Text = "defaultApp";
			// 
			// explorerMenu
			// 
			this.explorerMenu.Name = "explorerMenu";
			this.explorerMenu.Size = new System.Drawing.Size(180, 22);
			this.explorerMenu.Text = "explorer";
			// 
			// cmdMenu
			// 
			this.cmdMenu.Name = "cmdMenu";
			this.cmdMenu.Size = new System.Drawing.Size(180, 22);
			this.cmdMenu.Text = "cmd";
			// 
			// powershellMenu
			// 
			this.powershellMenu.Name = "powershellMenu";
			this.powershellMenu.Size = new System.Drawing.Size(180, 22);
			this.powershellMenu.Text = "powershell";
			// 
			// bashMenu
			// 
			this.bashMenu.Name = "bashMenu";
			this.bashMenu.Size = new System.Drawing.Size(180, 22);
			this.bashMenu.Text = "bash";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
			// 
			// createFileMenu
			// 
			this.createFileMenu.Name = "createFileMenu";
			this.createFileMenu.Size = new System.Drawing.Size(124, 22);
			this.createFileMenu.Text = "createFile";
			// 
			// createDirMenu
			// 
			this.createDirMenu.Name = "createDirMenu";
			this.createDirMenu.Size = new System.Drawing.Size(124, 22);
			this.createDirMenu.Text = "createDir";
			// 
			// additemMenu
			// 
			this.additemMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateNewMenu,
            this.fromSystemMenu});
			this.additemMenu.Name = "additemMenu";
			this.additemMenu.Size = new System.Drawing.Size(180, 22);
			this.additemMenu.Text = "additem";
			// 
			// generateNewMenu
			// 
			this.generateNewMenu.Enabled = false;
			this.generateNewMenu.Name = "generateNewMenu";
			this.generateNewMenu.Size = new System.Drawing.Size(180, 22);
			this.generateNewMenu.Text = "generateNew";
			this.generateNewMenu.Visible = false;
			// 
			// fromSystemMenu
			// 
			this.fromSystemMenu.Name = "fromSystemMenu";
			this.fromSystemMenu.Size = new System.Drawing.Size(180, 22);
			this.fromSystemMenu.Text = "fromSystem";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
			// 
			// cloneMenu
			// 
			this.cloneMenu.Name = "cloneMenu";
			this.cloneMenu.Size = new System.Drawing.Size(124, 22);
			this.cloneMenu.Text = "clone";
			// 
			// copyMenu
			// 
			this.copyMenu.Name = "copyMenu";
			this.copyMenu.Size = new System.Drawing.Size(124, 22);
			this.copyMenu.Text = "copy";
			// 
			// cutMenu
			// 
			this.cutMenu.Name = "cutMenu";
			this.cutMenu.Size = new System.Drawing.Size(124, 22);
			this.cutMenu.Text = "cut";
			// 
			// pasteMenu
			// 
			this.pasteMenu.Name = "pasteMenu";
			this.pasteMenu.Size = new System.Drawing.Size(124, 22);
			this.pasteMenu.Text = "paste";
			// 
			// removeMenu
			// 
			this.removeMenu.Name = "removeMenu";
			this.removeMenu.Size = new System.Drawing.Size(124, 22);
			this.removeMenu.Text = "remove";
			// 
			// deleteMenu
			// 
			this.deleteMenu.Name = "deleteMenu";
			this.deleteMenu.Size = new System.Drawing.Size(124, 22);
			this.deleteMenu.Text = "delete";
			// 
			// renameMenu
			// 
			this.renameMenu.Name = "renameMenu";
			this.renameMenu.Size = new System.Drawing.Size(124, 22);
			this.renameMenu.Text = "rename";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
			// 
			// propertyMenu
			// 
			this.propertyMenu.Name = "propertyMenu";
			this.propertyMenu.Size = new System.Drawing.Size(124, 22);
			this.propertyMenu.Text = "property";
			// 
			// ofd
			// 
			this.ofd.ShowReadOnly = true;
			// 
			// FileTreeBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.toolStrip);
			this.Name = "FileTreeBox";
			this.Size = new System.Drawing.Size(196, 196);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.popupMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ContextMenuStrip popupMenu;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.ToolStripButton btnRefresh;
		private System.Windows.Forms.ToolStripButton btnExpand;
		private System.Windows.Forms.ToolStripButton btnCollapse;
		private System.Windows.Forms.ToolStripMenuItem openMenu;
		private System.Windows.Forms.ToolStripMenuItem openInMenu;
		private System.Windows.Forms.ToolStripMenuItem defaultAppMenu;
		private System.Windows.Forms.ToolStripMenuItem explorerMenu;
		private System.Windows.Forms.ToolStripMenuItem cmdMenu;
		private System.Windows.Forms.ToolStripMenuItem powershellMenu;
		private System.Windows.Forms.ToolStripMenuItem bashMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem createFileMenu;
		private System.Windows.Forms.ToolStripMenuItem createDirMenu;
		private System.Windows.Forms.ToolStripMenuItem additemMenu;
		private System.Windows.Forms.ToolStripMenuItem generateNewMenu;
		private System.Windows.Forms.ToolStripMenuItem fromSystemMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem cloneMenu;
		private System.Windows.Forms.ToolStripMenuItem copyMenu;
		private System.Windows.Forms.ToolStripMenuItem cutMenu;
		private System.Windows.Forms.ToolStripMenuItem pasteMenu;
		private System.Windows.Forms.ToolStripMenuItem removeMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteMenu;
		private System.Windows.Forms.ToolStripMenuItem renameMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem propertyMenu;
	}
}
