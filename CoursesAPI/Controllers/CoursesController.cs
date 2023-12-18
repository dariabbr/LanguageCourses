using AutoMapper;
using CoursesAPI.DTO;
using CoursesAPI.Entities;
using CoursesAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 

namespace CoursesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICourseService _courseService; 

    public CoursesController(ICourseService courseService, IMapper mapper)
    {
        _courseService = courseService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllCourses()
    {
        var courses = _courseService.GetAllCourses();
        return Ok(_mapper.Map<List<CourseDto>>(courses));
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(int id)
    {
        var course = _courseService.GetCourseById(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CourseDto>(course));
    }

    [HttpPost]
    public IActionResult CreateCourse([FromBody] Course course)
    {
        _courseService.AddCourse(course);
        return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, _mapper.Map<CourseDto>(course));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCourse(int id, [FromBody] Course course)
    {
        var existingCourse = _courseService.GetCourseById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        _courseService.UpdateCourse(id, course);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(int id)
    {
        var course = _courseService.GetCourseById(id);
        if (course == null)
        {
            return NotFound();
        }

        _courseService.DeleteCourse(id);
        return NoContent();
    }
}