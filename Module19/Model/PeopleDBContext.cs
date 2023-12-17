using Microsoft.EntityFrameworkCore;

namespace Module19.Model
{
    public class PeopleDBContext:DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;

        public PeopleDBContext(DbContextOptions<PeopleDBContext> options) : base(options)
        {

        }

        public PeopleDBContext() 
        {
            
        }
    }
}
