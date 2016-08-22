using AutoDesk.Client.Common.BaseClasses;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;
using AutoDesk.Client.Proxy.Managers;
using AutoDesk.PCL.Common;
using Microsoft.Practices.Prism.Commands;
using static AutoDesk.PCL.Common.Enums.clsEnumeration;

namespace AutoDesk.Client.Shell.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _isShowPurchaserXAPLoaded;
        private IModuleActivationParameters _ModuleActivationParameters;
        private AutoDeskServiceManager _ServiceClient;

        public MainPageViewModel(IUnityContainer Container)
        {
            this.Container = Container;
        }

        public void onViewLoading(object sender, EventArgs e)
        {
            isShowPurchaserXAPLoaded = false;
            this.PrepareView();
        }

        private void PrepareView()
        {
            IsShowPurchaser = false;
            ShowGridSplitter = false;
            ShowPurchaserRegion = false;
            isExpanded = true;
            GetTreeData();
        }

        private void GetTreeData()
        {
            serviceRef.GetTreeData(OnPrepareViewCompleted);
        }

        private void OnPrepareViewCompleted(List<stcTreeData> result, Exception ex)
        {
            if (ex != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                                MessageBox.Show(ex.ToString()));
            }
            else
            {
                treeData = result;
            }
        }

        private void OnLoadModuleCompleted(bool validated, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString()));
            if (validated)
                this.GetTreeData();
        }

        private void OnLoadPurchaserModuleCompleted(bool validated, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString()));
            if (validated)
            {
                if (isShowPurchaserXAPLoaded == true)
                    isShowPurchaserXAPLoaded = false;
                this.ShowPurchaser();
            }
        }

        #region CarCompany
        private DelegateCommand _NewCarCompanyCommand;
        public DelegateCommand NewCarCompanyCommand
        {
            get
            {
                return _NewCarCompanyCommand ?? (_NewCarCompanyCommand = new DelegateCommand(AddNewCarCompany));
            }
        }

        private void AddNewCarCompany()
        {
            LoadCarCompanyModule(null);
        }

        public void LoadCarCompanyModule(stcTreeData treeData)
        {
            XAPDialogLoader xapLoader = null;
            Dictionary<string, object> _parameters = new Dictionary<string, object>();
            _parameters.Add("IsCarCompany", true);
            if (treeData == null)
            {
                _parameters.Add("Operation", "Add");
                _parameters.Add("Id", 0);
            }
            else
            {
                _parameters.Add("Operation", "Update");
                _parameters.Add("Id", treeData.Id);
            }
            _ModuleActivationParameters = new ModuleActivationParameters()
            {
                ModuleName = "AutoDesk.Client.CarCompany",
                RegionName = "CarCompanyRegion",
                Parameters = _parameters
            };
            xapLoader = new XAPDialogLoader(this.Container, "Add Car Company", _ModuleActivationParameters, OnLoadModuleCompleted,
                                                            550, 270, false, false, null, false);
            xapLoader.ShowDialog();
        }

        #endregion

        #region CarModel
        private DelegateCommand _NewCarModel;
        public DelegateCommand NewCarModel
        {
            get
            {
                return _NewCarModel ?? (_NewCarModel = new DelegateCommand(AddNewCarModel));
            }
        }

        private void AddNewCarModel()
        {
            LoadCarModelModule(null);
        }

        public void LoadCarModelModule(stcTreeData treeData)
        {
            XAPDialogLoader xapLoader = null;
            Dictionary<string, object> _parameters = new Dictionary<string, object>();
            _parameters.Add("IsCarCompany", false);
            if (treeData == null)
            {
                _parameters.Add("Operation", "Add");
                _parameters.Add("Id", 0);
            }
            else
            {
                if (CanAddNewCarModel)
                {
                    _parameters.Add("Operation", "Add");
                    _parameters.Add("Id", 0);
                    _parameters.Add("CompanyID", treeData.Id);
                }
                else
                {
                    _parameters.Add("Operation", "Update");
                    _parameters.Add("Id", treeData.Id);
                }
            }
            _ModuleActivationParameters = new ModuleActivationParameters()
            {
                ModuleName = "AutoDesk.Client.CarCompany",
                RegionName = "CarCompanyRegion",
                Parameters = _parameters
            };
            xapLoader = new XAPDialogLoader(this.Container, "Add Car Model", _ModuleActivationParameters, OnLoadModuleCompleted,
                                                            550, 300, false, false, null, false);
            xapLoader.ShowDialog();
        }

        #endregion

        #region Purchaser Module
        private DelegateCommand _ShowPurchaserCommand;
        public DelegateCommand ShowPurchaserCommand
        {
            get
            {
                return _ShowPurchaserCommand ?? (_ShowPurchaserCommand = new DelegateCommand(ShowPurchaser));
            }
        }

        public void ShowPurchaser()
        {
            if (!isShowPurchaserXAPLoaded)
            {
                ShowGridSplitter = true;
                ShowPurchaserRegion = true;
                Action<LoadModuleCompletedEventArgs> LoadCompletedAction = null;
                if (!isShowPurchaserXAPLoaded)
                {
                    LoadCompletedAction = (LoadModuleCompletedEventArgs e) =>
                    {
                        if (e.Error != null)
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.ToString()));
                            return;
                        }
                    };
                }
                isShowPurchaserXAPLoaded = true;
                Dictionary<string, object> _parameters = new Dictionary<string, object>();
                _parameters.Add("Type", Purchaser.ShowPurchaser);
                _parameters.Add("carModelID", CarModelID);
                _ModuleActivationParameters = new ModuleActivationParameters()
                {
                    ModuleName = "AutoDesk.Client.Purchaser",
                    RegionName = "ShowPurchaserRegion",
                    Parameters = _parameters
                };
                ModuleActivationHelper.ActivateModule(_ModuleActivationParameters, Container, LoadCompletedAction);
            }
            else
            {
                ShowGridSplitter = false;
                ShowPurchaserRegion = false;
                isShowPurchaserXAPLoaded = false;
            }
        }

        public void LoadPurchaserModule()
        {
            XAPDialogLoader xapLoader = null;
            Dictionary<string, object> _parameters = new Dictionary<string, object>();
            if (treeData != null)
            {
                _parameters.Add("Type", Purchaser.AddPurchaser);
                _parameters.Add("carModelID", CarModelID);
            }
            _ModuleActivationParameters = new ModuleActivationParameters()
            {
                ModuleName = "AutoDesk.Client.Purchaser",
                RegionName = "AddPurchaserRegion",
                Parameters = _parameters
            };
            xapLoader = new XAPDialogLoader(this.Container, "Add New Purchaser", _ModuleActivationParameters, OnLoadPurchaserModuleCompleted,
                                                            550, 300, false, false, null, false);
            xapLoader.ShowDialog();
        }

        #endregion

        #region Context Menu Events

        #region Delete
        private DelegateCommand<stcTreeData> _DeleteCommand;
        public DelegateCommand<stcTreeData> DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new DelegateCommand<stcTreeData>(Delete));
            }
        }

        private void Delete(stcTreeData treeData)
        {
            if (treeData != null)
            {
                if (treeData.ParentId == 0)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        string Message = treeData.Subs.Count > 0 ? "Are you sure you want to delete " + treeData.Name + " with all its Car Models ?" : "Are you sure you want to delete " + treeData.Name + " ?";
                        if (MessageBox.Show(Message, "Auto Desk Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            serviceRef.DeleteCarCompany(treeData.Id, OnDeleteCompleted);
                        }
                    });
                }
                else
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        if (MessageBox.Show("Are you sure you want to delete " + treeData.Name + "  ?", "Auto Desk Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            serviceRef.DeleteCarModel(treeData.Id, OnDeleteCompleted);
                        }
                    });
                }
                this.GetTreeData();
            }
        }

        private void OnDeleteCompleted(bool result, Exception ex)
        {
            if (ex != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString()));
            }
            else
            {
                if (result)
                    this.GetTreeData();
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Problem in Delete Operation."));
            }
        }
        #endregion

        #region Add Update
        private DelegateCommand<stcTreeData> _UpdateCommand;
        public DelegateCommand<stcTreeData> UpdateCommand
        {
            get
            {
                return _UpdateCommand ?? (_UpdateCommand = new DelegateCommand<stcTreeData>(Update));
            }
        }

        private void Update(stcTreeData treeData)
        {
            if (treeData != null)
            {
                if (treeData.ParentId == 0)
                {
                    LoadCarCompanyModule(treeData);
                }
                else
                    LoadCarModelModule(treeData);
            }
        }
        #endregion

        #region Add New Car Model
        private DelegateCommand<stcTreeData> _AddNewCarModelCommand;
        public DelegateCommand<stcTreeData> AddNewCarModelCommand
        {
            get
            {
                return _AddNewCarModelCommand ?? (_AddNewCarModelCommand = new DelegateCommand<stcTreeData>(AddNewCarModel));
            }
        }

        private void AddNewCarModel(stcTreeData treeData)
        {
            if (treeData != null)
            {
                if (treeData.ParentId == 0)
                {
                    if (CanAddNewCarModel)
                        LoadCarModelModule(treeData);
                }
            }
        }
        #endregion

        #region Add Purchaser
        private DelegateCommand _AddPurchaserCommand;
        public DelegateCommand AddPurchaserCommand
        {
            get
            {
                return _AddPurchaserCommand ?? (_AddPurchaserCommand = new DelegateCommand(AddPurchaser));
            }
        }

        private void AddPurchaser()
        {
            if (treeData != null)
            {
                LoadPurchaserModule();
            }
        }
        #endregion

        #endregion

        #region Search
        private DelegateCommand _SearchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _SearchCommand ?? (_SearchCommand = new DelegateCommand(Search)); }
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Please Enter Text to Search", "AutoDesk Error Message", MessageBoxButton.OK));
                return;
            }
            serviceRef.GetRefinedTreeData(SearchText, GetRefinedTreeDataCompleted);
        }

        private void GetRefinedTreeDataCompleted(List<stcTreeData> Result, Exception ex)
        {
            if (ex != null)
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(ex.ToString(), "AutoDesk Error Message", MessageBoxButton.OK));
            else
            {
                if (Result.Count == 0)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        if (MessageBox.Show("No Data Found.", "Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                            GetTreeData();
                    });
                }
                else
                    treeData = Result;
            }
        }
        #endregion

        #region Variables
        public IUnityContainer Container
        {
            get;
            set;
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

        private List<stcTreeData> _treeData;
        public List<stcTreeData> treeData
        {
            get { return _treeData; }
            set
            {
                if (_treeData == value)
                    return;
                _treeData = value;
                OnPropertyChanged("treeData");
            }
        }

        private bool _IsShowPurchaser;
        public bool IsShowPurchaser
        {
            get { return _IsShowPurchaser; }
            set
            {
                if (_IsShowPurchaser == value)
                    return;
                _IsShowPurchaser = value;
                OnPropertyChanged("IsShowPurchaser");
            }
        }

        private bool _CanAddNewCarModel;
        public bool CanAddNewCarModel
        {
            get { return _CanAddNewCarModel; }
            set
            {
                if (_CanAddNewCarModel == value) return;
                _CanAddNewCarModel = value;
                OnPropertyChanged("CanAddNewCarModel");
            }
        }

        private bool _ShowGridSplitter;
        public bool ShowGridSplitter
        {
            get { return _ShowGridSplitter; }
            set
            {
                if (_ShowGridSplitter == value)
                    return;
                _ShowGridSplitter = value;
                OnPropertyChanged("ShowGridSplitter");
            }
        }

        private bool _ShowPurchaserRegion;
        public bool ShowPurchaserRegion
        {
            get { return _ShowPurchaserRegion; }
            set
            {
                if (_ShowPurchaserRegion == value)
                    return;
                _ShowPurchaserRegion = value;
                OnPropertyChanged("ShowPurchaserRegion");
            }
        }

        private int _CarModelID;
        public int CarModelID
        {
            get { return _CarModelID; }
            set
            {
                if (_CarModelID == value) return;
                _CarModelID = value;
                OnPropertyChanged("CarModelID");
            }
        }

        public bool isShowPurchaserXAPLoaded
        {
            get { return _isShowPurchaserXAPLoaded; }
            set
            {
                if (_isShowPurchaserXAPLoaded == value) return;
                _isShowPurchaserXAPLoaded = value;
                OnPropertyChanged("isShowPurchaserXAPLoaded");
            }
        }

        private stcTreeData _SelectedModel;
        public stcTreeData SelectedModel
        {
            get { return _SelectedModel; }
            set
            {
                if (_SelectedModel != value) { isShowPurchaserXAPLoaded = false; }
                _SelectedModel = value;
                OnPropertyChanged("SelectedModel");
                if (_SelectedModel.Subs != null)
                {
                    this.IsShowPurchaser = false;
                    this.ShowGridSplitter = false;
                    this.ShowPurchaserRegion = false;
                    this.CanAddNewCarModel = true;
                    this.CanAddNewPurchaser = false;
                }
                else
                {
                    this.CanAddNewCarModel = false;
                    this.CanAddNewPurchaser = true;
                    this.IsShowPurchaser = true;
                    if (_SelectedModel != null)
                        this.CarModelID = _SelectedModel.Id;
                }
            }
        }

        private bool _CanAddNewPurchaser;
        public bool CanAddNewPurchaser
        {
            get { return _CanAddNewPurchaser; }
            set
            {
                _CanAddNewPurchaser = value;
                OnPropertyChanged("CanAddNewPurchaser");
            }
        }

        private bool _isExpanded;
        public bool isExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("isExpanded");
            }
        }

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText == value) return;
                _SearchText = value;
                OnPropertyChanged("SearchText");
                if (string.IsNullOrEmpty(_SearchText))
                    GetTreeData();
            }
        }
        #endregion
    }
}
