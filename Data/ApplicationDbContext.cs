using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotasApi.Models;

namespace NotasApi.Data
{
    public class ApplicationDbContext:DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){ } 

        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
