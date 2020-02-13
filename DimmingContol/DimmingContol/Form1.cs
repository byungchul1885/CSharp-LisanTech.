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

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Width = 2800;
            //this.Height = 2800;

            mainPanel.Enabled = true;
            mainPanel.Visible = true;

            controlPanel.Enabled = false;
            controlPanel.Visible = false;

            mainBunifuFlatButton.selected = true;

            logoBunifuTransition.Hide(logoPictureBox);
            sidemenuPanel.Visible = false;
            sidemenuPanel.Width = 60;            
            panelBunifuTransition.ShowSync(sidemenuPanel);
        }

        private void closeBunifuImageButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuBunifuImageButton_Click(object sender, EventArgs e)
        {
            if (sidemenuPanel.Width == 60)
            {
                //Point p = new Point(1, 0);

                sidemenuPanel.Visible = false;
                sidemenuPanel.Width = 260;
                //panelBunifuTransition.DefaultAnimation.SlideCoeff= p;

                panelBunifuTransition.ShowSync(sidemenuPanel);
                panelBunifuTransition.ShowSync(logoPictureBox);
            }
            else
            {
                //Point p = new Point(-1, 0);

                logoBunifuTransition.Hide(logoPictureBox);
                sidemenuPanel.Visible = false;
                sidemenuPanel.Width = 60;

                //panelBunifuTransition.DefaultAnimation.SlideCoeff = p;
                panelBunifuTransition.ShowSync(sidemenuPanel);
            }
        }


        private void mainBunifuFlatButton_Click(object sender, EventArgs e)
        {
            mainPanel.Enabled = true;
            mainPanel.Visible = true;

            controlPanel.Enabled = false;
            controlPanel.Visible = false;
        }

        private void controlBunifuFlatButton_Click(object sender, EventArgs e)
        {
            mainPanel.Enabled = false;
            mainPanel.Visible = false;

            controlPanel.Enabled = true;
            controlPanel.Visible = true;
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
