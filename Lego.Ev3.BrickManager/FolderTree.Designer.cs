﻿namespace Lego.Ev3.BrickManager
{
    partial class FolderTree
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
            this.tree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(235, 361);
            this.tree.TabIndex = 0;
            this.tree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tree_MouseDown);
            // 
            // FolderTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tree);
            this.Name = "FolderTree";
            this.Size = new System.Drawing.Size(235, 361);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
    }
}
