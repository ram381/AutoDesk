using AutoDesk.PCL.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.Proxy.Definitions
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute()]
    public interface IAutoDeskService
    {
        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetCarCompanies", ReplyAction = "http://tempuri.org/IAutoDeskService/GetCarCompaniesResponse")]
        System.IAsyncResult BeginGetCarCompanies(System.AsyncCallback callback, object asyncState);
        List<stccarCompany> EndGetCarCompanies(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetCarModels", ReplyAction = "http://tempuri.org/IAutoDeskService/GetCarModelsResponse")]
        System.IAsyncResult BeginGetCarModels(System.AsyncCallback callback, object asyncState);
        List<stccarModel> EndGetCarModels(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetCompanyByID", ReplyAction = "http://tempuri.org/IAutoDeskService/GetCompanyByIDResponse")]
        System.IAsyncResult BeginGetCompanyByID(int Id, System.AsyncCallback callback, object asyncState);
        stccarCompany EndGetCompanyByID(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetCarModelByID", ReplyAction = "http://tempuri.org/IAutoDeskService/GetCarModelByIDResponse")]
        System.IAsyncResult BeginGetCarModelByID(int Id, System.AsyncCallback callback, object asyncState);
        stccarModel EndGetCarModelByID(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetPurchasersByCarModel", ReplyAction = "http://tempuri.org/IAutoDeskService/GetPurchasersByCarModelResponse")]
        System.IAsyncResult BeginGetPurchasersByCarModel(int Id, System.AsyncCallback callback, object asyncState);
        List<stcPurchaser> EndGetPurchasersByCarModel(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetCarModelsByCompany", ReplyAction = "http://tempuri.org/IAutoDeskService/GetCarModelsByCompanyResponse")]
        System.IAsyncResult BeginGetCarModelsByCompany(int Id, System.AsyncCallback callback, object asyncState);
        List<stccarModel> EndGetCarModelsByCompany(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/AddEditCarCompany", ReplyAction = "http://tempuri.org/IAutoDeskService/AddEditCarCompanyResponse")]
        System.IAsyncResult BeginAddEditCarCompany(stccarCompany carCompany, System.AsyncCallback callback, object asyncState);
        string EndAddEditCarCompany(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/AddEditCarModel", ReplyAction = "http://tempuri.org/IAutoDeskService/AddEditCarModelResponse")]
        System.IAsyncResult BeginAddEditCarModel(stccarModel carModel, System.AsyncCallback callback, object asyncState);
        string EndAddEditCarModel(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetTreeData", ReplyAction = "http://tempuri.org/IAutoDeskService/GetTreeDataResponse")]
        System.IAsyncResult BeginGetTreeData(System.AsyncCallback callback, object asyncState);
        List<stcTreeData> EndGetTreeData(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/DeleteCarCompany", ReplyAction = "http://tempuri.org/IAutoDeskService/DeleteCarCompanyResponse")]
        System.IAsyncResult BeginDeleteCarCompany(int ID, System.AsyncCallback callback, object asyncState);
        bool EndDeleteCarCompany(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/DeleteCarModel", ReplyAction = "http://tempuri.org/IAutoDeskService/DeleteCarModelResponse")]
        System.IAsyncResult BeginDeleteCarModel(int ID, System.AsyncCallback callback, object asyncState);
        bool EndDeleteCarModel(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/AddEditPurchaser", ReplyAction = "http://tempuri.org/IAutoDeskService/AddEditPurchaserResponse")]
        System.IAsyncResult BeginAddEditPurchaser(stcPurchaser purchaser, System.AsyncCallback callback, object asyncState);
        string EndAddEditPurchaser(System.IAsyncResult result);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IAutoDeskService/GetRefinedTreeData", ReplyAction = "http://tempuri.org/IAutoDeskService/GetRefinedTreeDataResponse")]
        System.IAsyncResult BeginGetRefinedTreeData(string searchString, System.AsyncCallback callback, object asyncState);
        List<stcTreeData> EndGetRefinedTreeData(System.IAsyncResult result);
    }
}
