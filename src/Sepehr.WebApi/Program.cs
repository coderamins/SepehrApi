//  __________________  __________                                                                                                                       ____________
//  __________________ |    ____  \  \                  /  /\ \                      //////     __________________              o   o o  o            |               \\
//       |     |       |  |        |  |                /  /  \ \                  ///           __________________         o                  o       |   |            \\ 
//       |     |       |  |        /  /               /  /    \ \              ///                    |    |             o                       o    |   |            \\ 
//       |     |       |  |       /  /               /  /      \ \           ///                      |    |            o                         o   |   |            \\
//       |     |       |  |      /  /               /  /        \ \         ///                       |    |           o                           o  |   |           \\
//       |     |       |  |      \  \              /  / =======  \ \       ///                        |    |           o                            o |   |           \\ 
//       |     |       |  |       \  \            /  / ========== \ \      ///                        |    |           o                            o |   |            \\
//       |     |       |  |        \  \          /  /              \ \      ///                       |    |           o                            o |   |             \\
//       |     |       |  |         \  \        /  /                \ \      ///                      |    |            o                          o  |   |              \\
//       |     |       |  |          \  \      /  /                  \ \       ///                    |    |             o                        o   |   |               \\
//       |     |       |  |           \  \    /  /                    \ \         ///                 |    |               o                     o    |   |                 \\
//       |     |       |  |            \  \  /  /                      \ \           /////            |    |                 o                  o     |   |                  \\
//       |     |       |  |             \  \/  /                        \ \                           |    |                      o    o   o          |   |                   \\

using Sepehr.Application;
using Sepehr.Infrastructure;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Persistence;
using Sepehr.Infrastructure.Shared;
using Sepehr.WebApi.Extensions;
using Serilog;
using Sepehr.Infrastructure.Persistence.Context;
using System.Text.Json.Serialization;
using Microsoft.FeatureManagement;
using Sepehr.Domain.Entities;
using Sepehr.WebApi.Hubs;
using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Common;
using Sepehr.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

var appSetting = builder.Configuration.GetSection("AppSetting").Get<AppSetting>();
builder.Services.AddCors(options =>
{
    if (string.IsNullOrEmpty(appSetting.CorsPolicies))
    {
        options.AddPolicy(name: "CorsPolicy",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
    }
    else
    {
        options.AddPolicy(name: "CorsPolicy",
                          policy =>
                          {
                              policy.WithOrigins(builder.Configuration.GetSection("AppSetting").Get<AppSetting>().CorsPolicies)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplicationLayer();
builder.Services.AddApplication();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
//builder.Services.AddPersistenceLogInfrastructure(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFeatureManagement();
builder.Services.AddSwaggerExtension();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();

builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserChatConnection>>(opts => new Dictionary<string, UserChatConnection>());

builder.Services.AddHealthChecks();
builder.Services.AddSingleton<ApplicationDbContext>();
//builder.Services.AddHostedService<PermissionDiscoveryService>();

//configureLogging();
builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.MapHealthChecks("health");
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseCors("CorsPolicy");

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    Console.WriteLine("Migration successfuly applied!");
//#if !DEBUG
//    {
//        await DefaultInvoiceTypes.SeedAsync(applicationDbContext);
//        await DefaultFarePaymentTypes.SeedAsync(applicationDbContext);
//        await WarehouseTypes.SeedAsync(applicationDbContext);
//        await OrderSendTypes.SeedAsync(applicationDbContext);
//        await DefaultWarehouse.SeedAsync(applicationDbContext);
//        await DefaultBrands.SeedAsync(applicationDbContext);
//        await DefaultVehicleTypes.SeedAsync(applicationDbContext);
//        await DefaultReceivePaymentTypes.SeedAsync(applicationDbContext);
//        await DefaultProductTypes.SeedAsync(applicationDbContext);
//        await DefaultProductUnits.SeedAsync(applicationDbContext);
//        await DefaultStandards.SeedAsync(applicationDbContext);
//        await ProductStates.SeedAsync(applicationDbContext);
//        await DefaultCustomerValidity.SeedAsync(applicationDbContext);
//        await DefaultPurchaseOrderStatus.SeedAsync(applicationDbContext);
//        await DefaultTransferRemittanceTypes.SeedAsync(applicationDbContext);
//        await DefaultTransferRemittanceStatus.SeedAsync(applicationDbContext);
//        await DefaultReceivePayStatus.SeedAsync(applicationDbContext);
//        await DefaultBanks.SeedAsync(applicationDbContext);
//        await DefaultBasicUser.SeedAsync(applicationDbContext);
//        await DefaultOrderExitTypes.SeedAsync(applicationDbContext);
//        await DefaultPurchaseOrderSendType.SeedAsync(applicationDbContext);
//        await DefaultPurchaseOrderFarePaymentTypes.SeedAsync(applicationDbContext);
//        await DefaultPhoneNumberTypes.SeedAsync(applicationDbContext);
//        await DefaultCustomerLabelTypes.SeedAsync(applicationDbContext);
//        await DefaultPaymentRequestReasons.SeedAsync(applicationDbContext);
//        await DefaultPaymentRequestStatus.SeedAsync(applicationDbContext);
//        await DefaultOrderStatus.SeedAsync(applicationDbContext);
//        await DefaultFareAmountStatus.SeedAsync(applicationDbContext);
        
//    }
//#endif

    Log.Information("Finished Seeding Default Data");
    Log.Information("Application Starting");

    app.UseEndpoints(endpoint =>
    {
        endpoint.MapControllers();
    });
    //builder.WebHost.UseUrls("http://0.0.0.0:5000");

    app.MapHub<ChatHub>("/chat");
    //app.UseSignalR(routes =>
    //{
    //    routes.MapHub<ChatHub>("/chat");
    //});

    app.Run();
}

//void configureLogging()
//{
//    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//    var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile(
//        $"appsetting.{environment}.json", optional: true
//    ).Build();

//    Log.Logger = new LoggerConfiguration()
//        .Enrich.FromLogContext()
//        .Enrich.WithExceptionDetails()
//        .WriteTo.Debug()
//        .WriteTo.Console()
//        .WriteTo.Elasticsearch(ConfigurationElasticSink(configuration, environment))
//        .Enrich.WithProperty("Environment", environment)
//        .ReadFrom.Configuration(configuration)
//        .CreateLogger();
//}

//ElasticsearchSinkOptions ConfigurationElasticSink(IConfigurationRoot configuration,string environment)
//{
//    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
//    {
//        AutoRegisterTemplate = true,
//        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".","-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
//        NumberOfReplicas=1,
//        NumberOfShards=2
//    };
//}