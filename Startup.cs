using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NgApi.Models;

namespace NgApi
{
    public class Startup
    {
        public string connectionString { get; set; }         
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            connectionString = Configuration["secretConnectionString"];
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddDbContext<ApiContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DataAccessPostgreSqlProvider")));
            services.AddDbContext<ApiContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<DataSeed>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            seed.SeedData(20, 1000);
            

            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
