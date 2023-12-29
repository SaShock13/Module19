
using Microsoft.EntityFrameworkCore;

namespace GetDataLibrary
{
    public class PeopleDBContext:DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = module19db; Trusted_Connection = True; ");
        }
    }
}
