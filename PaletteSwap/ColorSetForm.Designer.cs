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
            this.spriteKeyBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spriteKeyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // spriteKeyBox
            // 
            this.spriteKeyBox.BackColor = System.Drawing.Color.Black;
            this.spriteKeyBox.Location = new System.Drawing.Point(49, 32);
            this.spriteKeyBox.Margin = new System.Windows.Forms.Padding(6);
            this.spriteKeyBox.Name = "spriteKeyBox";
            this.spriteKeyBox.Size = new System.Drawing.Size(90, 87);
            this.spriteKeyBox.TabIndex = 227;
            this.spriteKeyBox.TabStop = false;
            // 
            // ColorSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1768, 1377);
            this.Controls.Add(this.spriteKeyBox);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ColorSetForm";
            this.Text = "ColorSetForm";
            ((System.ComponentModel.ISupportInitialize)(this.spriteKeyBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox spriteKeyBox;
    }
}