﻿#pragma checksum "D:\Development Environment Setup\Projects\AutoDesk\AutoDesk.Client.CarCompany\Views\AddEditCarModel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "02E829ED6A429E2586EA3E7DD176846D"
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
    
    
    public partial class AddEditCarModel : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox txtCompanyModel;
        
        internal Telerik.Windows.Controls.RadComboBox ddlCompany;
        
        internal System.Windows.Controls.TextBox txtPartNo;
        
        internal System.Windows.Controls.TextBox txtSerialNo;
        
        internal System.Windows.Controls.TextBox txtCost;
        
        internal System.Windows.Controls.TextBox txtMilege;
        
        internal Telerik.Windows.Controls.RadButton btnSaveCarModel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/AutoDesk.Client.CarCompany;component/Views/AddEditCarModel.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtCompanyModel = ((System.Windows.Controls.TextBox)(this.FindName("txtCompanyModel")));
            this.ddlCompany = ((Telerik.Windows.Controls.RadComboBox)(this.FindName("ddlCompany")));
            this.txtPartNo = ((System.Windows.Controls.TextBox)(this.FindName("txtPartNo")));
            this.txtSerialNo = ((System.Windows.Controls.TextBox)(this.FindName("txtSerialNo")));
            this.txtCost = ((System.Windows.Controls.TextBox)(this.FindName("txtCost")));
            this.txtMilege = ((System.Windows.Controls.TextBox)(this.FindName("txtMilege")));
            this.btnSaveCarModel = ((Telerik.Windows.Controls.RadButton)(this.FindName("btnSaveCarModel")));
        }
    }
}

