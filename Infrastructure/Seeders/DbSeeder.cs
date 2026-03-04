using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public static class DbSeeder
{
    public static async Task SeedDataAsync(TrainingDbContext context)
    {
        if (!await context.Trainers.AnyAsync())
        {
            var trainers = new List<Trainer>
            {
                new Trainer { FullName = "Ahmad Ali", Email = "ahmad@test.com", Specialization = "Software Engineering" },
                new Trainer { FullName = "Sara Khaled", Email = "sara@test.com", Specialization = "Data Science" }
            };
            await context.Trainers.AddRangeAsync(trainers);
            await context.SaveChangesAsync();
        }

        if (!await context.Courses.AnyAsync())
        {
            var courses = new List<Course>
            {
                new Course { Title = "Advanced .NET Core API", Description = "Clean Architecture & Dapper" },
                new Course { Title = "Frontend with React", Description = "React, RTK Query, MUI" }
            };
            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }

        if (!await context.Trainees.AnyAsync())
        {
            var trainees = new List<Trainee>
            {
                new Trainee { Fullname = "asyyr", PhoneNumber = "0790000000" },
                new Trainee { Fullname = "Khaled", PhoneNumber = "0780000000" }
            };
            await context.Trainees.AddRangeAsync(trainees);
            await context.SaveChangesAsync();
        }

        if (!await context.Enrollments.AnyAsync())
        {
            var enrollments = new List<Enrollment>
            {
                new Enrollment { TraineeId = 1, CourseId = 1, EnrollmentDate = DateTime.Now },
                new Enrollment { TraineeId = 2, CourseId = 2, EnrollmentDate = DateTime.Now }
            };
            await context.Enrollments.AddRangeAsync(enrollments);
            await context.SaveChangesAsync();
        }

        if (!await context.Grades.AnyAsync())
        {
            var grades = new List<Grade>
            {
                new Grade { EnrollmentId = 1, Score = 95.5, Evaluation = "Excellent" },
                new Grade { EnrollmentId = 2, Score = 82.0, Evaluation = "Very Good" }
            };
            await context.Grades.AddRangeAsync(grades);
            await context.SaveChangesAsync();
        }

        if (!await context.Attendances.AnyAsync())
        {
            var attendanceList = new List<Attendance>
            {
                new Attendance { EnrollmentId = 1, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Status = true },
                new Attendance { EnrollmentId = 1, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), Status = true },
                new Attendance { EnrollmentId = 2, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), Status = false }
            };
            await context.Attendances.AddRangeAsync(attendanceList);
            await context.SaveChangesAsync();
        }
    }
}