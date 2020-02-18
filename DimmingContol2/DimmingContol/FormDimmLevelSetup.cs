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
    public partial class FormDimmLevelSetup : Form
    {
        public List<string> DimLevel { get; set; }


        public FormDimmLevelSetup()
        {
            InitializeComponent();

            DimLevel = new List<string>(new string[dimmLevelPanel.ColumnCount]);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormDimmLevelSetup_Load(object sender, EventArgs e)
        {
            foreach (Control c in dimmLevelPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("dimmTextBox"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "dimmTextBox".Length));
                    c.Text = DimLevel[levelIndex];
                }
            }
        }
    }
}
