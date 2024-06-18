using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static Stimulsoft.Report.Func;

namespace Sepehr.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        { 
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration=> configuration.RegisterServicesFromAssemblies(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddDNTCaptcha(options =>
            {
                // options.UseSessionStorageProvider(); // -> It doesn't rely on the server or client's times. Also it's the safest one.
                // options.UseMemoryCacheStorageProvider(); // -> It relies on the server's times. It's safer than the CookieStorageProvider.
                options
                    .UseCookieStorageProvider(
         /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
                                                        // .UseDistributedCacheStorageProvider(); // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
                                                        // .UseDistributedSerializationProvider();

                    // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
                    // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
                    //.UseCustomFont(Path.Combine(env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf")).AbsoluteExpiration(7)
                    .RateLimiterPermitLimit(
                        10) // for .NET 7x, Also you need to call app.UseRateLimiter() after calling app.UseRouting().
                            //.ShowExceptionsInResponse(env.IsDevelopment()).ShowThousandsSeparators(false)
                    .WithNoise(0.015f, 0.015f, 1, 0.0f).WithEncryptionKey("YYd909878")
                    .WithNonceKey("NETESCAPADES_NONCE");

            });

            return services;

        }
    }
}
