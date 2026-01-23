using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IContactUsService
    {
        Task<IEnumerable<ContactUs>> GetAllAsync();
        Task<ContactUs> AddAsync(ContactUs contact);
    }
}
