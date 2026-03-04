using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public partial class Trainer
{
    public int TrainerId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Specialization { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual User User { get; set; } = null!;
    [NotMapped] 
    public string? Email { get; set; }
}
