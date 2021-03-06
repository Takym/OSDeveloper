﻿namespace OSDeveloper.GUIs.Explorer
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
			this.SaveSolutions();
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
			this.btnNewSln = new System.Windows.Forms.ToolStripButton();
			this.treeView = new System.Windows.Forms.TreeView();
			this.iconList = new System.Windows.Forms.ImageList(this.components);
			this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openInMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.defaultAppMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.explorerMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.powershellMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bashMenu = new OSDeveloper.GUIs.Explorer.WslBashToolStripMenuItem();
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
            this.btnCollapse,
            this.btnNewSln});
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
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
			this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
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
			this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
			// 
			// btnNewSln
			// 
			this.btnNewSln.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnNewSln.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSln.Image")));
			this.btnNewSln.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNewSln.Name = "btnNewSln";
			this.btnNewSln.Size = new System.Drawing.Size(69, 22);
			this.btnNewSln.Text = "btnNewSln";
			this.btnNewSln.Click += new System.EventHandler(this.btnNewSln_Click);
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.HotTracking = true;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.iconList;
			this.treeView.LabelEdit = true;
			this.treeView.Location = new System.Drawing.Point(0, 25);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(196, 171);
			this.treeView.TabIndex = 1;
			this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
			this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
			this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
			this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeSelect);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
			this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseClick);
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
			this.openMenu.Size = new System.Drawing.Size(180, 22);
			this.openMenu.Text = "open";
			this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
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
			this.defaultAppMenu.Click += new System.EventHandler(this.defaultAppMenu_Click);
			// 
			// explorerMenu
			// 
			this.explorerMenu.Name = "explorerMenu";
			this.explorerMenu.Size = new System.Drawing.Size(180, 22);
			this.explorerMenu.Text = "explorer";
			this.explorerMenu.Click += new System.EventHandler(this.explorerMenu_Click);
			// 
			// cmdMenu
			// 
			this.cmdMenu.Name = "cmdMenu";
			this.cmdMenu.Size = new System.Drawing.Size(180, 22);
			this.cmdMenu.Text = "cmd";
			this.cmdMenu.Click += new System.EventHandler(this.cmdMenu_Click);
			// 
			// powershellMenu
			// 
			this.powershellMenu.Name = "powershellMenu";
			this.powershellMenu.Size = new System.Drawing.Size(180, 22);
			this.powershellMenu.Text = "powershell";
			this.powershellMenu.Click += new System.EventHandler(this.powershellMenu_Click);
			// 
			// bashMenu
			// 
			this.bashMenu.Name = "bashMenu";
			this.bashMenu.Size = new System.Drawing.Size(180, 22);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// createFileMenu
			// 
			this.createFileMenu.Name = "createFileMenu";
			this.createFileMenu.Size = new System.Drawing.Size(180, 22);
			this.createFileMenu.Text = "createFile";
			this.createFileMenu.Click += new System.EventHandler(this.createMenu_Click);
			// 
			// createDirMenu
			// 
			this.createDirMenu.Name = "createDirMenu";
			this.createDirMenu.Size = new System.Drawing.Size(180, 22);
			this.createDirMenu.Text = "createDir";
			this.createDirMenu.Click += new System.EventHandler(this.createMenu_Click);
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
			this.generateNewMenu.Size = new System.Drawing.Size(144, 22);
			this.generateNewMenu.Text = "generateNew";
			this.generateNewMenu.Click += new System.EventHandler(this.generateNewMenu_Click);
			// 
			// fromSystemMenu
			// 
			this.fromSystemMenu.Name = "fromSystemMenu";
			this.fromSystemMenu.Size = new System.Drawing.Size(144, 22);
			this.fromSystemMenu.Text = "fromSystem";
			this.fromSystemMenu.Click += new System.EventHandler(this.fromSystemMenu_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// cloneMenu
			// 
			this.cloneMenu.Name = "cloneMenu";
			this.cloneMenu.Size = new System.Drawing.Size(180, 22);
			this.cloneMenu.Text = "clone";
			this.cloneMenu.Click += new System.EventHandler(this.cloneMenu_Click);
			// 
			// copyMenu
			// 
			this.copyMenu.Name = "copyMenu";
			this.copyMenu.Size = new System.Drawing.Size(180, 22);
			this.copyMenu.Text = "copy";
			this.copyMenu.Click += new System.EventHandler(this.copyMenu_Click);
			// 
			// cutMenu
			// 
			this.cutMenu.Enabled = false;
			this.cutMenu.Name = "cutMenu";
			this.cutMenu.Size = new System.Drawing.Size(180, 22);
			this.cutMenu.Text = "cut";
			this.cutMenu.Visible = false;
			this.cutMenu.Click += new System.EventHandler(this.cutMenu_Click);
			// 
			// pasteMenu
			// 
			this.pasteMenu.Name = "pasteMenu";
			this.pasteMenu.Size = new System.Drawing.Size(180, 22);
			this.pasteMenu.Text = "paste";
			this.pasteMenu.Click += new System.EventHandler(this.pasteMenu_Click);
			// 
			// removeMenu
			// 
			this.removeMenu.Name = "removeMenu";
			this.removeMenu.Size = new System.Drawing.Size(180, 22);
			this.removeMenu.Text = "remove";
			this.removeMenu.Click += new System.EventHandler(this.removeMenu_Click);
			// 
			// deleteMenu
			// 
			this.deleteMenu.Name = "deleteMenu";
			this.deleteMenu.Size = new System.Drawing.Size(180, 22);
			this.deleteMenu.Text = "delete";
			this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// renameMenu
			// 
			this.renameMenu.Name = "renameMenu";
			this.renameMenu.Size = new System.Drawing.Size(180, 22);
			this.renameMenu.Text = "rename";
			this.renameMenu.Click += new System.EventHandler(this.renameMenu_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			// 
			// propertyMenu
			// 
			this.propertyMenu.Name = "propertyMenu";
			this.propertyMenu.Size = new System.Drawing.Size(180, 22);
			this.propertyMenu.Text = "property";
			this.propertyMenu.Click += new System.EventHandler(this.propertyMenu_Click);
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
		private System.Windows.Forms.ImageList iconList;
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
		private WslBashToolStripMenuItem bashMenu;
		private System.Windows.Forms.ToolStripButton btnNewSln;
	}
}
