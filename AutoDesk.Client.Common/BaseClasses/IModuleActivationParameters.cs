using System;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AutoDesk.Client.Common.BaseClasses
{
    public interface IModuleActivationParameters
    {
        string ModuleName { get; set; }
        string RegionName { get; set; }
        IDictionary Parameters { get; set; }

        IModuleActivationParameters Clone();  
    }
}
