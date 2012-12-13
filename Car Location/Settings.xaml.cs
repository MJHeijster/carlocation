// ***********************************************************************
// Assembly         : Car Location
// Author           : Jeroen
// Created          : 03-02-2012
//
// Last Modified By : Jeroen
// Last Modified On : 03-13-2012
// ***********************************************************************
// <copyright file="Settings.xaml.cs" company="">
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
using System.IO.IsolatedStorage;
using Car_Location.AppSettings;

namespace Car_Location
{
    /// <summary>
    /// Class Settings
    /// </summary>
    public partial class Settings : PhoneApplicationPage
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings" /> class.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            bool useGPS = GPSSettings.Get();

            gpsSwitch.IsChecked = useGPS;


            this.backButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);

        }

        /// <summary>
        /// Handles the Tap event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void backButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Handles the Tap event of the gpsSwitch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.GestureEventArgs" /> instance containing the event data.</param>
        private void gpsSwitch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GPSSettings.Set((bool)gpsSwitch.IsChecked);
        }

    }
}