[assembly: WebActivator.PreApplicationStartMethod(typeof(PSBlog.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(PSBlog.App_Start.NinjectWebCommon), "Stop")]

namespace PSBlog.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Reflection;
    using System.Web.Mvc;
    using PSBlog.Common;
    
    using PSBlog.Authentication;
    using PSBlog.Repository;
    using Ninject.Extensions.Logging;
    using System.Web.Security;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get { return bootstrapper.Kernel; } }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);            
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);

            //kernel.Inject(Membership.Provider);
            //kernel.Inject(Roles.Provider);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<IPSBlogMembershipProvider>().To<PSBlogMembershipProvider>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IBlogRepository>().To<BlogRepository>();
            kernel.Bind<IPostRepository>().To<PostRepository>();            
        }        
    }
}
