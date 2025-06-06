﻿using BuildingBlocks.Application.Behaviors;
using Card.Application.Profiles;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Card.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //config.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddFeatureManagement();
        services.AddAutoMapper(typeof(CardProfile));

        return services;
    }
}
