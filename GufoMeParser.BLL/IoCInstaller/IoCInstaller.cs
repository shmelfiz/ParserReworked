﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GufoMeParser.BLL.Managers.Interfaces;

namespace GufoMeParser.BLL.IoCInstaller
{
    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IManager)).WithService.AllInterfaces());
        }
    }
}
