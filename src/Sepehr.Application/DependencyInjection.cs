﻿using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Sepehr.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        { 
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration=> configuration.RegisterServicesFromAssemblies(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;

        }
    }
}
