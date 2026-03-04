using Application.DTOS.courseDTOS;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CourseService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync()
    {
        var courses = await _uow.Courses.GetAllCoursesAsync();
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    public async Task<CourseResponseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _uow.Courses.GetCourseByIdAsync(id);
        if (course == null) return null;
        
        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task CreateCourseAsync(CreateCourseDto dto)
    {
        if (dto.StartDate >= dto.EndDate)
            throw new Exception("Course start date must be before the end date.");

        bool isTitleExists = await _uow.Courses.CheckCourseTitleExistsAsync(dto.Title);
        if (isTitleExists)
            throw new Exception("Course title already exists. Please choose a different title.");

        var course = _mapper.Map<Course>(dto);
        await _uow.Courses.CreateCourseAsync(course);
        await _uow.CompleteAsync();
    }

    public async Task UpdateCourseAsync(int id, UpdateCourseDto dto)
    {
        var existingCourse = await _uow.Courses.GetCourseByIdAsync(id);
        if (existingCourse == null)
            throw new Exception("Course not found in the system.");

        if (dto.StartDate >= dto.EndDate)
            throw new Exception("Course start date must be before the end date.");

        if (existingCourse.Title != dto.Title)
        {
            bool isTitleExists = await _uow.Courses.CheckCourseTitleExistsAsync(dto.Title);
            if (isTitleExists)
                throw new Exception("The new course title is already in use by another course.");
        }

        existingCourse.Title = dto.Title;
        existingCourse.Description = dto.Description;
        existingCourse.Capacity = dto.Capacity;
        existingCourse.StartDate = dto.StartDate;
        existingCourse.EndDate = dto.EndDate;
        existingCourse.TrainerId = dto.TrainerId;

        await _uow.Courses.UpdateCourseAsync(existingCourse);
        await _uow.CompleteAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _uow.Courses.GetCourseByIdAsync(id);
        if (course == null)
            throw new Exception("Course not found in the system.");

        await _uow.Courses.DeleteCourseAsync(id);
        await _uow.CompleteAsync();
    }

    public async Task AssignTrainerToCourseAsync(int courseId, int trainerId)
    {
        var course = await _uow.Courses.GetCourseByIdAsync(courseId);
        if (course == null)
            throw new Exception("Course not found in the system.");

        var trainer = await _uow.Trainers.GetTrainerByIdAsync(trainerId);
        if (trainer == null)
            throw new Exception("Trainer not found in the system.");

        await _uow.Courses.AssignTrainerToCourseAsync(courseId, trainerId);
    }
}