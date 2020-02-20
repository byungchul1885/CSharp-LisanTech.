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
        public static event EventHandler DimmLevelValueReceivedFromController;
        public static event EventHandler MaintenanceFactorReceivedFromController;

        public FormMain()
        {
            InitializeComponent();

            FormInputDimmLevel.UserChangedDimmLevelValue += DimmLevelChagnedByUser;
            FormInputMaintenanceFactor.UserChangedMaintenanceFactor += MaintenanceFactorChagnedByUser;
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

            ascendingDirectionLabel.Text = Properties.Settings.Default.ascendingDirection;
            descendingDirectionLabel.Text = Properties.Settings.Default.descendingDirection;



            List<Bitmap> images = new List<Bitmap>();

            Bitmap finalImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("main");
            //channelPic.Image = (Image)O; //Set the Image property of channelPic to the returned object as Image

            //Bitmap bitmap0 = (Bitmap)Properties.Resources.ResourceManager.GetObject("main");
            //Bitmap bitmap1 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire10");
            Bitmap bitmap2 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire00");
            Bitmap bitmap3 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire01");
            Bitmap bitmap4 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire11");
            Bitmap bitmap5 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire02");
            Bitmap bitmap6 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire12");


            //images.Add((Bitmap)Properties.Resources.ResourceManager.GetObject("main"));
            images.Add((Bitmap)Properties.Resources.ResourceManager.GetObject("fire10"));
            //images.Add(bitmap2);
            //images.Add(bitmap3);
            //images.Add(bitmap4);
            //images.Add(bitmap5);
            //images.Add(bitmap6);


            using (Graphics g = Graphics.FromImage(finalImage))
            {
                foreach (Bitmap image in images)
                {
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                }
            }

            mainTLPanel.BackgroundImage = finalImage;

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

            using (var form = new FormInputWidthHeight())
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
                using (var form = new FormConn())
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
            string[] tempDim = new string[] {"",
                "100", "95", "90", "85", "80", "75", "70", "65", "60", "55", 
                "50", "45", "40", "35", "30", "25", "20", "15", "10", "5", 
                "0"};

            string[] tempMaintenanceFactor = new string[] {"",
                "55", "66", "77", "88", "100", "100"};

            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormControllerSetup())
                {
                    form.StartPosition = FormStartPosition.CenterParent;

                    form.ControllerIdx = Int32.Parse(button.Name.Remove(0, "controllerSetupButtonX".Length));
                    form.ControllerName = Properties.Settings.Default.ControllerName[form.ControllerIdx];

                    form.DimLevelValue.Clear();
                    form.DimLevelValue.AddRange(tempDim);

                    form.MaintenanceFactor.Clear();
                    form.MaintenanceFactor.AddRange(tempMaintenanceFactor);

                    form.ShowDialog();
                }
            }
        }

        private void DimmLevelChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputDimmLevel form)
            {
                DimmLevelValueReceivedFromController?.Invoke(form.DimLevelValue, e);
            }
        }

        private void MaintenanceFactorChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputMaintenanceFactor form)
            {
                MaintenanceFactorReceivedFromController?.Invoke(form.MaintenanceFactor, e);
            }
        }
    }
}
