using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPM.Domain
{
  public  class Department
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public ICollection<DepartmentFunction> DepartmentFunctions { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
  }
}

