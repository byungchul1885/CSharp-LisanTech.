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
using System.Windows.Threading;
using Bunifu.Framework.UI;

namespace DimmingContol
{
    public partial class FormMain : Form
    {

        private readonly List<Master> MBmaster = new List<Master>();
        private readonly List<DispatcherTimer> ReqInterval = new List<DispatcherTimer>();

        // private DispatcherTimer[] ReqInterval;

        // private ArrayList<DispatcherTimer> timerArrList = new ArrayList<DispatcherTimer>(4);



        /* 상행-주행 */
        private readonly List<Control> H70AA70AB = new List<Control>();
        private readonly List<Control> H70A870A9 = new List<Control>();
        private readonly List<Control> H709B = new List<Control>();
        private readonly List<Control> H709C = new List<Control>();
        private readonly List<Control> H7081B02 = new List<Control>(); /* button */
        private readonly List<Control> H7081B06 = new List<Control>(); /* button */

        /* 상행-추월 */
        private readonly List<Control> H709A = new List<Control>();
        private readonly List<Control> H709D = new List<Control>();
        private readonly List<Control> H7081B00 = new List<Control>(); /* button */
        private readonly List<Control> H7081B04 = new List<Control>(); /* button */

        /* 하행-추월 */
        private readonly List<Control> H70AE70AF = new List<Control>();
        private readonly List<Control> H70AC70AD = new List<Control>();
        private readonly List<Control> H70A0 = new List<Control>();
        private readonly List<Control> H70A2 = new List<Control>();
        private readonly List<Control> H7081B08 = new List<Control>(); /* button */
        private readonly List<Control> H7081B12 = new List<Control>(); /* button */

        /* 하행-주행 */
        private readonly List<Control> H70A1 = new List<Control>();
        private readonly List<Control> H70A3 = new List<Control>();
        private readonly List<Control> H7081B10 = new List<Control>(); /* button */
        private readonly List<Control> H7081B14 = new List<Control>(); /* button */


        public static event EventHandler DimmLevelValueReceivedFromController;
        public static event EventHandler MaintenanceFactorReceivedFromController;

        public FormMain()
        {
            InitializeComponent();

            InitControl();

            FormInputDimmLevel.UserChangedDimmLevelValue += DimmLevelChagnedByUser;
            FormInputMaintenanceFactor.UserChangedMaintenanceFactor += MaintenanceFactorChagnedByUser;

            for (int i = 0; i < Properties.Settings.Default.NumController; i++)
            {
                MBmaster.Add(new Master());
                ReqInterval.Add(new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 1000),
                    Tag = i
                });
            }
            

            //ReqInterval[0] = new DispatcherTimer
            //{
            //    Interval = new TimeSpan(0, 0, 0, 0, 1000)
            //};

        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void InitControl()
        {
            /* 상행-주행 -------------------------------------*/
            /* 외부휘도 */
            H70AA70AB.Add(externalLuminanceX00); /* 제어기 #0 */
            H70AA70AB.Add(externalLuminanceX01); /* 제어기 #1 */
            H70AA70AB.Add(externalLuminanceX02); /* 제어기 #2 */

            /* 내부휘도 */
            H70A870A9.Add(internalLuminanceX00);
            H70A870A9.Add(internalLuminanceX01);
            H70A870A9.Add(internalLuminanceX02);

            /* 주간등 디밍% */
            H709B.Add(dayLampDimmPercentX00);
            H709B.Add(dayLampDimmPercentX01);
            H709B.Add(dayLampDimmPercentX02);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B02.Add(dayLampButtonX00);
            H7081B02.Add(dayLampButtonX01);
            H7081B02.Add(dayLampButtonX02);

            /* 상시등 디밍% */
            H709C.Add(regularLampDimmPercentX00);
            H709C.Add(regularLampDimmPercentX01);
            H709C.Add(regularLampDimmPercentX02);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B06.Add(regularLampButtonX00);
            H7081B06.Add(regularLampButtonX01);
            H7081B06.Add(regularLampButtonX02);


            /* 상행-추월 -------------------------------------*/
            /* 외부휘도 */
            H70AA70AB.Add(externalLuminanceX10); /* 제어기 #0 */
            H70AA70AB.Add(externalLuminanceX11); /* 제어기 #1 */
            H70AA70AB.Add(externalLuminanceX12); /* 제어기 #2 */

            /* 내부휘도 */
            H70A870A9.Add(internalLuminanceX10);
            H70A870A9.Add(internalLuminanceX11);
            H70A870A9.Add(internalLuminanceX12);

            /* 주간등 디밍% */
            H709A.Add(dayLampDimmPercentX10);
            H709A.Add(dayLampDimmPercentX11);
            H709A.Add(dayLampDimmPercentX12);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B00.Add(dayLampButtonX10);
            H7081B00.Add(dayLampButtonX11);
            H7081B00.Add(dayLampButtonX12);

            /* 상시등 디밍% */
            H709D.Add(regularLampDimmPercentX10);
            H709D.Add(regularLampDimmPercentX11);
            H709D.Add(regularLampDimmPercentX12);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B04.Add(regularLampButtonX10);
            H7081B04.Add(regularLampButtonX11);
            H7081B04.Add(regularLampButtonX12);


            /* 하행-추월 -------------------------------------*/
            /* 외부휘도 */
            H70AE70AF.Add(externalLuminanceX20); /* 제어기 #0 */
            H70AE70AF.Add(externalLuminanceX21); /* 제어기 #1 */
            H70AE70AF.Add(externalLuminanceX22); /* 제어기 #2 */

            /* 내부휘도 */
            H70AC70AD.Add(internalLuminanceX20);
            H70AC70AD.Add(internalLuminanceX21);
            H70AC70AD.Add(internalLuminanceX22);

            /* 주간등 디밍% */
            H70A0.Add(dayLampDimmPercentX20);
            H70A0.Add(dayLampDimmPercentX21);
            H70A0.Add(dayLampDimmPercentX22);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B08.Add(dayLampButtonX20);
            H7081B08.Add(dayLampButtonX21);
            H7081B08.Add(dayLampButtonX22);

            /* 상시등 디밍% */
            H70A2.Add(regularLampDimmPercentX20);
            H70A2.Add(regularLampDimmPercentX21);
            H70A2.Add(regularLampDimmPercentX22);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B12.Add(regularLampButtonX20);
            H7081B12.Add(regularLampButtonX21);
            H7081B12.Add(regularLampButtonX22);


            /* 하행-주행 -------------------------------------*/
            /* 외부휘도 */
            H70AE70AF.Add(externalLuminanceX30); /* 제어기 #0 */
            H70AE70AF.Add(externalLuminanceX31); /* 제어기 #1 */
            H70AE70AF.Add(externalLuminanceX32); /* 제어기 #2 */

            /* 내부휘도 */
            H70AC70AD.Add(internalLuminanceX30);
            H70AC70AD.Add(internalLuminanceX31);
            H70AC70AD.Add(internalLuminanceX32);

            /* 주간등 디밍% */
            H70A1.Add(dayLampDimmPercentX30);
            H70A1.Add(dayLampDimmPercentX31);
            H70A1.Add(dayLampDimmPercentX32);

            /* 주간등 디밍 On-Off 버튼 */
            H7081B10.Add(dayLampButtonX30);
            H7081B10.Add(dayLampButtonX31);
            H7081B10.Add(dayLampButtonX32);

            /* 상시등 디밍% */
            H70A3.Add(regularLampDimmPercentX30);
            H70A3.Add(regularLampDimmPercentX31);
            H70A3.Add(regularLampDimmPercentX32);

            /* 상시등 디밍 On-Off 버튼 */
            H7081B14.Add(regularLampButtonX30);
            H7081B14.Add(regularLampButtonX31);
            H7081B14.Add(regularLampButtonX32);
        }
        private void FormMain_Load(object sender, EventArgs e)
        {

            //ReqInterval[0].Tick += ReqInterval_Tick;

            //ReqInterval[0].Start();


            //MBmaster.Add(new Master("192.168.0.100", 502));
            //MBmaster.Add(new Master());
            //MBmaster[0].Connect("192.168.0.100", 502);

            ReqInterval[0].Tick += ReqInterval_Tick;
            ReqInterval[0].Start();

            ReqInterval[1].Tick += ReqInterval_Tick;
            ReqInterval[1].Start();

            ReqInterval[2].Tick += ReqInterval_Tick;
            ReqInterval[2].Start();

            ReqInterval[3].Tick += ReqInterval_Tick;
            ReqInterval[3].Start();




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

                //if (label.BackColor == Color.FromArgb(32, 56, 100))
                //{
                //    label.Text = "";
                //}
            }

            tunnelLabelX0.Text = Properties.Settings.Default.ControllerName[0];
            tunnelLabelX1.Text = Properties.Settings.Default.ControllerName[1];
            tunnelLabelX2.Text = Properties.Settings.Default.ControllerName[2];

            ascendingDirectionLabel.Text = Properties.Settings.Default.ascendingDirection;
            descendingDirectionLabel.Text = Properties.Settings.Default.descendingDirection;



            List<Bitmap> images = new List<Bitmap>();

            Bitmap finalImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("main");
            //channelPic.Image = (Image)O; //Set the Image property of channelPic to the returned object as Image

            //Bitmap bitmap0 = (Bitmap)Properties.Resources.ResourceManager.GetObject("main");
            //Bitmap bitmap1 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire10");
            Bitmap bitmap2 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire00");
            Bitmap bitmap3 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire01");
            Bitmap bitmap4 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire11");
            Bitmap bitmap5 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire02");
            Bitmap bitmap6 = (Bitmap)Properties.Resources.ResourceManager.GetObject("fire12");


            //images.Add((Bitmap)Properties.Resources.ResourceManager.GetObject("main"));
            images.Add((Bitmap)Properties.Resources.ResourceManager.GetObject("fire10"));
            //images.Add(bitmap2);
            //images.Add(bitmap3);
            //images.Add(bitmap4);
            //images.Add(bitmap5);
            //images.Add(bitmap6);


            using (Graphics g = Graphics.FromImage(finalImage))
            {
                foreach (Bitmap image in images)
                {
                    g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                }
            }

            mainTLPanel.BackgroundImage = finalImage;

            

        }

        private void ReqInterval_Tick(object sender, EventArgs e)
        {
            if (sender is DispatcherTimer dt)
            {
                Debug.WriteLine($"ReqInterval_Tick Tag:{dt.Tag}");
            }
            else
            {
                Debug.WriteLine($"why");
            }
#if false
            reqCnt++;
            if (reqCnt > 5)
            {
                App.Config.ConnStat = "접속끊김";
                App.Config.ConnStatBackgBrush = Brushes.Red;
                App.Config.ConnStatForgBrush = Brushes.Yellow;
            }
#endif
        }

        private void CloseProgramButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void ScreenSizeButton_Click(object sender, EventArgs e)
        {
            using (var form = new FormInputWidthHeight())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.CurrWidthHeight = Properties.Settings.Default.WidthHeight;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.WidthHeight = form.ReturnValue;
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

                    form.ShowDialog();

                    if (form.ButtonAction == "conn")
                    {
                        Properties.Settings.Default.IP[buttonIndex] = form.IP;
                        Properties.Settings.Default.SubMask[buttonIndex] = form.SubMask;
                        Properties.Settings.Default.Gateway[buttonIndex] = form.Gateway;
                        Properties.Settings.Default.Port[buttonIndex] = form.Port;
                    }
                    else if (form.ButtonAction == "close")
                    {

                    }
                }
            }
        }

        private void ControllerSetupButton_Click(object sender, EventArgs e)
        {
            string[] tempDim = new string[] {"",
                "100", "95", "90", "85", "80", "75", "70", "65", "60", "55", 
                "50", "45", "40", "35", "30", "25", "20", "15", "10", "5", 
                "0"};

            string[] tempMaintenanceFactor = new string[] {"",
                "55", "66", "77", "88", "100", "100"};

            if (sender is BunifuFlatButton button)
            {
                using (var form = new FormControllerSetup())
                {
                    form.StartPosition = FormStartPosition.CenterParent;

                    form.ControllerIdx = Int32.Parse(button.Name.Remove(0, "controllerSetupButtonX".Length));
                    form.ControllerName = Properties.Settings.Default.ControllerName[form.ControllerIdx];

                    form.DimLevelValue.Clear();
                    form.DimLevelValue.AddRange(tempDim);

                    form.MaintenanceFactor.Clear();
                    form.MaintenanceFactor.AddRange(tempMaintenanceFactor);

                    form.ShowDialog();
                }
            }
        }

        private void DimmLevelChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputDimmLevel form)
            {
                DimmLevelValueReceivedFromController?.Invoke(form.DimLevelValue, e);
            }
        }

        private void MaintenanceFactorChagnedByUser(object sender, EventArgs e)
        {
            if (sender is FormInputMaintenanceFactor form)
            {
                MaintenanceFactorReceivedFromController?.Invoke(form.MaintenanceFactor, e);
            }
        }

        private void Parsing_7080H(byte[] values, int controllerIdx)
        {
            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7080 = new BitArray(temp); // B1

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7081 = new BitArray(temp); // B2

#if false
            temp = values.Skip(4).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7082 = new BitArray(temp); // B3

            App.RxValues.H7080B00 = H7080[0]; // Remote/Local 상태             0:Local, 1:Rem
            App.RxValues.H7080B01 = H7080[1]; // 화재 운전 상태                 1:화재운전

            App.RxValues.H7080B02 = H7080[2]; // 맑음/흐림등조도 자동운전 상태   1:조도운전
            if (App.RxValues.H7080B02)
                App.RxValues.IlLuminanceVisible = Visibility.Visible;

            App.RxValues.H7080B03 = H7080[3]; // 맑음/흐림등휘도 자동운전 상태   1:휘도운전
            if (App.RxValues.H7080B03)
                App.RxValues.LuminanceVisible = Visibility.Visible;

            App.RxValues.H7080B04 = H7080[4]; // 주간등조도/휘도운전모드 상태    1:조도/휘도운전
            App.RxValues.H7080B05 = H7080[5]; // 주간등 시간 운전모드 상태       1:시간운전
            App.RxValues.H7080B06 = H7080[6];  // Remote 수동운전 상태	        1:수동운전
            App.RxValues.H7080B07 = H7080[7];  // Local 수동운전 상태	        1:수동운전
#endif


#if false
            App.RxValues.H7081B00 = H7081[0]; // 상행 추월선 맑음등 상태
            App.RxValues.H7081B01 = H7081[1]; // 상행 주행선 맑음등 상태
            App.RxValues.H7081B02 = H7081[2]; // 상행 추월선 흐림등 상태
            App.RxValues.H7081B03 = H7081[3]; // 상행 주행선 흐림등 상태
            App.RxValues.H7081B04 = H7081[4]; // 상행 추월선 주간(일출일몰)등 상태
            App.RxValues.H7081B05 = H7081[5]; // 상행 주행선 주간(일출일몰)등 상태
            App.RxValues.H7081B06 = H7081[6];  // 상행 심야등 상태
            App.RxValues.H7081B07 = H7081[7];  // 상행 상시등 상태
            App.RxValues.H7081B08 = H7081[8];  // 하행 추월선 맑음등 상태
            App.RxValues.H7081B09 = H7081[9];  // 하행 주행선 맑음등 상태
            App.RxValues.H7081B10 = H7081[10];  // 하행 추월선 흐림등 상태
            App.RxValues.H7081B11 = H7081[11];  // 하행 주행선 흐림등 상태
            App.RxValues.H7081B12 = H7081[12];  // 하행 추월선 주간(일출일몰)등 상태
            App.RxValues.H7081B13 = H7081[13];  // 하행 주행선 주간(일출일몰)등 상태
            App.RxValues.H7081B14 = H7081[14];  // 하행 심야등 상태
            App.RxValues.H7081B15 = H7081[15];  // 하행 상시등 상태
#else
            H7081B00[controllerIdx].Text = H7081[0] ? "ON" : "OFF"; // 상행 추월선 맑음등 상태
            H7081B02[controllerIdx].Text = H7081[2] ? "ON" : "OFF"; // ? "ON" : "OFF" 상행 추월선 흐림등 상태
            H7081B04[controllerIdx].Text = H7081[4] ? "ON" : "OFF"; // 상행 추월선 주간(일출일몰)등 상태
            H7081B06[controllerIdx].Text = H7081[6] ? "ON" : "OFF";  // 상행 심야등 상태
            H7081B08[controllerIdx].Text = H7081[8] ? "ON" : "OFF";  // 하행 추월선 맑음등 상태
            H7081B10[controllerIdx].Text = H7081[10] ? "ON" : "OFF";  // 하행 추월선 흐림등 상태
            H7081B12[controllerIdx].Text = H7081[12] ? "ON" : "OFF";  // 하행 추월선 주간(일출일몰)등 상태
            H7081B14[controllerIdx].Text = H7081[14] ? "ON" : "OFF";  // 하행 심야등 상태
#endif

#if false
            App.RxValues.H7082B00 = H7082[0]; // 상행 가로등상시/입구부상태
            App.RxValues.H7082B01 = H7082[1]; // 상행 가로등격등/출구부상태
            App.RxValues.H7082B02 = H7082[2]; // 하행 가로등상시/입구부 상태
            App.RxValues.H7082B03 = H7082[3]; // 하행 가로등격등/출구부 상태
            App.RxValues.H7082B04 = H7082[4]; // 교통량 설계값 많음
            App.RxValues.H7082B05 = H7082[5]; // 교통량 설계값 보통
            App.RxValues.H7082B06 = H7082[6];  // 교통량 설계값 적음
            App.RxValues.H7082B07 = H7082[7];  // 교통량 실제값 많음
            App.RxValues.H7082B08 = H7082[8];  // 교통량 실제값 보통
            App.RxValues.H7082B09 = H7082[9];  // 교통량 실제값 적음

            //BitArray H7083 = new BitArray(values.Skip(6).Take(2).ToArray()); // B4
            //BitArray H7084 = new BitArray(values.Skip(8).Take(2).ToArray()); // B5
            //BitArray H7085 = new BitArray(values.Skip(10).Take(2).ToArray()); // B6
            //BitArray H7086 = new BitArray(values.Skip(12).Take(2).ToArray()); // B7
            //BitArray H7087 = new BitArray(values.Skip(14).Take(2).ToArray()); // B8
            //BitArray H7088 = new BitArray(values.Skip(16).Take(2).ToArray()); // B9
            //BitArray H7089 = new BitArray(values.Skip(18).Take(2).ToArray()); // B10
            //BitArray H708A = new BitArray(values.Skip(20).Take(2).ToArray()); // B11

            temp = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H708B = BitConverter.ToInt16(temp, 0); // 등기구 총수량

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H708C = BitConverter.ToInt16(temp, 0); // 터널등 수량 (100W)

            temp = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H708D = BitConverter.ToInt16(temp, 0); // 터널등 수량 (200W)

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H708E = BitConverter.ToInt16(temp, 0); // 가로등 수량 (100W)

            temp = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H708F = BitConverter.ToInt16(temp, 0); // 가로등 수량 (150W)

            temp = values.Skip(32).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7090 = BitConverter.ToInt16(temp, 0); // 가로등 수량 (250W)

            temp = values.Skip(34).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7091 = BitConverter.ToInt16(temp, 0); // 모뎀 불량 총수량

            temp = values.Skip(36).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7092 = BitConverter.ToInt16(temp, 0); // LED 모듈 불량 총수량

            temp = values.Skip(38).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7093 = BitConverter.ToInt16(temp, 0); // SMPS 불량 총수량

            temp = values.Skip(40).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7094 = BitConverter.ToInt16(temp, 0); // 등기구 누전 총 수량

            temp = values.Skip(42).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7095 = BitConverter.ToInt16(temp, 0); // 중계기 고장 총수량

            temp = values.Skip(44).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7096 = BitConverter.ToInt16(temp, 0); // 조명 제어기 장애 총수량

            temp = values.Skip(46).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7097 = BitConverter.ToInt16(temp, 0); // GPS 장애 총수량

            temp = values.Skip(48).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7098 = BitConverter.ToInt16(temp, 0); // 휘도 센서 장애 총수량

            temp = values.Skip(50).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H7099 = BitConverter.ToInt16(temp, 0); // 조도센서 장애 총수량
#endif
            temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
            H709A[controllerIdx].Text = BitConverter.ToInt16(temp, 0).ToString(); // 상행 맑음등디밍출력값 감시

            temp = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp);
            H709B[controllerIdx].Text = BitConverter.ToInt16(temp, 0).ToString(); // 상행 흐림등디밍출력값 감시

            temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
            H709C[controllerIdx].Text = BitConverter.ToInt16(temp, 0).ToString(); // 상행 주간(일출일몰)등 디밍출력값 감시

            temp = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp);
            H709D[controllerIdx].Text = BitConverter.ToInt16(temp, 0).ToString(); // 상행 심야등디밍출력값 감시
#if false
            temp = values.Skip(60).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709E = BitConverter.ToInt16(temp, 0); // 상행 가로등상시등디밍출력값 감시

            temp = values.Skip(62).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709F = BitConverter.ToInt16(temp, 0); // 상행 가로등격등디밍출력값 감시
#endif
        }

        private void Parsing_70A0H(byte[] values, int controllerIdx)
        {
            byte[] temp, temp1, temp2;

#if false
            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A0 = BitConverter.ToInt16(temp, 0); // 하행 맑음등디밍출력값 감시 

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A1 = BitConverter.ToInt16(temp, 0); // 하행 흐림등디밍출력값 감시

            temp = values.Skip(4).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A2 = BitConverter.ToInt16(temp, 0); // 하행 심야등디밍출력값 감시

            temp = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A3 = BitConverter.ToInt16(temp, 0); // 하행 주간(일출일몰)등 디밍출력값 감시

            temp = values.Skip(8).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A4 = BitConverter.ToInt16(temp, 0); // 하행 가로등상시등디밍출력값 감시

            temp = values.Skip(10).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A5 = BitConverter.ToInt16(temp, 0); // 하행 가로등격등디밍출력값 감시

            temp = values.Skip(12).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A6 = BitConverter.ToInt16(temp, 0); // 상행 디밍단계값

            temp = values.Skip(14).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H70A7 = BitConverter.ToInt16(temp, 0); // 하행 디밍단계값
#endif
            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            H70A870A9[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString(); // 상행 외부 조도 센서값 전송

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            H70AA70AB[controllerIdx].Text = BitConverter.ToSingle(temp2, 0).ToString(); // 상행 외부 휘도센서값 전송
#if false
            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70AC70AD = BitConverter.ToSingle(temp2, 0); // 하행 외부 조도 센서값 전송

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70AE70AF = BitConverter.ToSingle(temp2, 0); // 하행 외부 휘도센서값 전송
#endif
        }

        private void Parsing_73A0H(byte[] values, int controllerIdx)
        {
            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A0 = new BitArray(temp);

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A1 = new BitArray(temp);

#if false
            App.RxValues.H73A0B00 = H73A0[0]; // 통신이상
            App.RxValues.H73A1B00 = H73A1[0]; // CPU이상
            App.RxValues.H73A1B01 = H73A1[1]; // 파워모듈 이상

            //App.RxValues.H7080B01 = H73A1[1]; // 화재 운전 상태

            temp = values.Skip(6).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A3 = BitConverter.ToInt16(temp, 0); // 맑음등보수율

            temp = values.Skip(8).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A4 = BitConverter.ToInt16(temp, 0); // 흐림등보수율

            temp = values.Skip(10).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A5 = BitConverter.ToInt16(temp, 0); // 주간(일출일몰)등보수율

            temp = values.Skip(12).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A6 = BitConverter.ToInt16(temp, 0); // 심야/상시등보수율

            temp = values.Skip(14).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A7 = BitConverter.ToInt16(temp, 0); // 가로등 상시/입구부보수율

            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A8 = BitConverter.ToInt16(temp, 0); // 가로등격등/출구부보수율

            temp = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73A9 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AA = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AB = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AC = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AD = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AE = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73AF = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(32).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B0 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(34).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B1 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(36).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B2 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(38).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B3 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(40).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B4 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(42).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B5 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(44).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B6 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(46).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B7 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(48).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B8 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(50).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73B9 = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BA = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BB = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BC = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BD = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(60).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BE = BitConverter.ToInt16(temp, 0);

            temp = values.Skip(62).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H73BF = BitConverter.ToInt16(temp, 0);
#endif
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
