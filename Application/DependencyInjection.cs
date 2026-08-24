using KPM.Application.Features.Department;
using KPM.Application.Features.Function;
using KPM.Application.Features.Industry;
using KPM.Application.Features.Lesson;
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
      //dependency injection of the service(bussniess logic)
      services.AddScoped<IDepartmentService, DepartmentService>();
      services.AddScoped<IFunctionService, FunctionService>();
      services.AddScoped<IIndustryService, IndustryService>();
      services.AddScoped<ILessonService, LessonService>();

      return services;
      }
    }

  
}
