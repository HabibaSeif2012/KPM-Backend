using KPM.Application.DTOs.Lesson;
using Mapster;

namespace KPM.Application.Features.Lesson
{
  public class LessonMapsterConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<UpdateLessonDTO, Domain.Lesson>()
        .IgnoreNullValues(true);
    }
  }
}
