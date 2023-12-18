using AutoMapper;
using CoursesAPI.DTO;
using CoursesAPI.Entities;

namespace CoursesAPI.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
        CreateMap<Course, CourseDto>()
            .ReverseMap();

        CreateMap<LoginReturnDto, User>()
            .ReverseMap();
    }
}