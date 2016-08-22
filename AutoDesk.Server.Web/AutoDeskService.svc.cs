using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using BussinessRule;
using AutoDesk.PCL.Common;

namespace AutoDesk.Server.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutoDeskService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AutoDeskService.svc or AutoDeskService.svc.cs at the Solution Explorer and start debugging.

    //[ServiceContract(Namespace ="")]
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AutoDeskService : IAutoDeskService
    {
        public List<stccarCompany> GetCarCompanies()
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetCarCompanies();               
            }
        }

        public List<stccarModel> GetCarModels()
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetCarModels();
            }
        }

        public stccarCompany GetCompanyByID(int Id)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetCompanyByID(Id);
            }
        }

        public stccarModel GetCarModelByID(int Id)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetCarModelByID(Id);
            }
        }

        public List<stcPurchaser> GetPurchasersByCarModel(int Id)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetPurchasersByCarModel(Id);
            }
        }

        public List<stccarModel> GetCarModelsByCompany(int Id)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetCarModelsByCompany(Id);
            }
        }

        public void AddEditCarCompany(stccarCompany carCompany)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                bussinessrule.AddEditCarCompany(carCompany);
            }
        }

        public void AddEditCarModel(stccarModel carModel)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                bussinessrule.AddEditCarModel(carModel);
            }
        }

        public List<stcTreeData> GetTreeData()
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetTreeData();
            }
        }

        public bool DeleteCarCompany(int ID)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.DeleteCarCompany(ID);
            }
        }

        public bool DeleteCarModel(int ID)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.DeleteCarModel(ID);
            }
        }

        public void AddEditPurchaser(stcPurchaser purchaser)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                bussinessrule.AddEditPurchaser(purchaser);
            }
        }

        public List<stcTreeData> GetRefinedTreeData(string searchString)
        {
            using (AutoDekBR bussinessrule = new AutoDekBR())
            {
                return bussinessrule.GetRefinedTreeData(searchString);
            }
        }
    }
}
