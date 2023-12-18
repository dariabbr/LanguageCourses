namespace CoursesAPI.Helpers;

public static class GeneratorHelper
{
    public static List<int> GenerateRandomIntList(int count, int minValue, int maxValue)
    {
        if (count <= 0 || minValue >= maxValue)
        {
            throw new ArgumentException("Invalid input parameters.");
        }

        var random = new Random();
        var randomIntList = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int randomInt = random.Next(minValue, maxValue + 1); // +1 to include maxValue
            randomIntList.Add(randomInt);
        }

        return randomIntList.Distinct().ToList();
    }
}