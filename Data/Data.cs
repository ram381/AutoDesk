using AutoDesk.PCL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace Data
{
    public class DataBase : System.IDisposable
    {
        public List<stccarCompany> GetCarCompanies()
        {
            List<stccarCompany> CarCompanyList = new List<stccarCompany>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    CarCompanyList = db.TblCarCompanies.Select(p => p.ToPOCO()).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CarCompanyList;
        }

        public List<stccarModel> GetCarModels()
        {
            List<stccarModel> CarModelsList = new List<stccarModel>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    CarModelsList = db.TblCarModels.Select(p => p.ToPOCO()).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CarModelsList;
        }

        public stccarCompany GetCompanyByID(int ID)
        {
            stccarCompany Company = new stccarCompany();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    Company = db.TblCarCompanies.Where(p => p.Id == ID).Select(p => p.ToPOCO()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Company;
        }

        public stccarModel GetCarModelByID(int ID)
        {
            stccarModel CarModel = new stccarModel();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    CarModel = db.TblCarModels.Where(p => p.Id == ID).Select(p => p.ToPOCO()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CarModel;
        }

        public List<stccarModel> GetCarModelsByCompany(int ID)
        {
            List<stccarModel> CarModelsList = new List<stccarModel>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    CarModelsList = db.TblCarModels.Select(p => p.ToPOCO()).Where(p => p.CompanyId == ID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CarModelsList;
        }

        public List<stcPurchaser> GetPurchasersByCarModel(int ID)
        {
            List<stcPurchaser> Purchasers = new List<stcPurchaser>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    Purchasers = db.TblPurchasers.Where(p => p.CarModelId == ID).Select(p => p.ToPOCO()).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Purchasers;
        }

        public void AddEditCarCompany(stccarCompany carCompany)
        {
            TblCarCompany objcarCompany = new TblCarCompany();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    if (carCompany.Id == 0)
                    {
                        objcarCompany.ToObject(carCompany);
                        db.TblCarCompanies.InsertOnSubmit(objcarCompany);
                        db.SubmitChanges();
                    }
                    else
                    {
                        objcarCompany = (from carCompanyObject in db.TblCarCompanies where carCompanyObject.Id == carCompany.Id select carCompanyObject).FirstOrDefault();
                        if (objcarCompany != null)
                        {
                            objcarCompany.Id = carCompany.Id;
                            objcarCompany.Name = carCompany.Name;
                            objcarCompany.Owner = carCompany.Owner;
                            objcarCompany.Phone = carCompany.Phone;
                            objcarCompany.Address = carCompany.Address;
                            objcarCompany.Email = carCompany.Email;
                            db.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objcarCompany != null)
                {
                    Dispose();
                    objcarCompany = null;
                }
            }
        }

        public void AddEditCarModel(stccarModel carModel)
        {
            TblCarModel objcarModel = new TblCarModel();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    if (carModel.Id == 0)
                    {
                        objcarModel.ToObject(carModel);
                        db.TblCarModels.InsertOnSubmit(objcarModel);
                        db.SubmitChanges();
                    }
                    else
                    {
                        objcarModel = (from carModelObject in db.TblCarModels where carModelObject.Id == carModel.Id select carModelObject).FirstOrDefault();
                        if (objcarModel != null)
                        {
                            objcarModel.CompanyId = carModel.CompanyId;
                            objcarModel.Cost = carModel.Cost;
                            objcarModel.Id = carModel.Id;
                            objcarModel.Milege = carModel.Milege;
                            objcarModel.Model_Number = carModel.Model_Number;
                            objcarModel.Part_No = carModel.Part_No;
                            objcarModel.Serial_No = carModel.Serial_No;
                            db.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objcarModel != null)
                {
                    Dispose();
                    objcarModel = null;
                }
            }
        }

        public List<stcTreeData> GetTreeData()
        {
            List<stcTreeData> treeData = new List<stcTreeData>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    var q = from u in db.TblCarCompanies
                            select new stcTreeData
                            {
                                Id = u.Id,
                                Name = u.Name,
                                ParentId = 0
                                ,
                                Subs = (from sub in db.TblCarModels
                                        where sub.CompanyId == u.Id
                                        select new stcTreeData
                                        {
                                            Id = sub.Id,
                                            Name = sub.Model_Number,
                                            ParentId = u.Id,
                                            Subs = null
                                        }
                                        ).ToList()
                            };
                    treeData = q.OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return treeData;
        }

        public List<stcTreeData> GetRefinedTreeData(string searchString)
        {
            List<stcTreeData> treeData = new List<stcTreeData>();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    var q = from u in db.TblCarCompanies
                            where u.Name.Contains(searchString)
                            select new stcTreeData
                            {
                                Id = u.Id,
                                Name = u.Name,
                                ParentId = 0,
                                Subs = (from sub in db.TblCarModels
                                        where sub.CompanyId == u.Id
                                        select new stcTreeData
                                        {
                                            Id = sub.Id,
                                            Name = sub.Model_Number,
                                            ParentId = u.Id,
                                            Subs = null
                                        }
                                        ).ToList()
                            };
                    var m = from u in db.TblCarCompanies
                            select new stcTreeData
                            {
                                Id = u.Id,
                                Name = u.Name,
                                ParentId = 0,
                                Subs = (from sub in db.TblCarModels
                                        where sub.CompanyId == u.Id
                                        && sub.Model_Number.Contains(searchString)
                                        select new stcTreeData
                                        {
                                            Id = sub.Id,
                                            Name = sub.Model_Number,
                                            ParentId = u.Id,
                                            Subs = null
                                        }
                                        ).ToList()
                            };
                    List<stcTreeData> childResult = m.ToList();
                    List<stcTreeData> parentResult = q.ToList();
                    childResult.RemoveAll(x => x.Subs.Count == 0);
                    childResult.RemoveAll(x => parentResult.Any(a => a.Id == x.Id));
                    var Result = parentResult.Union(childResult);
                    treeData = Result.OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return treeData;
        }

        public bool DeleteCarCompany(int ID)
        {
            bool isDeleted = false;
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    if (ID > 0)
                    {
                        //Deletes Car Models Associated with Car Company
                        List<TblCarModel> objCarModel = db.TblCarModels.Where(p => p.CompanyId == ID).ToList(); ;
                        if (objCarModel != null && objCarModel.Count > 0)
                        {
                            db.TblCarModels.DeleteAllOnSubmit(objCarModel);
                            db.SubmitChanges();
                        }

                        TblCarCompany objcarCompany = (from carCompanyObject in db.TblCarCompanies where carCompanyObject.Id == ID select carCompanyObject).FirstOrDefault();
                        if (objcarCompany != null)
                        {
                            db.TblCarCompanies.DeleteOnSubmit(objcarCompany);
                            db.SubmitChanges();
                        }
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isDeleted = false;
                throw ex;
            }
            return isDeleted;
        }

        public bool DeleteCarModel(int ID)
        {
            bool isDeleted = false;
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    if (ID > 0)
                    {
                        TblCarModel objcarModel = (from carModelObject in db.TblCarModels where carModelObject.Id == ID select carModelObject).FirstOrDefault();
                        if (objcarModel != null)
                        {
                            db.TblCarModels.DeleteOnSubmit(objcarModel);
                            db.SubmitChanges();
                            isDeleted = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isDeleted = false;
                throw ex;
            }
            return isDeleted;
        }

        public void AddEditPurchaser(stcPurchaser purchaser)
        {
            TblPurchaser objPurchaser = new TblPurchaser();
            try
            {
                using (AutoDeskDataContext db = new AutoDeskDataContext())
                {
                    if (purchaser.Id == 0)
                    {
                        objPurchaser.ToObject(purchaser);
                        db.TblPurchasers.InsertOnSubmit(objPurchaser);
                        db.SubmitChanges();
                    }
                    else
                    {
                        objPurchaser = (from purchaserObject in db.TblPurchasers where purchaserObject.Id == purchaser.Id select purchaserObject).FirstOrDefault();
                        if (objPurchaser != null)
                        {
                            objPurchaser.CarModelId = purchaser.CarModelId;
                            objPurchaser.Name = purchaser.Name;
                            objPurchaser.Id = purchaser.Id;
                            objPurchaser.Address = purchaser.Address;
                            objPurchaser.Cell_Phone = purchaser.Cell_Phone;
                            objPurchaser.Home_Phone = purchaser.Home_Phone;
                            objPurchaser.Email = purchaser.Email;
                            db.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objPurchaser != null)
                {
                    Dispose();
                    objPurchaser = null;
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


}
