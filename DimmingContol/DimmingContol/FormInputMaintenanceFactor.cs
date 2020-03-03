using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace DimmingContol
{
    public partial class FormInputMaintenanceFactor : Form
    {
        public static event EventHandler UserChangedMaintenanceFactor;

        public List<string> MaintenanceFactor { get; set; }
        public string ControllerName { get; set; }
        public int ControllerIdx { get; set; }


        public FormInputMaintenanceFactor()
        {
            InitializeComponent();

            MaintenanceFactor = new List<string>();

            FormMain.DisconnectedFormController += Disconnected;
        }

        private void FormInputMaintenanceFactor_Load(object sender, EventArgs e)
        {
            foreach (Control c in maintenanceFactorPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("maintenanceFactorTextBox"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "maintenanceFactorTextBox".Length));
                    c.Text = MaintenanceFactor[levelIndex];
                }
            }

            titleLabel.Text = ControllerName + " 제어기 보수율 설정";
            inputValidation.Visible = false;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            MaintenanceFactor.Clear();
            MaintenanceFactor.AddRange(new string[maintenanceFactorPanel.ColumnCount + 1]);

            foreach (Control c in maintenanceFactorPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("maintenanceFactorTextBox"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "maintenanceFactorTextBox".Length));
                    MaintenanceFactor[levelIndex] = c.Text;
                }
            }

            MaintenanceFactor[0] = ControllerIdx.ToString();

            UserChangedMaintenanceFactor?.Invoke(this, e);

            Close();
        }


        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MaintenanceFactor_OnValueChanged(object sender, EventArgs e)
        {
            if (sender is BunifuMaterialTextbox tb)
            {
                var isNumeric = int.TryParse(tb.Text, out int n);
                if (!isNumeric || n > 100 || n < 0)
                {
                    inputValidation.Visible = true;
                    EnableTextBox(false);
                    tb.Enabled = true;
                    applyButton.Enabled = false;
                    tb.Focus();

                    if (!isNumeric)
                    {
                        inputValidation.Text = "숫자가 아닙니다";
                    }
                    else
                    {
                        inputValidation.Text = "입력범위를 벗어났습니다 (0 ~ 100)";
                    }
                }
                else
                {
                    inputValidation.Visible = false;
                    EnableTextBox(true);
                    applyButton.Enabled = true;
                }
            }
        }

        private void EnableTextBox(bool ok)
        {
            foreach (Control c in maintenanceFactorPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("maintenanceFactorTextBox"))
                {
                    c.Enabled = ok;
                    c.TabStop = ok;
                }
            }
        }

        private void Disconnected(object sender, EventArgs e)
        {
            if (sender is int idx)
            {
                if (idx != ControllerIdx) return;

                Close();
            }
        }

        private void FormInputMaintenanceFactor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.DisconnectedFormController -= Disconnected;
        }
    }
}
