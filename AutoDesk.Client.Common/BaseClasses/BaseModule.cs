using System.Reflection;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AutoDesk.Client.Common.BaseClasses
{
    public abstract class BaseModule : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;
        private AssemblyTitleAttribute _assemblyTitle;

        public BaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
        }

        #region IModule Members
        public virtual void Initialize()
        {
            //this.RegisterViewsAndServices();
            //this.RegisterViewRequestedEvent();
            this.ActivateModule();
        }

        #endregion

        protected virtual void OnUnloaded()
        {
        }

        protected virtual void RegisterViewRequestedEvent()
        {
            var eventAggregator = _unityContainer.Resolve<IEventAggregator>();
            eventAggregator = null;
        }

        //protected abstract void RegisterViewsAndServices();

        //protected abstract void OnViewRequested(string moduleName);

        protected abstract void ActivateModule();

        protected AssemblyTitleAttribute AssemblyTitle
        {
            get { return _assemblyTitle; }
            set { _assemblyTitle = value; }
        }

        protected IUnityContainer Container
        {
            get { return _unityContainer; }
        }

        protected IRegionManager RegionManager
        {
            get { return _regionManager; }
        }

    }
}
