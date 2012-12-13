using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;

namespace Car_Location.AppSettings
{
    public class FirstTime
    {
        public static void Run()
        {
            //Inverted values, if FirstTime is not set, it will return false.
            bool firstTime = true;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.TryGetValue<bool>("FirstTime", out firstTime);
            if (!firstTime)
            {
                var result = MessageBox.Show("Do you want to enable GPS? This way you can use Bing Maps integration. You can always change this in the settings.", "Attention!",
                                  MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    GPSSettings.Set(true);
                }
                settings["FirstTime"] = true;
            }
        }
    }
}