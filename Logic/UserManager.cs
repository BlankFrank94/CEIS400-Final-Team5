using CEIS400_Final_Team5.Data;

namespace CEIS400_Final_Team5.Logic
{
    public class UserManager
    {
        private readonly DataManager _data;
        public UserManager(DataManager data) => _data = data;

        public bool Login(string username, string password)
        {
            // TODO: replace with real auth
            return _data.Users.Exists(u => u.Username == username);
        }

        public Guid RegisterUser(string username, string hashedPassword, Role role)
        {
            // TODO: validation, hashing, conflicts, etc.
            User u = role == Role.Supervisor ? new Supervisor() : new Employee();
            u.Username = username;
            u.HashedPassword = hashedPassword;
            u.Role = role;
            _data.Users.Add(u);
            return u.Id;
        }

        public void UpdateProfile(Guid userId, string? department = null)
        {
            // TODO: update profile fields as needed
        }
    }
}
