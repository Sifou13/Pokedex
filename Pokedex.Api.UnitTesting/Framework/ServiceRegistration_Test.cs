using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokedex.Api.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokedex.Api.UnitTesting.Framework
{
    
    [TestClass]
    public class ServiceRegistration_Test
    {
        //This test should in theory move to to the Service Test project, once it is hosted in isolation and exposed via physical services (REST, gRPC)
        [TestMethod]
        public void ServiceRegistrationHelper_ResolveServiceContractToResolveAndVerifyDependencyChain_PreventMissingRegistration_Success()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.RegisterPokedexServices();

            ServiceProvider provider = services.BuildServiceProvider();

            try
            {
                provider.GetService(typeof(Pokedex.Services.Contract.Orchestrators.IPokemonInformationOrchestrator));
            }
            catch(Exception exception)
            {
                Assert.Fail($"One or more dependencies are not registered in { nameof(Pokedex.Api.Framework.ServiceRegistrationHelper.RegisterPokedexServices) } method of the {nameof(Pokedex.Api.Framework.ServiceRegistrationHelper)} class: {exception.Message}");
            }            
        }

        [TestMethod]
        public void ServiceRegistrationHelper_RemovingRandomResourceFromDependencyChain_ImitatingDeveloperForgettingToRegisterADependency_ExpectingFailure()
        {
            IServiceCollection services = new ServiceCollection();

            services.RegisterPokedexServices();

            ServiceDescriptor serviceToRemove = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(Pokedex.Services.Resources.DataAccess.Pokemon.Contract.IPokemonDA));
            services.Remove(serviceToRemove);
            
            ServiceProvider provider = services.BuildServiceProvider();
            
            try
            {
                provider.GetService(typeof(Pokedex.Services.Contract.Orchestrators.IPokemonInformationOrchestrator));
            }
            catch
            {
                return;
            }

            Assert.Fail($"This test should have caught an exception since we are testing for failures");
        }
    }
}
