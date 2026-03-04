using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TrainingDbContext _context; 
    public ITraineeRepo Trainees { get; private set; }
    public IUserRepo Users { get; private set; }
    public ITrainerRepo Trainers { get; private set; }
    public ICourseRepo Courses { get; private set; }
    public IEnrollmentRepo Enrollments { get; private set; }
    public IAttendanceRepo Attendances { get; private set; }
    public IGradeRepo Grades { get; private set; }
    
    public UnitOfWork(TrainingDbContext context)
    {
        _context = context;
        Trainees = new TraineeRepo(_context);
        Users = new UserRepo(_context);
        Trainers = new TrainerRepo(_context);
        Courses = new CourseRepo(_context);
        Enrollments = new EnrollmentRepository(_context);
        Attendances = new AttendanceRepository(_context);
        Grades = new GradeRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
}