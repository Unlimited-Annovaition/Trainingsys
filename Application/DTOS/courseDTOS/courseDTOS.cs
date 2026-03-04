namespace Application.DTOS.courseDTOS;

public class CourseResponseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? TrainerId { get; set; }
}

public class CreateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? TrainerId { get; set; }
}

public class UpdateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int? TrainerId { get; set; }
}