using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Persistence.Repositories;
using Sepehr.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sepehr.Application.Wrappers;
using Sepehr.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Stimulsoft.Base.Json;
using Sepehr.Application.Helpers;

namespace Sepehr.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            string constr = configuration.GetValue<string>("ConnectionStrName") ?? "SepehrConnection";
            Console.WriteLine("connected database name is=>" + constr);
            Console.WriteLine("Connection string is:" + configuration.GetConnectionString(constr));
            services.AddDbContext<ApplicationDbContext>
            (options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(constr), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            , ServiceLifetime.Singleton);

            #region Repositories
            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddScoped<IOrderRepositoryAsync, OrderRepositoryAsync>();
            services.AddScoped<IPurchaseOrderRepositoryAsync, PurchaseOrderRepositoryAsync>();
            services.AddScoped<ICustomerRepositoryAsync, CustomerRepositoryAsync>();
            services.AddScoped<IProductSupplierRepositoryAsync, ProductSupplierRepositoryAsync>();
            services.AddScoped<ICargoAnnouncementRepositoryAsync, CargoAnnouncementRepositoryAsync>();
            services.AddScoped<IReceivePayRepositoryAsync, ReceivePayRepositoryAsync>();
            services.AddScoped<IProductPriceRepositoryAsync, ProductPriceRepositoryAsync>();
            services.AddScoped<IProductTypeRepositoryAsync, ProductTypeRepositoryAsync>();
            services.AddScoped<IBrandRepositoryAsync, BrandRepositoryAsync>();
            services.AddScoped<IProductStandardRepositoryAsync, ProductStandardRepositoryAsync>();
            services.AddScoped<IProductStateRepositoryAsync, ProductStateRepositoryAsync>();
            services.AddScoped<IProductBrandRepositoryAsync, ProductBrandRepositoryAsync>();
            services.AddScoped<IProductInventoryRepositoryAsync, ProductInventoryRepositoryAsync>();
            services.AddScoped<IServiceRepositoryAsync, ServiceRepositoryAsync>();
            services.AddScoped<ICustomerOfficialCompanyRepositoryAsync, CustomerOfficialCompanyRepositoryAsync>();
            services.AddScoped<ILadingLicenseRepositoryAsync, LadingLicenseRepositoryAsync>();
            services.AddScoped<IAttachmentRepositoryAsync, AttachmentRepositoryAsync>();
            services.AddScoped<IWarehouseRepositoryAsync, WarehouseRepositoryAsync>();
            services.AddScoped<IPurchaseOrderRepositoryAsync, PurchaseOrderRepositoryAsync>();
            services.AddScoped<ITransferRemittanceRepositoryAsync, TransferRemittanceRepositoryAsync>();
            services.AddScoped<IShareHolderRepositoryAsync, ShareHolderRepositoryAsync>();
            services.AddScoped<IPettyCashRepositoryAsync, PettyCashRepositoryAsync>();
            services.AddScoped<ICashDeskRepositoryAsync, CashDeskRepositoryAsync>();
            services.AddScoped<IIncomeRepositoryAsync, IncomeRepositoryAsync>();
            services.AddScoped<ICostRepositoryAsync, CostRepositoryAsync>();
            services.AddScoped<IOrganizationBankRepositoryAsync, OrganizationBankRepositoryAsync>();
            services.AddScoped<IRentPaymentRepositoryAsync, RentPaymentRepositoryAsync>();
            services.AddScoped<ILadingExitPermitRepositoryAsync, LadingExitPermitRepositoryAsync>();
            services.AddScoped<IUnloadingPermitRepositoryAsync, UnloadingPermitRepositoryAsync>();
            services.AddScoped<ILadingPermitRepositoryAsync, LadingPermitRepositoryAsync>();
            services.AddScoped<IEntrancePermitRepositoryAsync, EntrancePermitRepositoryAsync>();

            services.AddScoped<IPersonnelRepositoryAsync, PersonnelRepositoryAsync>();
            services.AddScoped<IPaymentRequestRepositoryAsync, PaymentRequestRepositoryAsync>();
            services.AddScoped<IPersonnelPaymentRequestRepositoryAsync, PersonnelPaymentRequestRepositoryAsync>();
            services.AddScoped<ITransferWarehouseInventoryRepositoryAsync, TransferWarehouseInventoryRepositoryAsync>();
            services.AddTransient<IExportUtility, ExcelUtility>();

            services.AddScoped<IRoleMenuService, RoleMenuService>();
            services.AddScoped<IRoleMenuRepository, RoleMenuRepository>();
            services.AddScoped<IApplicationRoleRepositoryAsync, ApplicationRoleRepositoryAsync>();
            services.AddScoped<IPermissionRepositoryAsync, PermissionRepositoryAsync>();
            services.AddScoped<IRoleMenuRepositoryAsync, RoleMenuRepositoryAsync>();
            services.AddScoped<IRolePermissionRepositoryAsync, RolePermissionRepositoryAsync>();
            services.AddScoped<IApplicationUserRepositoryAsync, ApplicationUserRepositoryAsync>();
            services.AddScoped<IUserRoleRepositoryAsync, UserRoleRepositoryAsync>();
            services.AddScoped<IDriverFareAmountApproveRepositoryAsync, DriverFareAmountApproveRepositoryAsync>();
            services.AddScoped<ICustomerLabelRepositoryAsync, CustomerLabelRepositoryAsync>();
            services.AddScoped<ISaleReportRepository, SaleReportRepository>();
            services.AddScoped<IPermissionInitializerService, PermissionInitializerService>();
            #endregion

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });


        }
    }
}