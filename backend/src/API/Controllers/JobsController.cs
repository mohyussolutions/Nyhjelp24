using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly API.Services.IJobService _jobService;

        public JobsController(API.Services.IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _jobService.GetAllJobsAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            var createdJob = await _jobService.CreateJobAsync(job);
            return CreatedAtAction(nameof(GetJob), new { id = createdJob.Id }, createdJob);
        }
    }
}
