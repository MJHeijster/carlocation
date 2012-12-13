using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Car_Location.AppSettings
{
    public class LocationSettings
    {
        /// <summary>
        /// Gets the locations.
        /// </summary>
        /// <returns>List{Location}.</returns>
        public static List<Location> Get() {
            List<Location> list;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.TryGetValue<List<Location>>("LocationList", out list);
            return list;
        }
        /// <summary>
        /// Add the location
        /// </summary>
        /// <param name="location"></param>
        public static void Add(Location location)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            List<Location> list;
            settings.TryGetValue<List<Location>>("LocationList", out list);
            if (list == null)
            {
                list = new List<Location>();
            }
            int lastID = 1;
            settings.TryGetValue<int>("lastID", out lastID);
            location.ID = lastID + 1;
            list.Add(location);
            settings["LocationList"] = list;
            settings["lastID"] = location.ID;
        }
        /// <summary>
        /// Clear the list of locations
        /// </summary>
        public static void Clear()
        {
            List<Location> list = new List<Location>();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["LocationList"] = list;
            settings["lastID"] = 1;
        }
    }
}
