using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Api.Configurations.Swagger;

public static class SwaggerConfiguration
{
    public static void AddVersionedSwagger(this IServiceCollection services)
            {
                services.AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                });
    
                services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
    
                services.AddSwaggerGen(options =>
                {
                    options.CustomSchemaIds(p => p.FullName);
    
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
    
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        var version = typeof(SwaggerConfiguration).Assembly.GetName().Version;
                        var apiInfoDescription = new StringBuilder("Registro de contas a pagar");
                        //describe the info
                        var info = new OpenApiInfo
                        {
                            Title = $"{{service-name}} {description.GroupName}",
                            Version = version.ToString(),
                            Description = apiInfoDescription.ToString(),
                            License = new OpenApiLicense() { Name = $"App Version: {version.Major}.{version.Minor}.{version.Build}" }
                        };
    
                        if (description.IsDeprecated)
                        {
                            apiInfoDescription.Append(" This API version has been deprecated.");
                            info.Description = apiInfoDescription.ToString();
                        }
    
                        options.SwaggerDoc(description.GroupName, info);
                        options.OperationFilter<DefaultHeaderFilter>();
    
                        //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        //options.IncludeXmlComments(xmlPath);
                    }
    
                    options.ParameterFilter<DefaultParametersFilter>();
    
                    //add security definition
                    var securityDefinition = new OpenApiSecurityScheme
                    {
                        Description = "Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    };
    
                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityDefinition);
    
                    var openApiSecurityScheme = new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    };
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        openApiSecurityScheme,
                        new string[] { }
                    }});
                });
            }
    
            public static void UseVersionedSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                            options.RoutePrefix = string.Empty;
                        }
                    });
            }
}