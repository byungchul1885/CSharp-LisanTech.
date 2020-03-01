using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Bunifu.Framework.UI;


/*
 * [Properties.Settings] 저장 위치
 * C:\Users\byung\AppData\Local\DimmingContol\
 * 
 */

namespace DimmingContol
{
    public partial class FormMain : Form
    {
        private readonly string[] warningString = new string[4] {string.Empty, string.Empty, string.Empty, string.Empty};

        private readonly int[] rxFrameCnt = new int[4] { 0, 0, 0, 0 };

        private readonly List<Master> MBmaster = new List<Master>();
        private readonly List<DispatcherTimer> ReqInterval = new List<DispatcherTimer>();

        /* 터널 이름 */
        private readonly List<Control> TunnelName = new List<Control>();

        /* 상행-주행 */
        private readonly List<Control> H70AA70AB = new List<Control>();
        private readonly List<Control> H70A870A9 = new List<Control>();
        private readonly List<Control> H709B = new List<Control>();
        private readonly List<Control> H709C = new List<Control>();
        private readonly List<Control> H7081B02 = new List<Control>();
        private readonly List<Control> H7081B06 = new List<Control>();

        /* 상행-추월 */
        private readonly List<Control> H709A = new List<Control>();
        private readonly List<Control> H709D = new List<Control>();
        private readonly List<Control> H7081B00 = new List<Control>();
        private readonly List<Control> H7081B04 = new List<Control>();

        /* 하행-추월 */
        private readonly List<Control> H70AE70AF = new List<Control>();
        private readonly List<Control> H70AC70AD = new List<Control>();
        private readonly List<Control> H70A0 = new List<Control>();
        private readonly List<Control> H70A2 = new List<Control>();
        private readonly List<Control> H7081B08 = new List<Control>();
        private readonly List<Control> H7081B12 = new List<Control>();

        /* 하행-주행 */
        private readonly List<Control> H70A1 = new List<Control>();
        private readonly List<Control> H70A3 = new List<Control>();
        private readonly List<Control> H7081B10 = new List<Control>();
        private readonly List<Control> H7081B14 = new List<Control>();

        /* Warning Sign */
        private readonly List<Control> H7080B1 = new List<Control>();

        /* 디밍 레벨 */
        private readonly List<string> H73A9 = new List<string>();
        private readonly List<string> H73AA = new List<string>();
        private readonly List<string> H73AB = new List<string>();
        private readonly List<string> H73AC = new List<string>();
        private readonly List<string> H73AD = new List<string>();
        private readonly List<string> H73AE = new List<string>();
        private readonly List<string> H73AF = new List<string>();
        private readonly List<string> H73B0 = new List<string>();
        private readonly List<string> H73B1 = new List<string>();
        private readonly List<string> H73B2 = new List<string>();
        private readonly List<string> H73B3 = new List<string>();
        private readonly List<string> H73B4 = new List<string>();
        private readonly List<string> H73B5 = new List<string>();
        private readonly List<string> H73B6 = new List<string>();
        private readonly List<string> H73B7 = new List<string>();
        private readonly List<string> H73B8 = new List<string>();
        private readonly List<string> H73B9 = new List<string>();
        private readonly List<string> H73BA = new List<string>();
        private readonly List<string> H73BB = new List<string>();
        private readonly List<string> H73BC = new List<string>();
        private readonly List<string> H73BD = new List<string>();

        /* 보수율 */
        private readonly List<string> H73A3 = new List<string>();
        private readonly List<string> H73A4 = new List<string>();
        private readonly List<string> H73A5 = new List<string>();
        private readonly List<string> H73A6 = new List<string>();
        private readonly List<string> H73A7 = new List<string>();
        private readonly List<string> H73A8 = new List<string>();

        /* 운전 모드 */
        private readonly List<string> OpMode = new List<string>();

        /* 연결 버튼 */
        private readonly List<Control> ConnectButton = new List<Control>();

        /* 연결시도 회수 */
        private readonly List<int> reqCnt = new List<int>() { 0, 0, 0, 0 };


        public static event EventHandler DimmLevelValueReceivedFromController;

        public static event EventHandler MaintenanceFactorReceivedFromController;

        public static event EventHandler OpModeReceivedFromController;

        public static event EventHandler OnOffReceivedFromController;


        public FormMain()
        {
            InitializeComponent();

            InitControl();

            AddEventHandler();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void AddEventHandler()
        {
            FormInputDimmLevel.UserChangedDimmLevelValue += DimmLevelChagnedByUser;

            FormInputMaintenanceFactor.UserChangedMaintenanceFactor += MaintenanceFactorChagnedByUser;

            FormControllerSetup.OpModeButtonClicked += ChangeOpModeByUser;

            FormControllerSetup.OnOffButtonClicked += ChangeDimmOnOffByUser;
        }

        private void InitControl()
        {
            List<TableLayoutPanel> tlpl = new List<TableLayoutPanel>
            {
                mainTLPanel,
                tlPanel00, tlPanel10, tlPanel20, tlPanel30,
                tlPanel01, tlPanel11, tlPanel21, tlPanel31,
                tlPanel02, tlPanel12, tlPanel22, tlPanel32,
                tlPanel03, tlPanel13, tlPanel23, tlPanel33,
                luminancePanelX00, luminancePanelX10, 
                luminancePanelX01, luminancePanelX11, 
                luminancePanelX02, luminancePanelX12,
                luminancePanelX03, luminancePanelX13,
            };
            foreach (var item in tlpl)
            {
                typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, item, new object[] { true });
            }

            /* 상행-주행 -------------------------------------*/
            /* 외부휘도 */
            H70AA70AB.Add(externalLuminanceX00); /* 제어기 #0 */
            H70AA70AB.Add(externalLuminanceX01); /* 제어기 #1 */
            H70AA70AB.Add(externalLuminanceX02); /* 제어기 #2 */
            H70AA70AB.Add(externalLuminanceX03); /* 제어기 #3 */

            /* 내부휘도 */
            H70A870A9.Add(internalLuminanceX00);
            H70A870A9.Add(internalLuminanceX01);
            H70A870A9.Add(internalLuminanceX02);
            H70A870A9.Add(internalLuminanceX03);

            /* 주간등 디밍% */
            H709B.Add(dayLampDimmPercentX00);
            H709B.Add(dayLampDimmPercentX01);
            H709B.Add(dayLampDimmPercentX02);
            H709B.Add(dayLampDimmPercentX03);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B02.Add(dayLampOnOffLabelX00);
            H7081B02.Add(dayLampOnOffLabelX01);
            H7081B02.Add(dayLampOnOffLabelX02);
            H7081B02.Add(dayLampOnOffLabelX03);

            /* 상시등 디밍% */
            H709D.Add(regularLampDimmPercentX00);
            H709D.Add(regularLampDimmPercentX01);
            H709D.Add(regularLampDimmPercentX02);
            H709D.Add(regularLampDimmPercentX03);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B06.Add(regularLampOnOffLabelX00);
            H7081B06.Add(regularLampOnOffLabelX01);
            H7081B06.Add(regularLampOnOffLabelX02);
            H7081B06.Add(regularLampOnOffLabelX03);


            /* 상행-추월 -------------------------------------*/
            /* 주간등 디밍% */
            H709A.Add(dayLampDimmPercentX10);
            H709A.Add(dayLampDimmPercentX11);
            H709A.Add(dayLampDimmPercentX12);
            H709A.Add(dayLampDimmPercentX13);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B00.Add(dayLampOnOffLabelX10);
            H7081B00.Add(dayLampOnOffLabelX11);
            H7081B00.Add(dayLampOnOffLabelX12);
            H7081B00.Add(dayLampOnOffLabelX13);

            /* 상시등 디밍% */
            H709C.Add(regularLampDimmPercentX10);
            H709C.Add(regularLampDimmPercentX11);
            H709C.Add(regularLampDimmPercentX12);
            H709C.Add(regularLampDimmPercentX13);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B04.Add(regularLampOnOffLabelX10);
            H7081B04.Add(regularLampOnOffLabelX11);
            H7081B04.Add(regularLampOnOffLabelX12);
            H7081B04.Add(regularLampOnOffLabelX13);


            /* 하행-추월 -------------------------------------*/
            /* 외부휘도 */
            H70AE70AF.Add(externalLuminanceX20); /* 제어기 #0 */
            H70AE70AF.Add(externalLuminanceX21); /* 제어기 #1 */
            H70AE70AF.Add(externalLuminanceX22); /* 제어기 #2 */
            H70AE70AF.Add(externalLuminanceX23); /* 제어기 #3 */

            /* 내부휘도 */
            H70AC70AD.Add(internalLuminanceX20);
            H70AC70AD.Add(internalLuminanceX21);
            H70AC70AD.Add(internalLuminanceX22);
            H70AC70AD.Add(internalLuminanceX23);

            /* 주간등 디밍% */
            H70A0.Add(dayLampDimmPercentX20);
            H70A0.Add(dayLampDimmPercentX21);
            H70A0.Add(dayLampDimmPercentX22);
            H70A0.Add(dayLampDimmPercentX23);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B08.Add(dayLampOnOffLabelX20);
            H7081B08.Add(dayLampOnOffLabelX21);
            H7081B08.Add(dayLampOnOffLabelX22);
            H7081B08.Add(dayLampOnOffLabelX23);

            /* 상시등 디밍% */
            H70A2.Add(regularLampDimmPercentX20);
            H70A2.Add(regularLampDimmPercentX21);
            H70A2.Add(regularLampDimmPercentX22);
            H70A2.Add(regularLampDimmPercentX23);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B12.Add(regularLampOnOffLabelX20);
            H7081B12.Add(regularLampOnOffLabelX21);
            H7081B12.Add(regularLampOnOffLabelX22);
            H7081B12.Add(regularLampOnOffLabelX23);


            /* 하행-주행 -------------------------------------*/
            /* 주간등 디밍% */
            H70A1.Add(dayLampDimmPercentX30);
            H70A1.Add(dayLampDimmPercentX31);
            H70A1.Add(dayLampDimmPercentX32);
            H70A1.Add(dayLampDimmPercentX33);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B10.Add(dayLampOnOffLabelX30);
            H7081B10.Add(dayLampOnOffLabelX31);
            H7081B10.Add(dayLampOnOffLabelX32);
            H7081B10.Add(dayLampOnOffLabelX33);

            /* 상시등 디밍% */
            H70A3.Add(regularLampDimmPercentX30);
            H70A3.Add(regularLampDimmPercentX31);
            H70A3.Add(regularLampDimmPercentX32);
            H70A3.Add(regularLampDimmPercentX33);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B14.Add(regularLampOnOffLabelX30);
            H7081B14.Add(regularLampOnOffLabelX31);
            H7081B14.Add(regularLampOnOffLabelX32);
            H7081B14.Add(regularLampOnOffLabelX33);

            /* 연결 버튼 */
            ConnectButton.Add(connButtonX0);
            ConnectButton.Add(connButtonX1);
            ConnectButton.Add(connButtonX2);
            ConnectButton.Add(connButtonX3);

            /* Warning */
            H7080B1.Add(warningLabelX0);
            H7080B1.Add(warningLabelX1);
            H7080B1.Add(warningLabelX2);
            H7080B1.Add(warningLabelX3);

            /* 터널 이름 */
            TunnelName.Add(tunnelLabelX0);
            TunnelName.Add(tunnelLabelX1);
            TunnelName.Add(tunnelLabelX2);
            TunnelName.Add(tunnelLabelX3);

            for (int i = 0; i < Properties.Settings.Default.NumController; i++)
            {
                /* 디밍 레벨 */
                H73A9.Add(String.Empty);
                H73AA.Add(String.Empty);
                H73AB.Add(String.Empty);
                H73AC.Add(String.Empty);
                H73AD.Add(String.Empty);
                H73AE.Add(String.Empty);
                H73AF.Add(String.Empty);
                H73B0.Add(String.Empty);
                H73B1.Add(String.Empty);
                H73B2.Add(String.Empty);
                H73B3.Add(String.Empty);
                H73B4.Add(String.Empty);
                H73B5.Add(String.Empty);
                H73B6.Add(String.Empty);
                H73B7.Add(String.Empty);
                H73B8.Add(String.Empty);
                H73B9.Add(String.Empty);
                H73BA.Add(String.Empty);
                H73BB.Add(String.Empty);
                H73BC.Add(String.Empty);
                H73BD.Add(String.Empty);

                /* 보수율 */
                H73A3.Add(String.Empty);
                H73A4.Add(String.Empty);
                H73A5.Add(String.Empty);
                H73A6.Add(String.Empty);
                H73A7.Add(String.Empty);
                H73A8.Add(String.Empty);

                /* 운전 모드 */
                OpMode.Add(String.Empty);

                /* Modbus object */
                MBmaster.Add(new Master());

                /* 타이머 */
                ReqInterval.Add(new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 3000),
                    Tag = i
                });
                ReqInterval[i].Tick += ReqInterval_Tick;

                /* 터널 이름 */
                TunnelName[i].Text = Properties.Settings.Default.ControllerName[i];

                /* 초기화 */
                SetLabelEmpty(i);
            }

            ascendingDirectionLabel.Text = Properties.Settings.Default.ascendingDirection;
            descendingDirectionLabel.Text = Properties.Settings.Default.descendingDirection;
        }

        private void SetLabelEmpty(int idx)
        {
            H7080B1[idx].Text = string.Empty;
            H70AA70AB[idx].Text = string.Empty;
            H70A870A9[idx].Text = string.Empty;
            H709B[idx].Text = string.Empty;
            H7081B02[idx].Text = string.Empty;
            H709D[idx].Text = string.Empty;
            H7081B06[idx].Text = string.Empty;
            H709A[idx].Text = string.Empty;
            H7081B00[idx].Text = string.Empty;
            H709C[idx].Text = string.Empty;
            H7081B04[idx].Text = string.Empty;
            H70AE70AF[idx].Text = string.Empty;
            H70AC70AD[idx].Text = string.Empty;
            H70A0[idx].Text = string.Empty;
            H7081B08[idx].Text = string.Empty;
            H70A2[idx].Text = string.Empty;
            H7081B12[idx].Text = string.Empty;
            H70A1[idx].Text = string.Empty;
            H7081B10[idx].Text = string.Empty;
            H70A3[idx].Text = string.Empty;
            H7081B14[idx].Text = string.Empty;

            /* 디밍 레벨 */
            H73A9[idx] = String.Empty;
            H73AA[idx] = String.Empty;
            H73AB[idx] = String.Empty;
            H73AC[idx] = String.Empty;
            H73AD[idx] = String.Empty;
            H73AE[idx] = String.Empty;
            H73AF[idx] = String.Empty;
            H73B0[idx] = String.Empty;
            H73B1[idx] = String.Empty;
            H73B2[idx] = String.Empty;
            H73B3[idx] = String.Empty;
            H73B4[idx] = String.Empty;
            H73B5[idx] = String.Empty;
            H73B6[idx] = String.Empty;
            H73B7[idx] = String.Empty;
            H73B8[idx] = String.Empty;
            H73B9[idx] = String.Empty;
            H73BA[idx] = String.Empty;
            H73BB[idx] = String.Empty;
            H73BC[idx] = String.Empty;
            H73BD[idx] = String.Empty;

            /* 보수율 */
            H73A3[idx] = String.Empty;
            H73A4[idx] = String.Empty;
            H73A5[idx] = String.Empty;
            H73A6[idx] = String.Empty;
            H73A7[idx] = String.Empty;
            H73A8[idx] = String.Empty;

            /* 운전 모드 */
            OpMode[idx] = String.Empty;

            /* Warning */
            H7080B1[idx].Text += String.Empty;

            /* Tunnel Name color */
            TunnelName[idx].BackColor = Color.FromArgb(217, 217, 217);
            TunnelName[idx].ForeColor = Color.FromArgb(0, 32, 96);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] widthHeight = Properties.Settings.Default.WidthHeight.Split('*');

            Width = Int32.Parse(widthHeight[0].Trim());
            Height = Int32.Parse(widthHeight[1].Trim());

            float fontSizeMagnification;
            if (Width <= 1920)
            {
                fontSizeMagnification = 1.0F;
            }
            else if (Width <= 2048)
            {
                fontSizeMagnification = 1.03F;
            }
            else if (Width <= 2560)
            {
                fontSizeMagnification = 1.32F;
            }
            else if (Width <= 2880)
            {
                fontSizeMagnification = 1.52F;
            }
            else if (Width <= 3200)
            {
                fontSizeMagnification = 1.62F;
            }
            else if (Width <= 3840)
            {
                fontSizeMagnification = 1.68F;
            }
            else
            {
                fontSizeMagnification = 1.0F;
            }

            var c = GetAll(this, typeof(BunifuCustomLabel));
            foreach (var label in c)
            {
                if (label.Name != "titleLabel")
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size * fontSizeMagnification, FontStyle.Bold);
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();

            foreach (var item in MBmaster)
            {
                if (item.Connected)
                {
                    item.OnResponseData -= MBmaster_OnResponseData;
                    item.OnException -= MBmaster_OnException;
                    item.Disconnect();
                }
            }
        }

        private void ReqInterval_Tick(object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dt)
            {
                int controllerIdx = (int)dt.Tag;

                RequestNow(controllerIdx);
            }
            else
            {
                Debug.WriteLine($"why");
            }
        }

        private void CloseProgramButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScreenSizeButton_Click(object sender, EventArgs e)
        {
            using (var form = new FormInputWidthHeight())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.CurrWidthHeight = Properties.Settings.Default.WidthHeight;

                form.TunnelName.Clear();
                foreach (var item in Properties.Settings.Default.ControllerName)
                {
                    form.TunnelName.Add(item);
                }

                form.DirectionName.Clear();
                form.DirectionName.Add(Properties.Settings.Default.ascendingDirection);
                form.DirectionName.Add(Properties.Settings.Default.descendingDirection);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.WidthHeight = form.ReturnValue;

                    int i = 0;
                    foreach (var item in form.TunnelName)
                    {
                        Properties.Settings.Default.ControllerName[i++] = item;
                    }

                    Properties.Settings.Default.ascendingDirection = form.DirectionName[0];
                    Properties.Settings.Default.descendingDirection = form.DirectionName[1];

                    Properties.Settings.Default.Save();
                }
            }
        }

        private void ConnButton_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormConn())
                {
                    form.StartPosition = FormStartPosition.CenterParent;

                    int buttonIndex = Int32.Parse(button.Name.Remove(0, "connButtonX".Length));

                    form.IP = Properties.Settings.Default.IP[buttonIndex];
                    form.SubMask = Properties.Settings.Default.SubMask[buttonIndex];
                    form.Gateway = Properties.Settings.Default.Gateway[buttonIndex];
                    form.Port = Properties.Settings.Default.Port[buttonIndex];
                    form.ControllerName = Properties.Settings.Default.ControllerName[buttonIndex];
                    form.Connected = MBmaster[buttonIndex].Connected;

                    Properties.Settings.Default.Save();

                    form.ShowDialog();

                    if (form.ButtonAction == "conn")
                    {
                        Properties.Settings.Default.IP[buttonIndex] = form.IP;
                        Properties.Settings.Default.SubMask[buttonIndex] = form.SubMask;
                        Properties.Settings.Default.Gateway[buttonIndex] = form.Gateway;
                        Properties.Settings.Default.Port[buttonIndex] = form.Port;

                        try
                        {
                            reqCnt[buttonIndex] = 0;

                            MBmaster[buttonIndex].Connect(form.IP, ushort.Parse(form.Port));
                            MBmaster[buttonIndex].OnResponseData += MBmaster_OnResponseData;
                            MBmaster[buttonIndex].OnException += MBmaster_OnException;

                            ReqInterval[buttonIndex].Start();

                            RequestNow(buttonIndex);
                        }
                        catch (SystemException error)
                        {
                            MessageBox.Show(error.Message, "Error!!!");
                        }
                    }
                    else if (form.ButtonAction == "close")
                    {
                        Debug.WriteLine($"close");
                        MBmaster[buttonIndex].OnResponseData -= MBmaster_OnResponseData;
                        MBmaster[buttonIndex].OnException -= MBmaster_OnException;

                        MBmaster[buttonIndex].Disconnect();

                        ReqInterval[buttonIndex].Stop();

                        ConnectButton[buttonIndex].Text = "끊어짐";
                        ConnectButton[buttonIndex].BackColor = Color.FromArgb(255, 0, 0);

                        SetLabelEmpty(buttonIndex);
                    }
                }
            }
        }

        private void RequestNow(int controllerIdx)
        {
            if (reqCnt[controllerIdx] > 5)
            {
                ConnectButton[controllerIdx].Text = "끊어짐";
                ConnectButton[controllerIdx].BackColor = Color.FromArgb(255, 0, 0);

                MBmaster[controllerIdx].OnResponseData -= MBmaster_OnResponseData;
                MBmaster[controllerIdx].OnException -= MBmaster_OnException;
                MBmaster[controllerIdx].Disconnect();
            }

            ushort ID = Convert.ToUInt16((controllerIdx + 1) * 100);

            MBmaster[controllerIdx].ReadHoldingRegister(ID, 1, 0x7080, 32); // 0x7080 ~ 0x709F
        }

        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
        {
            Debug.WriteLine($"MBmaster_OnException: {exception}");

            string exc = "Modbus says error: ";
            switch (exception)
            {
                case Master.excIllegalFunction: exc += "Illegal function!"; break;
                case Master.excIllegalDataAdr: exc += "Illegal data adress!"; break;
                case Master.excIllegalDataVal: exc += "Illegal data value!"; break;
                case Master.excSlaveDeviceFailure: exc += "Slave device failure!"; break;
                case Master.excAck: exc += "Acknoledge!"; break;
                case Master.excGatePathUnavailable: exc += "Gateway path unavailbale!"; break;
                case Master.excExceptionTimeout: exc += "Slave timed out!"; break;
                case Master.excExceptionConnectionLost: exc += "Connection is lost!"; break;
                case Master.excExceptionNotConnected: exc += "Not connected!"; break;
            }

            MessageBox.Show(exc, "Modbus slave exception");
        }

        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            if (ID >= 1000) /* setup command response don't care */
            {
                return;
            }

            int quotient = ID / 100;
            int remainder = ID % 100;
            int controllerIdx = quotient - 1;

            Invoke(new Action(() =>
            {
                if (!MBmaster[controllerIdx].Connected) return;

                reqCnt[controllerIdx] = 0;

                ConnectButton[controllerIdx].Text = "연결";
                ConnectButton[controllerIdx].BackColor = Color.FromArgb(0, 176, 80);

                rxFrameCnt[controllerIdx]++;

                if (controllerIdx == 0)
                {
                    rxFrameCntX0.Text = rxFrameCnt[0].ToString();
                }
                else if (controllerIdx == 1)
                {
                    rxFrameCntX1.Text = rxFrameCnt[1].ToString();
                }
                else if (controllerIdx == 2)
                {
                    rxFrameCntX2.Text = rxFrameCnt[2].ToString();
                }
                else if (controllerIdx == 3)
                {
                    rxFrameCntX3.Text = rxFrameCnt[3].ToString();
                }
            }));

            if (remainder == 0)
            {
                Parsing_7080H(values, controllerIdx);
                System.Threading.Thread.Sleep(500);
                MBmaster[controllerIdx].ReadHoldingRegister(Convert.ToUInt16(ID + 1), 1, 0x70A0, 16);
            }
            else if (remainder == 1)
            {
                Parsing_70A0H(values, controllerIdx);
                System.Threading.Thread.Sleep(500);
                MBmaster[controllerIdx].ReadHoldingRegister(Convert.ToUInt16(ID + 1), 1, 0x73A0, 32);
            }
            else if (remainder == 2)
            {
                Parsing_73A0H(values, controllerIdx);
                System.Threading.Thread.Sleep(500);

                Invoke(new Action(() =>
                {
                    if (!MBmaster[controllerIdx].Connected) return;

                    H7080B1[controllerIdx].Text = warningString[controllerIdx];

                    if (string.IsNullOrEmpty(warningString[controllerIdx]))
                    {
                        TunnelName[controllerIdx].BackColor = Color.FromArgb(217, 217, 217);
                        TunnelName[controllerIdx].ForeColor = Color.FromArgb(0, 32, 96); 
                    }
                    else
                    {
                        Debug.WriteLine($"Red");
                        TunnelName[controllerIdx].BackColor = Color.Red;
                        TunnelName[controllerIdx].ForeColor = Color.Yellow;
                    }

                    warningString[controllerIdx] = string.Empty;
                }));

#if false // bccho, 2020-02-28, stop here
                MBmaster[controllerIdx].ReadHoldingRegister(Convert.ToUInt16(ID + 1), 1, 0x73C0, 32);
#endif
            }
            else if (remainder == 3)
            {
                Parsing_73C0H(values, controllerIdx);
                System.Threading.Thread.Sleep(500);
                MBmaster[controllerIdx].ReadHoldingRegister(Convert.ToUInt16(ID + 1), 1, 0x73E0, 24);
            }
            else if (remainder == 4)
            {
                Parsing_73E0H(values, controllerIdx);
                System.Threading.Thread.Sleep(500);
            }
        }

        private void ControllerSetupButton_Click(object sender, EventArgs e)
        {
            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormControllerSetup())
                {
                    int idx = Int32.Parse(button.Name.Remove(0, "controllerSetupButtonX".Length));

                    string[] DimmLevel = new string[] {"",
                        H73A9[idx], H73AA[idx], H73AB[idx], H73AC[idx], H73AD[idx],
                        H73AE[idx], H73AF[idx], H73B0[idx], H73B1[idx], H73B2[idx],
                        H73B3[idx], H73B4[idx], H73B5[idx], H73B6[idx], H73B7[idx],
                        H73B8[idx], H73B9[idx], H73BA[idx], H73BB[idx], H73BC[idx],
                        H73BC[idx],
                    };

                    string[] MaintenanceFactor = new string[] {"",
                        H73A3[idx], H73A4[idx], H73A5[idx], H73A6[idx], H73A7[idx],
                        H73A8[idx]};

                    /* 아래 순서 중요 */
                    string[] OnOff = new string[] {"",
                        H7081B00[idx].Text, H7081B04[idx].Text, H7081B02[idx].Text, H7081B06[idx].Text,
                        H7081B08[idx].Text, H7081B12[idx].Text, H7081B10[idx].Text, H7081B14[idx].Text,};
                    
                    form.StartPosition = FormStartPosition.CenterParent;

                    form.ControllerIdx = idx;
                    form.ControllerName = Properties.Settings.Default.ControllerName[idx];

                    form.DimLevelValue.Clear();
                    form.DimLevelValue.AddRange(DimmLevel);

                    form.MaintenanceFactor.Clear();
                    form.MaintenanceFactor.AddRange(MaintenanceFactor);

                    form.OpMode = OpMode[idx];

                    form.OnOff.Clear();
                    form.OnOff.AddRange(OnOff);

                    form.Connected = MBmaster[idx].Connected;

                    form.ShowDialog();
                }
            }
        }

        private void DimmLevelChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputDimmLevel form)
            {
                byte[] temp = new byte[42];
                int intTemp;
                int controllerIdx = Int32.Parse(form.DimLevelValue[0]);

                intTemp = Int32.Parse(form.DimLevelValue[1]);
                temp[0] = (byte)((intTemp & 0xFF00) >> 8);
                temp[1] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[2]);
                temp[2] = (byte)((intTemp & 0xFF00) >> 8);
                temp[3] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[3]);
                temp[4] = (byte)((intTemp & 0xFF00) >> 8);
                temp[5] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[4]);
                temp[6] = (byte)((intTemp & 0xFF00) >> 8);
                temp[7] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[5]);
                temp[8] = (byte)((intTemp & 0xFF00) >> 8);
                temp[9] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[6]);
                temp[10] = (byte)((intTemp & 0xFF00) >> 8);
                temp[11] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[7]);
                temp[12] = (byte)((intTemp & 0xFF00) >> 8);
                temp[13] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[8]);
                temp[14] = (byte)((intTemp & 0xFF00) >> 8);
                temp[15] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[9]);
                temp[16] = (byte)((intTemp & 0xFF00) >> 8);
                temp[17] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[10]);
                temp[18] = (byte)((intTemp & 0xFF00) >> 8);
                temp[19] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[11]);
                temp[20] = (byte)((intTemp & 0xFF00) >> 8);
                temp[21] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[12]);
                temp[22] = (byte)((intTemp & 0xFF00) >> 8);
                temp[23] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[13]);
                temp[24] = (byte)((intTemp & 0xFF00) >> 8);
                temp[25] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[14]);
                temp[26] = (byte)((intTemp & 0xFF00) >> 8);
                temp[27] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[15]);
                temp[28] = (byte)((intTemp & 0xFF00) >> 8);
                temp[29] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[16]);
                temp[30] = (byte)((intTemp & 0xFF00) >> 8);
                temp[31] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[17]);
                temp[32] = (byte)((intTemp & 0xFF00) >> 8);
                temp[33] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[18]);
                temp[34] = (byte)((intTemp & 0xFF00) >> 8);
                temp[35] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[19]);
                temp[36] = (byte)((intTemp & 0xFF00) >> 8);
                temp[37] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[20]);
                temp[38] = (byte)((intTemp & 0xFF00) >> 8);
                temp[39] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.DimLevelValue[21]);
                temp[40] = (byte)((intTemp & 0xFF00) >> 8);
                temp[41] = (byte)((intTemp & 0x00FF));

                MBmaster[controllerIdx].WriteMultipleRegister(1001, 1, 0x73A9, temp);
            }
        }

        private void MaintenanceFactorChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputMaintenanceFactor form)
            {
                int intTemp;
                int controllerIdx = Int32.Parse(form.MaintenanceFactor[0]);

                byte[] temp = new byte[12];

                intTemp = Int32.Parse(form.MaintenanceFactor[1]);
                temp[0] = (byte)((intTemp & 0xFF00) >> 8);
                temp[1] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.MaintenanceFactor[2]);
                temp[2] = (byte)((intTemp & 0xFF00) >> 8);
                temp[3] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.MaintenanceFactor[3]);
                temp[4] = (byte)((intTemp & 0xFF00) >> 8);
                temp[5] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.MaintenanceFactor[4]);
                temp[6] = (byte)((intTemp & 0xFF00) >> 8);
                temp[7] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.MaintenanceFactor[5]);
                temp[8] = (byte)((intTemp & 0xFF00) >> 8);
                temp[9] = (byte)((intTemp & 0x00FF));

                intTemp = Int32.Parse(form.MaintenanceFactor[6]);
                temp[10] = (byte)((intTemp & 0xFF00) >> 8);
                temp[11] = (byte)((intTemp & 0x00FF));

                MBmaster[controllerIdx].WriteMultipleRegister(1002, 1, 0x73A3, temp);
            }
        }

        private void ChangeOpModeByUser(object sender, EventArgs e)
        {
            if (sender is FormControllerSetup form)
            {
                byte[] temp = new byte[2];
                ushort address;

                BitArray H7083 = new BitArray(16);
                BitArray H7085 = new BitArray(16);

                if (form.OpModeChangeButtonNum == 0)
                {
                    H7083[0] = true;
                    H7083.CopyTo(temp, 0);
                    address = 0x7083;
                }
                else if (form.OpModeChangeButtonNum == 1)
                {
                    H7083[1] = true;
                    H7083.CopyTo(temp, 0);
                    address = 0x7083;
                }
                else
                {
                    H7085[4] = true;
                    H7085.CopyTo(temp, 0);
                    address = 0x7085;
                }

                Array.Reverse(temp);
                MBmaster[form.ControllerIdx].WriteMultipleRegister(1000, 1, address, temp);
            }
        }

        private void ChangeDimmOnOffByUser(object sender, EventArgs e)
        {
            if (sender is FormControllerSetup form)
            {
                byte[] temp = new byte[2];
                ushort address = 0;

                BitArray H7086 = new BitArray(16);
                BitArray H7087 = new BitArray(16);

                switch (form.OnOffButtonNum)
                {
                    /* 상행-추월-주간 */
                    case 1:     /* ON */
                        H7086[0] = true; H7086[2] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    case 11:    /* OFF */
                        H7086[1] = true; H7086[3] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    /* 상행-추월-상시 */
                    case 2:     /* ON */
                        H7086[8] = true; H7086[10] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    case 12:    /* OFF */
                        H7086[9] = true; H7086[11] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    /* 상행-주행-주간 */
                    case 3:     /* ON */
                        H7086[4] = true; H7086[6] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    case 13:    /* OFF */
                        H7086[5] = true; H7086[7] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    /* 상행-주행-상시 */
                    case 4:     /* ON */
                        H7086[12] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;
                    case 14:    /* OFF */
                        H7086[13] = true;
                        H7086.CopyTo(temp, 0);
                        address = 0x7086;
                        break;

                    /* 하행-추월-주간 -----------------*/
                    case 5:     /* ON */
                        H7087[0] = true; H7087[2] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    case 15:    /* OFF */
                        H7087[1] = true; H7087[3] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    /* 하행-추월-상시 */
                    case 6:     /* ON */
                        H7087[8] = true; H7087[10] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    case 16:    /* OFF */
                        H7087[9] = true; H7087[11] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    /* 하행-주행-주간 */
                    case 7:     /* ON */
                        H7087[4] = true; H7087[6] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    case 17:    /* OFF */
                        H7087[5] = true; H7087[7] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    /* 상행-주행-상시 */
                    case 8:     /* ON */
                        H7087[12] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                    case 18:    /* OFF */
                        H7087[13] = true;
                        H7087.CopyTo(temp, 0);
                        address = 0x7087;
                        break;
                }

                Array.Reverse(temp);
                MBmaster[form.ControllerIdx].WriteMultipleRegister(1003, 1, address, temp);
            }
        }

        private void Parsing_7080H(byte[] values, int controllerIdx)
        {
            if (!MBmaster[controllerIdx].Connected) return;

            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7080 = new BitArray(temp); // B1

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7081 = new BitArray(temp); // B2

            warningString[controllerIdx] += H7080[1] ? "화재운전 " : string.Empty;

            string opMode = H7080[0] ? "Remote" : "Local";
            opMode = H7080[6] ? "Remote수동" : opMode;
            OpMode[controllerIdx] = opMode;

            ArrayList al = new ArrayList
            {
                controllerIdx,
                opMode
            };

            OpModeReceivedFromController?.Invoke(al, null);

            try
            {
                Invoke(new Action(() =>
                {
                    if (!MBmaster[controllerIdx].Connected) return;

                    H7081B00[controllerIdx].Text = H7081[0] ? "ON" : "OFF"; // 상행 추월선 맑음등 상태
                    H7081B02[controllerIdx].Text = H7081[2] ? "ON" : "OFF"; // 상행 추월선 흐림등 상태
                    H7081B04[controllerIdx].Text = H7081[4] ? "ON" : "OFF"; // 상행 추월선 주간(일출일몰)등 상태
                    H7081B06[controllerIdx].Text = H7081[6] ? "ON" : "OFF";  // 상행 심야등 상태

                    H7081B08[controllerIdx].Text = H7081[8] ? "ON" : "OFF";  // 하행 추월선 맑음등 상태
                    H7081B10[controllerIdx].Text = H7081[10] ? "ON" : "OFF";  // 하행 추월선 흐림등 상태
                    H7081B12[controllerIdx].Text = H7081[12] ? "ON" : "OFF";  // 하행 추월선 주간(일출일몰)등 상태
                    H7081B14[controllerIdx].Text = H7081[14] ? "ON" : "OFF";  // 하행 심야등 상태

                    H7081B00[controllerIdx].ForeColor = H7081[0] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B02[controllerIdx].ForeColor = H7081[2] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B04[controllerIdx].ForeColor = H7081[4] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B06[controllerIdx].ForeColor = H7081[6] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B08[controllerIdx].ForeColor = H7081[8] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B10[controllerIdx].ForeColor = H7081[10] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B12[controllerIdx].ForeColor = H7081[12] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);
                    H7081B14[controllerIdx].ForeColor = H7081[14] ? Color.FromArgb(0, 102, 204) : Color.FromArgb(211, 82, 48);

                    temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
                    H709A[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 상행 맑음등디밍출력값 감시

                    temp = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp);
                    H709B[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 상행 흐림등디밍출력값 감시

                    temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
                    H709C[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 상행 주간(일출일몰)등 디밍출력값 감시

                    temp = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp);
                    H709D[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 상행 심야등디밍출력값 감시
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Parsing_7080H: {e}");
                return;
            }


            List<string> li = new List<string>
            {
                controllerIdx.ToString(),

                /* order is important */
                H7081B00[controllerIdx].Text,
                H7081B04[controllerIdx].Text,
                H7081B02[controllerIdx].Text,
                H7081B06[controllerIdx].Text,

                H7081B08[controllerIdx].Text,
                H7081B12[controllerIdx].Text,
                H7081B10[controllerIdx].Text,
                H7081B14[controllerIdx].Text
            };

            OnOffReceivedFromController?.Invoke(li, null);
        }

        private void Parsing_70A0H(byte[] values, int controllerIdx)
        {
            byte[] temp, temp1, temp2;

            try
            {
                Invoke(new Action(() =>
                {
                    if (!MBmaster[controllerIdx].Connected) return;

                    temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
                    H70A0[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 하행 맑음등디밍출력값 감시

                    temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
                    H70A1[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 하행 흐림등디밍출력값 감시

                    temp = values.Skip(4).Take(2).ToArray(); Array.Reverse(temp);
                    H70A2[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 하행 주간(일출일몰)등 디밍출력값 감시

                    temp = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp);
                    H70A3[controllerIdx].Text = "디밍 " + BitConverter.ToInt16(temp, 0).ToString() + " %"; // 하행 심야등디밍출력값 감시


                    temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
                    temp1 = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp1);
                    temp2 = temp.Concat(temp1).ToArray();
                    H70A870A9[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString() + " cd/㎡"; // 상행 외부 조도 센서값 전송

                    temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
                    temp1 = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp1);
                    temp2 = temp.Concat(temp1).ToArray();
                    H70AA70AB[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString() + " cd/㎡"; // 상행 외부 휘도센서값 전송

                    temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
                    temp1 = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp1);
                    temp2 = temp.Concat(temp1).ToArray();
                    H70AC70AD[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString() + " cd/㎡"; // 하행 외부 조도 센서값 전송

                    temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
                    temp1 = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp1);
                    temp2 = temp.Concat(temp1).ToArray();
                    H70AE70AF[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString() + " cd/㎡"; // 하행 외부 휘도센서값 전송
                }));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Parsing_70A0H: {e}");
            }
        }

        private void Parsing_73A0H(byte[] values, int controllerIdx)
        {
            if (!MBmaster[controllerIdx].Connected) return;

            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A0 = new BitArray(temp);

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A1 = new BitArray(temp);

            warningString[controllerIdx] += H73A0[0] ? "통신이상 " : string.Empty;
            warningString[controllerIdx] += H73A1[0] ? "CPU이상 " : string.Empty;
            warningString[controllerIdx] += H73A1[1] ? "파워모듈이상 " : string.Empty;

            temp = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp);
            H73A3[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 맑음등보수율

            temp = values.Skip(8).Take(2).ToArray(); Array.Reverse(temp);
            H73A4[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 흐림등보수율

            temp = values.Skip(10).Take(2).ToArray(); Array.Reverse(temp);
            H73A5[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 주간(일출일몰)등보수율

            temp = values.Skip(12).Take(2).ToArray(); Array.Reverse(temp);
            H73A6[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 심야/상시등보수율

            temp = values.Skip(14).Take(2).ToArray(); Array.Reverse(temp);
            H73A7[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 가로등 상시/입구부보수율

            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            H73A8[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); // 가로등격등/출구부보수율

            /* 디밍 레벨 */
            temp = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp);
            H73A9[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString();

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            H73AA[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp);
            H73AB[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            H73AC[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp);
            H73AD[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            H73AE[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp);
            H73AF[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(32).Take(2).ToArray(); Array.Reverse(temp);
            H73B0[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(34).Take(2).ToArray(); Array.Reverse(temp);
            H73B1[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(36).Take(2).ToArray(); Array.Reverse(temp);
            H73B2[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(38).Take(2).ToArray(); Array.Reverse(temp);
            H73B3[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(40).Take(2).ToArray(); Array.Reverse(temp);
            H73B4[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(42).Take(2).ToArray(); Array.Reverse(temp);
            H73B5[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(44).Take(2).ToArray(); Array.Reverse(temp);
            H73B6[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(46).Take(2).ToArray(); Array.Reverse(temp);
            H73B7[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(48).Take(2).ToArray(); Array.Reverse(temp);
            H73B8[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(50).Take(2).ToArray(); Array.Reverse(temp);
            H73B9[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
            H73BA[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp);
            H73BB[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
            H73BC[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;

            temp = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp);
            H73BD[controllerIdx] = BitConverter.ToInt16(temp, 0).ToString(); ;


            List<string> li2 = new List<string>
            {
                controllerIdx.ToString(),

                H73A3[controllerIdx], H73A4[controllerIdx], H73A5[controllerIdx],
                H73A6[controllerIdx], H73A7[controllerIdx], H73A8[controllerIdx],
            };
            MaintenanceFactorReceivedFromController?.Invoke(li2, null);


            List<string> li = new List<string>
            {
                controllerIdx.ToString(),

                H73A9[controllerIdx], H73AA[controllerIdx], H73AB[controllerIdx],
                H73AC[controllerIdx], H73AD[controllerIdx], H73AE[controllerIdx],
                H73AF[controllerIdx], H73B0[controllerIdx], H73B1[controllerIdx],
                H73B2[controllerIdx], H73B3[controllerIdx], H73B4[controllerIdx],
                H73B5[controllerIdx], H73B6[controllerIdx], H73B7[controllerIdx],
                H73B8[controllerIdx], H73B9[controllerIdx], H73BA[controllerIdx],
                H73BB[controllerIdx], H73BC[controllerIdx], H73BD[controllerIdx],
            };
            DimmLevelValueReceivedFromController?.Invoke(li, null);
        }

        private void Parsing_73C0H(byte[] values, int controllerIdx)
        {
#if false
            byte[] temp, temp1, temp2;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C0 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C1 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(4).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C2 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C3 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(8).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C4 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(10).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C5 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(12).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C6 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(14).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C7 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C8 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73C9 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73CA = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73CB = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73CC = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73CD = BitConverter.ToInt16(temp, 0);

            //1단계 - 맑음등1 조도디밍값
            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73CE73CF = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(32).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(34).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73D073D1 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(36).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(38).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73D273D3 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(40).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(42).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73D473D5 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(44).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(46).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73D673D7 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(48).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(50).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73D873D9 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73DA73DB = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73DC73DD = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(60).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(62).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73DE73DF = BitConverter.ToSingle(temp2, 0);
#endif
        }

        private void Parsing_73E0H(byte[] values, int controllerIdx)
        {
#if false
            byte[] temp, temp1, temp2;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73E073E1 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(4).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73E273E3 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(8).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(10).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73E473E5 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(12).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(14).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73E673E7 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73E873E9 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73EA73EB = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73EC73ED = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73EE73EF = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(32).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(34).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73F073F1 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(36).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(38).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73F273F3 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(40).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(42).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73F473F5 = BitConverter.ToSingle(temp2, 0);

            temp = values.Skip(44).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(46).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H73F673F7 = BitConverter.ToSingle(temp2, 0);
#endif
        }


    }
}
