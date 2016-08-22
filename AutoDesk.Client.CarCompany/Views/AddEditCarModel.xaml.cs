using AutoDesk.Client.CarCompany.ViewModels;
using AutoDesk.Client.Common.BaseClasses;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.CarCompany.Views
{
    public partial class AddEditCarModel : UserControl
    {
        private IUnityContainer _container;
        private AddEditCarModelViewModel _viewModel;
        private IModuleActivationParameters _moduleActivationParameters;       

        public AddEditCarModel(IUnityContainer Container, IModuleActivationParameters ModuleActivationParameters)
        {
            InitializeComponent();
            _container = Container;
            _moduleActivationParameters = ModuleActivationParameters;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new AddEditCarModelViewModel(_container, _moduleActivationParameters);
            _viewModel.onViewLoading(sender, e);
            this.DataContext = _viewModel;
        }
    }
}
