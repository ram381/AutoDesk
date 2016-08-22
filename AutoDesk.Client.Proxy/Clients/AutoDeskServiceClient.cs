using AutoDesk.Client.Proxy.Definitions;
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
using System.ServiceModel;
using AutoDesk.PCL.Common;
using System.Collections.Generic;

namespace AutoDesk.Client.Proxy.Clients
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public sealed class AutoDeskServiceClient : ClientBase<IAutoDeskService>, IAutoDeskService
    {
        public AutoDeskServiceClient()
        {
        }

        public AutoDeskServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public AutoDeskServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AutoDeskServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public AutoDeskServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public System.IAsyncResult BeginGetCarCompanies(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCarCompanies(callback, asyncState);
        }

        public List<stccarCompany> EndGetCarCompanies(System.IAsyncResult result)
        {
            return base.Channel.EndGetCarCompanies(result);
        }

        public System.IAsyncResult BeginGetCarModels(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCarModels(callback, asyncState);
        }

        public List<stccarModel> EndGetCarModels(System.IAsyncResult result)
        {
            return base.Channel.EndGetCarModels(result);
        }

        public System.IAsyncResult BeginGetCompanyByID(int Id, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCompanyByID(Id, callback, asyncState);
        }

        public stccarCompany EndGetCompanyByID(System.IAsyncResult result)
        {
            return base.Channel.EndGetCompanyByID(result);
        }

        public System.IAsyncResult BeginGetCarModelByID(int Id, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCarModelByID(Id, callback, asyncState);
        }

        public stccarModel EndGetCarModelByID(System.IAsyncResult result)
        {
            return base.Channel.EndGetCarModelByID(result);
        }

        public System.IAsyncResult BeginGetPurchasersByCarModel(int Id, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPurchasersByCarModel(Id, callback, asyncState);
        }

        public List<stcPurchaser> EndGetPurchasersByCarModel(System.IAsyncResult result)
        {
            return base.Channel.EndGetPurchasersByCarModel(result);
        }

        public System.IAsyncResult BeginGetCarModelsByCompany(int Id, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCarModelsByCompany(Id, callback, asyncState);
        }

        public List<stccarModel> EndGetCarModelsByCompany(System.IAsyncResult result)
        {
            return base.Channel.EndGetCarModelsByCompany(result);
        }

        public System.IAsyncResult BeginAddEditCarCompany(stccarCompany carCompany, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginAddEditCarCompany(carCompany, callback, asyncState);
        }

        public string EndAddEditCarCompany(System.IAsyncResult result)
        {
            return base.Channel.EndAddEditCarCompany(result);
        }

        public System.IAsyncResult BeginAddEditCarModel(stccarModel carModel, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginAddEditCarModel(carModel, callback, asyncState);
        }

        public string EndAddEditCarModel(System.IAsyncResult result)
        {
            return base.Channel.EndAddEditCarModel(result);
        }

        public System.IAsyncResult BeginGetTreeData(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetTreeData(callback, asyncState);
        }

        public List<stcTreeData> EndGetTreeData(System.IAsyncResult result)
        {
            return base.Channel.EndGetTreeData(result);
        }

        public System.IAsyncResult BeginDeleteCarCompany(int ID, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginDeleteCarCompany(ID, callback, asyncState);
        }

        public bool EndDeleteCarCompany(System.IAsyncResult result)
        {
            return base.Channel.EndDeleteCarCompany(result);
        }

        public System.IAsyncResult BeginDeleteCarModel(int ID, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginDeleteCarModel(ID, callback, asyncState);
        }

        public bool EndDeleteCarModel(System.IAsyncResult result)
        {
            return base.Channel.EndDeleteCarModel(result);
        }

        public System.IAsyncResult BeginAddEditPurchaser(stcPurchaser purchaser, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginAddEditPurchaser(purchaser, callback, asyncState);
        }

        public string EndAddEditPurchaser(System.IAsyncResult result)
        {
            return base.Channel.EndAddEditPurchaser(result);
        }

        public System.IAsyncResult BeginGetRefinedTreeData(string searchString, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRefinedTreeData(searchString, callback, asyncState);
        }

        public List<stcTreeData> EndGetRefinedTreeData(System.IAsyncResult result)
        {
            return base.Channel.EndGetRefinedTreeData(result);
        }
    }
}
