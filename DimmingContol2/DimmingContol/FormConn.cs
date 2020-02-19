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


        //public event EventHandler Loaded;


        public FormConn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is BunifuThinButton2 button)
            {
                IP = ipMetroTextbox.Text;
                SubMask = subMaskMetroTextbox.Text;
                Gateway = gateWayMetroTextbox.Text;
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
            subMaskMetroTextbox.Text = SubMask;
            gateWayMetroTextbox.Text = Gateway;
            portMetroTextbox.Text = Port;


            // Raise the event and pass any object
            //Loaded?.Invoke("hoho", e);
        }
    }
}
