﻿using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace Module19.Model
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
