using System.Windows;

namespace EXModbus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Config cfg = new Config();
        public static Config Config => cfg;

        private static RxValues rvs = new RxValues();
        public static RxValues RxValues => rvs;
    }
}
