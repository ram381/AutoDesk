using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
    public sealed class ServiceRetriever<TServiceType> where TServiceType : class
    {
        public static CustomBinding GetBinding()
        {
            var customBinding = new CustomBinding()
            {
                CloseTimeout = TimeSpan.FromSeconds(30),
                OpenTimeout = TimeSpan.FromSeconds(30),
                ReceiveTimeout = TimeSpan.FromSeconds(120),
                SendTimeout = TimeSpan.FromSeconds(120),
            };
            HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement()
            {
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue
            };
            customBinding.Elements.Add(httpBindingElement);
            return customBinding;
        }

        public static TServiceType GetService(string address, bool useCache = true)
        {
            ChannelFactory<TServiceType> factory;
            try
            {
                var endpointAddress = new EndpointAddress(address);
                var customBinding = new CustomBinding()
                {
                    CloseTimeout = TimeSpan.FromSeconds(30),
                    OpenTimeout = TimeSpan.FromSeconds(30),
                    ReceiveTimeout = TimeSpan.FromSeconds(50),
                    SendTimeout = TimeSpan.FromSeconds(50),
                };
                if (address.StartsWith("https"))
                {
                    HttpsTransportBindingElement httpBindingElement = new HttpsTransportBindingElement()
                    {
                        MaxBufferSize = int.MaxValue,
                        MaxReceivedMessageSize = int.MaxValue
                    };
                    customBinding.Elements.Add(httpBindingElement);
                }
                else
                {
                    HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement()
                    {
                        MaxBufferSize = int.MaxValue,
                        MaxReceivedMessageSize = int.MaxValue
                    };
                    customBinding.Elements.Add(httpBindingElement);
                }
                factory = new ChannelFactory<TServiceType>(customBinding, endpointAddress);
                return factory.CreateChannel();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(string.Format("Error: {0}\r\n{1}", ex.Message, ex.StackTrace));
            }
            return null;
        }
    }
}
