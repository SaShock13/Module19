using Microsoft.EntityFrameworkCore;

namespace Module19.Model
{
    public class PeopleDBContext:DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;

        public PeopleDBContext(DbContextOptions<PeopleDBContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Person>().HasData(
        //            new Person()
        //            {
        //                Id = 1,
        //                FirstName = "Иван",
        //                LastName = "Смирнов",
        //                SurName = "Петрович",
        //                PhoneNumber = "+7 (987) 654-32-10",
        //                PostalAddress = "Санкт-Петербург",
        //                Description = "Иван Петрович - тихий и упорядоченный человек с прекрасным чувством юмора. Он всегда готов помочь окружающим и часто выступает в качестве посредника в конфликтных ситуациях."
        //            },
        //            new Person()
        //            {
        //                Id =2,
        //                FirstName = "Александра",
        //                LastName = "Козлова",
        //                SurName = "Ивановна ",
        //                PhoneNumber = "654-32-11",
        //                PostalAddress = "Москва",
        //                Description = "Александра Ивановна - энергичная и целеустремленная женщина. Она обладает острым умом и всегда готова бросить вызов сложностям. Благодаря своейцелеустремленности, Александра Ивановна часто становится вдохновением для других людей."
        //            }

        //    );
        //}
    }
}
