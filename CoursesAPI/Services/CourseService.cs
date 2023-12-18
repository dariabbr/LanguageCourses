using Bogus;
using CoursesAPI.Entities;
using CoursesAPI.Enums;
using CoursesAPI.Helpers;
using CoursesAPI.Interfaces;

namespace CoursesAPI.Services;

public class CourseService : ICourseService
{
    public List<Course> Courses { get; set; }
    public bool IsSynced { get; set; }

    public CourseService()
    {
        var courseFaker = new Faker<Course>()
            .RuleFor(c => c.Id, f => f.IndexFaker + 1) 
            .RuleFor(c => c.StartLevel, f => f.PickRandom<ProficiencyLevel>())
            .RuleFor(c => c.EndLevel, f => f.PickRandom<ProficiencyLevel>())
            .RuleFor(c => c.Language, f => f.PickRandom<Language>())
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.StartDate, f => DateTime.UtcNow)
            .RuleFor(c => c.CourseCost,
                f => new Cost { Value = f.Finance.Amount(), Currency = f.PickRandom<CurrencyType>() })
            .RuleFor(u => u.StudentsIds, f => GeneratorHelper.GenerateRandomIntList(6, 1, 25))
            .RuleFor(u => u.TeachersIds, f => GeneratorHelper.GenerateRandomIntList(2, 1, 25))
            .RuleFor(c => c.Location, f => new Location
            {
                Address = f.Address.StreetAddress(),
                Link = f.Internet.Url(),
                LocationType = f.PickRandom<LocationType>()
            });

        Courses = courseFaker.Generate(8).ToList();
    }

    // Add a new course
    public void AddCourse(Course? course)
    {
        Courses.Add(course);
    }

    // Update an existing course
    public void UpdateCourse(int courseId, Course updatedCourse)
    {
        var course = Courses.FirstOrDefault(c => c.Id == courseId);
        if (course == null) return;

        course.Language = updatedCourse.Language;
        course.StartLevel = updatedCourse.StartLevel;
        course.EndLevel = updatedCourse.EndLevel;
        course.CourseCost = updatedCourse.CourseCost;
        course.Location = updatedCourse.Location;
    }

    public void DeleteCourse(int courseId)
    {
        Courses.RemoveAll(c => c.Id == courseId);
    }

    public Course? GetCourseById(int courseId)
    {
        return Courses.FirstOrDefault(c => c.Id == courseId);
    }

    public List<Course> GetCoursesByLanguage(Language language)
    {
        return Courses.Where(c => c.Language == language).ToList();
    }

    public List<Course> GetAllCourses() => Courses;
}