//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Routing;
//using Coda.Objects;
//using Ninject;
//using Ninject.Web.Common;

//namespace Coda
//{
//    public class BlogApplication : NinjectHttpApplication
//    {
//        protected override IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel();

//            kernel.Load(new RepositoryModule());
//            kernel.Bind<IBlogRepository>().To<BlogRepository>();

//            return kernel;
//        }

//        protected override void OnApplicationStarted()
//        {
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//            base.OnApplicationStarted();
//        }

//    }
//}
