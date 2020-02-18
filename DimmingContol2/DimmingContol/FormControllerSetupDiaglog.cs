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

                foreach (Control c in dimmLevelPanel.Controls)
                {
                    if (c.GetType() == typeof(BunifuCustomLabel) 
                        && c.Name.Contains("dimmLevelLabel"))
                    {
                        int levelIndex = Int32.Parse(c.Name.Remove(0, "dimmLevelLabel".Length));
                        form.DimLevel[levelIndex] = c.Text;
                    }
                }

                form.ShowDialog();
            }            
        }

        private void MaintenanceFactorSetup_Click(object sender, EventArgs e)
        {

        }
    }
}
