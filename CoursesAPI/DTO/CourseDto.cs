using CoursesAPI.Entities;
using CoursesAPI.Enums;

namespace CoursesAPI.DTO;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProficiencyLevel EndLevel { get; set; }
    public ProficiencyLevel StartLevel { get; set; }
    public Language Language { get; set; }
    public Cost CourseCost { get; set; }
    public Location Location { get; set; }
    public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    public List<int> StudentsIds { get; set; }

    public List<int> TeachersIds { get; set; }
}