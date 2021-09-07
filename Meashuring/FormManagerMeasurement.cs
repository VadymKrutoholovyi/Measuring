using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageProcessor.Meashuring
{
    public partial class FormManagerMeasurement : Form
    {
        public FormManagerMeasurement()
        {
            InitializeComponent();
        }

        private void cbPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPoint.Checked)
            {
                cbLine.Checked = false;
                cbCircle.Checked = false;
                cbRectangle.Checked = false;
            }
        }

        private void cbLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLine.Checked)
            {
                cbPoint.Checked = false;
                cbCircle.Checked = false;
                cbRectangle.Checked = false;
            }
        }

        private void cbCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCircle.Checked)
            {
                cbLine.Checked = false;
                cbPoint.Checked = false;
                cbRectangle.Checked = false;
            }
        }

        private void cbRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRectangle.Checked)
            {
                cbLine.Checked = false;
                cbCircle.Checked = false;
                cbPoint.Checked = false;
            }
        }

        private void lblShowProphiles_Click(object sender, EventArgs e)
        {
            if (this.Width == 230)
                this.Width = 1000;
            else
                this.Width = 230;
        }
    }
}