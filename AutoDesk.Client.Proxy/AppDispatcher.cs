using System;
using System.Windows;


#if SDK_WINDOWS
namespace ServicePRO.Sdk.Windows
#elif SDK_SILVERLIGHT
namespace ServicePRO.Sdk.Silverlight
#elif SERVERPROXY
namespace HelpSTAR.Server.Proxy
#else

namespace AutoDesk.Client.Proxy
#endif
{
    public static class AppDispatcher
    {
        public static void BeginInvoke(Action action)
        {
#if SDK_WINDOWS
            action();
#else
            Deployment.Current.Dispatcher.BeginInvoke(action);
#endif
        }
    }
}
