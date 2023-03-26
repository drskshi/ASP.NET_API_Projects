using Microsoft.EntityFrameworkCore;
using Savednumbers.Models;

namespace Savednumbers.Data
{
    public class SavednumbersDbContext : DbContext
    {
        public SavednumbersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Details>  Phonebook{ get; set; }
    }
}
