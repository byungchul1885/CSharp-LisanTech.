using System.Windows.Input;

namespace EXModbus
{
    public static class CustomCommands
    {
        private static RoutedUICommand connect;
        private static RoutedUICommand disconnect;

        private static RoutedUICommand resetCnt;

        private static RoutedUICommand clickToggleBtn;

        private static RoutedUICommand sendDataBtn;

        static CustomCommands()
        {
            connect = new RoutedUICommand("서버에 접속합니다", "Connect", typeof(CustomCommands));
            disconnect = new RoutedUICommand("서버 접속을 끊습니다", "Disconnect", typeof(CustomCommands));

            resetCnt = new RoutedUICommand("송수신 카운트를 초기화합니다", "ResetCnt", typeof(CustomCommands));

            clickToggleBtn = new RoutedUICommand();

            sendDataBtn = new RoutedUICommand();
        }

        public static RoutedUICommand Connect => connect;
        public static RoutedUICommand Disconnect => disconnect;

        public static RoutedUICommand ResetCnt => resetCnt;

        public static RoutedUICommand ClickToggleBtn => clickToggleBtn;

        public static RoutedUICommand SendDataBtn => sendDataBtn;
    }
}
