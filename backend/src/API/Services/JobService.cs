using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllAsync();
        }

        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _jobRepository.GetByIdAsync(id);
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            return await _jobRepository.AddAsync(job);
        }
    }
}
