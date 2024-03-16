﻿using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Logging;

public static class ServiceCollectionLoggingExtensions
{
    public static IServiceCollection AddLogging(this IServiceCollection services, ILogger logger)
    {
        services.AddSingleton(logger);

        return services;
    }
}