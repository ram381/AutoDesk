﻿#pragma checksum "D:\Development Environment Setup\Projects\AutoDesk\AutoDesk.Client.CarCompany\Views\AddEditCarCompany.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F7089DDF1BA7852E79D9A61656F5F171"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace AutoDesk.Client.CarCompany.Views {
    
    
    public partial class AddEditCarCompany : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox txtCompanyName;
        
        internal System.Windows.Controls.TextBox txtCompanyEmail;
        
        internal System.Windows.Controls.TextBox txtCompanyPhone;
        
        internal System.Windows.Controls.TextBox txtAddress;
        
        internal System.Windows.Controls.TextBox txtOwner;
        
        internal Telerik.Windows.Controls.RadButton btnSaveCarCompany;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDesk.Client.CarCompany;component/Views/AddEditCarCompany.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtCompanyName = ((System.Windows.Controls.TextBox)(this.FindName("txtCompanyName")));
            this.txtCompanyEmail = ((System.Windows.Controls.TextBox)(this.FindName("txtCompanyEmail")));
            this.txtCompanyPhone = ((System.Windows.Controls.TextBox)(this.FindName("txtCompanyPhone")));
            this.txtAddress = ((System.Windows.Controls.TextBox)(this.FindName("txtAddress")));
            this.txtOwner = ((System.Windows.Controls.TextBox)(this.FindName("txtOwner")));
            this.btnSaveCarCompany = ((Telerik.Windows.Controls.RadButton)(this.FindName("btnSaveCarCompany")));
        }
    }
}

