using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPM.Domain
{
  public class Lesson
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid FunctionId { get; set; }
    public Guid IndustryId { get; set; }
    public string ValueProposition { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string PersonToContact { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Department Department { get; set; }
    public Function Function { get; set; }
    public Industry Industry { get; set; }
  }
}
