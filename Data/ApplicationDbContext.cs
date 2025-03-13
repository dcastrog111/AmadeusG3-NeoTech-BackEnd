using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Models;

namespace AmadeusG3_Neo_Tech_BackEnd.Data{
    public class ApplicationDbContext: DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){

        }

        //Falta DbSet de los modelos
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Question_Options> Questions_Options { get; set; }
        
    }
}