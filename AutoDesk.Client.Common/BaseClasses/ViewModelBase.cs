using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.Common.BaseClasses
{
    public class ViewModelBase : Telerik.Windows.Controls.ViewModelBase
    {
        private IUnityContainer _container;
        protected object viewInstance;
        
        public ViewModelBase()
        {
            
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        public IUnityContainer Container
        {
            get { return _container; }
            set
            {
                if (_container == value)
                    return;
                _container = value;
                base.OnPropertyChanged("Container");
            }
        }

        [Dependency]
        public IRegionManager RegionManager
        {
            get;
            set;
        }

        //protected void NotifyPropertyChanged(string propertyName)
        //{
        //    Telerik.Windows.Controls.ViewModelBase.InvokeOnUIThread(() => base.OnPropertyChanged(propertyName));
        //}

        public virtual void OnViewLoaded(object sender, EventArgs e)
        {
            viewInstance = sender;
            this.SubscribeViewUnloaded();
        }

        private void OnViewLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.UnsubscribeViewLoaded();
            this.SubscribeViewUnloaded();
        }

        private void UnsubscribeViewLoaded()
        {
            Control viewControl = viewInstance as Control;
            if (viewControl != null)
            {
                viewControl.Loaded -= OnViewLoaded;
            }
        }

        private void SubscribeViewLoaded()
        {
            Control viewControl = viewInstance as Control;
            if (viewControl != null)
            {
                viewControl.Loaded += OnViewLoaded;
            }
        }

        private void SubscribeViewUnloaded()
        {
            Control viewControl = viewInstance as Control;
            if (viewControl != null)
            {
                viewControl.Unloaded += OnViewUnloaded;
            }
        }

        private void OnViewUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.UnsubscribeViewUnloaded();
            this.SubscribeViewLoaded();
        }

        private void UnsubscribeViewUnloaded()
        {
            Control viewControl = viewInstance as Control;
            if (viewControl != null)
            {
                viewControl.Unloaded -= OnViewUnloaded;
            }
        }
    }

    public sealed class RelayCommand<T> : ICommand
    {
        readonly Action<T> _TargetExecuteMethod;
        readonly Func<T, bool> _TargetCanExecuteMethod;

        public RelayCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members
        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }
        #endregion

    }

    public sealed class DelegateCommand : ICommand
    {
        private Action _callback;
        private bool _isEnabled;
        
        public DelegateCommand(Action callback)
        {
            _callback = callback;
            _isEnabled = true;
        }

   
        public void UpdateState(bool enabled)
        {
            _isEnabled = enabled;
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _isEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _callback();
        }

        #endregion
    }
}
