
using System.Collections.Generic;
using System.Linq;
using API.Models;

namespace API.Data
{
    public class UsersData
    {
        private readonly List<User> _users = new();

        public IEnumerable<User> GetAllUsers() => _users;

        public User? GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void AddUser(User user) => _users.Add(user);
        public void RemoveUser(User user) => _users.Remove(user);
        public void UpdateUser(User user) => _users.Remove(user);
        public void UpdateUser(int id, User user) => _users.Remove(user);
        
    }
}
