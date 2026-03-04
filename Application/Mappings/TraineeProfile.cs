using Application.DTOS;
using Application.DTOS.courseDTOS;
using Application.DTOS.TrainerDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class TraineeProfile : Profile
{
    public TraineeProfile()
    { 
        CreateMap<RegisterTraineeDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<RegisterTraineeDto, Trainee>();
        CreateMap<Trainee, TraineeResponseDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
        CreateMap<Trainee, TraineeResponseDTO>();
        CreateMap<RegisterTrainerDTO,User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<RegisterTrainerDTO, Trainer>();
        CreateMap<Trainer, TrainerResponseDTO>();

        CreateMap<Course, CourseResponseDto>();

        CreateMap<CreateCourseDto, Course>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate));
        CreateMap<Enrollment, EnrollmentResponseDto>();
        CreateMap<CreateEnrollmentDto, Enrollment>();
        
        CreateMap<Attendance, AttendanceResponseDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => 
                src.Date.HasValue ? src.Date.Value.ToDateTime(TimeOnly.MinValue) : default(DateTime)));
        
        CreateMap<CreateAttendanceDto, Attendance>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)));

        CreateMap<AttendancePercentageResult, AttendancePercentageDto>();
        
        CreateMap<Grade, GradeResponseDto>();
        CreateMap<CreateGradeDto, Grade>();
        CreateMap<TraineeGradeReportResult, TraineeGradeReportDto>();
    }
}