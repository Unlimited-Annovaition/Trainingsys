using System;
using System.Collections.Generic;

namespace Domain.Entities; 
public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Trainee? Trainee { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
