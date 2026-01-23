using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job?> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(Job job);
    }
}
