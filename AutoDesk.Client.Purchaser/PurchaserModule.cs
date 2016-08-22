using AutoDesk.Client.Common.BaseClasses;
using AutoDesk.Client.Purchaser.View;
using AutoDesk.PCL.Common.Enums;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
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

namespace AutoDesk.Client.Purchaser
{
    public class PurchaserModule : BaseModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private IModuleActivationParameters _moduleActivationParameters;

        public PurchaserModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager)
        {
            this.regionManager = regionManager;
            this.container = container;

            IModuleActivationParameters activationParameterstemp = container.Resolve<IModuleActivationParameters>(string.Format("{0}.ModuleParameters", "AutoDesk.Client.Purchaser"));
            _moduleActivationParameters = activationParameterstemp.Clone();
            activationParameterstemp = null;

            var eventAggregator = this.Container.Resolve<IEventAggregator>();
            var isModuleLoaded = eventAggregator.GetEvent<ModuleActivatingEvent>();
            isModuleLoaded.Subscribe(OnModuleLoaded, true);
            isModuleLoaded = null;
            eventAggregator = null;
        }

        protected void OnModuleLoaded(IModuleActivationParameters moduleActivationParameters)
        {
            if (moduleActivationParameters.ModuleName == "AutoDesk.Client.Purchaser")
                ActivateInRegion(moduleActivationParameters);
        }

        public override void Initialize()
        {
            ActivateInRegion(_moduleActivationParameters);
        }

        protected override void ActivateModule()
        {
            this.ActivateInRegion(_moduleActivationParameters);
        }

        private void ActivateInRegion(IModuleActivationParameters moduleActivationParameters)
        {
            IRegion mainRegion = this.RegionManager.Regions[moduleActivationParameters.RegionName];
            if (Convert.ToInt32(moduleActivationParameters.Parameters["Type"]) == (int)clsEnumeration.Purchaser.ShowPurchaser)
            {
                var view = mainRegion.GetView("ShowPurchaserRegion");
                view = mainRegion.GetView("ShowPurchaserRegion");
                if (view != null)
                    mainRegion.Remove(view);
                ShowPurchaser addremoveView = new ShowPurchaser(container);
                addremoveView.CarModelID = Convert.ToInt16(moduleActivationParameters.Parameters["carModelID"]);
                view = addremoveView;
                mainRegion.Add(view, "ShowPurchaserRegion");
            }
            else
            {
                var view = mainRegion.GetView("AddPurchaserRegion");
                view = mainRegion.GetView("AddPurchaserRegion");
                if (view != null)
                    mainRegion.Remove(view);
                AddPurchaser addremoveView = new AddPurchaser(container);
                addremoveView.CarModelID = Convert.ToInt16(moduleActivationParameters.Parameters["carModelID"]);
                view = addremoveView;
                mainRegion.Add(view, "AddPurchaserRegion");
            }
        }

        protected override void OnUnloaded()
        {
            var eventAggregator = this.Container.Resolve<IEventAggregator>();
            var moduleLoadedEvent = eventAggregator.GetEvent<ModuleActivatingEvent>();
            moduleLoadedEvent.Unsubscribe(OnModuleLoaded);
            moduleLoadedEvent = null;
            eventAggregator = null;
            _moduleActivationParameters = null;
        }
    }
}
