using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EXModbus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EXModebusMain : Window
    {
        private Master MBmaster;

        private DispatcherTimer ReqInterval;

        private int reqCnt;

        public EXModebusMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitBinding();

            ReqInterval = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 1000)
            };
            ReqInterval.Tick += ReqInterval_Tick;
        }

        private void ReqInterval_Tick(object sender, EventArgs e)
        {
            reqCnt++;
            if (reqCnt > 5)
            {
                App.Config.ConnStat = "접속끊김";
                App.Config.ConnStatBackgBrush = Brushes.Red;
                App.Config.ConnStatForgBrush = Brushes.Yellow;
            }
            //RequestNow();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void InitBinding()
        {
            gbConnect.DataContext = App.Config;

            gbClearCloudyCondition.DataContext = App.RxValues;

            gbMode.DataContext = App.RxValues;

            gbTunnelDimmingControlStat.DataContext = App.RxValues;

            gbDaylightOperatingState.DataContext = App.RxValues;

            gbUpLine.DataContext = App.RxValues;

            gbDownLine.DataContext = App.RxValues;

            gbMainController.DataContext = App.RxValues;

            gbConnectionStat.DataContext = App.Config;

            gbLightLossFactor.DataContext = App.RxValues;

            gbLuminanceLevel.DataContext = App.RxValues;

            gbIllLuminanceLevel.DataContext = App.RxValues;

            gbDaylightTime.DataContext = App.RxValues;

            gbStreetLampTime.DataContext = App.RxValues;

            bdStreetLampSet.DataContext = App.RxValues;
        }


        // ------------------------------------------------------------------------
        // Event for response data
        // ------------------------------------------------------------------------

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
            if (MBmaster.Connected == false)
                return;
            
            reqCnt = 0;

            App.Config.ConnStat = "정상접속중";
            App.Config.ConnStatBackgBrush = Brushes.Green;
            App.Config.ConnStatForgBrush = Brushes.AntiqueWhite;
            if (ID == 100)
            {
                Parsing_7080H(values);
                System.Threading.Thread.Sleep(500);
                MBmaster.ReadHoldingRegister(101, 1, 0x70A0, 16);
            }
            else if (ID == 101)
            {
                Parsing_70A0H(values);
                System.Threading.Thread.Sleep(500);
                MBmaster.ReadHoldingRegister(102, 1, 0x73A0, 32);
            }
            else if (ID == 102)
            {
                Parsing_73A0H(values);
                System.Threading.Thread.Sleep(500);
                MBmaster.ReadHoldingRegister(103, 1, 0x73C0, 32);
            }
            else if (ID == 103)
            {
                Parsing_73C0H(values);
                System.Threading.Thread.Sleep(500);
                MBmaster.ReadHoldingRegister(104, 1, 0x73E0, 24);
            }
            else if (ID == 104)
            {
                Parsing_73E0H(values);
                System.Threading.Thread.Sleep(500);
                MBmaster.ReadHoldingRegister(100, 1, 0x7080, 32); // 0x7080 ~ 0x709F
            }
        }

        private void Parsing_7080H(byte[] values)
        {
            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7080 = new BitArray(temp); // B1

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H7081 = new BitArray(temp); // B2

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
            App.RxValues.H7080B07 = H7080[7];  // Local 수동운전 상태	            1:수동운전

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

            temp = values.Skip(52).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709A = BitConverter.ToInt16(temp, 0); // 상행 맑음등디밍출력값 감시

            temp = values.Skip(54).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709B = BitConverter.ToInt16(temp, 0); // 상행 흐림등디밍출력값 감시

            temp = values.Skip(56).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709C = BitConverter.ToInt16(temp, 0); // 상행 주간(일출일몰)등 디밍출력값 감시

            temp = values.Skip(58).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709D = BitConverter.ToInt16(temp, 0); // 상행 심야등디밍출력값 감시

            temp = values.Skip(60).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709E = BitConverter.ToInt16(temp, 0); // 상행 가로등상시등디밍출력값 감시

            temp = values.Skip(62).Take(2).ToArray(); Array.Reverse(temp);
            App.RxValues.H709F = BitConverter.ToInt16(temp, 0); // 상행 가로등격등디밍출력값 감시
        }

        private void Parsing_70A0H(byte[] values)
        {
            byte[] temp, temp1, temp2;

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

            //temp = values.Skip(16).Take(2).ToArray();
            //Debug.WriteLine($"func: {temp[0]} {temp[1]}");
            //temp1 = values.Skip(18).Take(2).ToArray();
            //Debug.WriteLine($"func: {temp1[0]} {temp1[1]}");

            temp = values.Skip(16).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(18).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70A870A9 = BitConverter.ToSingle(temp2, 0); // 상행 외부 조도 센서값 전송

            temp = values.Skip(20).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(22).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70AA70AB = BitConverter.ToSingle(temp2, 0); // 상행 외부 휘도센서값 전송

            temp = values.Skip(24).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(26).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70AC70AD = BitConverter.ToSingle(temp2, 0); // 하행 외부 조도 센서값 전송

            temp = values.Skip(28).Take(2).ToArray(); Array.Reverse(temp);
            temp1 = values.Skip(30).Take(2).ToArray(); Array.Reverse(temp1);
            temp2 = temp.Concat(temp1).ToArray();
            App.RxValues.H70AE70AF = BitConverter.ToSingle(temp2, 0); // 하행 외부 휘도센서값 전송
        }

        private void Parsing_73A0H(byte[] values)
        {
            byte[] temp;

            temp = values.Skip(0).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A0 = new BitArray(temp);

            temp = values.Skip(2).Take(2).ToArray(); Array.Reverse(temp);
            BitArray H73A1 = new BitArray(temp);

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
        }

        private void Parsing_73C0H(byte[] values)
        {
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
        }

        private void Parsing_73E0H(byte[] values)
        {
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
        }


        // --------------
        // Command Define
        // --------------
        private void Connect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                MBmaster = new Master(App.Config.Ip, 502);

                MBmaster.OnResponseData += new Master.ResponseData(MBmaster_OnResponseData);

                MBmaster.OnException += new Master.ExceptionData(MBmaster_OnException);

                ReqInterval.Start();

                RequestNow();
            }
            catch (SystemException error)
            {
                MessageBox.Show(error.Message, "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RequestNow()
        {
            reqCnt++;
            if (reqCnt > 5)
            {
                App.Config.ConnStat = "접속끊김";
                App.Config.ConnStatBackgBrush = Brushes.Red;
                App.Config.ConnStatForgBrush = Brushes.Yellow;
            }
            MBmaster.ReadHoldingRegister(100, 1, 0x7080,  32); // 0x7080 ~ 0x709F
        }

        private void Connect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (MBmaster == null)
            {
                e.CanExecute = true;
                return;
            }

            if (MBmaster.Connected)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void Disconnect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MBmaster.Disconnect();

            ReqInterval.Stop();

            App.RxValues.TxFrmCnt = App.RxValues.RxFrmCnt = 0;

            App.Config.ConnStat = "접속끊김";
            App.Config.ConnStatBackgBrush = Brushes.Red;
            App.Config.ConnStatForgBrush = Brushes.Yellow;

            App.RxValues.SetDefaultValue();
        }

        private void Disconnect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (MBmaster == null)
            {
                e.CanExecute = false;
                return;
            }

            if (MBmaster.Connected)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void ResetCnt_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.RxValues.TxFrmCnt = App.RxValues.RxFrmCnt = 0;
        }

        private void ResetCnt_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClickToggleBtn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            byte[] temp = new byte[2];
            ushort address = 0;

            BitArray H7083 = new BitArray(16);
            BitArray H7084 = new BitArray(16);
            BitArray H7085 = new BitArray(16);
            BitArray H7086 = new BitArray(16);
            BitArray H7087 = new BitArray(16);
            BitArray H7088 = new BitArray(16);
            BitArray H7089 = new BitArray(16);

            if ((string)e.Parameter == "H7083B00" && App.RxValues.H7080B00I)
            {
                H7083[0] = true;
                H7083.CopyTo(temp, 0);
                address = 0x7083;
            }
            else if ((string)e.Parameter == "H7083B01" && App.RxValues.H7080B00)
            {
                H7083[1] = true;
                H7083.CopyTo(temp, 0);
                address = 0x7083;
            }
            else if ((string)e.Parameter == "H7085B04" && App.RxValues.H7080B06)
            {
                H7085[4] = true;
                H7085.CopyTo(temp, 0);
                address = 0x7085;
            }
            else if ((string)e.Parameter == "H7085B00" && App.RxValues.H7080B02)
            {
                H7085[0] = true;
                H7085.CopyTo(temp, 0);
                address = 0x7085;
            }
            else if ((string)e.Parameter == "H7085B01" && App.RxValues.H7080B03)
            {
                H7085[1] = true;
                H7085.CopyTo(temp, 0);
                address = 0x7085;
            }
            else if ((string)e.Parameter == "H7085B02" && App.RxValues.H7080B04)
            {
                H7085[2] = true;
                H7085.CopyTo(temp, 0);
                address = 0x7085;
            }
            else if ((string)e.Parameter == "H7085B03" && App.RxValues.H7080B05)
            {
                H7085[3] = true;
                H7085.CopyTo(temp, 0);
                address = 0x7085;
            }
            //상행 추월선 맑음등 ON 제어
            else if ((string)e.Parameter == "H7086B00" && App.RxValues.H7081B00)
            {
                H7086[0] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B01" && App.RxValues.H7081B00I)
            {
                H7086[1] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B02" && App.RxValues.H7081B01)
            {
                H7086[2] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B03" && App.RxValues.H7081B01I)
            {
                H7086[3] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B04" && App.RxValues.H7081B02)
            {
                H7086[4] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B05" && App.RxValues.H7081B02I)
            {
                H7086[5] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B06" && App.RxValues.H7081B03)
            {
                H7086[6] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B07" && App.RxValues.H7081B03I)
            {
                H7086[7] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B08" && App.RxValues.H7081B04)
            {
                H7086[8] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B09" && App.RxValues.H7081B04I)
            {
                H7086[9] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B10" && App.RxValues.H7081B05)
            {
                H7086[10] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B11" && App.RxValues.H7081B05I)
            {
                H7086[11] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B12" && App.RxValues.H7081B06)
            {
                H7086[12] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B13" && App.RxValues.H7081B06I)
            {
                H7086[13] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B14" && App.RxValues.H7081B07)
            {
                H7086[14] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            else if ((string)e.Parameter == "H7086B15" && App.RxValues.H7081B07I)
            {
                H7086[15] = true;
                H7086.CopyTo(temp, 0);
                address = 0x7086;
            }
            //하행 추월선 맑음등 ON 제어
            else if ((string)e.Parameter == "H7087B00" && App.RxValues.H7081B08)
            {
                H7087[0] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B01" && App.RxValues.H7081B08I)
            {
                H7087[1] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B02" && App.RxValues.H7081B09)
            {
                H7087[2] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B03" && App.RxValues.H7081B09I)
            {
                H7087[3] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B04" && App.RxValues.H7081B10)
            {
                H7087[4] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B05" && App.RxValues.H7081B10I)
            {
                H7087[5] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B06" && App.RxValues.H7081B11)
            {
                H7087[6] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B07" && App.RxValues.H7081B11I)
            {
                H7087[7] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B08" && App.RxValues.H7081B12)
            {
                H7087[8] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B09" && App.RxValues.H7081B12I)
            {
                H7087[9] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B10" && App.RxValues.H7081B13)
            {
                H7087[10] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B11" && App.RxValues.H7081B13I)
            {
                H7087[11] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B12" && App.RxValues.H7081B14)
            {
                H7087[12] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B13" && App.RxValues.H7081B14I)
            {
                H7087[13] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B14" && App.RxValues.H7081B15)
            {
                H7087[14] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            else if ((string)e.Parameter == "H7087B15" && App.RxValues.H7081B15I)
            {
                H7087[15] = true;
                H7087.CopyTo(temp, 0);
                address = 0x7087;
            }
            //상행 가로등 상시/입구부 ON 제어
            else if ((string)e.Parameter == "H7088B00" && App.RxValues.H7082B00)
            {
                H7088[0] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B01" && App.RxValues.H7082B00I)
            {
                H7088[1] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B02" && App.RxValues.H7082B01)
            {
                H7088[2] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B03" && App.RxValues.H7082B01I)
            {
                H7088[3] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B04" && App.RxValues.H7082B02)
            {
                H7088[4] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B05" && App.RxValues.H7082B02I)
            {
                H7088[5] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B06" && App.RxValues.H7082B03)
            {
                H7088[6] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }
            else if ((string)e.Parameter == "H7088B07" && App.RxValues.H7082B03I)
            {
                H7088[7] = true;
                H7088.CopyTo(temp, 0);
                address = 0x7088;
            }


            Array.Reverse(temp);
            MBmaster.WriteMultipleRegister(200, 1, address, temp);
        }

        private void ClickToggleBtn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (MBmaster?.Connected == true)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SendDataBtn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ushort address = 0;

            BitArray H7089 = new BitArray(16);
            BitArray H7084 = new BitArray(16);

            if ((string)e.Parameter == "H7084B00") // 전체 화재 상태 (발생)
            {
                byte[] temp = new byte[2];
                H7084[0] = true;
                H7084.CopyTo(temp, 0);
                address = 0x7084;
                Array.Reverse(temp);
                MBmaster.WriteMultipleRegister(201, 1, address, temp);
            }
            else if ((string)e.Parameter == "H7084B01") // 전체 화재 상태 (복구)
            {
                byte[] temp = new byte[2];
                H7084[1] = true;
                H7084.CopyTo(temp, 0);
                address = 0x7084;
                Array.Reverse(temp);
                MBmaster.WriteMultipleRegister(201, 1, address, temp);
            }
            else if ((string)e.Parameter == "H7089B00") //전체 점등 제어
            {
                byte[] temp = new byte[2];
                H7089[0] = true;
                H7089.CopyTo(temp, 0);
                address = 0x7089;
                Array.Reverse(temp);
                MBmaster.WriteMultipleRegister(201, 1, address, temp);
            }
            else if ((string)e.Parameter == "H7089B01") //전체 소등 제어
            {
                byte[] temp = new byte[2];
                H7089[1] = true;
                H7089.CopyTo(temp, 0);
                address = 0x7089;
                Array.Reverse(temp);
                MBmaster.WriteMultipleRegister(201, 1, address, temp);
            }
            else if ((string)e.Parameter == "H70B0") // 상행 디밍출력값 설정
            {
                byte[] temp = new byte[10];

                temp[0] = (byte)((App.RxValues.H70B0 & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.H70B0 & 0x00FF));

                temp[2] = (byte)((App.RxValues.H70B1 & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.H70B1 & 0x00FF));

                temp[4] = (byte)((App.RxValues.H70B2 & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.H70B2 & 0x00FF));

                temp[6] = (byte)((App.RxValues.H70B3 & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.H70B3 & 0x00FF));

                temp[8] = (byte)((App.RxValues.H70B4 & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.H70B4 & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x70B0, temp);
            }
            else if ((string)e.Parameter == "H70B6") // 하행 디밍출력값 설정
            {
                byte[] temp = new byte[10];

                temp[0] = (byte)((App.RxValues.H70B6 & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.H70B6 & 0x00FF));

                temp[2] = (byte)((App.RxValues.H70B7 & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.H70B7 & 0x00FF));

                temp[4] = (byte)((App.RxValues.H70B8 & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.H70B8 & 0x00FF));

                temp[6] = (byte)((App.RxValues.H70B9 & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.H70B9 & 0x00FF));

                temp[8] = (byte)((App.RxValues.H70BA & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.H70BA & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x70B6, temp);
            }
            else if ((string)e.Parameter == "H73A3") // 맑음등보수율 설정(%)
            {
                byte[] temp = new byte[12];

                temp[0] = (byte)((App.RxValues.SH73A3 & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.SH73A3 & 0x00FF));

                temp[2] = (byte)((App.RxValues.SH73A4 & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.SH73A4 & 0x00FF));

                temp[4] = (byte)((App.RxValues.SH73A5 & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.SH73A5 & 0x00FF));

                temp[6] = (byte)((App.RxValues.SH73A6 & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.SH73A6 & 0x00FF));

                temp[8] = (byte)((App.RxValues.SH73A7 & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.SH73A7 & 0x00FF));

                temp[10] = (byte)((App.RxValues.SH73A8 & 0xFF00) >> 8);
                temp[11] = (byte)((App.RxValues.SH73A8 & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x73A3, temp);
            }
            else if ((string)e.Parameter == "H73A9") // 1단계-맑음등1 휘도디밍값 (Cd/㎡)
            {
                byte[] temp = new byte[42];

                temp[0] = (byte)((App.RxValues.SH73A9 & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.SH73A9 & 0x00FF));

                temp[2] = (byte)((App.RxValues.SH73AA & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.SH73AA & 0x00FF));

                temp[4] = (byte)((App.RxValues.SH73AB & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.SH73AB & 0x00FF));

                temp[6] = (byte)((App.RxValues.SH73AC & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.SH73AC & 0x00FF));

                temp[8] = (byte)((App.RxValues.SH73AD & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.SH73AD & 0x00FF));

                temp[10] = (byte)((App.RxValues.SH73AE & 0xFF00) >> 8);
                temp[11] = (byte)((App.RxValues.SH73AE & 0x00FF));

                temp[12] = (byte)((App.RxValues.SH73AF & 0xFF00) >> 8);
                temp[13] = (byte)((App.RxValues.SH73AF & 0x00FF));

                temp[14] = (byte)((App.RxValues.SH73B0 & 0xFF00) >> 8);
                temp[15] = (byte)((App.RxValues.SH73B0 & 0x00FF));

                temp[16] = (byte)((App.RxValues.SH73B1 & 0xFF00) >> 8);
                temp[17] = (byte)((App.RxValues.SH73B1 & 0x00FF));

                temp[18] = (byte)((App.RxValues.SH73B2 & 0xFF00) >> 8);
                temp[19] = (byte)((App.RxValues.SH73B2 & 0x00FF));

                temp[20] = (byte)((App.RxValues.SH73B3 & 0xFF00) >> 8);
                temp[21] = (byte)((App.RxValues.SH73B3 & 0x00FF));

                temp[22] = (byte)((App.RxValues.SH73B4 & 0xFF00) >> 8);
                temp[23] = (byte)((App.RxValues.SH73B4 & 0x00FF));

                temp[24] = (byte)((App.RxValues.SH73B5 & 0xFF00) >> 8);
                temp[25] = (byte)((App.RxValues.SH73B5 & 0x00FF));

                temp[26] = (byte)((App.RxValues.SH73B6 & 0xFF00) >> 8);
                temp[27] = (byte)((App.RxValues.SH73B6 & 0x00FF));

                temp[28] = (byte)((App.RxValues.SH73B7 & 0xFF00) >> 8);
                temp[29] = (byte)((App.RxValues.SH73B7 & 0x00FF));

                temp[30] = (byte)((App.RxValues.SH73B8 & 0xFF00) >> 8);
                temp[31] = (byte)((App.RxValues.SH73B8 & 0x00FF));

                temp[32] = (byte)((App.RxValues.SH73B9 & 0xFF00) >> 8);
                temp[33] = (byte)((App.RxValues.SH73B9 & 0x00FF));

                temp[34] = (byte)((App.RxValues.SH73BA & 0xFF00) >> 8);
                temp[35] = (byte)((App.RxValues.SH73BA & 0x00FF));

                temp[36] = (byte)((App.RxValues.SH73BB & 0xFF00) >> 8);
                temp[37] = (byte)((App.RxValues.SH73BB & 0x00FF));

                temp[38] = (byte)((App.RxValues.SH73BC & 0xFF00) >> 8);
                temp[39] = (byte)((App.RxValues.SH73BC & 0x00FF));

                temp[40] = (byte)((App.RxValues.SH73BD & 0xFF00) >> 8);
                temp[41] = (byte)((App.RxValues.SH73BD & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x73A9, temp);
            }
            else if ((string)e.Parameter == "H73A3") // 주간(일출일몰)등 점등시간 설정(시) 
            {
                byte[] temp = new byte[16];

                temp[0] = (byte)((App.RxValues.SH73BF & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.SH73BF & 0x00FF));

                temp[2] = (byte)((App.RxValues.SH73C0 & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.SH73C0 & 0x00FF));

                temp[4] = (byte)((App.RxValues.SH73C1 & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.SH73C1 & 0x00FF));

                temp[6] = (byte)((App.RxValues.SH73C2 & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.SH73C2 & 0x00FF));

                temp[8] = (byte)((App.RxValues.SH73C3 & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.SH73C3 & 0x00FF));

                temp[10] = (byte)((App.RxValues.SH73C4 & 0xFF00) >> 8);
                temp[11] = (byte)((App.RxValues.SH73C4 & 0x00FF));

                temp[12] = (byte)((App.RxValues.SH73C5 & 0xFF00) >> 8);
                temp[13] = (byte)((App.RxValues.SH73C5 & 0x00FF));

                temp[14] = (byte)((App.RxValues.SH73C6 & 0xFF00) >> 8);
                temp[15] = (byte)((App.RxValues.SH73C6 & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x73BF, temp);
            }
            else if ((string)e.Parameter == "SH73C7") // 가로등 점등시간 설정(시)
            {
                byte[] temp = new byte[14];

                temp[0] = (byte)((App.RxValues.SH73C7 & 0xFF00) >> 8);
                temp[1] = (byte)((App.RxValues.SH73C7 & 0x00FF));

                temp[2] = (byte)((App.RxValues.SH73C8 & 0xFF00) >> 8);
                temp[3] = (byte)((App.RxValues.SH73C8 & 0x00FF));

                temp[4] = (byte)((App.RxValues.SH73C9 & 0xFF00) >> 8);
                temp[5] = (byte)((App.RxValues.SH73C9 & 0x00FF));

                temp[6] = (byte)((App.RxValues.SH73CA & 0xFF00) >> 8);
                temp[7] = (byte)((App.RxValues.SH73CA & 0x00FF));

                temp[8] = (byte)((App.RxValues.SH73CB & 0xFF00) >> 8);
                temp[9] = (byte)((App.RxValues.SH73CB & 0x00FF));

                temp[10] = (byte)((App.RxValues.SH73CC & 0xFF00) >> 8);
                temp[11] = (byte)((App.RxValues.SH73CC & 0x00FF));

                temp[12] = (byte)((App.RxValues.SH73CD & 0xFF00) >> 8);
                temp[13] = (byte)((App.RxValues.SH73CD & 0x00FF));

                MBmaster.WriteMultipleRegister(201, 1, 0x73C7, temp);
            }
            else if ((string)e.Parameter == "H73CE73CF") // 1단계-맑음등1 조도디밍값(lux)
            {
                byte[] temp = new byte[84];

                byte[] FloatToByte = BitConverter.GetBytes(App.RxValues.SH73CE73CF);
                temp[0] = FloatToByte[1];
                temp[1] = FloatToByte[0];
                temp[2] = FloatToByte[3];
                temp[3] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73D073D1);
                temp[4] = FloatToByte[1];
                temp[5] = FloatToByte[0];
                temp[6] = FloatToByte[3];
                temp[7] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73D273D3);
                temp[8] = FloatToByte[1];
                temp[9] = FloatToByte[0];
                temp[10] = FloatToByte[3];
                temp[11] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73D473D5);
                temp[12] = FloatToByte[1];
                temp[13] = FloatToByte[0];
                temp[14] = FloatToByte[3];
                temp[15] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73D673D7);
                temp[16] = FloatToByte[1];
                temp[17] = FloatToByte[0];
                temp[18] = FloatToByte[3];
                temp[19] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73D873D9);
                temp[20] = FloatToByte[1];
                temp[21] = FloatToByte[0];
                temp[22] = FloatToByte[3];
                temp[23] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73DA73DB);
                temp[24] = FloatToByte[1];
                temp[25] = FloatToByte[0];
                temp[26] = FloatToByte[3];
                temp[27] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73DC73DD);
                temp[28] = FloatToByte[1];
                temp[29] = FloatToByte[0];
                temp[30] = FloatToByte[3];
                temp[31] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73DE73DF);
                temp[32] = FloatToByte[1];
                temp[33] = FloatToByte[0];
                temp[34] = FloatToByte[3];
                temp[35] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73E073E1);
                temp[36] = FloatToByte[1];
                temp[37] = FloatToByte[0];
                temp[38] = FloatToByte[3];
                temp[39] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73E273E3);
                temp[40] = FloatToByte[1];
                temp[41] = FloatToByte[0];
                temp[42] = FloatToByte[3];
                temp[43] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73E473E5);
                temp[44] = FloatToByte[1];
                temp[45] = FloatToByte[0];
                temp[46] = FloatToByte[3];
                temp[47] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73E673E7);
                temp[48] = FloatToByte[1];
                temp[49] = FloatToByte[0];
                temp[50] = FloatToByte[3];
                temp[51] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73E873E9);
                temp[52] = FloatToByte[1];
                temp[53] = FloatToByte[0];
                temp[54] = FloatToByte[3];
                temp[55] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73EA73EB);
                temp[56] = FloatToByte[1];
                temp[57] = FloatToByte[0];
                temp[58] = FloatToByte[3];
                temp[59] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73EC73ED);
                temp[60] = FloatToByte[1];
                temp[61] = FloatToByte[0];
                temp[62] = FloatToByte[3];
                temp[63] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73EE73EF);
                temp[64] = FloatToByte[1];
                temp[65] = FloatToByte[0];
                temp[66] = FloatToByte[3];
                temp[67] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73F073F1);
                temp[68] = FloatToByte[1];
                temp[69] = FloatToByte[0];
                temp[70] = FloatToByte[3];
                temp[71] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73F273F3);
                temp[72] = FloatToByte[1];
                temp[73] = FloatToByte[0];
                temp[74] = FloatToByte[3];
                temp[75] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73F473F5);
                temp[76] = FloatToByte[1];
                temp[77] = FloatToByte[0];
                temp[78] = FloatToByte[3];
                temp[79] = FloatToByte[2];

                FloatToByte = BitConverter.GetBytes(App.RxValues.SH73F673F7);
                temp[80] = FloatToByte[1];
                temp[81] = FloatToByte[0];
                temp[82] = FloatToByte[3];
                temp[83] = FloatToByte[2];
                MBmaster.WriteMultipleRegister(201, 1, 0x73CE, temp);
            }
        }

        private void SendDataBtn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (MBmaster?.Connected == true)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            App.RxValues.H70B0 = App.RxValues.H709A;
            App.RxValues.H70B1 = App.RxValues.H709B;
            App.RxValues.H70B2 = App.RxValues.H709C;
            App.RxValues.H70B3 = App.RxValues.H709D;
            App.RxValues.H70B4 = App.RxValues.H709E;
            App.RxValues.H70B5 = App.RxValues.H709F;

            App.RxValues.H70B6 = App.RxValues.H70A0;
            App.RxValues.H70B7 = App.RxValues.H70A1;
            App.RxValues.H70B8 = App.RxValues.H70A2;
            App.RxValues.H70B9 = App.RxValues.H70A3;
            App.RxValues.H70BA = App.RxValues.H70A4;
            App.RxValues.H70BB = App.RxValues.H70A5;

            //맑음등보수율 설정
            App.RxValues.SH73A3 = App.RxValues.H73A3;
            App.RxValues.SH73A4 = App.RxValues.H73A4;
            App.RxValues.SH73A5 = App.RxValues.H73A5;
            App.RxValues.SH73A6 = App.RxValues.H73A6;
            App.RxValues.SH73A7 = App.RxValues.H73A7;
            App.RxValues.SH73A8 = App.RxValues.H73A8;

            //1단계-맑음등1 휘도디밍값 (Cd/㎡)
            App.RxValues.SH73A9 = App.RxValues.H73A9;
            App.RxValues.SH73AA = App.RxValues.H73AA;
            App.RxValues.SH73AB = App.RxValues.H73AB;
            App.RxValues.SH73AC = App.RxValues.H73AC;
            App.RxValues.SH73AD = App.RxValues.H73AD;
            App.RxValues.SH73AE = App.RxValues.H73AE;
            App.RxValues.SH73AF = App.RxValues.H73AF;
            App.RxValues.SH73B0 = App.RxValues.H73B0;
            App.RxValues.SH73B1 = App.RxValues.H73B1;
            App.RxValues.SH73B2 = App.RxValues.H73B2;
            App.RxValues.SH73B3 = App.RxValues.H73B3;
            App.RxValues.SH73B4 = App.RxValues.H73B4;
            App.RxValues.SH73B5 = App.RxValues.H73B5;
            App.RxValues.SH73B6 = App.RxValues.H73B6;
            App.RxValues.SH73B7 = App.RxValues.H73B7;
            App.RxValues.SH73B8 = App.RxValues.H73B8;
            App.RxValues.SH73B9 = App.RxValues.H73B9;
            App.RxValues.SH73BA = App.RxValues.H73BA;
            App.RxValues.SH73BB = App.RxValues.H73BB;
            App.RxValues.SH73BC = App.RxValues.H73BC;
            App.RxValues.SH73BD = App.RxValues.H73BD;

            // 주간(일출일몰)등 점등시간 
            App.RxValues.SH73BF = App.RxValues.H73BF;
            App.RxValues.SH73C0 = App.RxValues.H73C0;
            App.RxValues.SH73C1 = App.RxValues.H73C1;
            App.RxValues.SH73C2 = App.RxValues.H73C2;
            App.RxValues.SH73C3 = App.RxValues.H73C3;
            App.RxValues.SH73C4 = App.RxValues.H73C4;
            App.RxValues.SH73C5 = App.RxValues.H73C5;
            App.RxValues.SH73C6 = App.RxValues.H73C6;
            App.RxValues.SH73C7 = App.RxValues.H73C7;
            App.RxValues.SH73C8 = App.RxValues.H73C8;
            App.RxValues.SH73C9 = App.RxValues.H73C9;
            App.RxValues.SH73CA = App.RxValues.H73CA;
            App.RxValues.SH73CB = App.RxValues.H73CB;
            App.RxValues.SH73CC = App.RxValues.H73CC;
            App.RxValues.SH73CD = App.RxValues.H73CD;

            // 1단계-맑음등1 조도디밍값(lux)
            App.RxValues.SH73CE73CF = App.RxValues.H73CE73CF;
            App.RxValues.SH73D073D1 = App.RxValues.H73D073D1;
            App.RxValues.SH73D273D3 = App.RxValues.H73D273D3;
            App.RxValues.SH73D473D5 = App.RxValues.H73D473D5;
            App.RxValues.SH73D673D7 = App.RxValues.H73D673D7;
            App.RxValues.SH73D873D9 = App.RxValues.H73D873D9;
            App.RxValues.SH73DA73DB = App.RxValues.H73DA73DB;
            App.RxValues.SH73DC73DD = App.RxValues.H73DC73DD;
            App.RxValues.SH73DE73DF = App.RxValues.H73DE73DF;
            App.RxValues.SH73E073E1 = App.RxValues.H73E073E1;
            App.RxValues.SH73E273E3 = App.RxValues.H73E273E3;
            App.RxValues.SH73E473E5 = App.RxValues.H73E473E5;
            App.RxValues.SH73E673E7 = App.RxValues.H73E673E7;
            App.RxValues.SH73E873E9 = App.RxValues.H73E873E9;
            App.RxValues.SH73EA73EB = App.RxValues.H73EA73EB;
            App.RxValues.SH73EC73ED = App.RxValues.H73EC73ED;
            App.RxValues.SH73EE73EF = App.RxValues.H73EE73EF;
            App.RxValues.SH73F073F1 = App.RxValues.H73F073F1;
            App.RxValues.SH73F273F3 = App.RxValues.H73F273F3;
            App.RxValues.SH73F473F5 = App.RxValues.H73F473F5;
            App.RxValues.SH73F673F7 = App.RxValues.H73F673F7;
        }
    }
}
