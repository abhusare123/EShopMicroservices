using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application.Extension;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
