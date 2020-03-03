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
    public partial class FormError : Form
    {
        public string ErrorMsg { get; set; }

        public FormError()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormError_Load(object sender, EventArgs e)
        {
            errMsg.Text = ErrorMsg;
        }
    }
}
