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

namespace DimmingContol
{
    public partial class FormInputWidthHeight : Form
    {
        public string ReturnValue { get; set; }
        public string CurrWidthHeight { get; set; }
        public List<string> TunnelName { get; set; }
        public List<string> DirectionName { get; set; }

        public FormInputWidthHeight()
        {
            InitializeComponent();

            TunnelName = new List<string>();
            DirectionName = new List<string>();
        }


        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            var checkedButton = this.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            ReturnValue = checkedButton.Text;

            TunnelName[0] = TNameTBX0.Text;
            TunnelName[1] = TNameTBX1.Text;
            TunnelName[2] = TNameTBX2.Text;
            TunnelName[3] = TNameTBX3.Text;

            DirectionName[0] = DNameTBX0.Text;
            DirectionName[1] = DNameTBX1.Text;

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormResolutionDialog_Load(object sender, EventArgs e)
        {
            var checkedButton = this.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Text == CurrWidthHeight);
            checkedButton.Checked = true;

            TNameTBX0.Text = TunnelName[0];
            TNameTBX1.Text = TunnelName[1];
            TNameTBX2.Text = TunnelName[2];
            TNameTBX3.Text = TunnelName[3];

            DNameTBX0.Text = DirectionName[0];
            DNameTBX1.Text = DirectionName[1];
        }
    }
}
