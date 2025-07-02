using AutoMapper;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Teacher, TeacherDto>()
        .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.MiddleName}"));
        CreateMap<CreateTeacherDto, Teacher>();
        CreateMap<UpdateTeacherDto, Teacher>();


        CreateMap<Student, StudentDto>()
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(s => $"{s.LastName} {s.FirstName} {s.MiddleName}"))
        .ForMember(dest => dest.ClassName, opt => opt.MapFrom(s => s.Class != null ? s.Class.Name : null));
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();

        CreateMap<Subject, SubjectDto>()
        .ForMember(dest => dest.TeacherFullName, opt =>
        opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.LastName} {src.Teacher.FirstName}" : null));
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<UpdateSubjectDto, Subject>();

        CreateMap<Class, ClassDto>();
        CreateMap<CreateClassDto, Class>();
        CreateMap<UpdateClassDto, Class>();

        CreateMap<Position, PositionDto>();
        CreateMap<CreatePositionDto, Position>();
        CreateMap<UpdatePositionDto, Position>();

        CreateMap<Journal, JournalDto>()
        .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student != null ? $"{src.Student.LastName} {src.Student.FirstName}" : null))
        .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject != null ? src.Subject.Name : null))
        .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.Name : null))
        .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.LastName} {src.Teacher.FirstName}" : null));
        CreateMap<CreateJournalDto, Journal>();
        CreateMap<UpdateJournalDto, Journal>();
    }
}
