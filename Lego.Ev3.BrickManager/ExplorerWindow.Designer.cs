namespace Lego.Ev3.BrickManager
{
    partial class ExplorerWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitter = new System.Windows.Forms.Splitter();
            this.directoryContentPane = new Lego.Ev3.BrickManager.DirectoryContentPane();
            this.previewPane = new Lego.Ev3.BrickManager.PreviewPane();
            this.folderTree = new Lego.Ev3.BrickManager.FolderTree();
            this.statusBar = new Lego.Ev3.BrickManager.StatusBar();
            this.navigationBar = new Lego.Ev3.BrickManager.NavigationBar();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(187, 45);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(2, 506);
            this.splitter.TabIndex = 1;
            this.splitter.TabStop = false;
            // 
            // directoryContentPane
            // 
            this.directoryContentPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryContentPane.Location = new System.Drawing.Point(189, 45);
            this.directoryContentPane.Name = "directoryContentPane";
            this.directoryContentPane.Size = new System.Drawing.Size(667, 506);
            this.directoryContentPane.TabIndex = 2;
            // 
            // previewPane
            // 
            this.previewPane.BackColor = System.Drawing.SystemColors.Window;
            this.previewPane.Dock = System.Windows.Forms.DockStyle.Right;
            this.previewPane.Location = new System.Drawing.Point(886, 0);
            this.previewPane.Name = "previewPane";
            this.previewPane.Size = new System.Drawing.Size(174, 506);
            this.previewPane.TabIndex = 4;
            // 
            // folderTree
            // 
            this.folderTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.folderTree.Location = new System.Drawing.Point(0, 45);
            this.folderTree.Name = "folderTree";
            this.folderTree.Size = new System.Drawing.Size(187, 506);
            this.folderTree.TabIndex = 0;
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Location = new System.Drawing.Point(0, 551);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1060, 25);
            this.statusBar.TabIndex = 3;
            // 
            // navigationBar
            // 
            this.navigationBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.navigationBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationBar.Location = new System.Drawing.Point(0, 0);
            this.navigationBar.Name = "navigationBar";
            this.navigationBar.Size = new System.Drawing.Size(1060, 45);
            this.navigationBar.TabIndex = 5;
            // 
            // ExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.directoryContentPane);
            this.Controls.Add(this.previewPane);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.folderTree);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.navigationBar);
            this.Name = "ExplorerWindow";
            this.Size = new System.Drawing.Size(1060, 576);
            this.ResumeLayout(false);

        }

        #endregion

        private FolderTree folderTree;
        private System.Windows.Forms.Splitter splitter;
        private DirectoryContentPane directoryContentPane;
        private PreviewPane previewPane;
        private StatusBar statusBar;
        private NavigationBar navigationBar;
    }
}
