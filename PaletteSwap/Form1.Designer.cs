﻿namespace PaletteSwap
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.portraitBox = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loadACT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pal_sprite_skin1 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_skin2 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_skin3 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_skin4 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_pads1 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_pads2 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_pads3 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_pads4 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_pads5 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_cost1 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_cost2 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_cost3 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_cost4 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_cost5 = new System.Windows.Forms.PictureBox();
            this.pal_sprite_stripe1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_stripe1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "load palette";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 91);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(308, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "swap B & G";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // portraitBox
            // 
            this.portraitBox.Location = new System.Drawing.Point(633, 41);
            this.portraitBox.Name = "portraitBox";
            this.portraitBox.Size = new System.Drawing.Size(100, 102);
            this.portraitBox.TabIndex = 3;
            this.portraitBox.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(187, 41);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(412, 364);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "jab",
            "strong",
            "fierce",
            "short",
            "forward",
            "roundhouse",
            "start",
            "hold"});
            this.comboBox1.Location = new System.Drawing.Point(12, 267);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(12, 330);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "New color (ACT)";
            // 
            // loadACT
            // 
            this.loadACT.Location = new System.Drawing.Point(12, 356);
            this.loadACT.Name = "loadACT";
            this.loadACT.Size = new System.Drawing.Size(75, 23);
            this.loadACT.TabIndex = 8;
            this.loadACT.Text = "load";
            this.loadACT.UseVisualStyleBackColor = true;
            this.loadACT.Click += new System.EventHandler(this.loadACT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "skin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 489);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "pads";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 518);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "costume";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 549);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "stripe";
            // 
            // pal_sprite_skin1
            // 
            this.pal_sprite_skin1.Location = new System.Drawing.Point(73, 446);
            this.pal_sprite_skin1.Name = "pal_sprite_skin1";
            this.pal_sprite_skin1.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_skin1.TabIndex = 13;
            this.pal_sprite_skin1.TabStop = false;
            // 
            // pal_sprite_skin2
            // 
            this.pal_sprite_skin2.Location = new System.Drawing.Point(117, 446);
            this.pal_sprite_skin2.Name = "pal_sprite_skin2";
            this.pal_sprite_skin2.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_skin2.TabIndex = 14;
            this.pal_sprite_skin2.TabStop = false;
            // 
            // pal_sprite_skin3
            // 
            this.pal_sprite_skin3.Location = new System.Drawing.Point(159, 446);
            this.pal_sprite_skin3.Name = "pal_sprite_skin3";
            this.pal_sprite_skin3.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_skin3.TabIndex = 15;
            this.pal_sprite_skin3.TabStop = false;
            // 
            // pal_sprite_skin4
            // 
            this.pal_sprite_skin4.Location = new System.Drawing.Point(203, 446);
            this.pal_sprite_skin4.Name = "pal_sprite_skin4";
            this.pal_sprite_skin4.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_skin4.TabIndex = 16;
            this.pal_sprite_skin4.TabStop = false;
            // 
            // pal_sprite_pads1
            // 
            this.pal_sprite_pads1.Location = new System.Drawing.Point(73, 477);
            this.pal_sprite_pads1.Name = "pal_sprite_pads1";
            this.pal_sprite_pads1.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_pads1.TabIndex = 17;
            this.pal_sprite_pads1.TabStop = false;
            // 
            // pal_sprite_pads2
            // 
            this.pal_sprite_pads2.Location = new System.Drawing.Point(117, 477);
            this.pal_sprite_pads2.Name = "pal_sprite_pads2";
            this.pal_sprite_pads2.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_pads2.TabIndex = 18;
            this.pal_sprite_pads2.TabStop = false;
            // 
            // pal_sprite_pads3
            // 
            this.pal_sprite_pads3.Location = new System.Drawing.Point(159, 477);
            this.pal_sprite_pads3.Name = "pal_sprite_pads3";
            this.pal_sprite_pads3.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_pads3.TabIndex = 19;
            this.pal_sprite_pads3.TabStop = false;
            // 
            // pal_sprite_pads4
            // 
            this.pal_sprite_pads4.Location = new System.Drawing.Point(203, 477);
            this.pal_sprite_pads4.Name = "pal_sprite_pads4";
            this.pal_sprite_pads4.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_pads4.TabIndex = 20;
            this.pal_sprite_pads4.TabStop = false;
            // 
            // pal_sprite_pads5
            // 
            this.pal_sprite_pads5.Location = new System.Drawing.Point(244, 477);
            this.pal_sprite_pads5.Name = "pal_sprite_pads5";
            this.pal_sprite_pads5.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_pads5.TabIndex = 21;
            this.pal_sprite_pads5.TabStop = false;
            // 
            // pal_sprite_cost1
            // 
            this.pal_sprite_cost1.Location = new System.Drawing.Point(73, 506);
            this.pal_sprite_cost1.Name = "pal_sprite_cost1";
            this.pal_sprite_cost1.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_cost1.TabIndex = 22;
            this.pal_sprite_cost1.TabStop = false;
            // 
            // pal_sprite_cost2
            // 
            this.pal_sprite_cost2.Location = new System.Drawing.Point(117, 508);
            this.pal_sprite_cost2.Name = "pal_sprite_cost2";
            this.pal_sprite_cost2.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_cost2.TabIndex = 23;
            this.pal_sprite_cost2.TabStop = false;
            // 
            // pal_sprite_cost3
            // 
            this.pal_sprite_cost3.Location = new System.Drawing.Point(159, 508);
            this.pal_sprite_cost3.Name = "pal_sprite_cost3";
            this.pal_sprite_cost3.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_cost3.TabIndex = 24;
            this.pal_sprite_cost3.TabStop = false;
            // 
            // pal_sprite_cost4
            // 
            this.pal_sprite_cost4.Location = new System.Drawing.Point(203, 508);
            this.pal_sprite_cost4.Name = "pal_sprite_cost4";
            this.pal_sprite_cost4.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_cost4.TabIndex = 25;
            this.pal_sprite_cost4.TabStop = false;
            // 
            // pal_sprite_cost5
            // 
            this.pal_sprite_cost5.Location = new System.Drawing.Point(244, 508);
            this.pal_sprite_cost5.Name = "pal_sprite_cost5";
            this.pal_sprite_cost5.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_cost5.TabIndex = 26;
            this.pal_sprite_cost5.TabStop = false;
            // 
            // pal_sprite_stripe1
            // 
            this.pal_sprite_stripe1.Location = new System.Drawing.Point(73, 537);
            this.pal_sprite_stripe1.Name = "pal_sprite_stripe1";
            this.pal_sprite_stripe1.Size = new System.Drawing.Size(25, 25);
            this.pal_sprite_stripe1.TabIndex = 27;
            this.pal_sprite_stripe1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 660);
            this.Controls.Add(this.pal_sprite_stripe1);
            this.Controls.Add(this.pal_sprite_cost5);
            this.Controls.Add(this.pal_sprite_cost4);
            this.Controls.Add(this.pal_sprite_cost3);
            this.Controls.Add(this.pal_sprite_cost2);
            this.Controls.Add(this.pal_sprite_cost1);
            this.Controls.Add(this.pal_sprite_pads5);
            this.Controls.Add(this.pal_sprite_pads4);
            this.Controls.Add(this.pal_sprite_pads3);
            this.Controls.Add(this.pal_sprite_pads2);
            this.Controls.Add(this.pal_sprite_pads1);
            this.Controls.Add(this.pal_sprite_skin4);
            this.Controls.Add(this.pal_sprite_skin3);
            this.Controls.Add(this.pal_sprite_skin2);
            this.Controls.Add(this.pal_sprite_skin1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadACT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.portraitBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_skin4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_pads5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_cost5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pal_sprite_stripe1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox portraitBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loadACT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pal_sprite_skin1;
        private System.Windows.Forms.PictureBox pal_sprite_skin2;
        private System.Windows.Forms.PictureBox pal_sprite_skin3;
        private System.Windows.Forms.PictureBox pal_sprite_skin4;
        private System.Windows.Forms.PictureBox pal_sprite_pads1;
        private System.Windows.Forms.PictureBox pal_sprite_pads2;
        private System.Windows.Forms.PictureBox pal_sprite_pads3;
        private System.Windows.Forms.PictureBox pal_sprite_pads4;
        private System.Windows.Forms.PictureBox pal_sprite_pads5;
        private System.Windows.Forms.PictureBox pal_sprite_cost1;
        private System.Windows.Forms.PictureBox pal_sprite_cost2;
        private System.Windows.Forms.PictureBox pal_sprite_cost3;
        private System.Windows.Forms.PictureBox pal_sprite_cost4;
        private System.Windows.Forms.PictureBox pal_sprite_cost5;
        private System.Windows.Forms.PictureBox pal_sprite_stripe1;
    }
}

