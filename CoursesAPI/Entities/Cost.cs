using CoursesAPI.Enums;

namespace CoursesAPI.Entities;

public class Cost
{
    public decimal Value { get; set; }
    public CurrencyType Currency { get; set; }
}