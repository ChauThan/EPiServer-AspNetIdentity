using System;
using EPiIdentity.Shared.Identity;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Core;
using EPiServer.Web.Routing;
using EPiServerAspNetIdentity.CMS.Infrastructure.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EPiServerAspNetIdentity.CMS.Infrastructure.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.AddCmsAspNetIdentity<SiteUser>(new ApplicationOptions()
            {
                ConnectionStringName = "AspNetIdentity"
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Util/Login.aspx"),
                Provider = new CookieAuthenticationProvider()
                {
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<SiteUser>, SiteUser>(
                            validateInterval: TimeSpan.FromMinutes(30),
                            regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user)),
                    OnApplyRedirect = context => context.Response.Redirect(context.RedirectUri),
                    OnResponseSignIn = context =>
                        context.Response.Redirect(UrlResolver.Current.GetUrl(ContentReference.StartPage))
                }
            });
        }
    }
}
