using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using TanmiahCloneWithDatabase.Services;
using TanmiahCloneWithDatabase.Controllers;
using static TanmiahCloneWithDatabase.Services.GetBreadCrumb;

namespace TanmiahCloneWithDatabase
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICreateBreadCrumb, CreateBreadCrumb>();
            container.RegisterType<IGetBreadCrumb, GetBreadCrumb>();
            container.RegisterType<IUpdateBreadCrumb, UpdateBreadCrumb>();
            container.RegisterType<IBreadCrumbService, BreadCrumbService>();



            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}