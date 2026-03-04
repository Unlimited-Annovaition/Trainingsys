using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Grade
{
    public int GradeId { get; set; }

    public int EnrollmentId { get; set; }

    public double Score { get; set; }

    public string? Evaluation { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
public class TraineeGradeReportResult
{
    public string TraineeName { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public double Score { get; set; } 
    public string Evaluation { get; set; } = string.Empty;
}
