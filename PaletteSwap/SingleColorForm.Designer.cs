namespace PaletteSwap
{
    partial class SingleColorForm
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
            this.singleColorBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.singleColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // singleColorBox
            // 
            this.singleColorBox.BackColor = System.Drawing.Color.Black;
            this.singleColorBox.Location = new System.Drawing.Point(27, 25);
            this.singleColorBox.Name = "singleColorBox";
            this.singleColorBox.Size = new System.Drawing.Size(100, 50);
            this.singleColorBox.TabIndex = 0;
            this.singleColorBox.TabStop = false;
            // 
            // SingleColorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 174);
            this.Controls.Add(this.singleColorBox);
            this.Name = "SingleColorForm";
            this.Text = "SingleColorForm";
            ((System.ComponentModel.ISupportInitialize)(this.singleColorBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox singleColorBox;
    }
}