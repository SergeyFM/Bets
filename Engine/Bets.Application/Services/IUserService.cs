
public interface IUserService {
    User GetUserProfile(int userId);
    void UpdateUserProfile(User user);
}

public class FakeUserService : IUserService {
    public User GetUserProfile(int userId) {
        return new User { Id = userId, Name = "Иван Иванов", Email = "ivan@example.com", PhoneNumber = "+7 999 123 4567", DateOfBirth = "12 января 1990 года" };
    }

    public void UpdateUserProfile(User user) {
        // Fake implementation: Do nothing
    }
}
