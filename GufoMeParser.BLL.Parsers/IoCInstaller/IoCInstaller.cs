using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GufoMeParser.BLL.Parsers.Parsers.Interfaces;
using GufoMeParser.BLL.ParsersFactory.Interfaces;

namespace GufoMeParser.BLL.IoCInstaller
{
    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IParser)).WithService.AllInterfaces());
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IParserCreator)).WithService.AllInterfaces());
        }
    }
}
