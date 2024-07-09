using Api.Configurations;
using Api.Configurations.Auth;
using Api.Configurations.Swagger;
using Data;
//using Api.Middlewares;
using Data.Contexts;
using Data.Repositories;
using Domain;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllers();

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddAuthnticationConfiguration(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IUserContext, UserContext>();
builder.Services.AddTransient<IRepository, Repository>();

builder.Services.AddVersionedSwagger();

var app = builder.Build();

app.UsePathBase("/financial-transaction");

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

//app.UseMiddleware<CheckTenantPermissionMiddleware>();

var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();

app.UseVersionedSwagger(apiVersionProvider);

app.Run();