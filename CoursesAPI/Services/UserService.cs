using Bogus;
using CoursesAPI.Entities;
using CoursesAPI.Enums;
using CoursesAPI.Helpers;
using CoursesAPI.Interfaces;

namespace CoursesAPI.Services;

public class UserService : IUserService
{
    public List<User> Users { get; set; }
    public bool IsSynced { get; set; }

    public UserService()
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.IndexFaker + 1) 
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.MiddleName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.NativeLanguage, f => f.PickRandom<Language>())
            .RuleFor(u => u.CoursesIds, f => GeneratorHelper.GenerateRandomIntList(2, 1, 8))
            .RuleFor(u => u.Roles,
                f => new List<UserRole>
                    {
                        f.PickRandom<UserRole>(),
                        f.PickRandom<UserRole>(),
                        f.PickRandom<UserRole>()
                    }
                    .Distinct()
                    .ToList());

        Users = userFaker.Generate(25).ToList();
        Users.Add(new User
        {
            Id = 27,
            FirstName = "Daria",
            LastName = "Bervina",
            Password = "password_123",
            Email = "dashaber1909@gmail.com",
            Roles = new List<UserRole> { UserRole.Administrator }
        });
    }

    public void AddUser(User user) => Users.Add(user);


    public void UpdateUser(int userId, User updatedUser)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return;
        user.FirstName = updatedUser.FirstName;
        user.MiddleName = updatedUser.MiddleName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.NativeLanguage = updatedUser.NativeLanguage;
        user.Roles = updatedUser.Roles;
        user.CoursesIds = updatedUser.CoursesIds;
    }

    public void RemoveUser(int userId) => Users.RemoveAll(u => u.Id == userId);


    public void SubscribeUserToCourse(int userId, int courseId)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        user?.CoursesIds.Add(courseId);
    }

    public void FinishCourseForUser(int userId, int courseId)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        user?.CoursesIds.RemoveAll(x => x == courseId);
    }

    public List<User> GetAllUsers() => Users;
    public User? GetUserById(int id) => Users.FirstOrDefault(x => x.Id == id);
}