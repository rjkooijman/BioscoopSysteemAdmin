using BioscoopSysteemWebsite.Domain.Implementations;
using BioscoopSysteemWebsite.Domain.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioscoopSysteemTouch.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver {
        //Gemaakt door: Frank Molengraaf
       private IKernel kernel;
       public NinjectDependencyResolver(IKernel kernelParam) {
           kernel = kernelParam;
           AddBindings();
        }
       public object GetService(Type serviceType)
       {
           return kernel.TryGet(serviceType);
       }
       public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
       }
       private void AddBindings() {
            kernel.Bind<IFilmRepository>().To<PersistentFilmRepository>();
            kernel.Bind<IRepository>().To<PersistentRepository>();
       }
    }
}