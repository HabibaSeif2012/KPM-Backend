using KPM.Application.DTOs.Industry;
using Mapster;

namespace KPM.Application.Features.Industry
{
  public class IndustryMapsterConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<UpdateIndustryDTO, Domain.Industry>()
        .IgnoreNullValues(true);
    }
  }
}
