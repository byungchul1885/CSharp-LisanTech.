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
    public partial class FormExitSetupConfirm : Form
    {
        public string ButtonAction { get; set; }

        public FormExitSetupConfirm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                if (button.Name == "confirmButton")
                {
                    ButtonAction = "confirm";
                }
                else if (button.Name == "cancelButton")
                {
                    ButtonAction = "cancel";
                }
            }
            Close();
        }
    }
}
