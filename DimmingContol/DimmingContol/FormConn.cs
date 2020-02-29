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
    public partial class FormConn : Form
    {
        public string IP { get; set; }
        public string SubMask { get; set; }
        public string Gateway { get; set; }
        public string Port { get; set; }
        public string ButtonAction { get; set; }
        public string ControllerName { get; set; }
        public bool Connected { get; set; }

        public FormConn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                IP = ipMetroTextbox.Text;

#if false
                SubMask = subMaskMetroTextbox.Text;
                Gateway = gateWayMetroTextbox.Text;
#endif
                Port = portMetroTextbox.Text;

                if (button.Name == "connButton")
                {
                    ButtonAction = "conn";
                }
                else if (button.Name == "closeButton")
                {
                    ButtonAction = "close";
                }
                else if (button.Name == "cancelButton")
                {
                    ButtonAction = "cancel";
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormConnDialog_Load(object sender, EventArgs e)
        {
            titleLabel.Text = ControllerName + " 제어기 IP 접속 설정";
            ipMetroTextbox.Text = IP;

            if (Connected)
            {
                connButton.Enabled = false;
                ipMetroTextbox.Enabled = false;
                portMetroTextbox.Enabled = false;
            }
            else
            {
                closeButton.Enabled = false;
            }
#if false
            subMaskMetroTextbox.Text = SubMask;
            gateWayMetroTextbox.Text = Gateway;
#endif
            portMetroTextbox.Text = Port;
        }
    }
}
