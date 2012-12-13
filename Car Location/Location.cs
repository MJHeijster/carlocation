using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;

namespace Car_Location
{
    public class Location
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Location" /> is map.
        /// </summary>
        /// <value><c>true</c> if map; otherwise, <c>false</c>.</value>
        public bool Map
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        /// <value>The name of the location.</value>
        public string LocationName
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the square.
        /// </summary>
        /// <value>The square.</value>
        public string Square
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        public GeoCoordinate Coordinate
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public string Date
        {
            get;
            set;
        }
    }
}
