﻿using System;
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
    public partial class FormInputDimmLevel : Form
    {
        public static event EventHandler UserChangedDimmLevelValue;

        public List<string> DimLevelValue { get; set; }
        public string ControllerName { get; set; }
        public int ControllerIdx { get; set; }

        public FormInputDimmLevel()
        {
            InitializeComponent();

            DimLevelValue = new List<string>();

            FormMain.DisconnectedFormController += Disconnected;
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
                    c.Text = DimLevelValue[levelIndex];
                }
            }

            titleLabel.Text = ControllerName + " 제어기 단계별 디밍 설정";
            inputValidation.Visible = false;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            DimLevelValue.Clear();
            DimLevelValue.AddRange(new string[dimmLevelPanel.ColumnCount]);

            foreach (Control c in dimmLevelPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("dimmTextBox"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "dimmTextBox".Length));
                    DimLevelValue[levelIndex] = c.Text;
                }
            }

            DimLevelValue[0] = ControllerIdx.ToString();

            UserChangedDimmLevelValue?.Invoke(this, e);

            Close();
        }


        private void DimmTextBox_OnValueChanged(object sender, EventArgs e)
        {
            if (sender is BunifuMaterialTextbox tb)
            {
                var isNumeric = int.TryParse(tb.Text, out int n);
                if (!isNumeric || n > 9999 || n < 0)
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
                        inputValidation.Text = "입력범위를 벗어났습니다 (0 ~ 9999)";
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
            foreach (Control c in dimmLevelPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuMaterialTextbox)
                    && c.Name.Contains("dimmTextBox"))
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

        private void FormInputDimmLevel_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.DisconnectedFormController -= Disconnected;
        }
    }
}
