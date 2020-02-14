using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace DimmingContol
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void closeBunifuImageButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // FHD
            //Width = 1920;
            //Height = 1080;

            //Width = 2048;
            //Height = 1152;

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

            string[] widthAndHeight = Properties.Settings.Default.WidthHeight.Split('*');
            Width = Int32.Parse(widthAndHeight[0].Trim());
            Height = Int32.Parse(widthAndHeight[1].Trim());

            float fontSizeMagnification;
            if (Width <= 1920)
            {
                fontSizeMagnification = 1.0F;
            }
            else if (Width <= 2048)
            {
                fontSizeMagnification = 1.03F;
            }
            else if (Width <= 2560)
            {
                fontSizeMagnification = 1.32F;
            }
            else if (Width <= 2880)
            {
                fontSizeMagnification = 1.52F;
            }
            else if (Width <= 3200)
            {
                fontSizeMagnification = 1.62F;
            }
            else if (Width <= 3840)
            {
                fontSizeMagnification = 1.68F;
            }
            else
            {
                fontSizeMagnification = 1.0F;
            }

            var c = GetAll(this, typeof(BunifuCustomLabel));
            foreach (var label in c)
            {
                if (label.Name != "titleLabel")
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size * fontSizeMagnification, FontStyle.Bold);
                }

                //if (label.BackColor == Color.FromArgb(32, 56, 100))
                //{
                //    label.Text = "";
                //}
            }

            //btnPanel00.Enabled = false;
            //bunifuFlatButton2.Enabled = false;

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                button.Refresh();
                button.ResumeLayout();
                button.reset();
            }

            using (var form = new FormResolutionDialog())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.CurrWidthHeight = Properties.Settings.Default.WidthHeight;                

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.WidthHeight = form.ReturnValue;
                    Properties.Settings.Default.Save();

                    Debug.WriteLine($" form.ReturnValue {form.ReturnValue}");
                }
            }
        }

        private void connButton00_Click(object sender, EventArgs e)
        {
            using (var form = new FormConnDialog())
            {
                form.StartPosition = FormStartPosition.CenterParent;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    //Properties.Settings.Default.WidthHeight = form.ReturnValue;
                    //Properties.Settings.Default.Save();

                    //Debug.WriteLine($" form.ReturnValue {form.ReturnValue}");
                }
            }
        }
    }
}
