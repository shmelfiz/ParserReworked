using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GufoMeParser.DAL.Repository.Interfaces;

namespace GufoMeParser.DAL.IoCInstaller
{
    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IRepository)).WithService.AllInterfaces());
        }
    }
}
