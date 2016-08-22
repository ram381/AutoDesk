using AutoDesk.Client.Proxy.Managers;
using AutoDesk.Client.Purchaser.ViewModels;
using AutoDesk.PCL.Common;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.Purchaser.ViewModel
{
    public class ShowPurchaserViewModel : ViewModelBase
    {
        private AutoDeskServiceManager _ServiceClient;
        public IUnityContainer Container { get; set; }
        public int CarModelId { get; set; }
        public ShowPurchaserViewModel(IUnityContainer Container)
        {
            this.Container = Container;
        }

        public void onViewLoading(object sender, EventArgs e)
        {
            serviceRef.GetPurchasersByCarModel(CarModelId, OnViewLoadingCompleted);
        }

        private void OnViewLoadingCompleted(List<stcPurchaser> result, Exception ex)
        {
            if (ex != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                MessageBox.Show(ex.ToString()));
            }
            else
            {
                listControl = result;
            }
        }

        public AutoDeskServiceManager serviceRef
        {
            get
            {
                if (_ServiceClient == null)
                    _ServiceClient = new AutoDeskServiceManager();

                return _ServiceClient;
            }
        }

        private List<stcPurchaser> _listControl;
        public List<stcPurchaser> listControl
        {
            get { return _listControl; }
            set
            {
                if (_listControl == value)
                    return;
                _listControl = value;
                OnPropertyChanged("listControl");
            }
        }
    }
}
