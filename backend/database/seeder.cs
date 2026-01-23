using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Core.Entities;
using API.Models;

class Seeder
{
    static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        using var context = new AppDbContext(optionsBuilder.Options);
        context.Database.EnsureCreated();
        string seedingDir = Path.Combine("..", "seeding");

        SeedRegions(context, seedingDir);
        SeedUsersAndRelated(context, seedingDir);

        Console.WriteLine("Database seeding complete.");
    }

    static void SeedRegions(AppDbContext context, string seedingDir)
    {
        if (context.Counties.Any()) return;
        var regionsPath = Path.Combine(seedingDir, "Regions.json");
        if (!File.Exists(regionsPath)) return;
        var regionsData = File.ReadAllText(regionsPath);
        var regions = JsonSerializer.Deserialize<List<RegionDto>>(regionsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (regions == null) return;
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

    static void SeedUsersAndRelated(AppDbContext context, string seedingDir)
    {
        if (context.Users.Any()) return;
        var usersPath = Path.Combine(seedingDir, "Users.json");
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

        SeedJobs(context, seedingDir);
        SeedContactUs(context, seedingDir);
        SeedChatMessages(context, seedingDir);
    }

    static void SeedJobs(AppDbContext context, string seedingDir)
    {
        var jobsPath = Path.Combine(seedingDir, "Jobs.json");
        if (!File.Exists(jobsPath)) return;
        var jobsData = File.ReadAllText(jobsPath);
        var jobsDtos = JsonSerializer.Deserialize<List<JobSeedDto>>(jobsData);
        if (jobsDtos == null) return;
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

    static void SeedContactUs(AppDbContext context, string seedingDir)
    {
        var contactPath = Path.Combine(seedingDir, "ContactUs.json");
        if (!File.Exists(contactPath)) return;
        var contactData = File.ReadAllText(contactPath);
        var contacts = JsonSerializer.Deserialize<List<API.Models.ContactUs>>(contactData);
        if (contacts == null) return;
        context.ContactMessages.AddRange(contacts);
        context.SaveChanges();
    }

    static void SeedChatMessages(AppDbContext context, string seedingDir)
    {
        var chatPath = Path.Combine(seedingDir, "Messages.json");
        if (!File.Exists(chatPath)) return;
        var chatData = File.ReadAllText(chatPath);
        var messages = JsonSerializer.Deserialize<List<ChatMessage>>(chatData);
        if (messages == null) return;
        context.ChatMessages.AddRange(messages);
        context.SaveChanges();
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
