using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ApiEscola.Controllers.Specs
{
    public abstract class CommonBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CommonBase)).Location;
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", true)
                        .AddEnvironmentVariables().AddInMemoryCollection(new[]
                        {
                new KeyValuePair<string, string>("ConnectionString", ""),
                        });
                });
                
            var testServer = new TestServer(hostBuilder);
            return testServer;
        }
    }
}
