using Core.Entities;
using API.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class ContactUsService : IContactUsService
    {
        private static readonly List<ContactUs> _contacts = new();

        public Task<IEnumerable<ContactUs>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<ContactUs>>(_contacts);
        }

        public Task<ContactUs> AddAsync(ContactUs contact)
        {
            _contacts.Add(contact);
            return Task.FromResult(contact);
        }
    }
}
