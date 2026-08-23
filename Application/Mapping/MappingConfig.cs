using KPM.Application.DTOs.Department;
using KPM.Application.DTOs.Function;
using KPM.Application.DTOs.Industry;
using KPM.Application.DTOs.Lesson;
using KPM.Domain;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KPM.Application.Mapping
{
  public  class MappingConfig: IRegister
  {
    public void Register (TypeAdapterConfig config)
    {
      config.NewConfig<Lesson, LessonDto>();
      config.NewConfig<CreateLessonDTO, Lesson>();

      config.NewConfig<Function, FunctionDTO>();
      config.NewConfig<CreateFunctionDTO, Function>();

      config.NewConfig<Department, DepartmentDTO>();
      config.NewConfig<CreateDepartmentDTO, Department>();

      config.NewConfig<Industry, IndustryDTO>();
      config.NewConfig<CreateIndustryDTO, Industry>();


    }
  }
}
