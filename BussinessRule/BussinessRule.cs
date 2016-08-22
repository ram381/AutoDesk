using AutoDesk.PCL.Common;
using Data;
using System;
using System.Collections.Generic;

namespace BussinessRule
{
    public class AutoDekBR : IDisposable
    {
        DataBase db = new DataBase();

        public List<stccarCompany> GetCarCompanies()
        {
            return db.GetCarCompanies();
        }

        public List<stccarModel> GetCarModels()
        {
            return db.GetCarModels();
        }

        public stccarCompany GetCompanyByID(int Id)
        {
            return db.GetCompanyByID(Id);
        }

        public stccarModel GetCarModelByID(int Id)
        {
            return db.GetCarModelByID(Id);
        }

        public List<stcPurchaser> GetPurchasersByCarModel(int Id)
        {
            return db.GetPurchasersByCarModel(Id);
        }

        public List<stccarModel> GetCarModelsByCompany(int Id)
        {
            return db.GetCarModelsByCompany(Id);
        }

        public void AddEditCarCompany(stccarCompany carCompany)
        {
            db.AddEditCarCompany(carCompany);
        }

        public void AddEditCarModel(stccarModel carModel)
        {
            db.AddEditCarModel(carModel);
        }

        public List<stcTreeData> GetTreeData()
        {
            return db.GetTreeData();
        }

        public bool DeleteCarCompany(int ID)
        {
            return db.DeleteCarCompany(ID);
        }

        public bool DeleteCarModel(int ID)
        {
            return db.DeleteCarModel(ID);
        }

        public void AddEditPurchaser(stcPurchaser purchaser)
        {
            db.AddEditPurchaser(purchaser);
        }

        public List<stcTreeData> GetRefinedTreeData(string searchString)
        {
            return db.GetRefinedTreeData(searchString);
        }

        void IDisposable.Dispose()
        {

        }
    }
}
