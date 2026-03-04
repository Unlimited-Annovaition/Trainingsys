namespace Domain.Entities;

public class AttendancePercentageResult// عملته عشان اقدر استقبل نسبه الحضور  
{
    public int EnrollmentId { get; set; }
    public int TotalSessions { get; set; }
    public int AttendedSessions { get; set; }
    public double AttendancePercentage { get; set; }
}