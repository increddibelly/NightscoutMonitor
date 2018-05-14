using System.Linq;
using System.Windows;
using System.Windows.Input;
using NightscoutMonitor.Properties;

namespace NightscoutMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentScreen;
        private int moveCount;
        private const bool RIGHT = true;
        private const bool LEFT = false;

        public MainWindow()
        {
            InitializeComponent();
            
            var screenCount = WpfScreen.AllScreens().Count();
            currentScreen = screenCount;
            moveCount = screenCount * 2;

            Browser.Address = Settings.Default.TargetUri;

            SetPosition();
            Visibility = Visibility.Visible;
        }

        private void SetPosition()
        {
            var screens = WpfScreen.AllScreens().ToArray();
            var farBottomRight = 
                screens.OrderByDescending(scr => scr.WorkingArea.Right).First();

            Left = farBottomRight.WorkingArea.BottomRight.X - Width;
            Top = farBottomRight.WorkingArea.BottomRight.Y - Height;
        }

        public void Move()
        {
            var screens = WpfScreen.AllScreens().ToArray();

            // determine target locations
            var nextScreenSide = ++moveCount % 2 == 0 ; // false == left, true == right

            // screenSide = left ? time to move onto the next screen. Make sure to rotate back to the first screen
            currentScreen = ((nextScreenSide == LEFT ? ++currentScreen : currentScreen) % screens.Length);

            var screen = screens[currentScreen];
            var dockPoint = nextScreenSide == RIGHT
                ? screen.WorkingArea.BottomRight
                : screen.WorkingArea.BottomLeft;

            Left = nextScreenSide == RIGHT
                ? dockPoint.X - Width
                : dockPoint.X;
            Top = dockPoint.Y - Height;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //this.WindowStyle = WindowStyle.ToolWindow;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //this.WindowStyle = WindowStyle.None;
        }
    }
}
