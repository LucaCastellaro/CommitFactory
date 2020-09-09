using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using CommitFactory.Persistence.Models;
using CommitFactory.Services;
using System;

namespace CommitFactory
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            this._config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TasksDatabaseSettings>(_config.GetSection(nameof(TasksDatabaseSettings)));

            services.AddSingleton<ITasksDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TasksDatabaseSettings>>().Value);

            services.AddSingleton<TasksService>();
            services.AddSingleton<LogService>();

            var IsLogEnabled = this._config.GetValue<string>("IsLogEnabled");
            LogService.IsLogEnabled = (!string.IsNullOrWhiteSpace(IsLogEnabled) && IsLogEnabled.Equals(CommonConstants.EnableLog));

            services.AddCors(options =>
            {
                options.AddPolicy("Policy",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
