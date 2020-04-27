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
            this.portraitKeyBox = new System.Windows.Forms.PictureBox();
            this.spriteKeyBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.portraitKeyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteKeyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // portraitKeyBox
            // 
            this.portraitKeyBox.BackColor = System.Drawing.Color.Black;
            this.portraitKeyBox.Location = new System.Drawing.Point(24, 17);
            this.portraitKeyBox.Name = "portraitKeyBox";
            this.portraitKeyBox.Size = new System.Drawing.Size(88, 234);
            this.portraitKeyBox.TabIndex = 227;
            this.portraitKeyBox.TabStop = false;
            // 
            // spriteKeyBox
            // 
            this.spriteKeyBox.BackColor = System.Drawing.Color.Black;
            this.spriteKeyBox.Location = new System.Drawing.Point(24, 257);
            this.spriteKeyBox.Name = "spriteKeyBox";
            this.spriteKeyBox.Size = new System.Drawing.Size(45, 45);
            this.spriteKeyBox.TabIndex = 228;
            this.spriteKeyBox.TabStop = false;
            // 
            // ColorSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 537);
            this.Controls.Add(this.spriteKeyBox);
            this.Controls.Add(this.portraitKeyBox);
            this.Name = "ColorSetForm";
            this.Text = "ColorSetForm";
            ((System.ComponentModel.ISupportInitialize)(this.portraitKeyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteKeyBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox portraitKeyBox;
        private System.Windows.Forms.PictureBox spriteKeyBox;
    }
}