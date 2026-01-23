using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Text.Json;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Regions if empty
            if (!context.Counties.Any())
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Data/Seeding/Regions.json");
                if (File.Exists(filePath))
                {
                    var regionsData = File.ReadAllText(filePath);
                    var regions = JsonSerializer.Deserialize<List<RegionDto>>(regionsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (regions != null)
                    {
                        foreach (var region in regions)
                        {
                            var county = new County { Name = region.Name, Code = region.Code };
                            context.Counties.Add(county);
                            
                            foreach (var munDto in region.Municipalities)
                            {
                                 context.Municipalities.Add(new Municipality 
                                 { 
                                     Name = munDto.Name, 
                                     Code = munDto.Code, 
                                     County = county 
                                 });
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }

            // Seed Users if empty
            if (!context.Users.Any())
            {
                var usersPath = Path.Combine(AppContext.BaseDirectory, "Data/Seeding/Users.json");
                if (File.Exists(usersPath))
                {
                    var usersData = File.ReadAllText(usersPath);
                    var users = JsonSerializer.Deserialize<List<User>>(usersData);
                    if (users != null)
                    {
                        context.Users.AddRange(users);
                        context.SaveChanges();
                    }
                }

                // Seed Jobs
                var jobsPath = Path.Combine(AppContext.BaseDirectory, "Data/Seeding/Jobs.json");
                if (File.Exists(jobsPath) && context.Users.Any())
                {
                    var jobsData = File.ReadAllText(jobsPath);
                    var jobsDtos = JsonSerializer.Deserialize<List<JobSeedDto>>(jobsData);
                    
                    if (jobsDtos != null)
                    {
                        var jobs = new List<Job>();
                        foreach (var dto in jobsDtos)
                        {
                            var user = context.Users.FirstOrDefault(u => u.Email == dto.CreatedByEmail);
                            if (user != null)
                            {
                                jobs.Add(new Job
                                {
                                    Title = dto.Title,
                                    Description = dto.Description,
                                    Location = dto.Location,
                                    Price = dto.Price,
                                    Status = dto.Status,
                                    CreatedByUserId = user.Id
                                });
                            }
                        }
                        context.Jobs.AddRange(jobs);
                        context.SaveChanges();
                    }
                }

                // Seed ContactUs
                var contactPath = Path.Combine(AppContext.BaseDirectory, "Data/Seeding/ContactUs.json");
                if (File.Exists(contactPath))
                {
                    var contactData = File.ReadAllText(contactPath);
                    var contacts = JsonSerializer.Deserialize<List<API.Models.ContactUs>>(contactData);
                    if (contacts != null)
                    {
                        context.ContactMessages.AddRange(contacts);
                        context.SaveChanges();
                    }
                }

                // Seed ChatMessages
                var chatPath = Path.Combine(AppContext.BaseDirectory, "Data/Seeding/Messages.json");
                if (File.Exists(chatPath))
                {
                    var chatData = File.ReadAllText(chatPath);
                    var messages = JsonSerializer.Deserialize<List<API.Models.ChatMessage>>(chatData);
                    if (messages != null)
                    {
                        context.ChatMessages.AddRange(messages);
                        context.SaveChanges();
                    }
                }
            }
        }
    }



    public class JobSeedDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string CreatedByEmail { get; set; }
    }

    public class RegionDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<MunicipalityDto> Municipalities { get; set; }
    }

    public class MunicipalityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
