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

namespace AutoDesk.Client.Proxy
{
    public sealed class ServerSettings
    {
        public static string AutoDeskServiceUrl { get; set; }

        static ServerSettings()
        {

        }

        public static void SetServiceUri(string serviceUrl)
        {
            if (serviceUrl.LastIndexOf("/") == serviceUrl.Length - 1 && serviceUrl.Length > 0)
                serviceUrl = serviceUrl.Substring(0, serviceUrl.Length - 1);

            AutoDeskServiceUrl = new Uri(serviceUrl + "/AutoDeskService.svc").AbsoluteUri;
        }
    }
}
