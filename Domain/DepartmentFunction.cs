using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPM.Domain
{
  public  class DepartmentFunction
  {
    public Guid FunctionId { get; set; }
    public Guid DepartmentId { get; set; }
    public Function Function { get; set; }
    public Department Department { get; set; }
  }
}
