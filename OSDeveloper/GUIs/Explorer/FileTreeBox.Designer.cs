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
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.btnRefresh = new System.Windows.Forms.ToolStripButton();
			this.btnExpand = new System.Windows.Forms.ToolStripButton();
			this.btnCollapse = new System.Windows.Forms.ToolStripButton();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.createFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.additemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.explorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.powershellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fromSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.openToolStripMenuItem,
            this.openInToolStripMenuItem,
            this.toolStripMenuItem1,
            this.createFileToolStripMenuItem,
            this.createDirToolStripMenuItem,
            this.additemToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cloneToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripMenuItem3,
            this.propertyToolStripMenuItem});
			this.popupMenu.Name = "popupMenu";
			this.popupMenu.Size = new System.Drawing.Size(125, 308);
			// 
			// ofd
			// 
			this.ofd.ShowReadOnly = true;
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
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.openToolStripMenuItem.Text = "open";
			// 
			// openInToolStripMenuItem
			// 
			this.openInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultAppToolStripMenuItem,
            this.explorerToolStripMenuItem,
            this.cmdToolStripMenuItem,
            this.powershellToolStripMenuItem,
            this.bashToolStripMenuItem});
			this.openInToolStripMenuItem.Name = "openInToolStripMenuItem";
			this.openInToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.openInToolStripMenuItem.Text = "openIn";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
			// 
			// createFileToolStripMenuItem
			// 
			this.createFileToolStripMenuItem.Name = "createFileToolStripMenuItem";
			this.createFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.createFileToolStripMenuItem.Text = "createFile";
			// 
			// createDirToolStripMenuItem
			// 
			this.createDirToolStripMenuItem.Name = "createDirToolStripMenuItem";
			this.createDirToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.createDirToolStripMenuItem.Text = "createDir";
			// 
			// additemToolStripMenuItem
			// 
			this.additemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateNewToolStripMenuItem,
            this.fromSystemToolStripMenuItem});
			this.additemToolStripMenuItem.Name = "additemToolStripMenuItem";
			this.additemToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.additemToolStripMenuItem.Text = "additem";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
			// 
			// cloneToolStripMenuItem
			// 
			this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
			this.cloneToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.cloneToolStripMenuItem.Text = "clone";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.copyToolStripMenuItem.Text = "copy";
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.cutToolStripMenuItem.Text = "cut";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.pasteToolStripMenuItem.Text = "paste";
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.removeToolStripMenuItem.Text = "remove";
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.deleteToolStripMenuItem.Text = "delete";
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.renameToolStripMenuItem.Text = "rename";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(121, 6);
			// 
			// propertyToolStripMenuItem
			// 
			this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
			this.propertyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.propertyToolStripMenuItem.Text = "property";
			// 
			// defaultAppToolStripMenuItem
			// 
			this.defaultAppToolStripMenuItem.Name = "defaultAppToolStripMenuItem";
			this.defaultAppToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.defaultAppToolStripMenuItem.Text = "defaultApp";
			// 
			// explorerToolStripMenuItem
			// 
			this.explorerToolStripMenuItem.Name = "explorerToolStripMenuItem";
			this.explorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.explorerToolStripMenuItem.Text = "explorer";
			// 
			// cmdToolStripMenuItem
			// 
			this.cmdToolStripMenuItem.Name = "cmdToolStripMenuItem";
			this.cmdToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.cmdToolStripMenuItem.Text = "cmd";
			// 
			// powershellToolStripMenuItem
			// 
			this.powershellToolStripMenuItem.Name = "powershellToolStripMenuItem";
			this.powershellToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.powershellToolStripMenuItem.Text = "powershell";
			// 
			// bashToolStripMenuItem
			// 
			this.bashToolStripMenuItem.Name = "bashToolStripMenuItem";
			this.bashToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.bashToolStripMenuItem.Text = "bash";
			// 
			// generateNewToolStripMenuItem
			// 
			this.generateNewToolStripMenuItem.Name = "generateNewToolStripMenuItem";
			this.generateNewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.generateNewToolStripMenuItem.Text = "generateNew";
			// 
			// fromSystemToolStripMenuItem
			// 
			this.fromSystemToolStripMenuItem.Name = "fromSystemToolStripMenuItem";
			this.fromSystemToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.fromSystemToolStripMenuItem.Text = "fromSystem";
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
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem defaultAppToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem explorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cmdToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem powershellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bashToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem createFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createDirToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem additemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fromSystemToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
	}
}
