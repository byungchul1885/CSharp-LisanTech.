using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmingContol
{
    public static class Config
    {
        private static string _Ip = "192.9.211.61";
        public static string Ip
        {
            get => _Ip;
            set
            {
                if (_Ip != value)
                {
                    _Ip = value;
                }
            }
        }

        private static string _ConnStat = "접속끊김";
        public static string ConnStat
        {
            get => _ConnStat;
            set
            {
                if (_ConnStat != value)
                {
                    _ConnStat = value;
                }
            }
        }

        private static Brush _ConnStatForgBrush = Brushes.Yellow;
        public static Brush ConnStatForgBrush
        {
            get => _ConnStatForgBrush;
            set
            {
                if (_ConnStatForgBrush != value)
                {
                    _ConnStatForgBrush = value;
                }
            }
        }

        private static Brush _ConnStatBackgBrush = Brushes.Red;
        public static Brush ConnStatBackgBrush
        {
            get => _ConnStatBackgBrush;
            set
            {
                if (_ConnStatBackgBrush != value)
                {
                    _ConnStatBackgBrush = value;
                }
            }
        }
    }
}
