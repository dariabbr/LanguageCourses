using CoursesAPI.Entities;
using CoursesAPI.Models;

namespace CoursesAPI.Interfaces;

public interface IUserService
{
    void AddUser(User user);
    void UpdateUser(int userId, User updatedUser);
    void RemoveUser(int userId);
    void SubscribeUserToCourse(int userId, int courseId);
    void FinishCourseForUser(int userId, int courseId);
    List<User> GetAllUsers();
    User? GetUserById(int id);
    public bool IsSynced { get; set; }
    public List<User> Users { get; set; }
}