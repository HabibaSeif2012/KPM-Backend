using KPM.Application.DTOs.Function;
using Mapster;

namespace KPM.Application.Features.Function
{
  public class FunctionMapsterConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<UpdateFunctionDTO, Domain.Function>()
        .IgnoreNullValues(true);
    }
  }
}
