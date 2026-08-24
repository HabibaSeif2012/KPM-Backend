namespace KPM.Application.DTOs.Lesson
{
  public class UpdateLessonDTO
  {
    public string? Title { get; set; }
    public string? ProjectName { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? FunctionId { get; set; }
    public Guid? IndustryId { get; set; }
    public string? ValueProposition { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public string? PersonToContact { get; set; }
  }
}
