using CoursesAPI.Entities;
using CoursesAPI.Enums;
using CoursesAPI.Models;

namespace CoursesAPI.Interfaces;

public interface ICourseService
{
    void AddCourse(Course course);
    void UpdateCourse(int courseId, Course updatedCourse);
    void DeleteCourse(int courseId);
    Course? GetCourseById(int courseId);
    List<Course> GetCoursesByLanguage(Language language);
    List<Course> GetAllCourses();

    public bool IsSynced { get; set; }
    public List<Course> Courses { get; set; }
}