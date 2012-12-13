using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Car_Location
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            _splashTimer = new System.Windows.Threading.DispatcherTimer();
            this.Loaded += new RoutedEventHandler(SplashScreen_Loaded);
            progressBar.IsIndeterminate = true;

        }
        System.Windows.Threading.DispatcherTimer _splashTimer;

        void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            if (_splashTimer != null)
            {
                _splashTimer.Interval = new TimeSpan(0, 0, 2);
                _splashTimer.Tick += new EventHandler(_splashScreen_Tick);
                _splashTimer.Start();
            }
        }

        void _splashScreen_Tick(object sender, EventArgs e)
        {
            _splashTimer.Stop();
            _splashTimer.Tick -= new EventHandler(_splashScreen_Tick);
            _splashTimer = null;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}