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


            //Bread Crumb Interface Register
            container.RegisterType<ICreateBreadCrumb, CreateBreadCrumb>();
            container.RegisterType<IGetBreadCrumb, GetBreadCrumb>();
            container.RegisterType<IUpdateBreadCrumb, UpdateBreadCrumb>();
            container.RegisterType<IBreadCrumbService, BreadCrumbService>();

            //Cart Interface Register
            container.RegisterType<IUpdateCart, UpdateCart>();
            container.RegisterType<ICreateCart, CreateCart>();
            container.RegisterType<IGetCart, GetCart>();
            container.RegisterType<ICartService, CartService>();

            //HeaderCart Interface Register




            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}