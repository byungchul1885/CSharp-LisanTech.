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

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] widthHeight = Properties.Settings.Default.WidthHeight.Split('*');
            Width = Int32.Parse(widthHeight[0].Trim());
            Height = Int32.Parse(widthHeight[1].Trim());

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

            tunnelLabelX0.Text = Properties.Settings.Default.ControllerName[0];
            tunnelLabelX1.Text = Properties.Settings.Default.ControllerName[1];
            tunnelLabelX2.Text = Properties.Settings.Default.ControllerName[2];
        }

        private void CloseProgramButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void ScreenSizeButton_Click(object sender, EventArgs e)
        {
            //if (sender is BunifuFlatButton button)
            //{
            //    button.Refresh();
            //    button.ResumeLayout();
            //    button.reset();
            //}

            using (var form = new FormResolutionDialog())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.CurrWidthHeight = Properties.Settings.Default.WidthHeight;                

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.WidthHeight = form.ReturnValue;

                    Debug.WriteLine($" form.ReturnValue {form.ReturnValue}");
                }
            }
        }

        private void ConnButton_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormConnDialog())
                {
                    form.StartPosition = FormStartPosition.CenterParent;

                    int buttonIndex = Int32.Parse(button.Name.Remove(0, "connButtonX".Length));

                    form.IP = Properties.Settings.Default.IP[buttonIndex];
                    form.SubMask = Properties.Settings.Default.SubMask[buttonIndex];
                    form.Gateway = Properties.Settings.Default.Gateway[buttonIndex];
                    form.Port = Properties.Settings.Default.Port[buttonIndex];
                    form.ControllerName = Properties.Settings.Default.ControllerName[buttonIndex];

                    //form.Loaded += test_loaded;

                    form.ShowDialog();

                    if (form.ButtonAction == "conn")
                    {
                        Properties.Settings.Default.IP[buttonIndex] = form.IP;
                        Properties.Settings.Default.SubMask[buttonIndex] = form.SubMask;
                        Properties.Settings.Default.Gateway[buttonIndex] = form.Gateway;
                        Properties.Settings.Default.Port[buttonIndex] = form.Port;
                    }
                    else if (form.ButtonAction == "close")
                    {

                    }
                }
            }
        }

        private void ControllerSetupButton_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormControllerSetupDiaglog())
                {
                    form.StartPosition = FormStartPosition.CenterParent;

                    //int buttonIndex = Int32.Parse(button.Name.Remove(0, "connButtonX".Length));

                    //form.IP = Properties.Settings.Default.IP[buttonIndex];
                    //form.SubMask = Properties.Settings.Default.SubMask[buttonIndex];
                    //form.Gateway = Properties.Settings.Default.Gateway[buttonIndex];
                    //form.Port = Properties.Settings.Default.Port[buttonIndex];
                    //form.ControllerName = Properties.Settings.Default.ControllerName[buttonIndex];

                    ////form.Loaded += test_loaded;

                    form.ShowDialog();

                    //if (form.ButtonAction == "conn")
                    //{
                    //    Properties.Settings.Default.IP[buttonIndex] = form.IP;
                    //    Properties.Settings.Default.SubMask[buttonIndex] = form.SubMask;
                    //    Properties.Settings.Default.Gateway[buttonIndex] = form.Gateway;
                    //    Properties.Settings.Default.Port[buttonIndex] = form.Port;
                    //}
                    //else if (form.ButtonAction == "close")
                    //{

                    //}
                }
            }
        }

        //public void test_loaded(object sender, EventArgs e)
        //{
        //    Debug.WriteLine($" test_loaded");
        //}
    }
}
