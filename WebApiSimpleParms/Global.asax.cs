using System.Web;
using System.Web.Http;
using System.Web.SessionState;//need for session state for web api controller

namespace WebApiSimpleParms
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        /// <summary>
        /// need for session state for web api controller
        /// </summary>
        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
}
