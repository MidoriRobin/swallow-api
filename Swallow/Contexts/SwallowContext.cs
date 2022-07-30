using Microsoft.EntityFrameworkCore;

namespace Swallow.Models;
    public class SwallowContext : DbContext {

        // TODO: Finish https://www.c-sharpcorner.com/article/crud-operations-in-postgresql-with-ef-core-and-asp-net-core-web-api/
        // https://www.endpointdev.com/blog/2021/07/dotnet-5-web-api/
        public SwallowContext() { }
        public SwallowContext(DbContextOptions<SwallowContext> options) : base (options) 
        { 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }