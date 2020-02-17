using Xunit;
using System;
using NSI.Test.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSI.BLL.Services.Interfaces;

namespace NSI.Test.IntegrationTests {
    public class StartupTests {
        [Fact]
        public void ConfigureServicesTest()
        {
            IConfiguration configuration = MockConfiguration.MockEmpthy();
            ServiceCollection services = new ServiceCollection();
            Startup startup = new Startup(configuration);
            
            startup.ConfigureServices(services);
            
            Assert.NotEmpty(services); 
        }

    }
}