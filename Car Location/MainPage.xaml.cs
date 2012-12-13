// ***********************************************************************
// Assembly         : Car Location
// Author           : Jeroen
// Created          : 02-21-2012
//
// Last Modified By : Jeroen
// Last Modified On : 03-22-2012
// ***********************************************************************
// <copyright file="MainPage.xaml.cs" company="">
//     . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using System.Windows.Navigation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Car_Location.AppSettings;

namespace Car_Location
{
    /// <summary>
    /// Class MainPage
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static MainPage instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static MainPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainPage();
                }
                return instance;
            }
        }
        bool isLoaded = false;
        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            FirstTime.Run();
        }

        /// <summary>
        /// Enables the tilt effects.
        /// </summary>
        public void SetTilt()
        {
            this.MainListBox.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.MainGrid.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.Stack1.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.BingMaps.SetValue(TiltEffect.IsTiltEnabledProperty, true);
        }
        /// <summary>
        /// Handles the DoWork event of the worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        void worker_DoWork (object sender, DoWorkEventArgs args) {
            this.progressBar.Foreground = (Brush)Application.Current.Resources["PhoneForegroundBrush"];
            this.progressBar.IsIndeterminate = true;
            this.progressBar.Visibility = System.Windows.Visibility.Visible;
            App.ViewModel.LoadData();
        }
        /// <summary>
        /// Handles the RunWorkerCompleted event of the worker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
        void worker_RunWorkerCompleted(object sender, 
                               RunWorkerCompletedEventArgs e) {
                                   this.progressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the Loaded event of the MainPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (!App.ViewModel.IsDataLoaded)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted 
  += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.WorkerReportsProgress = true;
                worker.RunWorkerAsync();

            }
            if (!isLoaded)
            {
                StartupStoryboard.Begin();
                isLoaded = true;
                
                ApplicationBarIconButton b = new ApplicationBarIconButton();
                b.Text = "Settings";
                b.Click += new EventHandler(b_Click);
                bool dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
                if (dark)
                    b.IconUri = new Uri("/Images/settingsdark.png", UriKind.Relative);
                else
                    b.IconUri = new Uri("/Images/settingslight.png", UriKind.Relative);
                ApplicationBar.Buttons.Add(b);
            }
            bool useGPS = GPSSettings.Get();
            if (useGPS)
                BingMaps.Visibility = Visibility.Visible;
            else
                BingMaps.Visibility = Visibility.Collapsed;
            appBar.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ViewModel.LoadData();
            //Remove the entry to the splash screen
            NavigationService.RemoveBackEntry();
        }
        /// <summary>
        /// Handles the Tap event of the StackPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri theUri = new Uri("/AddManual.xaml", UriKind.Relative);
            NavigationService.Navigate(theUri);
        }

        /// <summary>
        /// Handles the Tap event of the StackPanel2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void StackPanel2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Uri theUri = new Uri("/AddBing.xaml", UriKind.Relative);
            NavigationService.Navigate(theUri);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;


            // Navigate to the new page
            List<Location> list = LocationSettings.Get();
            ItemViewModel dc = new ItemViewModel();
            dc = App.ViewModel.Items[MainListBox.SelectedIndex];
            int link = dc.Link;
            if (link == -2)
            {
                var result = MessageBox.Show("Do you want to remove all locations?", "Attention!",
                                   MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    LocationSettings.Clear();
                    App.ViewModel.LoadData();
                    return;
                }
            }
            Location location = new Location();
            location.Map = false;
            try
            {
                foreach (var Location in list)
                {
                    if (Location.ID == link)
                    {
                        location = Location;
                        break;
                    }
                }
            }
            catch
            {

            }
            if (location.Map)
                NavigationService.Navigate(new Uri("/ViewDetails.xaml?selectedItem=" + link, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Handles the Click event of the ApplicationBarMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Privacy.xaml", UriKind.Relative));
        }
        /// <summary>
        /// Handles the Click event of the b control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void b_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }


    }
}