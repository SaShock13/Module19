using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module19.Authorization;

namespace Module19.Model
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        
            public AuthDbContext(DbContextOptions<AuthDbContext> options)
                : base(options)
            {
                Database.EnsureCreated();
            }
       
    }
}
