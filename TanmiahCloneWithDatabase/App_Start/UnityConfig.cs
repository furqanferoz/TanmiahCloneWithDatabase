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

            //Header Interface Register
            container.RegisterType<IUpdateHeader, UpdateHeader>();
            container.RegisterType<ICreateHeader, CreateHeader>();
            container.RegisterType<IGetHeader, GetHeader>();
            container.RegisterType<IHeaderCartService, HeaderCartService>();

            //Ingredient Interface Register
            container.RegisterType<IUpdateIngredient, UpdateIngredient>();
            container.RegisterType<ICreateIngredient, CreateIngredient>();
            container.RegisterType<IGetIngredient, GetIngredient>();
            container.RegisterType<IIngredientService, IngredientService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}