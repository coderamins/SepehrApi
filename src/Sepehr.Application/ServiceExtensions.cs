
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sepehr.Application.Behaviours;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces;
using System.Reflection;

namespace Sepehr.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Assembly).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient<IPasswordHelper, PasswordHelper>();
        }

    
    }
}