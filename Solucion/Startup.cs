using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SB.Data;
using SB.Entities;
using SB.Interfaces;
using SB.Module;
using SB.Resources;

namespace Solucion
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();
            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddDbContext<SqlServerContext>();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<DbContext, SqlServerContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #region Services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IResponseService, ResponseService>();
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
            services.AddTransient<IUserEs, UserEs>();
            services.AddTransient<ICatalogEs, CatalogEs>();
            #endregion

            #region Validators
            services.AddTransient<IValidator<User>, UserValidator>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("Policy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            loggerFactory.AddDebug();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
