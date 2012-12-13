using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using Car_Location.AppSettings;


namespace Car_Location
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            List<Location> list = LocationSettings.Get();
            if (list != null)
            {

            }
            else
            {
                list = new List<Location>();
            }
            this.Items.Clear();
            foreach (var Location in list)
            {
                if (!Location.Map)
                {
                    string square = string.Empty;
                    if (!string.IsNullOrEmpty(Location.Square))
                    {
                        square = "\nSquare : " + Location.Square;
                    }
                    this.Items.Insert(0, new ItemViewModel() { LineOne = Location.Date, LineTwo = "Location: " + Location.LocationName + square });
                }
                if (Location.Map)
                    this.Items.Insert(0, new ItemViewModel() { LineOne = Location.Date, LineTwo = "Open in Bing Maps", Link = Location.ID });
            }
            if (this.Items.Count > 0)
                this.Items.Add(new ItemViewModel() { LineOne = "Delete all locations", Link = -2 });
            else
                this.Items.Add(new ItemViewModel() { LineOne = "No locations stored." });
            this.IsDataLoaded = true;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}