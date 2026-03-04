namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ITraineeRepo Trainees { get; }
    IUserRepo Users { get; }
    ITrainerRepo Trainers { get; }
    ICourseRepo Courses { get; }
    IEnrollmentRepo Enrollments { get; }
    IAttendanceRepo Attendances { get; }
    IGradeRepo Grades { get; }
    Task<int> CompleteAsync();
}