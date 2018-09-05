using Lottery.WebAPI.App_Start;
using System.Web.Http;

namespace Lottery.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}
