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
    public partial class FormControllerSetupDiaglog : Form
    {
        public FormControllerSetupDiaglog()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DimmLevelSetup_Click(object sender, EventArgs e)
        {
            using (var form = new FormDimmLevelSetup())
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

        private void MaintenanceFactorSetup_Click(object sender, EventArgs e)
        {

        }
    }
}
