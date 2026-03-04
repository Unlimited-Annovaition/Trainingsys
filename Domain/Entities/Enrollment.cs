using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int TraineeId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Trainee Trainee { get; set; } = null!;
}
