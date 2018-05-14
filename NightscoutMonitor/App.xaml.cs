using System.Windows;
using System.Windows.Controls;

namespace NightscoutMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow win;

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            win = MainWindow as MainWindow;
            var result = (e.Source as MenuItem)?.Header as string;

            if (result == "Quit")
            {
                win?.Close();
            }

            if (result == "Move")
            {
                win.Move();
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            NightscoutMonitor.Properties.Settings.Default.Save();
        }
    }
}
