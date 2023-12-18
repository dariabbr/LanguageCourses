using CoursesAPI.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoursesAPI.Helpers;

public class SyncActionFilter : IAsyncActionFilter
{
    private readonly ICourseService _courseService;
    private readonly IUserService _userService;

    public SyncActionFilter(ICourseService courseService, IUserService userService)
    {
        _courseService = courseService;
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_courseService.IsSynced && _userService.IsSynced)
            await next();

        var courses = _courseService.Courses;
        var users = _userService.Users;
        courses.ForEach(x =>
        {
            x.Students = users.Where(y => x.StudentsIds.Contains(y.Id)).ToList();
            x.Teachers = users.Where(y => x.TeachersIds.Contains(y.Id)).ToList();
            
            x.Students.ForEach(y =>
            {
                y.Courses.Add(x);
                y.CoursesIds.Add(x.Id);
            });
            
            x.Teachers.ForEach(y =>
            {
                y.Courses.Add(x);
                y.CoursesIds.Add(x.Id);
            });
        });

        _userService.IsSynced = true;
        _courseService.IsSynced = true;
        await next();
    }
}