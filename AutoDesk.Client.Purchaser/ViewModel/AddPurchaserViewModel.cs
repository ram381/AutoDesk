using AutoDesk.Client.Common.BaseClasses;
using AutoDesk.Client.Common.Events;
using AutoDesk.Client.Proxy.Managers;
using AutoDesk.Client.Purchaser.ViewModels;
using AutoDesk.PCL.Common;
using AutoDesk.PCL.Common.Enums;
using Microsoft.Practices.Prism.Events;
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
    public class AddPurchaserViewModel : ViewModels.ViewModelBase
    {
        public delegate void SavePurchaserErrorHandler(string error);
        public event SavePurchaserErrorHandler OnSavePurchaserError;
        
        public AddPurchaserViewModel(IUnityContainer Container)
        {
            Purchaser = new stcPurchaser();
            this.Container = Container;            
        }

        private void addPurchaserVM_OnSavePurchaserError(string error)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(error, "AutoDesk Error Message", MessageBoxButton.OK));
        }

        public void SaveCarModelError(string error)
        {
            if (OnSavePurchaserError != null)
                OnSavePurchaserError(error);
        }

        public void onViewLoading(object sender, EventArgs e)
        {
            this.OnSavePurchaserError += addPurchaserVM_OnSavePurchaserError;
            serviceRef.GetCarModels(OnGetCarModelsCompleted);
        }

        private void OnGetCarModelsCompleted(List<stccarModel> result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString(), "AutoDesk Error Message", MessageBoxButton.OK));
            else
            {
                CarModelList = new ObservableCollection<stccarModel>(result);
            }
        }

        private void Save()
        {
            Purchaser.CarModelId = CarModelId;
            if (!CheckRequiredFields())
                return;
            serviceRef.AddEditPurchaser(Purchaser, OnSaveCompleted);
        }

        private void OnSaveCompleted(string result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString(), "AutoDesk Error Message", MessageBoxButton.OK));
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (MessageBox.Show("Purchaser Saved Successfully.", "Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        IEventAggregator eventAggregator = Container.Resolve<IEventAggregator>();
                        var messengerEvent = eventAggregator.GetEvent<MessengerEvent>();
                        messengerEvent.Publish(new Messenger()
                        {
                            Id = clsEnumeration.MessengerId.AddEditPurchaser,
                            Data = Purchaser,
                        });
                        eventAggregator = null;
                        messengerEvent = null;
                        Dispose(true);
                    }
                });
            }
        }

        private bool CheckRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(Purchaser.Name))
            {
                SaveCarModelError("Purchaser Name cannot be Empty");
                return false;
            }
            if (Purchaser.CarModelId <= 0)
            {
                SaveCarModelError("Please Select Car Model.");
                return false;
            }
            return true;
        }

        private DelegateCommand _btnSaveClickCommand;
        public DelegateCommand BtnSaveClickCommand
        {
            get
            {
                return _btnSaveClickCommand ?? (_btnSaveClickCommand = new DelegateCommand(Save));
            }
        }

        public IUnityContainer Container { get; set; }

        private ObservableCollection<stccarModel> _carModelList;
        public ObservableCollection<stccarModel> CarModelList
        {
            get { return _carModelList; }
            set
            {
                _carModelList = value;
                if (value != null)
                {
                    _carModelList = value;
                }
                OnPropertyChanged("CarModelList");
            }
        }

        private int _CarModelId;
        public int CarModelId
        {
            get { return _CarModelId; }
            set
            {
                _CarModelId = value;
                OnPropertyChanged("CarModelId");
            }
        }

        private AutoDeskServiceManager _ServiceClient;
        public AutoDeskServiceManager serviceRef
        {
            get
            {
                if (_ServiceClient == null)
                    _ServiceClient = new AutoDeskServiceManager();

                return _ServiceClient;
            }
        }

        private stcPurchaser _purchaser;
        public stcPurchaser Purchaser
        {
            get { return _purchaser; }
            set
            {
                _purchaser = value;
                OnPropertyChanged("Purchaser");
            }
        }

        private bool _isDisposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;
            this.OnSavePurchaserError -= addPurchaserVM_OnSavePurchaserError;
            base.Dispose(disposing);
        }

    }
}
