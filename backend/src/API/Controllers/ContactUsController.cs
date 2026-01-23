using Core.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _service;

        public ContactUsController(IContactUsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetAll()
        {
            var contacts = await _service.GetAllAsync();
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<ActionResult> Submit([FromBody] ContactUs contact)
        {
            await _service.AddAsync(contact);
            return Ok();
        }
    }
}
