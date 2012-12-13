// ***********************************************************************
// Assembly         : Car Location
// Author           : Jeroen
// Created          : 02-21-2012
//
// Last Modified By : Jeroen
// Last Modified On : 03-13-2012
// ***********************************************************************
// <copyright file="AddManual.xaml.cs" company="">
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
    /// Class AddManual
    /// </summary>
    public partial class AddManual : PhoneApplicationPage
    {
        List<Location> list;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddManual" /> class.
        /// </summary>
        public AddManual()
        {
            InitializeComponent();
            list = LocationSettings.Get();
            if (list == null)
            {
                list = new List<Location>();
            }

            this.saveButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
            this.cancelButton.SetValue(TiltEffect.IsTiltEnabledProperty, true);
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
        /// Handles the Tap event of the saveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GestureEventArgs" /> instance containing the event data.</param>
        private void saveButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            Location location = new Location();
            
            location.LocationName = textBoxLocation.Text;
            location.Square = textBoxSquare.Text;
            location.Date = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            
            LocationSettings.Add(location);
            NavigationService.GoBack();
        }
    }
}