using Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string name, string email, string password);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
