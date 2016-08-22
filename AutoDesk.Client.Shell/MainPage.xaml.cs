using AutoDesk.Client.Shell.ViewModels;
using AutoDesk.PCL.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace AutoDesk.Client.Shell
{
    public partial class MainPage : UserControl
    {
        public IModuleManager _moduleManager;
        public IUnityContainer _container;
        private MainPageViewModel _viewModel;

        public MainPage(IModuleManager moduleManager, IUnityContainer container)
        {
            InitializeComponent();
            _moduleManager = moduleManager;
            _container = container;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainPageViewModel(_container);
            _viewModel.onViewLoading(sender, e);
            this.DataContext = _viewModel;
        }

        private void ContextMenuOpened(object sender, RoutedEventArgs e)
        {
            RadTreeViewItem item = ContextMenu.GetClickedElement<RadTreeViewItem>();
            if (item == null)
            {
                ((RadContextMenu)sender).IsOpen = false;
                return;
            }
            else
                _viewModel.SelectedModel = (stcTreeData)item.Item;
        }
    }
}
