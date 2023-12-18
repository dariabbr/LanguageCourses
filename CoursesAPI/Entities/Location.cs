using CoursesAPI.Enums;

namespace CoursesAPI.Entities;

public class Location
{
    public string? Address { get; set; }
    public string? Link { get; set; }
    public LocationType LocationType { get; set; }
}