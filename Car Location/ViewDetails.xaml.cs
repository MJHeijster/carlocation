// ***********************************************************************
// Assembly         : Car Location
// Author           : Jeroen
// Created          : 02-24-2012
//
// Last Modified By : Jeroen
// Last Modified On : 03-13-2012
// ***********************************************************************
// <copyright file="ViewDetails.xaml.cs" company="">
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
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;
using System.Device.Location;
using Microsoft.Phone.Marketplace;
using Car_Location.AppSettings;

namespace Car_Location
{
    /// <summary>
    /// Class Page1
    /// </summary>
    public partial class Page1 : PhoneApplicationPage
    {
        ItemViewModel dc = new ItemViewModel();
        Pushpin pushpin = new Pushpin();
        bool goBack = false;
        Location location = new Location();
        bool isLoaded = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="Page1" /> class.
        /// </summary>
        public Page1()
        {
            InitializeComponent();
            bool useGPS = GPSSettings.Get();
            if (!useGPS)
            {
                MessageBox.Show("You have disabled GPS access, please enable it in settings.", "Attention!",
                                   MessageBoxButton.OK);
                goBack = true;
            }
            this.navigateButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.backButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
        }
        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (goBack)
                NavigationService.GoBack();
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex) && !isLoaded)
            {
                map1.ZoomLevel = 18;
                map1.Mode = new AerialMode();
                int index = int.Parse(selectedIndex);
                List<Location> list = LocationSettings.Get();

                foreach (var Location in list)
                {
                    if (Location.ID.ToString() == selectedIndex)
                    {
                        location = Location;
                        break;
                    }
                }
                if (location.Coordinate != null)
                {
                    map1.Center = location.Coordinate;
                    pushpin.Location = location.Coordinate;
                    pushpin.Background = new SolidColorBrush(Colors.LightGray);

                    try
                    {
                        map1.Children.Add(pushpin);
                    }
                    catch
                    {
                        //pin is already on the map.
                    }
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the backButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        /// <summary>
        /// Handles the Click event of the navigateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void navigateButton_Click(object sender, RoutedEventArgs e)
        {
           
                BingMapsDirectionsTask directionTask = new BingMapsDirectionsTask();
                directionTask.End = new LabeledMapLocation("Car", location.Coordinate);
                directionTask.Show();
            
        }

        
    }
}