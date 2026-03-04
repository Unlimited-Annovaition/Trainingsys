using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class Trainee
{
    public int TraineeId { get; set; }

    public string Fullname { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual User User { get; set; } = null!;
}
