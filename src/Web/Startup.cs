using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application;
using Application.Common.Interfaces;
using CaseExtensions;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.Filters;
using Web.Services;

namespace Web;

public class Startup
{
  private IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddApplication();

    services.AddInfrastructure(Configuration);

    services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

    services.AddScoped<ICurrentUserService, CurrentUserService>();

    services.AddRouting(options => { options.LowercaseUrls = true; });

    services.AddControllers(options =>
      {
        options.Filters.Add<ApiExceptionFilterAttribute>();
      })
      .AddFluentValidation(fv =>
      {
        fv.AutomaticValidationEnabled = false;
        fv.ValidatorOptions.PropertyNameResolver = (_, member, _) => member != null ? member.Name.ToCamelCase() : null;
      })
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
      });

    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Socially",
        Version = "v1"
      });

      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
      });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            }
          },
          Array.Empty<string>()
        }
      });
    });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Error");
      app.UseHttpsRedirection();
      app.UseHsts();
    }

    app.UseSwagger();

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Socially"));

    app.UseHealthChecks("/health");

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    if (env.IsDevelopment())
    {
      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";
        spa.Options.DevServerPort = 3000;

        spa.UseReactDevelopmentServer("start");
      });
    }
  }
}
