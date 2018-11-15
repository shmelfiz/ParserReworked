using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;

namespace GufoMeParser.BLL.Infrastructure
{
    public static class Container
    {
        private static IWindsorContainer _container { get; set; }

        public static T Resolve<T>() where T : class
        {
            _container.Register(Types.FromAssembly(typeof(T).Assembly)
            .BasedOn(typeof(T)).Unless(t => t.IsAbstract).WithServiceAllInterfaces());

            var resolved = _container.Resolve<T>();

            return resolved;
        }

        public static IWindsorContainer GetContainerInstance()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
            }

            return _container;
        }

        public static void Initialize()
        {
            if (_container == null)
            {
                _container = new WindsorContainer();
            }

            _container.Install(FromAssembly.Named("GufoMeParser.BLL"));
        }

        public static void InjectDependencies(object objectToInject)
        {
            if(objectToInject == null)
            {
                return;
            }

            var currentType = objectToInject.GetType();

            var propertiesToInject = currentType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(propertiesToInject == null || propertiesToInject.Count() == 0)
            {
                return;
            }

            foreach(var property in propertiesToInject)
            {
                var propertyValue = property.GetValue(objectToInject, null);

                if(propertyValue != null)
                {
                    return;
                }

                var objectForProperty = _container.Resolve(property.PropertyType);

                property.SetValue(objectToInject, objectForProperty ?? null);
            }
        }
    }
}