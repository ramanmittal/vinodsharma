using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vinodsharma.Entities;
using vinodsharma.Models;

namespace vinodsharma.IOC
{
    public class DependencyLoader
    {
        public static void LoadModules()
        {
            var container = new Container();
            LoadDependency(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void LoadDependency(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ApplicationDbContext, ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(container.GetInstance<ApplicationDbContext>()));
            container.Register<IDataProtectionProvider>(() => Startup.DataProtectionProvider);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));

           

        }
    }
}