using AutoDesk.Client.Common.BaseClasses;
using AutoDesk.Client.Purchaser.ViewModel;
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
    public partial class AddPurchaser : UserControl
    {
        private IUnityContainer _container;
        private AddPurchaserViewModel _viewModel;
        public int CarModelID = 0;

        public AddPurchaser(IUnityContainer Container)
        {
            InitializeComponent();
            _container = Container;            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new AddPurchaserViewModel(_container);
            _viewModel.onViewLoading(sender, e);
            _viewModel.CarModelId = CarModelID;
            this.DataContext = _viewModel;
        }
    }
}
