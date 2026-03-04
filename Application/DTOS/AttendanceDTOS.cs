namespace Application.DTOS;


public class AttendanceResponseDto
{
    public int AttendanceId { get; set; }
    public int EnrollmentId { get; set; }
    public DateTime Date { get; set; } 
    public bool Status { get; set; }
}

public class CreateAttendanceDto
{
    public int EnrollmentId { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}

public class UpdateAttendanceDto
{
    public int EnrollmentId { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
}

public class AttendancePercentageDto
{
    public int EnrollmentId { get; set; }
    public int TotalSessions { get; set; }
    public int AttendedSessions { get; set; }
    public double AttendancePercentage { get; set; }
}