using AutoDesk.PCL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoDesk.PCL.Common
{
    [DataContract]
    public sealed class stccarCompany : INotifyPropertyChanged
    {
        [DataMember]
        public int _Id;
        public int Id
        {
            get { return _Id; }
            set {
                if (_Id == value)
                    return;
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        [DataMember]
        public string _Name;
        public string Name {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [DataMember]
        public string _Email;
        public string Email {
            get { return _Email; }
            set
            {
                if (_Email == value)
                    return;
                _Email = value;
                NotifyPropertyChanged("Email");
            }
        }

        [DataMember]
        public string _Phone;
        public string Phone {
            get { return _Phone; }
            set
            {
                if (_Phone == value)
                    return;
                _Phone = value;
                NotifyPropertyChanged("Phone");
            }
        }

        [DataMember]
        public string _Address;
        public string Address {
            get { return _Address; }
            set
            {
                if (_Address == value)
                    return;
                _Address = value;
                NotifyPropertyChanged("Address");
            }
        }

        [DataMember]
        public string _Owner;
        public string Owner {
            get { return _Owner; }
            set
            {
                if (_Owner == value)
                    return;
                _Owner = value;
                NotifyPropertyChanged("Owner");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    [DataContract]
    public sealed class stccarModel : INotifyPropertyChanged
    {
        [DataMember]
        public int _Id;
        public int Id {
            get { return _Id; }
            set
            {
                if (_Id == value)
                    return;
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        [DataMember]
        public string _Model_Number;
        public string Model_Number { get { return _Model_Number; }
            set
            {
                if (_Model_Number == value)
                    return;
                _Model_Number = value;
                NotifyPropertyChanged("Model_Number");
            }
        }

        [DataMember]
        public int _CompanyId;
        public int CompanyId { get { return _CompanyId; }
            set
            {
                if (_CompanyId == value)
                    return;
                _CompanyId = value;
                NotifyPropertyChanged("CompanyId");
            }
        }

        [DataMember]
        public string _Part_No;
        public string Part_No {
            get { return _Part_No; }
            set
            {
                if (_Part_No == value)
                    return;
                _Part_No = value;
                NotifyPropertyChanged("Part_No");
            }
        }

        [DataMember]
        public string _Serial_No;
        public string Serial_No {
            get { return _Serial_No; }
            set
            {
                if (_Serial_No == value)
                    return;
                _Serial_No = value;
                NotifyPropertyChanged("Serial_No");
            }
        }

        [DataMember]
        public int _Milege;
        public int Milege {
            get { return _Milege; }
            set
            {
                if (_Milege == value)
                    return;
                _Milege = value;
                NotifyPropertyChanged("Milege");
            }
        }

        [DataMember]
        public long _Cost;
        public long Cost {
            get { return _Cost; }
            set
            {
                if (_Cost == value)
                    return;
                _Cost = value;
                NotifyPropertyChanged("Cost");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    [DataContract]
    public sealed class stcPurchaser : INotifyPropertyChanged
    {
        [DataMember]
        public int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id == value)
                    return;
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        [DataMember]
        public string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [DataMember]
        public string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address == value)
                    return;
                _Address = value;
                NotifyPropertyChanged("Address");
            }
        }

        [DataMember]
        public string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email == value)
                    return;
                _Email = value;
                NotifyPropertyChanged("Email");
            }
        }

        [DataMember]
        public string _Home_Phone;
        public string Home_Phone { get { return _Home_Phone; }
            set
            {
                if (_Home_Phone == value)
                    return;
                _Home_Phone = value;
                NotifyPropertyChanged("Home_Phone");
            }
        }

        [DataMember]
        public string _Cell_Phone;
        public string Cell_Phone {
            get { return _Cell_Phone; }
            set
            {
                if (_Cell_Phone == value)
                    return;
                _Cell_Phone = value;
                NotifyPropertyChanged("Cell_Phone");
            }
        }

        [DataMember]
        public int _CarModelID;
        public int CarModelId {
            get { return _CarModelID; }
            set
            {
                if (_CarModelID == value)
                    return;
                _CarModelID = value;
                NotifyPropertyChanged("CarModelId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }   
    
    [DataContract]
    public sealed class stcTreeData : INotifyPropertyChanged
    {
        [DataMember]
        public int _ParentId;
        public int ParentId
        {
            get { return _ParentId; }
            set
            {
                if (_ParentId == value)
                    return;
                _ParentId = value;
                NotifyPropertyChanged("ParentId");
            }
        }

        [DataMember]
        public int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id == value)
                    return;
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        [DataMember]
        public string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        [DataMember]
        public List<stcTreeData> _Subs;
        public List<stcTreeData> Subs
        {
            get { return _Subs; }
            set
            {
                if (_Subs == value)
                    return;
                _Subs = value;
                NotifyPropertyChanged("Subs");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public sealed class Messenger
    {
        public clsEnumeration.MessengerId Id { get; set; }
        public object Data { get; set; }
    }
}
