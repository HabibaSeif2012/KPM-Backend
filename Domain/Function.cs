namespace KPM.Domain
{
  public class Function
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public ICollection<DepartmentFunction> DepartmentFunctions { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
  }
}
