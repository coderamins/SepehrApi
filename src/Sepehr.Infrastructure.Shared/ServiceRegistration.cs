using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Domain.Settings;
using Sepehr.Application.Interfaces;
using Sepehr.Infrastructure.Shared.Services;

namespace Sepehr.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(
            this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.Configure<SmsSettings>(_config.GetSection("SmsSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISmsService, SmsService>();
        }
    }
}