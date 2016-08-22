using System;
using System.Net;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System.Windows;
using AutoDesk.Client.Proxy;
using AutoDesk.Client.Common.BaseClasses;

namespace AutoDesk.Client.Shell
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
        }

        protected override DependencyObject CreateShell()
        {
            ServerSettings.SetServiceUri("http://localhost:31932");
            var view = Container.TryResolve<MainPage>();
            Application.Current.RootVisual = view;
            return view ;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/AutoDesk.Client.Shell;component/ModulesCatalog.xaml", UriKind.Relative));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.RegisterType<IModuleActivationParameters, ModuleActivationParameters>();
        }
    }
}
