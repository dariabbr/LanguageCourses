using CoursesAPI.Enums;

namespace CoursesAPI.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProficiencyLevel EndLevel { get; set; }
    public ProficiencyLevel StartLevel { get; set; }
    public Language Language { get; set; }
    public Cost CourseCost { get; set; }
    public Location Location { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime EndDate => StartDate.AddDays(6);

    public List<User> Students { get; set; }
    public List<int> StudentsIds { get; set; } = new();

    public List<User> Teachers { get; set; } = new();
    public List<int> TeachersIds { get; set; }

}