using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace EXModbus
{
    public class RxValues : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetDefaultValue()
        {
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(this, String.Empty, null);
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(this, false, null);
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(this, 0, null);
                }
                else if (propertyInfo.PropertyType == typeof(float))
                {
                    propertyInfo.SetValue(this, 0, null);
                }
                else if (propertyInfo.PropertyType == typeof(Visibility))
                {
                    propertyInfo.SetValue(this, Visibility.Collapsed, null);
                }
            }

            H7080B00I = false;
        }

        private bool _H7080B00 = false;
        public bool H7080B00
        {
            get => _H7080B00;
            set
            {
                H7080B00I = !value;

                if (_H7080B00 != value)
                {
                    _H7080B00 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B00"));
                }
            }
        }

        private bool _H7080B00I = false;
        public bool H7080B00I
        {
            get => _H7080B00I;
            set
            {
                if (_H7080B00I != value)
                {
                    _H7080B00I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B00I"));
                }
            }
        }

        /// 화재운전 /////////////////////////////
        private bool _H7080B01;
        public bool H7080B01
        {
            get => _H7080B01;
            set
            {
                if (_H7080B01 != value)
                {
                    _H7080B01 = value;

                    if (value)
                    {
                        H7080B01Brush = Brushes.PaleVioletRed;
                    }
                    else
                    {
                        H7080B01Brush = Brushes.Gainsboro;
                    }
                }
            }
        }

        private Brush _H7080B01Brush = Brushes.Gainsboro;
        public Brush H7080B01Brush
        {
            get => _H7080B01Brush;
            set
            {
                if (_H7080B01Brush != value)
                {
                    _H7080B01Brush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B01Brush"));
                }
            }
        }


        private bool _H7080B02;
        public bool H7080B02
        {
            get => _H7080B02;
            set
            {
                if (_H7080B02 != value)
                {
                    _H7080B02 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B02"));
                }
            }
        }

        private bool _H7080B03;
        public bool H7080B03
        {
            get => _H7080B03;
            set
            {
                if (_H7080B03 != value)
                {
                    _H7080B03 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B03"));
                }
            }
        }

        private bool _H7080B04;
        public bool H7080B04
        {
            get => _H7080B04;
            set
            {
                if (_H7080B04 != value)
                {
                    _H7080B04 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B04"));
                }
            }
        }

        private bool _H7080B05;
        public bool H7080B05
        {
            get => _H7080B05;
            set
            {
                if (_H7080B05 != value)
                {
                    _H7080B05 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B05"));
                }
            }
        }

        private bool _H7080B06;
        public bool H7080B06
        {
            get => _H7080B06;
            set
            {
                if (_H7080B06 != value)
                {
                    _H7080B06 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B06"));
                }
            }
        }

        //// Local 수동운전 ///////////////////////////////////////
        private bool _H7080B07;
        public bool H7080B07
        {
            get => _H7080B07;
            set
            {
                if (_H7080B07 != value)
                {
                    _H7080B07 = value;

                    if (value)
                    {
                        H7080B07Brush = Brushes.PaleVioletRed;
                    }
                    else
                    {
                        H7080B07Brush = Brushes.Gainsboro;
                    }
                }
            }
        }

        private Brush _H7080B07Brush = Brushes.Gainsboro;
        public Brush H7080B07Brush
        {
            get => _H7080B07Brush;
            set
            {
                if (_H7080B07Brush != value)
                {
                    _H7080B07Brush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7080B07Brush"));
                }
            }
        }

        private bool _H7081B00;
        public bool H7081B00
        {
            get => _H7081B00;
            set
            {
                H7081B00I = !value;

                if (_H7081B00 != value)
                {
                    _H7081B00 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B00"));
                }
            }
        }

        private bool _H7081B00I = false;
        public bool H7081B00I
        {
            get => _H7081B00I;
            set
            {
                if (_H7081B00I != value)
                {
                    _H7081B00I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B00I"));
                }
            }
        }


        private bool _H7081B01;
        public bool H7081B01
        {
            get => _H7081B01;
            set
            {
                H7081B01I = !value;

                if (_H7081B01 != value)
                {
                    _H7081B01 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B01"));
                }
            }
        }

        private bool _H7081B01I = false;
        public bool H7081B01I
        {
            get => _H7081B01I;
            set
            {
                if (_H7081B01I != value)
                {
                    _H7081B01I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B01I"));
                }
            }
        }

        private bool _H7081B02;
        public bool H7081B02
        {
            get => _H7081B02;
            set
            {
                H7081B02I = !value;

                if (_H7081B02 != value)
                {
                    _H7081B02 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B02"));
                }
            }
        }

        private bool __H7081B02I = false;
        public bool H7081B02I
        {
            get => __H7081B02I;
            set
            {
                if (__H7081B02I != value)
                {
                    __H7081B02I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B02I"));
                }
            }
        }

        private bool _H7081B03;
        public bool H7081B03
        {
            get => _H7081B03;
            set
            {
                H7081B03I = !value;

                if (_H7081B03 != value)
                {
                    _H7081B03 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B03"));
                }
            }
        }

        private bool _H7081B03I = false;
        public bool H7081B03I
        {
            get => _H7081B03I;
            set
            {
                if (_H7081B03I != value)
                {
                    _H7081B03I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B03I"));
                }
            }
        }

        private bool _H7081B04;
        public bool H7081B04
        {
            get => _H7081B04;
            set
            {
                H7081B04I = !value;

                if (_H7081B04 != value)
                {
                    _H7081B04 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B04"));
                }
            }
        }

        private bool _H7081B04I = false;
        public bool H7081B04I
        {
            get => _H7081B04I;
            set
            {
                if (_H7081B04I != value)
                {
                    _H7081B04I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B04I"));
                }
            }
        }

        private bool _H7081B05;
        public bool H7081B05
        {
            get => _H7081B05;
            set
            {
                H7081B05I = !value;

                if (_H7081B05 != value)
                {
                    _H7081B05 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B05"));
                }
            }
        }

        private bool _H7081B05I = false;
        public bool H7081B05I
        {
            get => _H7081B05I;
            set
            {
                if (_H7081B05I != value)
                {
                    _H7081B05I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B05I"));
                }
            }
        }

        private bool _H7081B06;
        public bool H7081B06
        {
            get => _H7081B06;
            set
            {
                H7081B06I = !value;

                if (_H7081B06 != value)
                {
                    _H7081B06 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B06"));
                }
            }
        }

        private bool _H7081B06I = false;
        public bool H7081B06I
        {
            get => _H7081B06I;
            set
            {
                if (_H7081B06I != value)
                {
                    _H7081B06I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B06I"));
                }
            }
        }

        private bool _H7081B07;
        public bool H7081B07
        {
            get => _H7081B07;
            set
            {
                H7081B07I = !value;

                if (_H7081B07 != value)
                {
                    _H7081B07 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B07"));
                }
            }
        }

        private bool _H7081B07I = false;
        public bool H7081B07I
        {
            get => _H7081B07I;
            set
            {
                if (_H7081B07I != value)
                {
                    _H7081B07I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B07I"));
                }
            }
        }

        private bool _H7081B08;
        public bool H7081B08
        {
            get => _H7081B08;
            set
            {
                H7081B08I = !value;

                if (_H7081B08 != value)
                {
                    _H7081B08 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B08"));
                }
            }
        }

        private bool _H7081B08I = false;
        public bool H7081B08I
        {
            get => _H7081B08I;
            set
            {
                if (_H7081B08I != value)
                {
                    _H7081B08I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B08I"));
                }
            }
        }

        private bool _H7081B09;
        public bool H7081B09
        {
            get => _H7081B09;
            set
            {
                H7081B09I = !value;

                if (_H7081B09 != value)
                {
                    _H7081B09 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B09"));
                }
            }
        }

        private bool _H7081B09I = false;
        public bool H7081B09I
        {
            get => _H7081B09I;
            set
            {
                if (_H7081B09I != value)
                {
                    _H7081B09I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B09I"));
                }
            }
        }

        private bool _H7081B10;
        public bool H7081B10
        {
            get => _H7081B10;
            set
            {
                H7081B10I = !value;

                if (_H7081B10 != value)
                {
                    _H7081B10 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B10"));
                }
            }
        }

        private bool _H7081B10I = false;
        public bool H7081B10I
        {
            get => _H7081B10I;
            set
            {
                if (_H7081B10I != value)
                {
                    _H7081B10I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B10I"));
                }
            }
        }

        private bool _H7081B11;
        public bool H7081B11
        {
            get => _H7081B11;
            set
            {
                H7081B11I = !value;

                if (_H7081B11 != value)
                {
                    _H7081B11 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B11"));
                }
            }
        }

        private bool _H7081B11I = false;
        public bool H7081B11I
        {
            get => _H7081B11I;
            set
            {
                if (_H7081B11I != value)
                {
                    _H7081B11I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B11I"));
                }
            }
        }

        private bool _H7081B12;
        public bool H7081B12
        {
            get => _H7081B12;
            set
            {
                H7081B12I = !value;

                if (_H7081B12 != value)
                {
                    _H7081B12 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B12"));
                }
            }
        }

        private bool _H7081B12I = false;
        public bool H7081B12I
        {
            get => _H7081B12I;
            set
            {
                if (_H7081B12I != value)
                {
                    _H7081B12I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B12I"));
                }
            }
        }

        private bool _H7081B13;
        public bool H7081B13
        {
            get => _H7081B13;
            set
            {
                H7081B13I = !value;

                if (_H7081B13 != value)
                {
                    _H7081B13 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B13"));
                }
            }
        }

        private bool _H7081B13I = false;
        public bool H7081B13I
        {
            get => _H7081B13I;
            set
            {
                if (_H7081B13I != value)
                {
                    _H7081B13I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B13I"));
                }
            }
        }

        private bool _H7081B14;
        public bool H7081B14
        {
            get => _H7081B14;
            set
            {
                H7081B14I = !value;

                if (_H7081B14 != value)
                {
                    _H7081B14 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B14"));
                }
            }
        }

        private bool _H7081B14I = false;
        public bool H7081B14I
        {
            get => _H7081B14I;
            set
            {
                if (_H7081B14I != value)
                {
                    _H7081B14I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B14I"));
                }
            }
        }

        private bool _H7081B15;
        public bool H7081B15
        {
            get => _H7081B15;
            set
            {
                H7081B15I = !value;

                if (_H7081B15 != value)
                {
                    _H7081B15 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B15"));
                }
            }
        }

        private bool _H7081B15I = false;
        public bool H7081B15I
        {
            get => _H7081B15I;
            set
            {
                if (_H7081B15I != value)
                {
                    _H7081B15I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7081B15I"));
                }
            }
        }

        private bool _H7082B00;
        public bool H7082B00
        {
            get => _H7082B00;
            set
            {
                H7082B00I = !value;

                if (_H7082B00 != value)
                {
                    _H7082B00 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B00"));
                }
            }
        }

        private bool _H7082B00I = false;
        public bool H7082B00I
        {
            get => _H7082B00I;
            set
            {
                if (_H7082B00I != value)
                {
                    _H7082B00I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B00I"));
                }
            }
        }

        private bool _H7082B01;
        public bool H7082B01
        {
            get => _H7082B01;
            set
            {
                H7082B01I = !value;

                if (_H7082B01 != value)
                {
                    _H7082B01 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B01"));
                }
            }
        }

        private bool _H7082B01I = false;
        public bool H7082B01I
        {
            get => _H7082B01I;
            set
            {
                if (_H7082B01I != value)
                {
                    _H7082B01I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B01I"));
                }
            }
        }

        private bool _H7082B02;
        public bool H7082B02
        {
            get => _H7082B02;
            set
            {
                H7082B02I = !value;

                if (_H7082B02 != value)
                {
                    _H7082B02 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B02"));
                }
            }
        }

        private bool _H7082B02I = false;
        public bool H7082B02I
        {
            get => _H7082B02I;
            set
            {
                if (_H7082B02I != value)
                {
                    _H7082B02I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B02I"));
                }
            }
        }

        private bool _H7082B03;
        public bool H7082B03
        {
            get => _H7082B03;
            set
            {
                H7082B03I = !value;

                if (_H7082B03 != value)
                {
                    _H7082B03 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B03"));
                }
            }
        }

        private bool _H7082B03I = false;
        public bool H7082B03I
        {
            get => _H7082B03I;
            set
            {
                if (_H7082B03I != value)
                {
                    _H7082B03I = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7082B03I"));
                }
            }
        }

        private string _TrafficDesign;
        public string TrafficDesign
        {
            get => _TrafficDesign;
            set
            {
                if (_TrafficDesign != value)
                {
                    _TrafficDesign = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrafficDesign"));
                }
            }
        }

        private bool _H7082B04;
        public bool H7082B04
        {
            get => _H7082B04;
            set
            {
                if (value)
                {
                    TrafficDesign = "설계값 많음";
                }

                if (_H7082B04 != value)
                {
                    _H7082B04 = value;
                }
            }
        }

        private bool _H7082B05;
        public bool H7082B05
        {
            get => _H7082B05;
            set
            {
                if (value)
                {
                    TrafficDesign = "설계값 보통";
                }
                if (_H7082B05 != value)
                {
                    _H7082B05 = value;
                }
            }
        }

        private bool _H7082B06;
        public bool H7082B06
        {
            get => _H7082B06;
            set
            {
                if (value)
                {
                    TrafficDesign = "설계값 적음";
                }
                if (_H7082B06 != value)
                {
                    _H7082B06 = value;
                }
            }
        }

        private string _TrafficReal;
        public string TrafficReal
        {
            get => _TrafficReal;
            set
            {
                if (_TrafficReal != value)
                {
                    _TrafficReal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrafficReal"));
                }
            }
        }

        private bool _H7082B07;
        public bool H7082B07
        {
            get => _H7082B07;
            set
            {
                if (value)
                {
                    TrafficReal = "실제값 많음";
                }
                if (_H7082B07 != value)
                {
                    _H7082B07 = value;
                }
            }
        }

        private bool _H7082B08;
        public bool H7082B08
        {
            get => _H7082B08;
            set
            {
                if (value)
                {
                    TrafficReal = "실제값 보통";
                }
                if (_H7082B08 != value)
                {
                    _H7082B08 = value;
                }
            }
        }

        private bool _H7082B09;
        public bool H7082B09
        {
            get => _H7082B09;
            set
            {
                if (value)
                {
                    TrafficReal = "실제값 적음";
                }
                if (_H7082B09 != value)
                {
                    _H7082B09 = value;
                }
            }
        }


        private int _H708B;
        public int H708B
        {
            get => _H708B;
            set
            {
                if (_H708B != value)
                {
                    _H708B = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H708B"));
                }
            }
        }

        private int _H708C;
        public int H708C
        {
            get => _H708C;
            set
            {
                if (_H708C != value)
                {
                    _H708C = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H708C"));
                }
            }
        }

        private int _H708D;
        public int H708D
        {
            get => _H708D;
            set
            {
                if (_H708D != value)
                {
                    _H708D = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H708D"));
                }
            }
        }

        private int _H708E;
        public int H708E
        {
            get => _H708E;
            set
            {
                if (_H708E != value)
                {
                    _H708E = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H708E"));
                }
            }
        }

        private int _H708F;
        public int H708F
        {
            get => _H708F;
            set
            {
                if (_H708F != value)
                {
                    _H708F = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H708F"));
                }
            }
        }

        private int _H7090;
        public int H7090
        {
            get => _H7090;
            set
            {
                if (_H7090 != value)
                {
                    _H7090 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7090"));
                }
            }
        }

        private int _H7091;
        public int H7091
        {
            get => _H7091;
            set
            {
                if (_H7091 != value)
                {
                    _H7091 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7091"));
                }
            }
        }

        private int _H7092;
        public int H7092
        {
            get => _H7092;
            set
            {
                if (_H7092 != value)
                {
                    _H7092 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7092"));
                }
            }
        }

        private int _H7093;
        public int H7093
        {
            get => _H7093;
            set
            {
                if (_H7093 != value)
                {
                    _H7093 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7093"));
                }
            }
        }

        private int _H7094;
        public int H7094
        {
            get => _H7094;
            set
            {
                if (_H7094 != value)
                {
                    _H7094 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7094"));
                }
            }
        }

        private int _H7095;
        public int H7095
        {
            get => _H7095;
            set
            {
                if (_H7095 != value)
                {
                    _H7095 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7095"));
                }
            }
        }

        private int _H7096;
        public int H7096
        {
            get => _H7096;
            set
            {
                if (_H7096 != value)
                {
                    _H7096 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7096"));
                }
            }
        }

        private int _H7097;
        public int H7097
        {
            get => _H7097;
            set
            {
                if (_H7097 != value)
                {
                    _H7097 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7097"));
                }
            }
        }

        private int _H7098;
        public int H7098
        {
            get => _H7098;
            set
            {
                if (_H7098 != value)
                {
                    _H7098 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7098"));
                }
            }
        }

        private int _H7099;
        public int H7099
        {
            get => _H7099;
            set
            {
                if (_H7099 != value)
                {
                    _H7099 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H7099"));
                }
            }
        }

        private int _H709A;
        public int H709A
        {
            get => _H709A;
            set
            {
                if (_H709A != value)
                {
                    _H709A = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709A"));
                }
            }
        }

        private int _H709B;
        public int H709B
        {
            get => _H709B;
            set
            {
                if (_H709B != value)
                {
                    _H709B = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709B"));
                }
            }
        }

        private int _H709C;
        public int H709C
        {
            get => _H709C;
            set
            {
                if (_H709C != value)
                {
                    _H709C = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709C"));
                }
            }
        }

        private int _H709D;
        public int H709D
        {
            get => _H709D;
            set
            {
                if (_H709D != value)
                {
                    _H709D = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709D"));
                }
            }
        }

        private int _H709E;
        public int H709E
        {
            get => _H709E;
            set
            {
                if (_H709E != value)
                {
                    _H709E = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709E"));
                }
            }
        }

        private int _H709F;
        public int H709F
        {
            get => _H709F;
            set
            {
                if (_H709F != value)
                {
                    _H709F = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H709F"));
                }
            }
        }

        private int _H70A0;
        public int H70A0
        {
            get => _H70A0;
            set
            {
                if (_H70A0 != value)
                {
                    _H70A0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A0"));
                }
            }
        }

        private int _H70A1;
        public int H70A1
        {
            get => _H70A1;
            set
            {
                if (_H70A1 != value)
                {
                    _H70A1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A1"));
                }
            }
        }

        private int _H70A2;
        public int H70A2
        {
            get => _H70A2;
            set
            {
                if (_H70A2 != value)
                {
                    _H70A2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A2"));
                }
            }
        }

        private int _H70A3;
        public int H70A3
        {
            get => _H70A3;
            set
            {
                if (_H70A3 != value)
                {
                    _H70A3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A3"));
                }
            }
        }

        private int _H70A4;
        public int H70A4
        {
            get => _H70A4;
            set
            {
                if (_H70A4 != value)
                {
                    _H70A4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A4"));
                }
            }
        }

        private int _H70A5;
        public int H70A5
        {
            get => _H70A5;
            set
            {
                if (_H70A5 != value)
                {
                    _H70A5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A5"));
                }
            }
        }

        private int _H70A6;
        public int H70A6
        {
            get => _H70A6;
            set
            {
                if (_H70A6 != value)
                {
                    _H70A6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A6"));
                }
            }
        }

        private int _H70A7;
        public int H70A7
        {
            get => _H70A7;
            set
            {
                if (_H70A7 != value)
                {
                    _H70A7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A7"));
                }
            }
        }

        private float _H70A870A9;
        public float H70A870A9
        {
            get => _H70A870A9;
            set
            {
                if (_H70A870A9 != value)
                {
                    _H70A870A9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70A870A9"));
                }
            }
        }

        private float _H70AA70AB;
        public float H70AA70AB
        {
            get => _H70AA70AB;
            set
            {
                if (_H70AA70AB != value)
                {
                    _H70AA70AB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70AA70AB"));
                }
            }
        }

        private float _H70AC70AD;
        public float H70AC70AD
        {
            get => _H70AC70AD;
            set
            {
                if (_H70AC70AD != value)
                {
                    _H70AC70AD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70AC70AD"));
                }
            }
        }

        private float _H70AE70AF;
        public float H70AE70AF
        {
            get => _H70AE70AF;
            set
            {
                if (_H70AE70AF != value)
                {
                    _H70AE70AF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70AE70AF"));
                }
            }
        }

        ///////////////////////// 통신이상
        private bool _H73A0B00;
        public bool H73A0B00
        {
            get => _H73A0B00;
            set
            {
                if (_H73A0B00 != value)
                {
                    _H73A0B00 = value;
                    if (value)
                    {
                        H73A0B00Brush = Brushes.PaleVioletRed;
                    }
                    else
                    {
                        H73A0B00Brush = Brushes.Gainsboro;
                    }
                }
            }
        }

        private Brush _H73A0B00Brush = Brushes.Gainsboro;
        public Brush H73A0B00Brush
        {
            get => _H73A0B00Brush;
            set
            {
                if (_H73A0B00Brush != value)
                {
                    _H73A0B00Brush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A0B00Brush"));
                }
            }
        }



        //////////////////////// CPU이상
        private bool _H73A1B00;
        public bool H73A1B00
        {
            get => _H73A1B00;
            set
            {
                if (_H73A1B00 != value)
                {
                    _H73A1B00 = value;

                    if (value)
                    {
                        H73A1B00Brush = Brushes.PaleVioletRed;
                    }
                    else
                    {
                        H73A1B00Brush = Brushes.Gainsboro;
                    }
                }
            }
        }

        private Brush _H73A1B00Brush = Brushes.Gainsboro;
        public Brush H73A1B00Brush
        {
            get => _H73A1B00Brush;
            set
            {
                if (_H73A1B00Brush != value)
                {
                    _H73A1B00Brush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A1B00Brush"));
                }
            }
        }

        /////////////////// 파워모듈 이상
        private bool _H73A1B01;
        public bool H73A1B01
        {
            get => _H73A1B01;
            set
            {
                if (_H73A1B01 != value)
                {
                    _H73A1B01 = value;

                    if (value)
                    {
                        H73A1B01Brush = Brushes.PaleVioletRed;
                    }
                    else
                    {
                        H73A1B01Brush = Brushes.Gainsboro;
                    }
                }
            }
        }

        private Brush _H73A1B01Brush = Brushes.Gainsboro;
        public Brush H73A1B01Brush
        {
            get => _H73A1B01Brush;
            set
            {
                if (_H73A1B01Brush != value)
                {
                    _H73A1B01Brush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A1B01Brush"));
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////
        //맑음등보수율 설정(%)
        private int _H73A3;
        public int H73A3
        {
            get => _H73A3;
            set
            {
                if (_H73A3 != value)
                {
                    _H73A3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A3"));
                }
            }
        }

        private int _H73A4;
        public int H73A4
        {
            get => _H73A4;
            set
            {
                if (_H73A4 != value)
                {
                    _H73A4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A4"));
                }
            }
        }

        private int _H73A5;
        public int H73A5
        {
            get => _H73A5;
            set
            {
                if (_H73A5 != value)
                {
                    _H73A5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A5"));
                }
            }
        }

        private int _H73A6;
        public int H73A6
        {
            get => _H73A6;
            set
            {
                if (_H73A6 != value)
                {
                    _H73A6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A6"));
                }
            }
        }

        private int _H73A7;
        public int H73A7
        {
            get => _H73A7;
            set
            {
                if (_H73A7 != value)
                {
                    _H73A7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A7"));
                }
            }
        }

        private int _H73A8;
        public int H73A8
        {
            get => _H73A8;
            set
            {
                if (_H73A8 != value)
                {
                    _H73A8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A8"));
                }
            }
        }

        private int _SH73A3;
        public int SH73A3
        {
            get => _SH73A3;
            set
            {
                if (_SH73A3 != value)
                {
                    _SH73A3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A3"));
                }
            }
        }

        private int _SH73A4;
        public int SH73A4
        {
            get => _SH73A4;
            set
            {
                if (_SH73A4 != value)
                {
                    _SH73A4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A4"));
                }
            }
        }

        private int _SH73A5;
        public int SH73A5
        {
            get => _SH73A5;
            set
            {
                if (_SH73A5 != value)
                {
                    _SH73A5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A5"));
                }
            }
        }

        private int _SH73A6;
        public int SH73A6
        {
            get => _SH73A6;
            set
            {
                if (_SH73A6 != value)
                {
                    _SH73A6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A6"));
                }
            }
        }

        private int _SH73A7;
        public int SH73A7
        {
            get => _SH73A7;
            set
            {
                if (_SH73A7 != value)
                {
                    _SH73A7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A7"));
                }
            }
        }

        private int _SH73A8;
        public int SH73A8
        {
            get => _SH73A8;
            set
            {
                if (_SH73A8 != value)
                {
                    _SH73A8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A8"));
                }
            }
        }


        //1단계-맑음등1 휘도디밍값 (Cd/㎡)
        private int _H73A9;
        public int H73A9
        {
            get => _H73A9;
            set
            {
                if (_H73A9 != value)
                {
                    _H73A9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73A9"));
                }
            }
        }

        private int _H73AA;
        public int H73AA
        {
            get => _H73AA;
            set
            {
                if (_H73AA != value)
                {
                    _H73AA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AA"));
                }
            }
        }

        private int _H73AB;
        public int H73AB
        {
            get => _H73AB;
            set
            {
                if (_H73AB != value)
                {
                    _H73AB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AB"));
                }
            }
        }

        private int _H73AC;
        public int H73AC
        {
            get => _H73AC;
            set
            {
                if (_H73AC != value)
                {
                    _H73AC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AC"));
                }
            }
        }

        private int _H73AD;
        public int H73AD
        {
            get => _H73AD;
            set
            {
                if (_H73AD != value)
                {
                    _H73AD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AD"));
                }
            }
        }

        private int _H73AE;
        public int H73AE
        {
            get => _H73AE;
            set
            {
                if (_H73AE != value)
                {
                    _H73AE = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AE"));
                }
            }
        }

        private int _H73AF;
        public int H73AF
        {
            get => _H73AF;
            set
            {
                if (_H73AF != value)
                {
                    _H73AF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73AF"));
                }
            }
        }

        private int _H73B0;
        public int H73B0
        {
            get => _H73B0;
            set
            {
                if (_H73B0 != value)
                {
                    _H73B0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B0"));
                }
            }
        }

        private int _H73B1;
        public int H73B1
        {
            get => _H73B1;
            set
            {
                if (_H73B1 != value)
                {
                    _H73B1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B1"));
                }
            }
        }

        private int _H73B2;
        public int H73B2
        {
            get => _H73B2;
            set
            {
                if (_H73B2 != value)
                {
                    _H73B2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B2"));
                }
            }
        }

        private int _H73B3;
        public int H73B3
        {
            get => _H73B3;
            set
            {
                if (_H73B3 != value)
                {
                    _H73B3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B3"));
                }
            }
        }

        private int _H73B4;
        public int H73B4
        {
            get => _H73B4;
            set
            {
                if (_H73B4 != value)
                {
                    _H73B4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B4"));
                }
            }
        }

        private int _H73B5;
        public int H73B5
        {
            get => _H73B5;
            set
            {
                if (_H73B5 != value)
                {
                    _H73B5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B5"));
                }
            }
        }

        private int _H73B6;
        public int H73B6
        {
            get => _H73B6;
            set
            {
                if (_H73B6 != value)
                {
                    _H73B6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B6"));
                }
            }
        }

        private int _H73B7;
        public int H73B7
        {
            get => _H73B7;
            set
            {
                if (_H73B7 != value)
                {
                    _H73B7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B7"));
                }
            }
        }

        private int _H73B8;
        public int H73B8
        {
            get => _H73B8;
            set
            {
                if (_H73B8 != value)
                {
                    _H73B8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B8"));
                }
            }
        }

        private int _H73B9;
        public int H73B9
        {
            get => _H73B9;
            set
            {
                if (_H73B9 != value)
                {
                    _H73B9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73B9"));
                }
            }
        }

        private int _H73BA;
        public int H73BA
        {
            get => _H73BA;
            set
            {
                if (_H73BA != value)
                {
                    _H73BA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BA"));
                }
            }
        }

        private int _H73BB;
        public int H73BB
        {
            get => _H73BB;
            set
            {
                if (_H73BB != value)
                {
                    _H73BB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BB"));
                }
            }
        }

        private int _H73BC;
        public int H73BC
        {
            get => _H73BC;
            set
            {
                if (_H73BC != value)
                {
                    _H73BC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BC"));
                }
            }
        }

        private int _H73BD;
        public int H73BD
        {
            get => _H73BD;
            set
            {
                if (_H73BD != value)
                {
                    _H73BD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BD"));
                }
            }
        }


        private int _SH73A9;
        public int SH73A9
        {
            get => _SH73A9;
            set
            {
                if (_SH73A9 != value)
                {
                    _SH73A9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73A9"));
                }
            }
        }

        private int _SH73AA;
        public int SH73AA
        {
            get => _SH73AA;
            set
            {
                if (_SH73AA != value)
                {
                    _SH73AA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AA"));
                }
            }
        }

        private int _SH73AB;
        public int SH73AB
        {
            get => _SH73AB;
            set
            {
                if (_SH73AB != value)
                {
                    _SH73AB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AB"));
                }
            }
        }

        private int _SH73AC;
        public int SH73AC
        {
            get => _SH73AC;
            set
            {
                if (_SH73AC != value)
                {
                    _SH73AC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AC"));
                }
            }
        }

        private int _SH73AD;
        public int SH73AD
        {
            get => _SH73AD;
            set
            {
                if (_SH73AD != value)
                {
                    _SH73AD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AD"));
                }
            }
        }

        private int _SH73AE;
        public int SH73AE
        {
            get => _SH73AE;
            set
            {
                if (_SH73AE != value)
                {
                    _SH73AE = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AE"));
                }
            }
        }

        private int _SH73AF;
        public int SH73AF
        {
            get => _SH73AF;
            set
            {
                if (_SH73AF != value)
                {
                    _SH73AF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73AF"));
                }
            }
        }

        private int _SH73B0;
        public int SH73B0
        {
            get => _SH73B0;
            set
            {
                if (_SH73B0 != value)
                {
                    _SH73B0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B0"));
                }
            }
        }

        private int _SH73B1;
        public int SH73B1
        {
            get => _SH73B1;
            set
            {
                if (_SH73B1 != value)
                {
                    _SH73B1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B1"));
                }
            }
        }

        private int _SH73B2;
        public int SH73B2
        {
            get => _SH73B2;
            set
            {
                if (_SH73B2 != value)
                {
                    _SH73B2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B2"));
                }
            }
        }

        private int _SH73B3;
        public int SH73B3
        {
            get => _SH73B3;
            set
            {
                if (_SH73B3 != value)
                {
                    _SH73B3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B3"));
                }
            }
        }

        private int _SH73B4;
        public int SH73B4
        {
            get => _SH73B4;
            set
            {
                if (_SH73B4 != value)
                {
                    _SH73B4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B4"));
                }
            }
        }

        private int _SH73B5;
        public int SH73B5
        {
            get => _SH73B5;
            set
            {
                if (_SH73B5 != value)
                {
                    _SH73B5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B5"));
                }
            }
        }

        private int _SH73B6;
        public int SH73B6
        {
            get => _SH73B6;
            set
            {
                if (_SH73B6 != value)
                {
                    _SH73B6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B6"));
                }
            }
        }

        private int _SH73B7;
        public int SH73B7
        {
            get => _SH73B7;
            set
            {
                if (_SH73B7 != value)
                {
                    _SH73B7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B7"));
                }
            }
        }

        private int _SH73B8;
        public int SH73B8
        {
            get => _SH73B8;
            set
            {
                if (_SH73B8 != value)
                {
                    _SH73B8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B8"));
                }
            }
        }

        private int _SH73B9;
        public int SH73B9
        {
            get => _SH73B9;
            set
            {
                if (_SH73B9 != value)
                {
                    _SH73B9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73B9"));
                }
            }
        }

        private int _SH73BA;
        public int SH73BA
        {
            get => _SH73BA;
            set
            {
                if (_SH73BA != value)
                {
                    _SH73BA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73BA"));
                }
            }
        }

        private int _SH73BB;
        public int SH73BB
        {
            get => _SH73BB;
            set
            {
                if (_SH73BB != value)
                {
                    _SH73BB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73BB"));
                }
            }
        }

        private int _SH73BC;
        public int SH73BC
        {
            get => _SH73BC;
            set
            {
                if (_SH73BC != value)
                {
                    _SH73BC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73BC"));
                }
            }
        }

        private int _SH73BD;
        public int SH73BD
        {
            get => _SH73BD;
            set
            {
                if (_SH73BD != value)
                {
                    _SH73BD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73BD"));
                }
            }
        }

        // 휘도/조도센서 지속 시간 설정
        private int _H73BE;
        public int H73BE
        {
            get => _H73BE;
            set
            {
                if (_H73BE != value)
                {
                    _H73BE = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BE"));
                }
            }
        }

        // 주간(일출일몰)등 점등시간 
        private int _H73BF;
        public int H73BF
        {
            get => _H73BF;
            set
            {
                if (_H73BF != value)
                {
                    _H73BF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73BF"));
                }
            }
        }

        private int _H73C0;
        public int H73C0
        {
            get => _H73C0;
            set
            {
                if (_H73C0 != value)
                {
                    _H73C0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C0"));
                }
            }
        }

        private int _H73C1;
        public int H73C1
        {
            get => _H73C1;
            set
            {
                if (_H73C1 != value)
                {
                    _H73C1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C1"));
                }
            }
        }

        private int _H73C2;
        public int H73C2
        {
            get => _H73C2;
            set
            {
                if (_H73C2 != value)
                {
                    _H73C2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C2"));
                }
            }
        }

        private int _H73C3;
        public int H73C3
        {
            get => _H73C3;
            set
            {
                if (_H73C3 != value)
                {
                    _H73C3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C3"));
                }
            }
        }

        private int _H73C4;
        public int H73C4
        {
            get => _H73C4;
            set
            {
                if (_H73C4 != value)
                {
                    _H73C4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C4"));
                }
            }
        }

        private int _H73C5;
        public int H73C5
        {
            get => _H73C5;
            set
            {
                if (_H73C5 != value)
                {
                    _H73C5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C5"));
                }
            }
        }

        private int _H73C6;
        public int H73C6
        {
            get => _H73C6;
            set
            {
                if (_H73C6 != value)
                {
                    _H73C6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C6"));
                }
            }
        }

        private int _H73C7;
        public int H73C7
        {
            get => _H73C7;
            set
            {
                if (_H73C7 != value)
                {
                    _H73C7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C7"));
                }
            }
        }

        private int _H73C8;
        public int H73C8
        {
            get => _H73C8;
            set
            {
                if (_H73C8 != value)
                {
                    _H73C8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C8"));
                }
            }
        }

        private int _H73C9;
        public int H73C9
        {
            get => _H73C9;
            set
            {
                if (_H73C9 != value)
                {
                    _H73C9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73C9"));
                }
            }
        }

        private int _H73CA;
        public int H73CA
        {
            get => _H73CA;
            set
            {
                if (_H73CA != value)
                {
                    _H73CA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73CA"));
                }
            }
        }

        private int _H73CB;
        public int H73CB
        {
            get => _H73CB;
            set
            {
                if (_H73CB != value)
                {
                    _H73CB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73CB"));
                }
            }
        }

        private int _H73CC;
        public int H73CC
        {
            get => _H73CC;
            set
            {
                if (_H73CC != value)
                {
                    _H73CC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73CC"));
                }
            }
        }

        private int _H73CD;
        public int H73CD
        {
            get => _H73CD;
            set
            {
                if (_H73CD != value)
                {
                    _H73CD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73CD"));
                }
            }
        }


        private int _SH73BF;
        public int SH73BF
        {
            get => _SH73BF;
            set
            {
                if (_SH73BF != value)
                {
                    _SH73BF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73BF"));
                }
            }
        }

        private int _SH73C0;
        public int SH73C0
        {
            get => _SH73C0;
            set
            {
                if (_SH73C0 != value)
                {
                    _SH73C0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C0"));
                }
            }
        }

        private int _SH73C1;
        public int SH73C1
        {
            get => _SH73C1;
            set
            {
                if (_SH73C1 != value)
                {
                    _SH73C1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C1"));
                }
            }
        }

        private int _SH73C2;
        public int SH73C2
        {
            get => _SH73C2;
            set
            {
                if (_SH73C2 != value)
                {
                    _SH73C2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C2"));
                }
            }
        }

        private int _SH73C3;
        public int SH73C3
        {
            get => _SH73C3;
            set
            {
                if (_SH73C3 != value)
                {
                    _SH73C3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C3"));
                }
            }
        }

        private int _SH73C4;
        public int SH73C4
        {
            get => _SH73C4;
            set
            {
                if (_SH73C4 != value)
                {
                    _SH73C4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C4"));
                }
            }
        }

        private int _SH73C5;
        public int SH73C5
        {
            get => _SH73C5;
            set
            {
                if (_SH73C5 != value)
                {
                    _SH73C5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C5"));
                }
            }
        }

        private int _SH73C6;
        public int SH73C6
        {
            get => _SH73C6;
            set
            {
                if (_SH73C6 != value)
                {
                    _SH73C6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C6"));
                }
            }
        }

        private int _SH73C7;
        public int SH73C7
        {
            get => _SH73C7;
            set
            {
                if (_SH73C7 != value)
                {
                    _SH73C7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C7"));
                }
            }
        }

        private int _SH73C8;
        public int SH73C8
        {
            get => _SH73C8;
            set
            {
                if (_SH73C8 != value)
                {
                    _SH73C8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C8"));
                }
            }
        }

        private int _SH73C9;
        public int SH73C9
        {
            get => _SH73C9;
            set
            {
                if (_SH73C9 != value)
                {
                    _SH73C9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73C9"));
                }
            }
        }

        private int _SH73CA;
        public int SH73CA
        {
            get => _SH73CA;
            set
            {
                if (_SH73CA != value)
                {
                    _SH73CA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73CA"));
                }
            }
        }

        private int _SH73CB;
        public int SH73CB
        {
            get => _SH73CB;
            set
            {
                if (_SH73CB != value)
                {
                    _SH73CB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73CB"));
                }
            }
        }

        private int _SH73CC;
        public int SH73CC
        {
            get => _SH73CC;
            set
            {
                if (_SH73CC != value)
                {
                    _SH73CC = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73CC"));
                }
            }
        }

        private int _SH73CD;
        public int SH73CD
        {
            get => _SH73CD;
            set
            {
                if (_SH73CD != value)
                {
                    _SH73CD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73CD"));
                }
            }
        }




        // 송신 frame count
        private int _TxFrmCnt;
        public int TxFrmCnt
        {
            get => _TxFrmCnt;
            set
            {
                if (_TxFrmCnt != value)
                {
                    _TxFrmCnt = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TxFrmCnt"));
                }
            }
        }

        // 수신 frame count
        private int _RxFrmCnt;
        public int RxFrmCnt
        {
            get => _RxFrmCnt;
            set
            {
                if (_RxFrmCnt != value)
                {
                    _RxFrmCnt = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RxFrmCnt"));
                }
            }
        }


        // 상행 맑음등디밍출력값
        private int _H70B0;
        public int H70B0
        {
            get => _H70B0;
            set
            {
                if (_H70B0 != value)
                {
                    _H70B0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B0"));
                }
            }
        }

        private int _H70B1;
        public int H70B1
        {
            get => _H70B1;
            set
            {
                if (_H70B1 != value)
                {
                    _H70B1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B1"));
                }
            }
        }

        private int _H70B2;
        public int H70B2
        {
            get => _H70B2;
            set
            {
                if (_H70B2 != value)
                {
                    _H70B2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B2"));
                }
            }
        }

        private int _H70B3;
        public int H70B3
        {
            get => _H70B3;
            set
            {
                if (_H70B3 != value)
                {
                    _H70B3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B3"));
                }
            }
        }

        private int _H70B4;
        public int H70B4
        {
            get => _H70B4;
            set
            {
                if (_H70B4 != value)
                {
                    _H70B4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B4"));
                }
            }
        }

        private int _H70B5;
        public int H70B5
        {
            get => _H70B5;
            set
            {
                if (_H70B5 != value)
                {
                    _H70B5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B5"));
                }
            }
        }

        private int _H70B6;
        public int H70B6
        {
            get => _H70B6;
            set
            {
                if (_H70B6 != value)
                {
                    _H70B6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B6"));
                }
            }
        }

        private int _H70B7;
        public int H70B7
        {
            get => _H70B7;
            set
            {
                if (_H70B7 != value)
                {
                    _H70B7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B7"));
                }
            }
        }

        private int _H70B8;
        public int H70B8
        {
            get => _H70B8;
            set
            {
                if (_H70B8 != value)
                {
                    _H70B8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B8"));
                }
            }
        }

        private int _H70B9;
        public int H70B9
        {
            get => _H70B9;
            set
            {
                if (_H70B9 != value)
                {
                    _H70B9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70B9"));
                }
            }
        }

        private int _H70BA;
        public int H70BA
        {
            get => _H70BA;
            set
            {
                if (_H70BA != value)
                {
                    _H70BA = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70BA"));
                }
            }
        }

        private int _H70BB;
        public int H70BB
        {
            get => _H70BB;
            set
            {
                if (_H70BB != value)
                {
                    _H70BB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H70BB"));
                }
            }
        }

        // 1단계-맑음등1 조도디밍값(lux)
        private float _H73CE73CF;
        public float H73CE73CF
        {
            get => _H73CE73CF;
            set
            {
                if (_H73CE73CF != value)
                {
                    _H73CE73CF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73CE73CF"));
                }
            }
        }

        private float _H73D073D1;
        public float H73D073D1
        {
            get => _H73D073D1;
            set
            {
                if (_H73D073D1 != value)
                {
                    _H73D073D1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73D073D1"));
                }
            }
        }

        private float _H73D273D3;
        public float H73D273D3
        {
            get => _H73D273D3;
            set
            {
                if (_H73D273D3 != value)
                {
                    _H73D273D3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73D273D3"));
                }
            }
        }

        private float _H73D473D5;
        public float H73D473D5
        {
            get => _H73D473D5;
            set
            {
                if (_H73D473D5 != value)
                {
                    _H73D473D5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73D473D5"));
                }
            }
        }

        private float _H73D673D7;
        public float H73D673D7
        {
            get => _H73D673D7;
            set
            {
                if (_H73D673D7 != value)
                {
                    _H73D673D7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73D673D7"));
                }
            }
        }

        private float _H73D873D9;
        public float H73D873D9
        {
            get => _H73D873D9;
            set
            {
                if (_H73D873D9 != value)
                {
                    _H73D873D9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73D873D9"));
                }
            }
        }

        private float _H73DA73DB;
        public float H73DA73DB
        {
            get => _H73DA73DB;
            set
            {
                if (_H73DA73DB != value)
                {
                    _H73DA73DB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73DA73DB"));
                }
            }
        }

        private float _H73DC73DD;
        public float H73DC73DD
        {
            get => _H73DC73DD;
            set
            {
                if (_H73DC73DD != value)
                {
                    _H73DC73DD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73DC73DD"));
                }
            }
        }

        private float _H73DE73DF;
        public float H73DE73DF
        {
            get => _H73DE73DF;
            set
            {
                if (_H73DE73DF != value)
                {
                    _H73DE73DF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73DE73DF"));
                }
            }
        }

        private float _H73E073E1;
        public float H73E073E1
        {
            get => _H73E073E1;
            set
            {
                if (_H73E073E1 != value)
                {
                    _H73E073E1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73E073E1"));
                }
            }
        }

        private float _H73E273E3;
        public float H73E273E3
        {
            get => _H73E273E3;
            set
            {
                if (_H73E273E3 != value)
                {
                    _H73E273E3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73E273E3"));
                }
            }
        }

        private float _H73E473E5;
        public float H73E473E5
        {
            get => _H73E473E5;
            set
            {
                if (_H73E473E5 != value)
                {
                    _H73E473E5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73E473E5"));
                }
            }
        }

        private float _H73E673E7;
        public float H73E673E7
        {
            get => _H73E673E7;
            set
            {
                if (_H73E673E7 != value)
                {
                    _H73E673E7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73E673E7"));
                }
            }
        }

        private float _H73E873E9;
        public float H73E873E9
        {
            get => _H73E873E9;
            set
            {
                if (_H73E873E9 != value)
                {
                    _H73E873E9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73E873E9"));
                }
            }
        }

        private float _H73EA73EB;
        public float H73EA73EB
        {
            get => _H73EA73EB;
            set
            {
                if (_H73EA73EB != value)
                {
                    _H73EA73EB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73EA73EB"));
                }
            }
        }

        private float _H73EC73ED;
        public float H73EC73ED
        {
            get => _H73EC73ED;
            set
            {
                if (_H73EC73ED != value)
                {
                    _H73EC73ED = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73EC73ED"));
                }
            }
        }

        private float _H73EE73EF;
        public float H73EE73EF
        {
            get => _H73EE73EF;
            set
            {
                if (_H73EE73EF != value)
                {
                    _H73EE73EF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73EE73EF"));
                }
            }
        }

        private float _H73F073F1;
        public float H73F073F1
        {
            get => _H73F073F1;
            set
            {
                if (_H73F073F1 != value)
                {
                    _H73F073F1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73F073F1"));
                }
            }
        }

        private float _H73F273F3;
        public float H73F273F3
        {
            get => _H73F273F3;
            set
            {
                if (_H73F273F3 != value)
                {
                    _H73F273F3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73F273F3"));
                }
            }
        }

        private float _H73F473F5;
        public float H73F473F5
        {
            get => _H73F473F5;
            set
            {
                if (_H73F473F5 != value)
                {
                    _H73F473F5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73F473F5"));
                }
            }
        }

        private float _H73F673F7;
        public float H73F673F7
        {
            get => _H73F673F7;
            set
            {
                if (_H73F673F7 != value)
                {
                    _H73F673F7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("H73F673F7"));
                }
            }
        }

        private float _SH73CE73CF;
        public float SH73CE73CF
        {
            get => _SH73CE73CF;
            set
            {
                if (_SH73CE73CF != value)
                {
                    _SH73CE73CF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73CE73CF"));
                }
            }
        }

        private float _SH73D073D1;
        public float SH73D073D1
        {
            get => _SH73D073D1;
            set
            {
                if (_SH73D073D1 != value)
                {
                    _SH73D073D1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73D073D1"));
                }
            }
        }

        private float _SH73D273D3;
        public float SH73D273D3
        {
            get => _SH73D273D3;
            set
            {
                if (_SH73D273D3 != value)
                {
                    _SH73D273D3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73D273D3"));
                }
            }
        }

        private float _SH73D473D5;
        public float SH73D473D5
        {
            get => _SH73D473D5;
            set
            {
                if (_SH73D473D5 != value)
                {
                    _SH73D473D5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73D473D5"));
                }
            }
        }

        private float _SH73D673D7;
        public float SH73D673D7
        {
            get => _SH73D673D7;
            set
            {
                if (_SH73D673D7 != value)
                {
                    _SH73D673D7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73D673D7"));
                }
            }
        }

        private float _SH73D873D9;
        public float SH73D873D9
        {
            get => _SH73D873D9;
            set
            {
                if (_SH73D873D9 != value)
                {
                    _SH73D873D9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73D873D9"));
                }
            }
        }

        private float _SH73DA73DB;
        public float SH73DA73DB
        {
            get => _SH73DA73DB;
            set
            {
                if (_SH73DA73DB != value)
                {
                    _SH73DA73DB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73DA73DB"));
                }
            }
        }

        private float _SH73DC73DD;
        public float SH73DC73DD
        {
            get => _SH73DC73DD;
            set
            {
                if (_SH73DC73DD != value)
                {
                    _SH73DC73DD = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73DC73DD"));
                }
            }
        }

        private float _SH73DE73DF;
        public float SH73DE73DF
        {
            get => _SH73DE73DF;
            set
            {
                if (_SH73DE73DF != value)
                {
                    _SH73DE73DF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73DE73DF"));
                }
            }
        }

        private float _SH73E073E1;
        public float SH73E073E1
        {
            get => _SH73E073E1;
            set
            {
                if (_SH73E073E1 != value)
                {
                    _SH73E073E1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73E073E1"));
                }
            }
        }

        private float _SH73E273E3;
        public float SH73E273E3
        {
            get => _SH73E273E3;
            set
            {
                if (_SH73E273E3 != value)
                {
                    _SH73E273E3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73E273E3"));
                }
            }
        }

        private float _SH73E473E5;
        public float SH73E473E5
        {
            get => _SH73E473E5;
            set
            {
                if (_SH73E473E5 != value)
                {
                    _SH73E473E5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73E473E5"));
                }
            }
        }

        private float _SH73E673E7;
        public float SH73E673E7
        {
            get => _SH73E673E7;
            set
            {
                if (_SH73E673E7 != value)
                {
                    _SH73E673E7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73E673E7"));
                }
            }
        }

        private float _SH73E873E9;
        public float SH73E873E9
        {
            get => _SH73E873E9;
            set
            {
                if (_SH73E873E9 != value)
                {
                    _SH73E873E9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73E873E9"));
                }
            }
        }

        private float _SH73EA73EB;
        public float SH73EA73EB
        {
            get => _SH73EA73EB;
            set
            {
                if (_SH73EA73EB != value)
                {
                    _SH73EA73EB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73EA73EB"));
                }
            }
        }

        private float _SH73EC73ED;
        public float SH73EC73ED
        {
            get => _SH73EC73ED;
            set
            {
                if (_SH73EC73ED != value)
                {
                    _SH73EC73ED = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73EC73ED"));
                }
            }
        }

        private float _SH73EE73EF;
        public float SH73EE73EF
        {
            get => _SH73EE73EF;
            set
            {
                if (_SH73EE73EF != value)
                {
                    _SH73EE73EF = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73EE73EF"));
                }
            }
        }

        private float _SH73F073F1;
        public float SH73F073F1
        {
            get => _SH73F073F1;
            set
            {
                if (_SH73F073F1 != value)
                {
                    _SH73F073F1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73F073F1"));
                }
            }
        }

        private float _SH73F273F3;
        public float SH73F273F3
        {
            get => _SH73F273F3;
            set
            {
                if (_SH73F273F3 != value)
                {
                    _SH73F273F3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73F273F3"));
                }
            }
        }

        private float _SH73F473F5;
        public float SH73F473F5
        {
            get => _SH73F473F5;
            set
            {
                if (_SH73F473F5 != value)
                {
                    _SH73F473F5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73F473F5"));
                }
            }
        }

        private float _SH73F673F7;
        public float SH73F673F7
        {
            get => _SH73F673F7;
            set
            {
                if (_SH73F673F7 != value)
                {
                    _SH73F673F7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SH73F673F7"));
                }
            }
        }

        // 조도
        private Visibility _gbIllLuminanceVisible = Visibility.Collapsed;
        public Visibility IlLuminanceVisible
        {
            get => _gbIllLuminanceVisible;
            set
            {
                if (_gbIllLuminanceVisible != value)
                {
                    _gbIllLuminanceVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IlLuminanceVisible"));
                }
            }
        }

        // 휘도
        private Visibility _gbLuminanceVisible = Visibility.Collapsed;
        public Visibility LuminanceVisible
        {
            get => _gbLuminanceVisible;
            set
            {
                if (_gbLuminanceVisible != value)
                {
                    _gbLuminanceVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LuminanceVisible"));
                }
            }
        }
    }
}
