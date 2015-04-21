using System.Collections.Generic;

namespace TagKid.Framework.WebApi.Configuration
{
    public class ServiceConfig
    {
        public static ServiceConfig Current { get; private set; }

        private ServiceConfig()
        {
            ServiceMappings = new List<ServiceMapping>();
            Current = this;
        }

        public List<ServiceMapping> ServiceMappings { get; private set; }

        public static ControllerConfigurator Builder()
        {
            return new ControllerConfigurator(new ServiceConfig());
        }
    }
}