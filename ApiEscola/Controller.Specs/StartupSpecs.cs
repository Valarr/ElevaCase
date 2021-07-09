using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;


namespace Ge.Business.Specs
{
    public class StartupSpecs
    {
        private IConfiguration Configuration { get; }

        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();

        private Scope RequestScope(IContext context)
        {
            if (scopeProvider.Value == null)
            {
                scopeProvider.Value = new Scope();
            }

            return scopeProvider.Value;
        }


        public StartupSpecs(IConfiguration configuration)
        {
            Configuration = configuration;
            Spire.License.LicenseProvider.SetLicenseKey(
                "AdsBAMmmkMdXvISlhdwdHmqIRkXkNeE15hmgLZOOYyntL+QicCjzwoJ+RZcDyI9kRq1GDFkj1u1OIJoovOwy0PRHPfPiLbjzehsYoilYzt9WZNlzWTvGaN7ckaxn5mfkIE0hnkv5HaieeXC/DrjOz1Lu+m3gNo+TRUyw9C7xZimmlAa3eHRl1bHENEej72l8+rrHWyGxC2S+aH0crluJS4RLSiMHfsBJp9xNd1QsQzRqPcBLcnGlv3Elti7PGc57H2NJQd56K/XagiBm0qv2mXNXgmgPPNPwOanDp5OrqKruYSDy5EGAHuVFxFnRXpDRq3se9Df27KH0gpr89IxLTHONDYf2fGeBxCoHEVoRzAhSquPuXU4u0y1/x4ixNJyo9v0J/U3IOwlzPELaC0ApKHPexjlXASfCEnr95m0Fe7LO1F0rQ41s3oODZfyn42t/9Mn6D3aEObVQHWdtartGNJkgjlCWCHYR8zyu9TXI9kQ5Hpf+vEcURxTShafOCWgbxb8GQLu1Gcmxc+lBzcAEQeiyTwAdx3ogFPeQA0gSbIamm5MrpxXNM+5wArWJcx/44zlcgz+9s1NP6jAG7At6fgQfusLkQEgt+626NeZDqyqQ0Wv27B/27EEkFHFQXYE2qndTDA29oxUzNC9TpCrKiH6RZAHVdbI4eqzshwIMfUhdmTN/jhkRrgJAw2oA/Dofa2uAyNgNnR6SPsVSKl3vHRVIdHtBrIhPtFK+FksmlJfRrpSbtrbiqxKYwmT+6jqn7m1arktmo5qJLrvxi3wB7t2/8mCWsIqN0cjLUy5clMhO+vvH4wWR0VYNviupQNSYOeKAZEmBGkgW16erUJBOZbtjVg+3t5YeDtZd8FYhGew8Ci3mQLuUsbCjs3P87BhaFx/yeOLpjVaL/5iN9KWBx+7YQEOJpn7i9AVaunv2nuK1VD5tUlsDr7mjfwhgE1swPP8SWHURL5IKA5+/6YM1i1/oR0MI57oXVJZrSs8z9D5V/G7OYc/N969X8i0PG+PV+bkxTokGwImDibvtugEjrK25gyhTyMeQNLqZ8EMW7BTAiwCOJO3QW4DsSGC1eSKSpGldEZhy3zI0G83UQfz48gqr0l6WFEAXfsUcaFsKoSvkPC41kVMD2Yf18mlLzztAW7mSVO574Z0NvQbDqf23SnCW81PWZ8I113YTdJlOzGayWP4rra1lW390iYkS04nLcK9ZPoL0Cg3ylKU9oiwk33QZ0qjtSgqyUSM3bigyPKoaLmPjXs5OEefYkrGIsrY5R1MpXcHxjaDIXJrps82XNmO40mwHUTNPiOoGFxyfDsR6OFEaG4PF+FiHBKPeaOYuPKyoOE23dKSc8nRc/sHu2J3rGgKcjW/jCQ2dncc019Y9lJSkFtrgpMe93idqRX/esLsoix2faju6dUm0d+CY/KdqJW7qx1I5S6W6qcj9v7/l05ymqZREtx1fBJiwppr9RUeKU7NDslCcd5+S55eKgeNCtrYXgiFzJ0pNGclJMMRc7iioZFUGHYz2OAEwxEcG");
            Spire.License.LicenseProvider.LoadLicense();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("Ge.Api")))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddGeJsonOptionsSpecs();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition", "Content-Length", "X-Total-Count", "X-Total-Pages");
            }));

            services.AddElevaContext(Configuration, RequestScope);

            if (ElevaContext.Instance.Container.CanResolve<IGeApplicationLogger>())
            {
                ElevaContext.Instance.Container.Unbind<IGeApplicationLogger>();
            }
            ElevaContext.Instance.Container.Bind<IGeApplicationLogger>().To<FakeApplicationLogger>().InSingletonScope();

            services.AddAuthentication(options => { options.DefaultScheme = ElevaIdAuthenticationDefaults.AuthenticationScheme; }).AddElevaId();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicy(
                    new[] {new GeAuthorizationRequirement()},
                    new[] {ElevaIdAuthenticationDefaults.AuthenticationScheme}
                );
            });

            services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            services.AddSingleton<IAuthorizationHandler, GeAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();

            var culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private sealed class Scope : DisposableObject
        {
        }
    }
}