using AutoDesk.PCL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public partial class TblCarCompany
    {
        public stccarCompany ToPOCO()
        {
            stccarCompany company = new stccarCompany();
            company.Id = this.Id;
            company.Address = this.Address;
            company.Email = this.Email;
            company.Name = this.Name;
            company.Owner = this.Owner;
            company.Phone = this.Phone;            
            return company;
        }

        public void ToObject(stccarCompany company)
        {
            this.Id = company.Id;
            this.Address = company.Address;
            this.Email = company.Email;
            this.Name = company.Name;
            this.Owner = company.Owner;
            this.Phone = company.Phone;            
        }
    }

    public partial class TblCarModel
    {
        public stccarModel ToPOCO()
        {
            stccarModel carModel = new stccarModel();
            carModel.Id = this.Id;
            carModel.CompanyId = this.CompanyId;
            carModel.Cost = this.Cost.Value;
            carModel.Milege = this.Milege.Value;
            carModel.Model_Number = this.Model_Number;
            carModel.Part_No = this.Part_No;
            carModel.Serial_No = this.Serial_No;
            return carModel;
        }

        public void ToObject(stccarModel carModel)
        {
            this.Id = carModel.Id;
            this.CompanyId = carModel.CompanyId;
            this.Cost = carModel.Cost;
            this.Milege = carModel.Milege;
            this.Model_Number = carModel.Model_Number;
            this.Part_No = carModel.Part_No;
            this.Serial_No = carModel.Serial_No;
        }
    }

    public partial class TblPurchaser
    {
        public stcPurchaser ToPOCO()
        {
            stcPurchaser purchaser = new stcPurchaser();
            purchaser.Id = this.Id;
            purchaser.Name = this.Name;
            purchaser.Address = this.Address;
            purchaser.CarModelId = this.CarModelId;
            purchaser.Cell_Phone = this.Cell_Phone;
            purchaser.Email = this.Email;
            purchaser.Home_Phone = this.Home_Phone;
            return purchaser;
        }

        public void ToObject(stcPurchaser stcPurchaser)
        {
            this.Id = stcPurchaser.Id;
            this.Name = stcPurchaser.Name;
            this.CarModelId = stcPurchaser.CarModelId;
            this.Cell_Phone = stcPurchaser.Cell_Phone;
            this.Email = stcPurchaser.Email;
            this.Home_Phone = stcPurchaser.Home_Phone;
            this.Address = stcPurchaser.Address;
        }
    }
}
