namespace Lego.Ev3.BrickManager
{
    partial class TextFileEditor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextFileEditor));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textFileEditorLineNumbers = new Lego.Ev3.BrickManager.TextFileEditorLineNumbers();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 355);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(583, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(583, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.Location = new System.Drawing.Point(15, 64);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(568, 288);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(70, 38);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(207, 20);
            this.textBoxFileName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Filename:";
            // 
            // textFileEditorLineNumbers
            // 
            this.textFileEditorLineNumbers._SeeThroughMode_ = false;
            this.textFileEditorLineNumbers.AutoSizing = true;
            this.textFileEditorLineNumbers.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textFileEditorLineNumbers.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textFileEditorLineNumbers.BackgroundGradient_BetaColor = System.Drawing.Color.Transparent;
            this.textFileEditorLineNumbers.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.textFileEditorLineNumbers.BorderLines_Color = System.Drawing.SystemColors.ActiveCaption;
            this.textFileEditorLineNumbers.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.textFileEditorLineNumbers.BorderLines_Thickness = 1F;
            this.textFileEditorLineNumbers.DockSide = Lego.Ev3.BrickManager.TextFileEditorLineNumbers.LineNumberDockSide.Left;
            this.textFileEditorLineNumbers.GridLines_Color = System.Drawing.Color.SlateGray;
            this.textFileEditorLineNumbers.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.textFileEditorLineNumbers.GridLines_Thickness = 1F;
            this.textFileEditorLineNumbers.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.textFileEditorLineNumbers.LineNrs_AntiAlias = true;
            this.textFileEditorLineNumbers.LineNrs_AsHexadecimal = false;
            this.textFileEditorLineNumbers.LineNrs_ClippedByItemRectangle = true;
            this.textFileEditorLineNumbers.LineNrs_LeadingZeroes = true;
            this.textFileEditorLineNumbers.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.textFileEditorLineNumbers.Location = new System.Drawing.Point(-3, 64);
            this.textFileEditorLineNumbers.Margin = new System.Windows.Forms.Padding(0);
            this.textFileEditorLineNumbers.MarginLines_Color = System.Drawing.SystemColors.ActiveCaption;
            this.textFileEditorLineNumbers.MarginLines_Side = Lego.Ev3.BrickManager.TextFileEditorLineNumbers.LineNumberDockSide.Right;
            this.textFileEditorLineNumbers.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.textFileEditorLineNumbers.MarginLines_Thickness = 1F;
            this.textFileEditorLineNumbers.Name = "textFileEditorLineNumbers";
            this.textFileEditorLineNumbers.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.textFileEditorLineNumbers.ParentRichTextBox = this.richTextBox;
            this.textFileEditorLineNumbers.Show_BackgroundGradient = true;
            this.textFileEditorLineNumbers.Show_BorderLines = true;
            this.textFileEditorLineNumbers.Show_GridLines = true;
            this.textFileEditorLineNumbers.Show_LineNrs = true;
            this.textFileEditorLineNumbers.Show_MarginLines = true;
            this.textFileEditorLineNumbers.Size = new System.Drawing.Size(17, 288);
            this.textFileEditorLineNumbers.TabIndex = 4;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // TextFileEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(583, 377);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.textFileEditorLineNumbers);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextFileEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Robot Text File (*.rtf)";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextFileEditor_FormClosing);
            this.Load += new System.EventHandler(this.TextFileEditor_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox;
        private TextFileEditorLineNumbers textFileEditorLineNumbers;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}