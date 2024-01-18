using API.Authentication.Core.Entities;

namespace API.Authentication.Core.Repositories
{
    public class UserRepository
    {
        private IList<User> Users = new List<User>();

        public UserRepository()
        {
            Users.Add(new User { Id = 1, Username = "Admin", Password = "123"});
            Users.Add(new User { Id = 2, Username = "User", Password = "123"});
            Users.Add(new User { Id = 3, Username = "Guest", Password = "123" });
        }

        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(p => p.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(p => p.Username == username);
        }

        public IList<User> GetUsers()
        {
            return Users;
        }

        public bool UserExists(int id)
        {
            return Users.Any(p => p.Id == id);
        }

        public bool UserExists(string username)
        {
            return Users.Any(p => p.Username == username);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var userToUpdate = Users.FirstOrDefault(p => p.Id == user.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Username = user.Username;
                userToUpdate.Password = user.Password;
            }
        }

        public void DeleteUser(int id)
        {
            var userToDelete = Users.FirstOrDefault(p => p.Id == id);

            if (userToDelete != null)
            {
                Users.Remove(userToDelete);
            }
        }

        public bool Authenticate(string username, string password)
        {
            return Users.Any(u => u.Username == username && u.Password == password);
        }
    }
}
