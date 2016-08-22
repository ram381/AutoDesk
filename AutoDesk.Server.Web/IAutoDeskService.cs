using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoDesk.PCL.Common;

namespace AutoDesk.Server.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAutoDeskService" in both code and config file together.
    [ServiceContract]
    public interface IAutoDeskService
    {
        [OperationContract]
        List<stccarCompany> GetCarCompanies();

        [OperationContract]
        List<stccarModel> GetCarModels();

        [OperationContract]
        stccarCompany GetCompanyByID(int Id);

        [OperationContract]
        stccarModel GetCarModelByID(int Id);

        [OperationContract]
        List<stcPurchaser> GetPurchasersByCarModel(int Id);

        [OperationContract]
        List<stccarModel> GetCarModelsByCompany(int Id);

        [OperationContract]
        void AddEditCarCompany(stccarCompany carCompany);

        [OperationContract]
        void AddEditCarModel(stccarModel carModel);

        [OperationContract]
        List<stcTreeData> GetTreeData();

        [OperationContract]
        bool DeleteCarCompany(int ID);

        [OperationContract]
        bool DeleteCarModel(int ID);

        [OperationContract]
        void AddEditPurchaser(stcPurchaser purchaser);

        [OperationContract]
        List<stcTreeData> GetRefinedTreeData(string searchString);
    }
}
