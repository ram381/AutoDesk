using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.CarCompany.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private bool disposed = false;
        private ViewModelBase Instance;
        private List<ViewModelBase> Instances;
        private Dictionary<string, object> PropertyDictionary;

        protected ViewModelBase()
        {
            Instances = new List<ViewModelBase>();
            PropertyDictionary = new Dictionary<string, object>();

            var properties = GetType().GetProperties();
            foreach (var property in properties.Where(property => PropertyDictionary.ContainsKey(property.Name)))
            {
                property.SetValue(this, PropertyDictionary[property.Name], null);
            }
            Instances.Add(this);
            properties = null;
        }

        public event PropertyChangedEventHandler PropertyChanged = OnPropertyChangedCallMe;

        private static void OnPropertyChangedCallMe(object sender, PropertyChangedEventArgs e) { }

        private void OnPublicPropertyChanged(string property)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(property))
            {
                var value = GetType().GetProperty(property).GetValue(this, null);
                if (!PropertyDictionary.ContainsKey(property)) PropertyDictionary.Add(property, value);
                else PropertyDictionary[property] = value;

                foreach (var instance in Instances)
                {
                    var relatedProperty = instance.GetType().GetProperty(property);
                    relatedProperty.SetValue(instance, value, null);
                    PropertyChanged(instance, new PropertyChangedEventArgs(property));
                }
            }
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                disposed = true;
                Dispose(disposed);
                PropertyDictionary.Clear();
                Instances.Clear();
                PropertyDictionary = null;
                PropertyChanged = null;
                Instances = null;
                Instance = null;                
            }
        }

    }
}
