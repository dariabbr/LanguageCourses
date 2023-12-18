using CoursesAPI.Enums;

namespace CoursesAPI.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Language NativeLanguage { get; set; }

    public List<Course> Courses { get; set; } = new();
    public List<int> CoursesIds { get; set; } = new();
    
    public List<UserRole> Roles { get; set; }
}