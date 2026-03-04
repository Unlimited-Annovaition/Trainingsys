

public class EnrollmentResponseDto
{
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public int TraineeId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public double? Grade { get; set; }
    public bool? IsPassed { get; set; }
}

public class CreateEnrollmentDto
{
    public int CourseId { get; set; }
    public int TraineeId { get; set; }
    public DateTime EnrollmentDate { get; set; }
}

public class UpdateEnrollmentDto
{
    public int CourseId { get; set; }
    public int TraineeId { get; set; }
    public DateTime EnrollmentDate { get; set; }
}