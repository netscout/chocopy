using System;
using System.IO;
using System.Windows;

namespace BlackChecker
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            var logFilePath = Path.Combine(logPath, $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}.txt");

            System.IO.File.WriteAllText(logFilePath, e.Exception.ToString());

            MessageBox.Show($"{logFilePath}를 확인하세요.", "오류가 발생했습니다.");
        }
    }
}
