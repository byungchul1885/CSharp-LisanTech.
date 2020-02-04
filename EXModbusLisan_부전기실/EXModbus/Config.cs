using System.ComponentModel;
using System.Windows.Media;

namespace EXModbus
{
    public class Config : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Ip = "192.9.211.64";
        public string Ip
        {
            get => _Ip;
            set
            {
                if (_Ip != value)
                {
                    _Ip = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ip"));
                }
            }
        }

        private string _ConnStat = "접속끊김";
        public string ConnStat
        {
            get => _ConnStat;
            set
            {
                if (_ConnStat != value)
                {
                    _ConnStat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnStat"));
                }
            }
        }

        private Brush _ConnStatForgBrush = Brushes.Yellow;
        public Brush ConnStatForgBrush
        {
            get => _ConnStatForgBrush;
            set
            {
                if (_ConnStatForgBrush != value)
                {
                    _ConnStatForgBrush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnStatForgBrush"));
                }
            }
        }

        private Brush _ConnStatBackgBrush = Brushes.Red;
        public Brush ConnStatBackgBrush
        {
            get => _ConnStatBackgBrush;
            set
            {
                if (_ConnStatBackgBrush != value)
                {
                    _ConnStatBackgBrush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnStatBackgBrush"));
                }
            }
        }
    }
}
