//__________________  __________                                                                                                                       ____________
//__________________ |    ____  \  \                  /  /\ \                      //////     __________________              o   o o  o            |               \\
//     |     |       |  |        |  |                /  /  \ \                  ///           __________________         o                  o       |   |            \\ 
//     |     |       |  |        /  /               /  /    \ \              ///                    |    |             o                       o    |   |            \\ 
//     |     |       |  |       /  /               /  /      \ \           ///                      |    |            o                         o   |   |            \\
//     |     |       |  |      /  /               /  /        \ \         ///                       |    |           o                           o  |   |           \\
//     |     |       |  |      \  \              /  / =======  \ \       ///                        |    |           o                            o |   |           \\ 
//     |     |       |  |       \  \            /  / ========== \ \      ///                        |    |           o                            o |   |            \\
//     |     |       |  |        \  \          /  /              \ \      ///                       |    |           o                            o |   |             \\
//     |     |       |  |         \  \        /  /                \ \      ///                      |    |            o                          o  |   |              \\
//     |     |       |  |          \  \      /  /                  \ \       ///                    |    |             o                        o   |   |               \\
//     |     |       |  |           \  \    /  /                    \ \         ///                 |    |               o                     o    |   |                 \\
//     |     |       |  |            \  \  /  /                      \ \           /////            |    |                 o                  o     |   |                  \\
//     |     |       |  |             \  \/  /                        \ \                           |    |                      o    o   o          |   |                   \\
//                                   ____________________________  _____________________________   _____________________________   
//                                   ____________________________  _____________________________   _____________________________   
using Sepehr.Application;
using Sepehr.Infrastructure;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Persistence;
using Sepehr.Infrastructure.Shared;
using Sepehr.WebApi.Extensions;
using Serilog;
using Sepehr.Infrastructure.Persistence.Context;
using System.Text.Json.Serialization;
using Sepehr.Infrastructure.PersistenceLog;
using Microsoft.FeatureManagement;
using Sepehr.Domain.Entities;
using Sepehr.WebApi.Hubs;
using Microsoft.EntityFrameworkCore;
using Sepehr.Infrastructure.Persistence.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
                      policy =>
                      {
                          policy
                          //.AllowAnyOrigin()
                          .WithOrigins(
                              "http://localhost:3000",
                              "https://localhost:3000",
                              "https://raminsolhi.ir",
                              "http://manage.raminsolhi.ir",
                              "https://manage.raminsolhi.ir",
                              "http://192.168.10.125:8084",
                              "http://localhost:8084",
                              "http://manage.storm.net",
                              "http://storm.net",
                              "http://www.storm.net",
                              "https://manage.iraniansepehr.com",
                              "http://manage.iraniansepehr.com"
                          //    "http://*.iraniansepehr.com",
                          //    "*.iraniansepehr.com"
                          )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplicationLayer();
builder.Services.AddApplication();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddPersistenceLogInfrastructure(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFeatureManagement();
builder.Services.AddSwaggerExtension();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();


//builder.Services.AddScoped<IdentityContext>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IAuthenticatedUserService, Sepehr.WebApi.Services.AuthenticatedUserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserChatConnection>>(opts => new Dictionary<string, UserChatConnection>());

builder.Services.AddHealthChecks();

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
#if DEBUG
    {
        await DefaultInvoiceTypes.SeedAsync(applicationDbContext);
        await DefaultFarePaymentTypes.SeedAsync(applicationDbContext);
        await WarehouseTypes.SeedAsync(applicationDbContext);
        await OrderSendTypes.SeedAsync(applicationDbContext);
        await DefaultWarehouse.SeedAsync(applicationDbContext);
        await DefaultBrands.SeedAsync(applicationDbContext);
        await DefaultVehicleTypes.SeedAsync(applicationDbContext);
        await DefaultReceivePaymentTypes.SeedAsync(applicationDbContext);
        await DefaultProductTypes.SeedAsync(applicationDbContext);
        await DefaultProductUnits.SeedAsync(applicationDbContext);
        await DefaultStandards.SeedAsync(applicationDbContext);
        await ProductStates.SeedAsync(applicationDbContext);
        await DefaultCustomerValidity.SeedAsync(applicationDbContext);
        await DefaultPurchaseOrderStatus.SeedAsync(applicationDbContext);
        await DefaultTransferRemittanceTypes.SeedAsync(applicationDbContext);
        await DefaultTransferRemittanceStatus.SeedAsync(applicationDbContext);
        await DefaultReceivePayStatus.SeedAsync(applicationDbContext);
        await DefaultBanks.SeedAsync(applicationDbContext);
        await DefaultBasicUser.SeedAsync(applicationDbContext);
        await DefaultOrderExitTypes.SeedAsync(applicationDbContext);
        await DefaultPurchaseOrderSendType.SeedAsync(applicationDbContext);
        await DefaultPurchaseOrderFarePaymentTypes.SeedAsync(applicationDbContext);
        await DefaultPhoneNumberTypes.SeedAsync(applicationDbContext);
        await DefaultCustomerLabelTypes.SeedAsync(applicationDbContext);
        await DefaultPaymentRequestReasons.SeedAsync(applicationDbContext);
        await DefaultPaymentRequestStatus.SeedAsync(applicationDbContext);

    }
#endif

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