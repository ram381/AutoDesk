using AutoDesk.Client.Proxy.Definitions;
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

namespace AutoDesk.Client.Proxy.Managers
{
    public sealed class AutoDeskServiceManager
    {
        private readonly string _serviceUrl;
        private IAutoDeskService factory;

        public AutoDeskServiceManager()
        {
            _serviceUrl = ServerSettings.AutoDeskServiceUrl;
            factory = ServiceRetriever<IAutoDeskService>.GetService(_serviceUrl);
        }

        ~AutoDeskServiceManager()
        {
            factory = null;
        }

        public void GetCarCompanies(Action<List<stccarCompany>, Exception> callbackAction)
        {
            factory.BeginGetCarCompanies((callResult =>
            {
                try
                {
                    List<stccarCompany> result = factory.EndGetCarCompanies(callResult);
                    AppDispatcher.BeginInvoke(() => callbackAction(result, null));
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callbackAction(null, e));
                }
            }), factory);
        }

        public void GetCarModels(Action<List<stccarModel>, Exception> callback)
        {
            factory.BeginGetCarModels((callResult =>
            {
                try
                {
                    var result = factory.EndGetCarModels(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, e));
                }
            }), factory);
        }

        public void GetCompanyByID(int Id, Action<stccarCompany, Exception> callback)
        {
            factory.BeginGetCompanyByID(Id, (callResult =>
            {
                try
                {
                    stccarCompany result = factory.EndGetCompanyByID(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, e));
                }
            }), factory);
        }

        public void GetCarModelByID(int Id, Action<stccarModel, Exception> callback)
        {
            factory.BeginGetCarModelByID(Id, (callResult =>
            {
                try
                {
                    stccarModel result = factory.EndGetCarModelByID(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, e));
                }
            }), factory);
        }

        public void GetPurchasersByCarModel(int ID, Action<List<stcPurchaser>, Exception> callback)
        {
            factory.BeginGetPurchasersByCarModel(ID, (callResult =>
             {
                 try
                 {
                     List<stcPurchaser> result = factory.EndGetPurchasersByCarModel(callResult);
                     AppDispatcher.BeginInvoke(() => callback(result, null));
                 }
                 catch (Exception e)
                 {
                     AppDispatcher.BeginInvoke(() => callback(null, e));
                 }
             }), factory);
        }

        public void GetCarModelsByCompany(int ID, Action<List<stccarModel>, Exception> callback)
        {
            factory.BeginGetCarModelsByCompany(ID, (callResult =>
            {
                try
                {
                    var result = factory.EndGetCarModelsByCompany(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, e));
                }
            }), factory);
        }

        public void AddEditCarCompany(stccarCompany carCompany, Action<string, Exception> callback)
        {
            factory.BeginAddEditCarCompany(carCompany, (callResult =>
            {
                try
                {
                    var result = factory.EndAddEditCarCompany(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback("", e));
                }
            }), factory);
        }

        public void AddEditCarModel(stccarModel carModel, Action<string, Exception> callback)
        {
            factory.BeginAddEditCarModel(carModel, (callResult =>
            {
                try
                {
                    var result = factory.EndAddEditCarModel(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback("", e));
                }
            }), factory);
        }

        public void GetTreeData(Action<List<stcTreeData>, Exception> callback)
        {
            factory.BeginGetTreeData(callResult =>
            {
                try
                {
                    List<stcTreeData> result = factory.EndGetTreeData(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception ex)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, ex));
                }
            }, factory);
        }

        public void DeleteCarCompany(int ID, Action<bool, Exception> callback)
        {
            factory.BeginDeleteCarCompany(ID, callResult =>
             {
                 try
                 {
                     bool result = factory.EndDeleteCarCompany(callResult);
                     AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                 }
                 catch (Exception ex)
                 {
                     AppDispatcher.BeginInvoke(() => callback(false, ex));
                 }
             }, factory);
        }

        public void DeleteCarModel(int ID, Action<bool, Exception> callback)
        {
            factory.BeginDeleteCarModel(ID, callResult =>
             {
                 try
                 {
                     bool result = factory.EndDeleteCarModel(callResult);
                     AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                 }
                 catch (Exception ex)
                 {
                     AppDispatcher.BeginInvoke(() => callback(false, ex));
                 }
             }, factory);
        }

        public void AddEditPurchaser(stcPurchaser purchaser, Action<string, Exception> callback)
        {
            factory.BeginAddEditPurchaser(purchaser, (callResult =>
            {
                try
                {
                    var result = factory.EndAddEditPurchaser(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception e)
                {
                    AppDispatcher.BeginInvoke(() => callback("", e));
                }
            }), factory);
        }

        public void GetRefinedTreeData(string searchString, Action<List<stcTreeData>, Exception> callback)
        {
            factory.BeginGetRefinedTreeData(searchString, callResult =>
            {
                try
                {
                    List<stcTreeData> result = factory.EndGetRefinedTreeData(callResult);
                    AppDispatcher.BeginInvoke(delegate { callback(result, null); });
                }
                catch (Exception ex)
                {
                    AppDispatcher.BeginInvoke(() => callback(null, ex));
                }
            }, factory);
        }
    }
}
