using Lottery.WebAPI.App_Start;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Lottery.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfig.Initialize(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration
                .Formatters
                .JsonFormatter
                .SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
