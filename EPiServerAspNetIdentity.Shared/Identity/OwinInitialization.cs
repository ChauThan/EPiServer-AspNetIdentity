using System.Web;
using EPiIdentity.Shared.Identity;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace EPiServerAspNetIdentity.Shared.Identity
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class OwinInitialization : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
            //Add initialization logic, this method is called once after CMS has been initialized
        }

        public void Uninitialize(InitializationEngine context)
        {
            //Add uninitialization logic
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            var services = context.Services;
            services.AddTransient<IOwinContext>(locator => HttpContext.Current.GetOwinContext());
            services.AddTransient<ApplicationUserManager<SiteUser>>(locator => locator.GetInstance<IOwinContext>().GetUserManager<ApplicationUserManager<SiteUser>>());
            services.AddTransient<IAuthenticationManager>(locator => locator.GetInstance<IOwinContext>().Authentication);
        }
    }
}