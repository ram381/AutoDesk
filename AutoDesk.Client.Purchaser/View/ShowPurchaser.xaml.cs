using AutoDesk.Client.Purchaser.ViewModel;
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

namespace AutoDesk.Client.Purchaser.View
{
    public partial class ShowPurchaser : UserControl
    {
        public IModuleManager _moduleManager;
        public IUnityContainer _container;
        private ShowPurchaserViewModel _viewModel;
        public int CarModelID = 0;

        public ShowPurchaser(IUnityContainer Container)
        {
            InitializeComponent();
            _container = Container;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new ShowPurchaserViewModel(_container);
            _viewModel.CarModelId = CarModelID;
            _viewModel.onViewLoading(sender, e);
            this.DataContext = _viewModel;
        }
    }
}
