﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaletteSwap
{
    public partial class SingleColorForm : Form
    {
        MainForm mainform;

        public SingleColorForm(MainForm parentform)
        {
            InitializeComponent();
            mainform = parentform;
            Reload();
        }

        public void Reload()
        {
            var b = mainform.currentCharacter.GetPreviewBitmap();
            singleColorBox.BackgroundImageLayout = ImageLayout.None;
            singleColorBox.Height = b.Height;
            singleColorBox.Width = b.Width;
            singleColorBox.BackgroundImage = b;

            setBackColor();
        }

        public SingleColorForm()
        {
            InitializeComponent();
        }

        public void setBackColor()
        {
            singleColorBox.BackColor = mainform.backgroundcolor;
        }

        private void SingleColorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Bitmap bmp = new Bitmap(singleColorBox.BackgroundImage);
                Clipboard.SetData(System.Windows.Forms.DataFormats.Bitmap, bmp);
            }
        }
    }
}
