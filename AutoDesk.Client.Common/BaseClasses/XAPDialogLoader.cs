using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Telerik.Windows.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using System.Linq;
using System.Text;
using AutoDesk.Client.Common.Events;
using AutoDesk.PCL.Common;
using AutoDesk.PCL.Common.Enums;
using static AutoDesk.PCL.Common.Enums.clsEnumeration;

namespace AutoDesk.Client.Common.BaseClasses
{
    public sealed class XAPDialogLoader
    {
        private readonly IUnityContainer _unityContainer;
        private readonly string _dialogCaption;
        private ContentControl toolbarContent;
        private int _dialogWidth, _dialogHeight;
        private Uri _dialogIcon;
        private bool _hasValidationPromptOnClose = false, _ignoreValidationPrompt = false;
        private bool _isNonModalDialog = false;
        private bool _hasToolbar = false;
        private IModuleActivationParameters _moduleActivationParam;
        private Action<bool, Exception> _callbackAction;

        public XAPDialogLoader(IUnityContainer unityContainer, string dialogCaption, IModuleActivationParameters activationParam,
                                Action<bool, Exception> callbackAction,
                                int dialogWidth = 800, int dialogHeight = 600,
                                bool hasValidationPromptOnClose = false,
                                bool isNonModalDialog = false,
                                Uri dialogIcon = null,
                                bool hasToolbar = false
                                )
        {
            GC.Collect();
            _unityContainer = unityContainer;
            _dialogCaption = dialogCaption;
            _dialogIcon = dialogIcon;
            _dialogWidth = dialogWidth;
            _dialogHeight = dialogHeight;
            _isNonModalDialog = isNonModalDialog;
            _hasToolbar = hasToolbar;
            _hasValidationPromptOnClose = hasValidationPromptOnClose;
            _moduleActivationParam = activationParam.Clone();
            _callbackAction = callbackAction;
            RegionManager.UpdateRegions();
        }

        public void ShowDialog()
        {
            var regionManager = _unityContainer.Resolve<IRegionManager>();
            RadWindow dialogWindow = new RadWindow()
            {
                WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen,
                ModalBackground = new SolidColorBrush(Colors.Transparent),
                Height = _dialogHeight,
                Width = _dialogWidth,
                IsTopmost = false,
                Style = Application.Current.Resources["ModalWindowStyle"] as Style,
                Icon = new Image() { Source = new System.Windows.Media.Imaging.BitmapImage(_dialogIcon) }
            };
            RadBusyIndicator busyIndicator = new RadBusyIndicator()
            {
                Name = "busyIndicator",
                IsBusy = false,
                BusyContent = "Loading...",
                IsIndeterminate = true,
            };
            if (_hasToolbar)
            {
                StringBuilder headerTemplateXaml = new StringBuilder();
                headerTemplateXaml.Append("<DataTemplate\r\n");
                headerTemplateXaml.Append("xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n");
                headerTemplateXaml.Append("xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n");
                headerTemplateXaml.Append("xmlns:telerik=\"http://schemas.telerik.com/2008/xaml/presentation\"\r\n");
                headerTemplateXaml.Append(">\r\n");
                headerTemplateXaml.Append("<Grid>\r\n");
                headerTemplateXaml.Append("<Grid.ColumnDefinitions>\r\n");
                headerTemplateXaml.Append("<ColumnDefinition />\r\n");
                headerTemplateXaml.Append("<ColumnDefinition Width=\"Auto\" />\r\n");
                headerTemplateXaml.Append("</Grid.ColumnDefinitions>\r\n");
                headerTemplateXaml.Append(string.Format("<TextBlock Grid.Column=\"0\" Text=\"{0}\" HorizontalAlignment=\"Left\" />\r\n", _dialogCaption));
                headerTemplateXaml.Append("<ContentControl x:Name=\"toolbar\" Grid.Column=\"1\" HorizontalAlignment=\"Right\" Margin=\"3,0\" />\r\n");
                headerTemplateXaml.Append("</Grid>\r\n");
                headerTemplateXaml.Append("</DataTemplate>");
                DataTemplate headerTemplate = (DataTemplate)System.Windows.Markup.XamlReader.Load(headerTemplateXaml.ToString());
                headerTemplate.DataType = typeof(RadWindow);
                Grid gridControl = headerTemplate.LoadContent() as Grid;
                toolbarContent = gridControl.FindName("toolbar") as ContentControl;
                dialogWindow.Header = gridControl;
                RegionManager.SetRegionManager(toolbarContent, regionManager);
            }
            else
            {
                dialogWindow.Header = _dialogCaption;
            }
            Grid layoutRoot = new Grid() { Name = "LayoutRoot" };
            ContentControl regionContentControl = new ContentControl() { Name = "ContentRegion", HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
            EventHandler<WindowClosedEventArgs> DialogClosed = null;
            EventHandler<WindowPreviewClosedEventArgs> DialogPreviewClosed = null;
            RoutedEventHandler DialogLoaded = null;
            SubscriptionToken subscriptionToken = null;
            var eventAggregator = _unityContainer.Resolve<IEventAggregator>();
            DialogLoaded = delegate (object sender, RoutedEventArgs e)
            {
                dialogWindow.Loaded -= DialogLoaded;
                try
                {
                    var messengerEvent = eventAggregator.GetEvent<MessengerEvent>();
                    var moduleManager = _unityContainer.Resolve<IModuleManager>();
                    var moduleCatalog = _unityContainer.Resolve<IModuleCatalog>();
                    var module = moduleCatalog.Modules.FirstOrDefault(moduleInfo => moduleInfo.ModuleName.Equals(_moduleActivationParam.ModuleName));
                    var win = (sender as RadWindow);
                    var modalBackground = win.ParentOfType<Canvas>();

                    RegionManager.SetRegionManager(regionContentControl, regionManager);
                    RegionManager.SetRegionName(regionContentControl, _moduleActivationParam.RegionName);

                    subscriptionToken = messengerEvent.Subscribe(delegate (Messenger messenger)
                    {
                        if (messenger.Id == MessengerId.AddEditCarCompany || messenger.Id == MessengerId.AddEditCarModel || messenger.Id == MessengerId.AddEditPurchaser)
                        {
                            if (messenger.Data != null)
                                dialogWindow.DialogResult = true;
                            else
                                dialogWindow.DialogResult = false;
                            messenger.Data = null;
                            _ignoreValidationPrompt = true;
                            dialogWindow.Close();
                        }
                    }, true);

                    if (module.State == ModuleState.NotStarted)
                    {
                        EventHandler<LoadModuleCompletedEventArgs> LoadModuleHandler = null;
                        LoadModuleHandler = (object d, LoadModuleCompletedEventArgs de) =>
                        {
                            busyIndicator.IsBusy = false;
                            moduleManager.LoadModuleCompleted -= LoadModuleHandler;
                        };
                        busyIndicator.IsBusy = true;
                        moduleManager.LoadModuleCompleted += LoadModuleHandler;
                        _unityContainer.RegisterInstance<IModuleActivationParameters>(string.Format("{0}.ModuleParameters", _moduleActivationParam.ModuleName), _moduleActivationParam);
                        moduleManager.LoadModule(_moduleActivationParam.ModuleName);
                    }
                    else
                    {
                        _unityContainer.Teardown(_moduleActivationParam);
                        var moduleLoadedEvent = eventAggregator.GetEvent<ModuleActivatingEvent>();
                        moduleLoadedEvent.Publish(_moduleActivationParam);
                    }
                }
                catch (Exception ex)
                {
                    _ignoreValidationPrompt = true;
                    _hasValidationPromptOnClose = false;
                    dialogWindow.Close();
                    _callbackAction(false, ex);
                }
            };
            DialogPreviewClosed = (object sender, WindowPreviewClosedEventArgs e) =>
                {
                    if (!_ignoreValidationPrompt &&
                        _hasValidationPromptOnClose)
                    {
                        e.Cancel = true;
                    }
                };
            DialogClosed = (object sender, WindowClosedEventArgs e) =>
                        {
                            dialogWindow.MouseRightButtonDown -= dialogWindow_MouseRightButtonDown;
                            dialogWindow.Closed -= DialogClosed;
                            if (_hasValidationPromptOnClose)
                            {
                                dialogWindow.PreviewClosed -= DialogPreviewClosed;
                            }
                            var messengerEvent = eventAggregator.GetEvent<MessengerEvent>();
                            messengerEvent.Unsubscribe(subscriptionToken);
                            messengerEvent = null;
                            try
                            {
                                if (regionManager.Regions.ContainsRegionWithName(_moduleActivationParam.RegionName))
                                {
                                    var hostedRegion = regionManager.Regions[_moduleActivationParam.RegionName];
                                    foreach (object view in hostedRegion.Views)
                                    {
                                        hostedRegion.Remove(view);
                                    }
                                    regionManager.Regions.Remove(_moduleActivationParam.RegionName);
                                }
                            }
                            catch { }
                            if (_hasToolbar)
                            {
                                toolbarContent.Content = null;
                                toolbarContent = null;
                                dialogWindow.Header = null;
                            }
                            RegionManager.UpdateRegions();
                            if (_callbackAction != null)
                            {
                                if (dialogWindow.DialogResult.HasValue && dialogWindow.DialogResult.Value)
                                {
                                    _callbackAction(true, null);
                                }
                                else
                                {
                                    _callbackAction(false, null);
                                }
                            }
                            _unityContainer.Teardown(_moduleActivationParam);
                            _moduleActivationParam = null;
                            GC.Collect();
                            RegionManager.UpdateRegions();
                        };

            busyIndicator.Content = layoutRoot;
            layoutRoot.Children.Add(regionContentControl);
            dialogWindow.Content = busyIndicator;
            dialogWindow.Closed += DialogClosed;
            dialogWindow.Loaded += DialogLoaded;
            dialogWindow.Tag = Guid.NewGuid();
            if (_hasValidationPromptOnClose)
            {
                dialogWindow.PreviewClosed += DialogPreviewClosed;
            }
            if (!_isNonModalDialog)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => dialogWindow.ShowDialog());
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => dialogWindow.Show());
            }
        }


        void dialogWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
