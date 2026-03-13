
using System.Collections.Generic;
using System.Linq;
using API.Models;

namespace API.Data
{
    public class JobsData
    {
        private readonly List<Job> _jobs = new();

        public IEnumerable<Job> GetAllJobs() => _jobs;

        public Job? GetJobById(int id) => _jobs.FirstOrDefault(j => j.Id == id);

        public void AddJob(Job job) => _jobs.Add(job);
    }
}
