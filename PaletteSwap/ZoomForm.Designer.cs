namespace PaletteSwap
{
    partial class ZoomForm
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
            this.zoomBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBox)).BeginInit();
            this.SuspendLayout();
            // 
            // zoomBox
            // 
            this.zoomBox.BackColor = System.Drawing.Color.Black;
            this.zoomBox.Location = new System.Drawing.Point(12, 12);
            this.zoomBox.Name = "zoomBox";
            this.zoomBox.Size = new System.Drawing.Size(393, 346);
            this.zoomBox.TabIndex = 0;
            this.zoomBox.TabStop = false;
            this.zoomBox.Click += new System.EventHandler(this.zoomBox_Click);
            // 
            // ZoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.zoomBox);
            this.Name = "ZoomForm";
            this.Text = "ZoomForm";
            ((System.ComponentModel.ISupportInitialize)(this.zoomBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox zoomBox;
    }
}