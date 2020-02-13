using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimmingContol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void closeBunifuImageButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // FHD
            //Width = 1920;
            //Height = 1080;

            Width = 2048;
            Height = 1152;

            // QHD
            //Width = 2560;
            //Height = 1440;

            //Width = 2880;
            //Height = 1620;

            //Width = 3200;
            //Height = 1800;

            // 4K UHDTV
            //Width = 3840;
            //Height = 2160;
        }
    }
}
