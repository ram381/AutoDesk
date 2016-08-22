using AutoDesk.Client.CarCompany.Views;
using AutoDesk.Client.Common.BaseClasses;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
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

namespace AutoDesk.Client.CarCompany
{
    public class CarCompanyModule : BaseModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private IModuleActivationParameters _moduleActivationParameters;

        public CarCompanyModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager)
        {
            this.regionManager = regionManager;
            this.container = container;

            IModuleActivationParameters activationParameterstemp = container.Resolve<IModuleActivationParameters>(string.Format("{0}.ModuleParameters", "AutoDesk.Client.CarCompany"));
            _moduleActivationParameters = activationParameterstemp.Clone();
            activationParameterstemp = null;

            var eventAggregator = this.Container.Resolve<IEventAggregator>();
            var isModuleLoaded = eventAggregator.GetEvent<ModuleActivatingEvent>();
            isModuleLoaded.Subscribe(OnModuleLoaded, true);
            isModuleLoaded = null;
            eventAggregator = null;
        }

        public override void Initialize()
        {
            ActivateInRegion(_moduleActivationParameters);
        }

        protected override void ActivateModule()
        {
            ActivateInRegion(_moduleActivationParameters);
        }

        protected void OnModuleLoaded(IModuleActivationParameters moduleActivationParameters)
        {
            if (moduleActivationParameters.ModuleName == "AutoDesk.Client.CarCompany")
                ActivateInRegion(moduleActivationParameters);
        }

        private void ActivateInRegion(IModuleActivationParameters moduleActivationParameters)
        {
            IRegion mainRegion = this.RegionManager.Regions[moduleActivationParameters.RegionName];
            var view = mainRegion.GetView("AddEditCarCompany");
            if (Convert.ToBoolean(moduleActivationParameters.Parameters["IsCarCompany"]) == true)
            {
                view = mainRegion.GetView("AddEditCarCompany");
                if (view != null)
                    mainRegion.Remove(view);
                AddEditCarCompany addremoveView = new AddEditCarCompany(container, moduleActivationParameters);
                view = addremoveView;
                mainRegion.Add(view, "AddEditCarCompany");
            }
            else
            {
                view = mainRegion.GetView("AddEditCarModel");
                if (view != null)
                    mainRegion.Remove(view);
                AddEditCarModel addremoveView = new AddEditCarModel(container, moduleActivationParameters);
                view = addremoveView;
                mainRegion.Add(view, "AddEditCarModel");
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
