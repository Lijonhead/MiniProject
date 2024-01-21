using Microsoft.EntityFrameworkCore;
using MiniProject.Data;
using MiniProject.Models;

namespace MiniProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();
            app.MapGet("/", () => "Hellow World");

            app.MapGet("/GetAllPeople", async (AppDbContext context) =>
            {
                var user = await context.People.ToListAsync();

                var result = user.Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.PhoneNumber,
                });
                return Results.Json(result);

            });

            app.MapGet("/GetAllIntrestsForPerson/{PeopleId}", async (AppDbContext context, int PeopleId) =>
            {
                var user = await context.People
                .Include(u => u.personIntrests)
                .ThenInclude(u => u.Intrest)
                .SingleOrDefaultAsync(u => u.PersonId == PeopleId);

                if (user == null)
                {
                    return Results.NotFound($"User with ID {PeopleId} Not Found");
                }

                var result = new
                {
                    user.FirstName,

                    Intrests = user.personIntrests.Select(u => new { u.Intrest.Titel }).ToList(),
                };
                return Results.Json(result);
            });

            app.MapGet("/GetAllLinksForPerson/{PeopleId}", async (AppDbContext context, int PeopleId) =>
            {
                var user = await context.People
                    .Include(u => u.personIntrests)
                    .ThenInclude(pi => pi.links)
                    .SingleOrDefaultAsync(u => u.PersonId == PeopleId);

                if (user == null)
                {
                    return Results.NotFound($"User with ID {PeopleId} Not Found");
                }

                var result = new
                {
                    user.FirstName,

                    Links = user.personIntrests
                        .SelectMany(pi => pi.links)
                        .Select(l => new { l.Url })
                        .ToList(),
                };

                return Results.Json(result);
            });

            app.MapPost("/AddInterestForPerson/{PeopleId}", async (AppDbContext context, int PeopleId, Intrest newInterest) =>
            {
                var user = await context.People
                    .Include(u => u.personIntrests)
                    .SingleOrDefaultAsync(u => u.PersonId == PeopleId);

                if (user == null)
                {
                    return Results.NotFound($"User with ID {PeopleId} Not Found");
                }

                
                var interest = new Intrest
                {
                    Titel = newInterest.Titel,
                    Description = newInterest.Description,
                    
                };

                var personInterest = new PersonIntrest
                {
                    Person = user,
                    Intrest = interest,
                };

                context.Add(interest);
                context.Add(personInterest);
                await context.SaveChangesAsync();

                return Results.Created($"/GetAllIntrestForPerson/{PeopleId}", $"Interest added for  {user.FirstName}");

               
            });

            app.MapPost("/AddLinksForPersonInterest/{PeopleId}/{InterestId}", async (AppDbContext context, int PeopleId, int InterestId, Link newLink) =>
            {
                var user = await context.People
                    .Include(u => u.personIntrests)
                    .ThenInclude(pi => pi.Intrest)
                    .SingleOrDefaultAsync(u => u.PersonId == PeopleId);

                if (user == null)
                {
                    return Results.NotFound($"User with ID {PeopleId} Not Found");
                }

                var interest = user.personIntrests.FirstOrDefault(pi => pi.IntrestId == InterestId);

                if (interest == null)
                {
                    return Results.NotFound($"Interest with ID {InterestId} not found for user with ID {PeopleId}");
                }

                var link = new Link
                {
                    Url = newLink.Url,
                    PersonIntrest = interest,
                    
                };

                context.Links.Add(link);
                await context.SaveChangesAsync();

                return Results.Created($"/GetAllLinksForPerson/{PeopleId}", $"Link added for person with ID {PeopleId} and interest with ID {InterestId}");
            });




            app.Run();
        }
    }
}

