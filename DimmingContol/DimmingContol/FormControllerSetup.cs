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
        public string OpModePrevious { get; set; }
        public bool Connected { get; set; }

        public List<BunifuFlatButton> Buttons { get; set; }
        public List<BunifuCustomLabel> OnOffLabel { get; set; }
        public List<BunifuFlatButton> OnButton { get; set; }
        public List<BunifuFlatButton> OffButton { get; set; }



        public static event EventHandler OpModeButtonClicked;

        public static event EventHandler OnOffButtonClicked;

        public FormControllerSetup()
        {
            InitializeComponent();

            DimLevelValue = new List<string>();
            MaintenanceFactor = new List<string>();
            OnOff = new List<string>();
            Buttons = new List<BunifuFlatButton>();
            OnOffLabel = new List<BunifuCustomLabel>();
            OnButton = new List<BunifuFlatButton>();
            OffButton = new List<BunifuFlatButton>();

            FormMain.DimmLevelValueReceivedFromController += DimmLevelValueRefresh;
            FormMain.MaintenanceFactorReceivedFromController += MaintenanceFactorRefresh;
            FormMain.OpModeReceivedFromController += OpModeRefresh;
            FormMain.OnOffReceivedFromController += OnOffRefresh;

            Buttons.Add(offButton01);
            Buttons.Add(offButton02);
            Buttons.Add(offButton03);
            Buttons.Add(offButton04);
            Buttons.Add(offButton05);
            Buttons.Add(offButton06);
            Buttons.Add(offButton07);
            Buttons.Add(offButton08);
            Buttons.Add(onButton01);
            Buttons.Add(onButton02);
            Buttons.Add(onButton03);
            Buttons.Add(onButton04);
            Buttons.Add(onButton05);
            Buttons.Add(onButton06);
            Buttons.Add(onButton07);
            Buttons.Add(onButton08);
            Buttons.Add(mfSetButton);
            Buttons.Add(dimmValueSetButton);
            Buttons.Add(remoteButton);
            Buttons.Add(remoteManualButton);
            Buttons.Add(localButton);

            OnOffLabel.Add(onoffLabel01);
            OnOffLabel.Add(onoffLabel02);
            OnOffLabel.Add(onoffLabel03);
            OnOffLabel.Add(onoffLabel04);
            OnOffLabel.Add(onoffLabel05);
            OnOffLabel.Add(onoffLabel06);
            OnOffLabel.Add(onoffLabel07);
            OnOffLabel.Add(onoffLabel08);

            OnButton.Add(onButton01);
            OnButton.Add(onButton02);
            OnButton.Add(onButton03);
            OnButton.Add(onButton04);
            OnButton.Add(onButton05);
            OnButton.Add(onButton06);
            OnButton.Add(onButton07);
            OnButton.Add(onButton08);

            OffButton.Add(offButton01);
            OffButton.Add(offButton02);
            OffButton.Add(offButton03);
            OffButton.Add(offButton04);
            OffButton.Add(offButton05);
            OffButton.Add(offButton06);
            OffButton.Add(offButton07);
            OffButton.Add(offButton08);
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

        private void FormControllerSetup_Load(object sender, EventArgs e)
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
            opModeLabel.Text = OpModePrevious = OpMode;

            SetButtonsState();
        }

        private void SetButtonsState()
        {
            if (!Connected)
            {
                SetAllButtonsState(false);
            }
            else if (OpMode == "Local")
            {
                SetAllButtonsState(false);
                remoteButton.Enabled = true;
            }
            else if (OpMode == "Remote")
            {
                SetAllButtonsState(false);
                localButton.Enabled = true;
                remoteManualButton.Enabled = true;
            }
            else if (OpMode == "Remote수동")
            {
                SetAllButtonsState(true);
                remoteManualButton.Enabled = false;
                localButton.Enabled = true;
                remoteButton.Enabled = false;

                int i = 0;
                foreach (var item in OnOffLabel)
                {
                    if (item.Text == "ON")
                    {
                        OnButton[i].Enabled = false;
                    }
                    else
                    {
                        OffButton[i].Enabled = false;
                    }
                    i++;
                }
            }
        }

        private void SetAllButtonsState(bool on)
        {
            foreach (var item in Buttons)
            {
                item.Enabled = on;
            }
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
                            try
                            {
                                Invoke(new Action(() =>
                                {
                                    c.Text = DimLevelValue[levelIndex];
                                }));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"DimmLevelValueRefresh: {ex}");
                                return;
                            }
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
                            try
                            {
                                Invoke(new Action(() =>
                                {
                                    c.Text = MaintenanceFactor[levelIndex];
                                }));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"MaintenanceFactorRefresh: {ex}");
                                return;
                            }
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
                    try
                    {
                        Invoke(new Action(() =>
                        {
                            if (OpModePrevious != (string)mode[1])
                            {
                                Debug.WriteLine($"{OpModePrevious} {OpMode}");

                                opModeLabel.Text = OpMode = OpModePrevious = (string)mode[1];
                                SetButtonsState();
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"OpModeRefresh: {ex}");
                        return;
                    }
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
                            try
                            {
                                Invoke(new Action(() =>
                                {
                                    c.Text = OnOff[levelIndex];
                                    c.ForeColor = c.Text == "OFF" ? c.ForeColor = Color.FromArgb(255, 85, 85) : Color.FromArgb(250, 250, 210);
                                    SetButtonsState();
                                }));
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"OnOffRefresh: {ex}");
                                return;
                            }
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
