using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace HelpSTAR.Web
{
    public partial class Choreograf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string serviceUri = ConfigurationManager.AppSettings["ServiceUri"];
            //if (!serviceUri.EndsWith("/"))
            //{
            //    serviceUri += "/";
            //}
            //ScriptManager1.Services.Add(new ServiceReference(string.Format("{0}{1}", serviceUri, "Services/JSRequestServices.svc")));
        }
    }
}