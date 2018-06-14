using ElevaCase.API.Entities;
using ElevaCase.API.Helpers;
using ElevaCase.API.Models;
using ElevaCase.API.Models.Escola;
using ElevaCase.API.Models.Turma;
using ElevaCase.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ElevaCase.API
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            });

            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["connectionStrings:localConnectionString"];
            services.AddDbContext<ElevaContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IElevaRepository, ElevaRepository>();

            //Code Responsible for allowing requests from the same origin
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, ElevaContext libraryContext)
        {

            loggerFactory.AddDebug(LogLevel.Information);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature =  context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(
                            "Uma falha inesperada aconteceu. Tente novamente mais tarde.");
                    });
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(
                            "Uma falha inesperada aconteceu. Tente novamente mais tarde.");
                    });
                });
            }

            AutoMapper.Mapper
                .Initialize(cfg => {

                    //Escola

                    cfg.CreateMap<Escola, EscolaDto>();

                    cfg.CreateMap<EscolaForCreationDto, Escola>();

                    cfg.CreateMap<EscolaForUpdateDto, Escola>();

                    cfg.CreateMap<EscolaForUpdateDto, TurmaForUpdateDto>();

                    // Turma

                    cfg.CreateMap<Turma, TurmaDto>()
                        .ForMember(dest => dest.NomeCompleto,
                            opt => opt.MapFrom(src => $"{src.Ano}º do {src.Etapa} - {src.Numero}"));

                    cfg.CreateMap<TurmaForCreationDto, Turma>();

                    cfg.CreateMap<TurmaForUpdateDto, Turma>();

                    cfg.CreateMap<Turma, TurmaForUpdateDto>();


                });

            libraryContext.EnsureSeedDataForContext();

            //Code Responsible for allowing requests from the same origin
            app.UseCors("AllowAllHeaders");
            app.UseMvc(); 
        }
    }
}
