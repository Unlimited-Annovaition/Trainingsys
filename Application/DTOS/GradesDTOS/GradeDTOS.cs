namespace Application.DTOS;

public class GradeResponseDto
{
    public int GradeId { get; set; }
    public int EnrollmentId { get; set; }
    public double Score { get; set; }
    public string Evaluation { get; set; } = string.Empty;
}

public class CreateGradeDto
{
    public int EnrollmentId { get; set; }
    public double Score { get; set; }
    public string Evaluation { get; set; } = string.Empty;
}

public class UpdateGradeDto
{
    public int EnrollmentId { get; set; }
    public double Score { get; set; }
    public string Evaluation { get; set; } = string.Empty;
}

public class TraineeGradeReportDto
{
    public string TraineeName { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public double Score { get; set; }
    public string Evaluation { get; set; } = string.Empty;
}