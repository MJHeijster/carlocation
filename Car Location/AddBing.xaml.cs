// ***********************************************************************
// Assembly         : Car Location
// Author           : Jeroen
// Created          : 02-21-2012
//
// Last Modified By : Jeroen
// Last Modified On : 03-13-2012
// ***********************************************************************
// <copyright file="AddBing.xaml.cs" company="">
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
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using Car_Location.AppSettings;

namespace Car_Location
{
    /// <summary>
    /// Class AddBing
    /// </summary>
    public partial class AddBing : PhoneApplicationPage
    {
        Pushpin pushpin = new Pushpin();
        GeoCoordinateWatcher watcher;

        bool newPush = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddBing" /> class.
        /// </summary>
        public AddBing()
        {
            InitializeComponent();
            
            if (watcher == null)
            {
                //---get the highest accuracy---
                StartLocationService(GeoPositionAccuracy.High);

                map1.ZoomLevel = 20;
                map1.Mode = new AerialMode();

            }
            this.saveButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.cancelButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
        }
        /// <summary>
        /// Helper method to start up the location data acquisition
        /// </summary>
        /// <param name="accuracy">The accuracy level</param>
        private void StartLocationService(GeoPositionAccuracy accuracy)
        {
            // Reinitialize the GeoCoordinateWatcher
            watcher = new GeoCoordinateWatcher(accuracy);
            watcher.MovementThreshold = 20;

            // Add event handlers for StatusChanged and PositionChanged events
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            // Start data acquisition
            watcher.Start();
        }

        /// <summary>
        /// Handler for the PositionChanged event. This invokes MyStatusChanged on the UI thread and
        /// passes the GeoPositionStatusChangedEventArgs
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MyPositionChanged(e));
        }

        /// <summary>
        /// Custom method called from the PositionChanged event handler
        /// </summary>
        /// <param name="e">The e.</param>
        void MyPositionChanged(GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            map1.Center = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
            pushpin.Location = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);
            pushpin.Background = new SolidColorBrush(Colors.LightGray);
            if (newPush)
                map1.Children.Add(pushpin);
            newPush = false;
        }


        /// <summary>
        /// Handles the Tap event of the cancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void cancelButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            NavigationService.GoBack();
        }

        /// <summary>
        /// Handles the Click event of the saveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Location location = new Location();
            location.Map = true;
            location.Date = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            location.Coordinate = pushpin.Location;
            LocationSettings.Add(location);
            NavigationService.GoBack();
        }

    }
}