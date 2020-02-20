using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BlackChecker
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository _repo = new Repository();
        DispatcherTimer _timer = null;
        bool _onMonitoring = false;
        string _processName = "BlackDesert64";

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _repo;
        }

        private void Init()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            _timer.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
            
            //초기 시간 설정
            var now = DateTime.Now;

            _repo.RemainTime = new TimeSpan();
            _repo.Now = now;

            _repo.FromDays = GetFromDays();
            _repo.FromHours = 4;
            _repo.FromMinutes = 59;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _timer.IsEnabled = false;
            _timer.Tick -= _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            _repo.Now = now;

            if(now.Second % 5 == 0 && _onMonitoring)
            {
                MonitorProcess();
            }
        }
    
        private int GetFromDays()
        {
            return rbToday.IsChecked.HasValue && rbToday.IsChecked.Value ? 0 : 1;
        }

        private void RbToday_Checked(object sender, RoutedEventArgs e)
        {
            _repo.FromDays = 0;
        }

        private void RbTommorow_Checked(object sender, RoutedEventArgs e)
        {
            _repo.FromDays = 1;
        }

        private void BtnReserve_Click(object sender, RoutedEventArgs e)
        {
            _repo.IsReserved = true;
            ShutdownComputer((int)_repo.RemainTime.TotalSeconds);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _repo.IsReserved = false;
            ShutdownComputer(cancel:true);
        }

        private void CbMonitorBlack_Checked(object sender, RoutedEventArgs e)
        {
            _onMonitoring = true;
        }

        private void CbMonitorBlack_Unchecked(object sender, RoutedEventArgs e)
        {
            _onMonitoring = false;
        }

        private void ShutdownComputer(int seconds = 0, bool cancel = false)
        {
            if(cancel)
            {
                Process.Start("shutdown", $"/a");
            }
            else
            {
                Process.Start("shutdown", $"/s /t {seconds}");
            }
        }

        private void MonitorProcess()
        {
            bool _alive = true;

            var results = Process.GetProcessesByName(_processName);
            if (results.Length == 0)
            {
                _alive = false;
            }

            tbStatus.Text = _alive ? "실행중" : "미실행";

            if (!_repo.IsReserved)
            {
                return;
            }
            if(!_alive)
            {
                ShutdownComputer(cancel: true);
                _timer.IsEnabled = false;
                Thread.Sleep(3000);
                ShutdownComputer(60);

                var logPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "logs");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                var logFilePath = System.IO.Path.Combine(logPath, $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond} 비정상종료함.txt");

                System.IO.File.WriteAllText(logFilePath, "");
            }
        }
    }
}
