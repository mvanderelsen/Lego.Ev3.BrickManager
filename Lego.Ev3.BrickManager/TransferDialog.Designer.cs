namespace Lego.Ev3.BrickManager
{
    partial class TransferDialog
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
            this.labelAction = new System.Windows.Forms.Label();
            this.labelContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.BackColor = System.Drawing.Color.Transparent;
            this.labelAction.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAction.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelAction.Location = new System.Drawing.Point(12, 23);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(42, 14);
            this.labelAction.TabIndex = 12;
            this.labelAction.Text = "label2";
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.BackColor = System.Drawing.Color.Transparent;
            this.labelContent.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelContent.Location = new System.Drawing.Point(12, 49);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(42, 14);
            this.labelContent.TabIndex = 13;
            this.labelContent.Text = "label2";
            // 
            // TransferDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lego.Ev3.BrickManager.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(349, 161);
            this.ControlBox = false;
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.labelAction);
            this.Name = "TransferDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.Label labelContent;
    }
}