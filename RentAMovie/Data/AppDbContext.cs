using Microsoft.EntityFrameworkCore;
using RentAMovie.Models;

namespace RentAMovie.Data
{
  
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

        //в базата ще се създаде след миграция и ъпдейт таблица Users
        public DbSet<User> Users => Set<User>();

        //в базата ще се създаде след миграция и ъпдейт таблица Movies
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Rent> Rents => Set<Rent>();
            
        }
    
}
