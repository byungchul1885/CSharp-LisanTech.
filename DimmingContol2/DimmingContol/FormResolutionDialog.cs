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
    public partial class FormResolutionDialog : Form
    {
        public string ReturnValue { get; set; }
        public string CurrWidthHeight { get; set; }

        public FormResolutionDialog()
        {
            InitializeComponent();
        }


        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            var checkedButton = this.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);

            //Debug.WriteLine($"{checkedButton.Text}");
            //Debug.WriteLine($"{CurrWidthHeight}");

            ReturnValue = checkedButton.Text;
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
        }
    }
}
