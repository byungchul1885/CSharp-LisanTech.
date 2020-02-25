using System;
using System.Collections;
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
    public partial class FormControllerSetup : Form
    {
        public List<string> DimLevelValue { get; set; }
        public List<string> MaintenanceFactor { get; set; }
        public List<string> OnOff { get; set; }
        public int ControllerIdx { get; set; }
        public string ControllerName { get; set; }
        public int OpModeChangeButtonNum { get; set; }
        public int OnOffButtonNum { get; set; }

        public string OpMode { get; set; }

        public static event EventHandler OpModeButtonClicked;

        public static event EventHandler OnOffButtonClicked;

        public FormControllerSetup()
        {
            InitializeComponent();

            DimLevelValue = new List<string>();
            MaintenanceFactor = new List<string>();
            OnOff = new List<string>();

            FormMain.DimmLevelValueReceivedFromController += DimmLevelValueRefresh;
            FormMain.MaintenanceFactorReceivedFromController += MaintenanceFactorRefresh;
            FormMain.OpModeReceivedFromController += OpModeRefresh;
            FormMain.OnOffReceivedFromController += OnOffRefresh;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DimmLevelSetup_Click(object sender, EventArgs e)
        {
            using (var form = new FormInputDimmLevel())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ControllerName = ControllerName;
                form.ControllerIdx = ControllerIdx;

                form.DimLevelValue.Clear();
                form.DimLevelValue.AddRange(DimLevelValue);

                form.ShowDialog();
            }
        }

        private void MaintenanceFactorSetup_Click(object sender, EventArgs e)
        {
            using (var form = new FormInputMaintenanceFactor())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ControllerName = ControllerName;
                form.ControllerIdx = ControllerIdx;

                form.MaintenanceFactor.Clear();
                form.MaintenanceFactor.AddRange(MaintenanceFactor);

                form.ShowDialog();
            }
        }

        private void FormControllerSetupDiaglog_Load(object sender, EventArgs e)
        {
            foreach (Control c in dimmLevelPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuCustomLabel)
                    && c.Name.Contains("dimmLevelLabel"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "dimmLevelLabel".Length));
                    c.Text = DimLevelValue[levelIndex];
                }
            }

            foreach (Control c in maintenanceFactorPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuCustomLabel)
                    && c.Name.Contains("maintenanceFactorLabel"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "maintenanceFactorLabel".Length));
                    c.Text = MaintenanceFactor[levelIndex];
                }
            }

            foreach (Control c in onOffPanel.Controls)
            {
                if (c.GetType() == typeof(BunifuCustomLabel)
                    && c.Name.Contains("onoffLabel"))
                {
                    int levelIndex = Int32.Parse(c.Name.Remove(0, "onoffLabel".Length));
                    c.Text = OnOff[levelIndex];
                    if (c.Text == "OFF")
                    {
                        c.ForeColor = Color.FromArgb(255, 85, 85);
                    }
                }
            }

            titleLabel.Text = ControllerName + " 제어기 설정 상태";
            opModeLabel.Text = OpMode;
        }

        private void DimmLevelValueRefresh(object sender, EventArgs e)
        {
            if (sender is List<string> li)
            {
                if (Int32.Parse(li[0]) == ControllerIdx)
                {
                    DimLevelValue.Clear();
                    DimLevelValue.AddRange(li);

                    foreach (Control c in dimmLevelPanel.Controls)
                    {
                        if (c.GetType() == typeof(BunifuCustomLabel)
                            && c.Name.Contains("dimmLevelLabel"))
                        {
                            int levelIndex = Int32.Parse(c.Name.Remove(0, "dimmLevelLabel".Length));
                            Invoke(new Action(() =>
                            {
                                c.Text = DimLevelValue[levelIndex];
                            }));
                        }
                    }
                }
            }
        }

        private void MaintenanceFactorRefresh(object sender, EventArgs e)
        {   
            if (sender is List<string> li)
            {
                if (Int32.Parse(li[0]) == ControllerIdx)
                {
                    MaintenanceFactor.Clear();
                    MaintenanceFactor.AddRange(li);

                    foreach (Control c in maintenanceFactorPanel.Controls)
                    {
                        if (c.GetType() == typeof(BunifuCustomLabel)
                            && c.Name.Contains("maintenanceFactorLabel"))
                        {
                            int levelIndex = Int32.Parse(c.Name.Remove(0, "maintenanceFactorLabel".Length));
                            Invoke(new Action(() =>
                            {
                                c.Text = MaintenanceFactor[levelIndex];
                            }));
                        }
                    }
                }
            }
        }

        private void OpModeRefresh(object sender, EventArgs e)
        {
            if (sender is ArrayList mode)
            {
                if ((int)mode[0] == ControllerIdx)
                {
                    Invoke(new Action(() =>
                    {
                        opModeLabel.Text = (string)mode[1];
                    }));
                }
            }
        }

        private void OnOffRefresh(object sender, EventArgs e)
        {
            if (sender is List<string> li)
            {
                if (Int32.Parse(li[0]) == ControllerIdx)
                {
                    OnOff.Clear();
                    OnOff.AddRange(li);

                    foreach (Control c in onOffPanel.Controls)
                    {
                        if (c.GetType() == typeof(BunifuCustomLabel)
                            && c.Name.Contains("onoffLabel"))
                        {
                            int levelIndex = Int32.Parse(c.Name.Remove(0, "onoffLabel".Length));
                            Invoke(new Action(() =>
                            {
                                c.Text = OnOff[levelIndex];
                            }));
                        }
                    }
                }
            }
        }


        private void OpModeChange_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton bt)
            {
                OpModeChangeButtonNum = Int32.Parse(bt.Tag.ToString());

                OpModeButtonClicked?.Invoke(this, e);
            }
        }

        private void FormControllerSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.DimmLevelValueReceivedFromController -= DimmLevelValueRefresh;
            FormMain.MaintenanceFactorReceivedFromController -= MaintenanceFactorRefresh;
            FormMain.OpModeReceivedFromController -= OpModeRefresh;
            FormMain.OnOffReceivedFromController -= OnOffRefresh;
        }

        private void DimmOnOff_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton bt)
            {
                OnOffButtonNum = Int32.Parse(bt.Tag.ToString());

                OnOffButtonClicked?.Invoke(this, e);
            }
        }
    }
}
