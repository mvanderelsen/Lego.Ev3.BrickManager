namespace Lego.Ev3.BrickManager
{
    partial class NavigationBar
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
            this.panel = new System.Windows.Forms.Panel();
            this.bar = new System.Windows.Forms.ToolStrip();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.bar);
            this.panel.Location = new System.Drawing.Point(190, 11);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(791, 26);
            this.panel.TabIndex = 3;
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.SystemColors.Window;
            this.bar.Dock = System.Windows.Forms.DockStyle.None;
            this.bar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bar.Location = new System.Drawing.Point(1, 0);
            this.bar.Name = "bar";
            this.bar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bar.Size = new System.Drawing.Size(102, 25);
            this.bar.TabIndex = 1;
            this.bar.Text = "toolStrip1";
            // 
            // NavigationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.panel);
            this.Name = "NavigationBar";
            this.Size = new System.Drawing.Size(1142, 51);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStrip bar;
    }
}
