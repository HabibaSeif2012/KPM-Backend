using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace KPM.Application
{
 
    public static class DependencyInjection
    {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        mapsterConfig.Scan(typeof(DependencyInjection).Assembly);

        services.AddSingleton(mapsterConfig);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
      }
    }

  
}
