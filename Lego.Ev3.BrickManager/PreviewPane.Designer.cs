namespace Lego.Ev3.BrickManager
{
    partial class PreviewPane
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
            this.icon = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.previewImage = new System.Windows.Forms.PictureBox();
            this.previewContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.Location = new System.Drawing.Point(21, 57);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(64, 64);
            this.icon.TabIndex = 16;
            this.icon.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelName.Location = new System.Drawing.Point(18, 13);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 19);
            this.labelName.TabIndex = 17;
            this.labelName.Text = "Name";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelType.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelType.Location = new System.Drawing.Point(18, 32);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(31, 14);
            this.labelType.TabIndex = 18;
            this.labelType.Text = "Type";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInfo.Font = new System.Drawing.Font("Calibri", 9F);
            this.textBoxInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBoxInfo.Location = new System.Drawing.Point(18, 134);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(140, 156);
            this.textBoxInfo.TabIndex = 20;
            this.textBoxInfo.Text = "info";
            // 
            // previewImage
            // 
            this.previewImage.Location = new System.Drawing.Point(-2, 255);
            this.previewImage.Name = "previewImage";
            this.previewImage.Size = new System.Drawing.Size(178, 128);
            this.previewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previewImage.TabIndex = 22;
            this.previewImage.TabStop = false;
            // 
            // previewContent
            // 
            this.previewContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.previewContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewContent.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewContent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.previewContent.Location = new System.Drawing.Point(18, 310);
            this.previewContent.Multiline = true;
            this.previewContent.Name = "previewContent";
            this.previewContent.Size = new System.Drawing.Size(140, 167);
            this.previewContent.TabIndex = 23;
            this.previewContent.Text = "content";
            // 
            // PreviewPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.previewContent);
            this.Controls.Add(this.previewImage);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.icon);
            this.Name = "PreviewPane";
            this.Size = new System.Drawing.Size(174, 506);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.PictureBox previewImage;
        private System.Windows.Forms.TextBox previewContent;
    }
}
