using System;
using System.Net;
using System.Windows;
using AutoDesk.Client.Common.BaseClasses;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AutoDesk.Client.Shell
{
    public class ShellModule : BaseModule
    {
        private IModuleManager _moduleManager;
        private IModuleCatalog _moduleCatalog;
        
        public ShellModule(IUnityContainer container, IRegionManager regionManager, IModuleManager moduleManager, IModuleCatalog _cat)
            : base(container, regionManager)
        {
            _moduleManager = moduleManager;
            _moduleCatalog = _cat;                     
        }

        protected override void ActivateModule()
        {

        }
    }
}
