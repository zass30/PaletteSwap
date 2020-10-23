namespace PaletteSwap
{
    partial class ColorSetForm
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
            this.combinedKeyBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.combinedKeyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // combinedKeyBox
            // 
            this.combinedKeyBox.BackColor = System.Drawing.Color.Black;
            this.combinedKeyBox.Location = new System.Drawing.Point(24, 17);
            this.combinedKeyBox.Name = "combinedKeyBox";
            this.combinedKeyBox.Size = new System.Drawing.Size(88, 234);
            this.combinedKeyBox.TabIndex = 227;
            this.combinedKeyBox.TabStop = false;
            // 
            // ColorSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 537);
            this.Controls.Add(this.combinedKeyBox);
            this.KeyPreview = true;
            this.Name = "ColorSetForm";
            this.Text = "ColorSetForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColorSetForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.combinedKeyBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox combinedKeyBox;
    }
}