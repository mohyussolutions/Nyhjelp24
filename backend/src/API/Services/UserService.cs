using Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            // In real app: Use hashed password comparison
            var users = await _userRepository.GetAllAsync();
            return users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = password, // Ideally hash this
                Role = "User"
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            
            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
