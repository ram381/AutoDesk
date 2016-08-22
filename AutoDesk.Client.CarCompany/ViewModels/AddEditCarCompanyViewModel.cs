using AutoDesk.Client.Common.BaseClasses;
using AutoDesk.PCL.Common;
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
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;
using Microsoft.Practices.Unity;
using AutoDesk.Client.Proxy.Managers;
using Microsoft.Practices.Prism.Events;
using AutoDesk.Client.Common.Events;
using AutoDesk.PCL.Common.Enums;

namespace AutoDesk.Client.CarCompany.ViewModels
{
    public class AddEditCarCompanyViewModel : ViewModelBase
    {
        public delegate void SaveCarCompanyErrorHandler(string error);
        public event SaveCarCompanyErrorHandler OnSaveCarCompanyError;
        private IModuleActivationParameters _moduleActivationParameters;

        public AddEditCarCompanyViewModel(IUnityContainer Container, IModuleActivationParameters ModuleActivationParameters)
        {
            CarCompany = new stccarCompany();
            this.Container = Container;
            _moduleActivationParameters = ModuleActivationParameters.Clone();
            if (_moduleActivationParameters != null)
            {
                this.Operation = Convert.ToString(_moduleActivationParameters.Parameters["Operation"]);
                this.CarCompany.Id = Convert.ToInt32(_moduleActivationParameters.Parameters["Id"]);
            }
        }

        public void onViewLoading(object sender, EventArgs e)
        {
            this.OnSaveCarCompanyError += addEditCarCompanyVM_OnSaveCarCompanyError;
            if (Operation == "Update" && CarCompany.Id > 0)
                serviceRef.GetCompanyByID(CarCompany.Id, OnGetCarCompanyByIdCompleted);
        }

        private void OnGetCarCompanyByIdCompleted(stccarCompany result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString(), "AutoDesk Error Message", MessageBoxButton.OK));
            else
                CarCompany = result;
        }

        private void addEditCarCompanyVM_OnSaveCarCompanyError(string error)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(error, "AutoDesk Message", MessageBoxButton.OK));
        }

        public void SaveCarCompanyError(string error)
        {
            if (OnSaveCarCompanyError != null)
                OnSaveCarCompanyError(error);
        }

        private void btnSave_Click()
        {
            Save();
        }

        private void Save()
        {
            if (!CheckRequiredFields())
                return;
            serviceRef.AddEditCarCompany(CarCompany, OnSaveCompleted);
        }

        private void OnSaveCompleted(string result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString()));
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (MessageBox.Show("Car Company Saved Successfully.", "AutoDesk Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        IEventAggregator eventAggregator = Container.Resolve<IEventAggregator>();
                        var messengerEvent = eventAggregator.GetEvent<MessengerEvent>();
                        messengerEvent.Publish(new Messenger()
                        {
                            Id = clsEnumeration.MessengerId.AddEditCarCompany,
                            Data = CarCompany,
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
            if (string.IsNullOrWhiteSpace(CarCompany.Name))
            {
                SaveCarCompanyError("Car Company Name cannot be Empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(CarCompany.Owner))
            {
                SaveCarCompanyError("Company Owner cannot be Empty");
                return false;
            }
            return true;
        }

        #region Properties

        private DelegateCommand _btnSaveClickCommand;
        public DelegateCommand BtnSaveClickCommand
        {
            get
            {
                return _btnSaveClickCommand ?? (_btnSaveClickCommand = new DelegateCommand(btnSave_Click));
            }
        }

        private stccarCompany _carCompany;
        public stccarCompany CarCompany
        {
            get { return _carCompany; }
            set
            {
                if (_carCompany == value) return;
                _carCompany = value;
                if (value != null)
                {
                    _carCompany = value;
                }
                OnPropertyChanged("CarCompany");
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
            this.OnSaveCarCompanyError -= addEditCarCompanyVM_OnSaveCarCompanyError;
            base.Dispose(disposing);
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

        public IUnityContainer Container { get; set; }

        #endregion
    }
}
