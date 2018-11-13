using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using Castle.MicroKernel.Registration;

namespace GufoMeParser.BLL.Infrastructure
{
    public static class Container
    {
        private static IWindsorContainer _Container { get; set; }

        static Container()
        {
            CreateContainer();
        }

        public static T Resolve<T>() where T : class
        {
            _Container.Register(Types.FromAssembly(typeof(T).Assembly)
            .BasedOn(typeof(T)).Unless(t => t.IsAbstract).WithServiceAllInterfaces());

            var resolved = _Container.Resolve<T>();

            return resolved;
        }

        private static void CreateContainer()
        {
            _Container = new WindsorContainer();
        }
    }
}