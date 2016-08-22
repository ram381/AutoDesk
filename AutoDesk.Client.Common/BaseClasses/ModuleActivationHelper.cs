using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Windows;

namespace AutoDesk.Client.Common.BaseClasses
{
    public static class ModuleActivationHelper
    {
        public static void ActivateModule(IModuleActivationParameters activationParameters, IUnityContainer unityContainer, Action<LoadModuleCompletedEventArgs> activationCompletedAction)
        {
            IModuleManager moduleManager = unityContainer.Resolve<IModuleManager>();
            IModuleCatalog moduleCatalog = unityContainer.Resolve<IModuleCatalog>();
            IEventAggregator eventAggregator = unityContainer.Resolve<IEventAggregator>();
            ModuleInfo moduleInfo = moduleCatalog.Modules.FirstOrDefault(catalog => catalog.ModuleName.Equals(activationParameters.ModuleName));
            if (moduleInfo != null)
            {
                if (moduleInfo.State == ModuleState.NotStarted)
                {
                    EventHandler<LoadModuleCompletedEventArgs> moduleLoadingCompleted = null;
                    moduleLoadingCompleted = (object sender, LoadModuleCompletedEventArgs e) =>
                    {
                        moduleManager.LoadModuleCompleted -= moduleLoadingCompleted;
                        if (activationCompletedAction != null)
                        {
                            activationCompletedAction(e);
                        }
                    };
                    moduleManager.LoadModuleCompleted += moduleLoadingCompleted;
                    unityContainer.RegisterInstance<IModuleActivationParameters>(string.Format("{0}.ModuleParameters", activationParameters.ModuleName), activationParameters);
                    moduleManager.LoadModule(activationParameters.ModuleName);
                }
                //else
                //{
                //    ModuleActivatingEvent activatingEvent = eventAggregator.GetEvent<ModuleActivatingEvent>();
                //    activatingEvent.Publish(activationParameters);
                //}
            }
            else
            {
                MessageBox.Show(string.Format("{0} module is not defined"), "AutoDesk", MessageBoxButton.OK);
            }
        }
    }
}