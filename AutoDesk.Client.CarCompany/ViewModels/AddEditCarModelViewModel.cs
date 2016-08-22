using AutoDesk.Client.Common.BaseClasses;
using AutoDesk.PCL.Common;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;
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
using Microsoft.Practices.Unity;
using AutoDesk.Client.Proxy.Managers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using AutoDesk.Client.Common.Events;
using AutoDesk.PCL.Common.Enums;

namespace AutoDesk.Client.CarCompany.ViewModels
{
    public class AddEditCarModelViewModel : ViewModelBase
    {
        public delegate void SaveCarModelErrorHandler(string error);
        public event SaveCarModelErrorHandler OnSaveCarModelError;
        private IModuleActivationParameters _moduleActivationParameters;

        public AddEditCarModelViewModel(IUnityContainer Container, IModuleActivationParameters ModuleActivationParameters)
        {
            CarModel = new stccarModel();
            this.Container = Container;
            _moduleActivationParameters = ModuleActivationParameters.Clone();
            if (_moduleActivationParameters != null)
            {
                this.Operation = Convert.ToString(_moduleActivationParameters.Parameters["Operation"]);
                this.CarModel.Id = Convert.ToInt32(_moduleActivationParameters.Parameters["Id"]);
            }
        }

        public void onViewLoading(object sender, EventArgs e)
        {
            this.OnSaveCarModelError += addEditCarModelVM_OnSaveCarModelError;
            serviceRef.GetCarCompanies(OnGetCarCompaniesCompleted);
        }

        private void OnGetCarModelByIdComplete(stccarModel result, Exception ex)
        {
            if (ex != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString()));
            }
            else
            {
                CarModel = result;
                SelectedCompanyId = result.CompanyId;
            }
        }

        private void OnGetCarCompaniesCompleted(List<stccarCompany> result, Exception ex)
        {
            CarCompanyList = new ObservableCollection<stccarCompany>(result);
            CarCompanyList.Add(new stccarCompany { Id = 0, Name = "-- Select --" });
            SelectedCompanyId = CarCompanyList[CarCompanyList.Count - 1].Id;
            PerformOperation();
        }

        private void PerformOperation()
        {
            if (Operation == "Update" && CarModel.Id > 0)
            {
                serviceRef.GetCarModelByID(CarModel.Id, OnGetCarModelByIdComplete);
            }
            else
                this.SelectedCompanyId = Convert.ToInt32(_moduleActivationParameters.Parameters["CompanyID"]);
        }

        private void addEditCarModelVM_OnSaveCarModelError(string error)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(error, "AutoDesk Error Message", MessageBoxButton.OK));
        }

        public void SaveCarModelError(string error)
        {
            if (OnSaveCarModelError != null)
                OnSaveCarModelError(error);
        }

        private void Save()
        {
            CarModel.CompanyId = SelectedCompanyId;
            if (!CheckRequiredFields())
                return;
            serviceRef.AddEditCarModel(CarModel, OnSaveCompleted);
        }

        private void OnSaveCompleted(string result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString(), "AutoDesk Error Message", MessageBoxButton.OK));
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                 {
                                     if (MessageBox.Show("Car Model Saved Successfully.", "Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                                     {
                                         IEventAggregator eventAggregator = Container.Resolve<IEventAggregator>();
                                         var messengerEvent = eventAggregator.GetEvent<MessengerEvent>();
                                         messengerEvent.Publish(new Messenger()
                                         {
                                             Id = clsEnumeration.MessengerId.AddEditCarModel,
                                             Data = CarModel,
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
            if (string.IsNullOrWhiteSpace(CarModel.Model_Number))
            {
                SaveCarModelError("Car Model Name cannot be Empty");
                return false;
            }
            if (CarModel.CompanyId <= 0)
            {
                SaveCarModelError("Please Select Car Company.");
                return false;
            }
            if (CarModel.Cost <= 0)
            {
                SaveCarModelError("Please enter Cost of Car Model.");
                return false;
            }
            if (CarModel.Milege <= 0)
            {
                SaveCarModelError("Please enter Proper Milege for Car Model.");
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

        #region Properties
        public IUnityContainer Container { get; set; }

        private stccarModel _carModel;
        public stccarModel CarModel
        {
            get { return _carModel; }
            set
            {
                _carModel = value;
                if (value != null)
                {
                    _carModel = value;
                }
                OnPropertyChanged("CarModel");
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

        private bool _isDisposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;
            this.OnSaveCarModelError -= addEditCarModelVM_OnSaveCarModelError;
            base.Dispose(disposing);
        }

        private ObservableCollection<stccarCompany> _carCompanyList;
        public ObservableCollection<stccarCompany> CarCompanyList
        {
            get { return _carCompanyList; }
            set
            {
                if (_carCompanyList == value) return;
                _carCompanyList = value;
                OnPropertyChanged("CarCompanyList");
            }
        }

        private int _SelectedCompanyId;
        public int SelectedCompanyId
        {
            get { return _SelectedCompanyId; }
            set
            {
                _SelectedCompanyId = value;
                OnPropertyChanged("SelectedCompanyId");
            }
        }

        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set
            {
                if (_operation == value) return;
                _operation = value;
                OnPropertyChanged("Operation");
            }
        }

        #endregion
    }
}
