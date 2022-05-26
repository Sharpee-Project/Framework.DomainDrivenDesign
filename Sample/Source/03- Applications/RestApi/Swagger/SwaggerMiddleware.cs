using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Sample.Applications.RestApi.Swagger;

public static class SwaggerMiddleware
{
    private static OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Sharpee - Accounts Relation API",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has ben deprecated.";
        }

        return info;
    }

    public static void AddSwaggerMiddleware(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider()
            .GetRequiredService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
            //c.ExampleFilters();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Account Structure",
                Type = SecuritySchemeType.ApiKey
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

            var documentPath = Path.Combine(Directory.GetCurrentDirectory(), "RestApi.xml");
            c.IncludeXmlComments(documentPath);
        });

        //services.AddSwaggerExamplesFromAssemblyOf<SomeCommandExample>();
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}