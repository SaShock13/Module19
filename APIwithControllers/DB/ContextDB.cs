using Microsoft.EntityFrameworkCore;

namespace APIContacts.DB
{
    public class ContextDB : DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;

        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {

        }

        public ContextDB()
        {

        }
    }
}
