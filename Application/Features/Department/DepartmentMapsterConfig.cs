using KPM.Application.DTOs.Department;
using Mapster;

namespace KPM.Application.Features.Department
{
  public class DepartmentMapsterConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<UpdateDepartmentDTO, Domain.Department>()
        .IgnoreNullValues(true);
    }
  }
}
