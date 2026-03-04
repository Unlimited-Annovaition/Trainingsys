using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EnrollmentId { get; set; }

    public DateOnly? Date { get; set; }

    public bool Status { get; set; }

    public virtual Enrollment Enrollment { get; set; } = null!;
}
