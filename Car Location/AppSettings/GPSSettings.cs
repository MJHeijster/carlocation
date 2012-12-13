using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Car_Location.AppSettings
{
   public class GPSSettings
    {

        /// <summary>
        /// Get if the application allows GPS.
        /// </summary>
        /// <returns><c>true</c> if GPS is allowed, <c>false</c> otherwise</returns>
       public static bool Get() {
           bool useGPS = true;
           var settings = IsolatedStorageSettings.ApplicationSettings;
           settings.TryGetValue<bool>("GPS", out useGPS);
           return useGPS;
       }

       /// <summary>
       /// Sets if the application allows GPS.
       /// </summary>
       /// <param name="setValue">if set to <c>true</c> [set value].</param>
       public static void Set(bool setValue) {
           var settings = IsolatedStorageSettings.ApplicationSettings;
           settings["GPS"] = setValue;
       }
    }
}
