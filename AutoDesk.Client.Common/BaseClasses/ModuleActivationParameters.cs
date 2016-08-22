using System;
using System.Collections;

namespace AutoDesk.Client.Common.BaseClasses
{
    public sealed class ModuleActivationParameters : IModuleActivationParameters
    {
        public string ModuleName { get; set; }
        public string RegionName { get; set; }
        public IDictionary Parameters { get; set; }

        public IModuleActivationParameters Clone()
        {
            IModuleActivationParameters returnValue = MemberwiseClone() as IModuleActivationParameters;
            returnValue.ModuleName = this.ModuleName;
            returnValue.RegionName = this.RegionName;
            returnValue.Parameters = this.Parameters;
            return returnValue;
        }
    }
}
